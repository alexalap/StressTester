using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpRequestSender.BusinessLogic
{
    public enum RequesterMode
    {
        Manual,
        PlannedAbsolute,
        PlannedRelative
    }

    class SiteRequester
    {
        public delegate void OnTickDelegate(int tick);
        public delegate void OnAddressedTickDelegate(int tick, string address);
        public event OnTickDelegate Tick;
        public event OnTickDelegate PlanTick;
        public event OnTickDelegate OnPlanFinish;
        public event OnAddressedTickDelegate AddressedTick;

        private HttpClient client = new HttpClient();
        private string address;
        private CancellationTokenSource cancellation = new CancellationTokenSource();
        private CancellationTokenSource plannedCancellation = new CancellationTokenSource();
        private SessionMetrics sessionMetrics;
        private System.Timers.Timer timer;
        private System.Timers.Timer plannedTimer;
        private bool isPaused = false;
        private int numberOfRequestsPerSec;
        private bool isMeasuring = false;
        private object lockObject = new object();
        private int tickCount = 0;
        private int timeSinceLastTick = 0;
        private DateTime pauseTime;

        public string Status { get; private set; } = "Idle";

        public SiteRequester(string address, SessionMetrics sessionMetrics, float timeoutSeconds = 100)
        {
            this.address = address;
            this.sessionMetrics = sessionMetrics;
            this.client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
        }

        /// <summary>
        /// Sends a HTTP request and logs its response. Time is measured with a Stopwatch between sending the request and receiving the response.
        /// </summary>
        public async Task GetResponse()
        {
            Stopwatch stopper = new Stopwatch();
            stopper.Start();
            try
            {
                HttpResponseMessage response = await client.GetAsync(address, cancellation.Token).ConfigureAwait(false);

                stopper.Stop();
                lock (lockObject)
                {
                    sessionMetrics.AddResponse(address, response.StatusCode.ToString(), stopper.ElapsedMilliseconds);
                }
                Logger.Log(LogPriority.INFO, "Response received.\n" + stopper.ElapsedMilliseconds);
            }
            catch (TaskCanceledException)
            {
                Logger.Log(LogPriority.WARNING, "Request has been cancelled due to timeout.");
            }
            catch (Exception e)
            {
                Logger.Log(LogPriority.ERROR, "A problem occured while asking for response.\n" + e.Message);
            }
        }

        /// <summary>
        /// Starts the measurement.
        /// </summary>
        /// <param name="numberOfRequestsPerSec">Number of requests per second. </param>
        /// <param name="schedule">Schedule of planned stress testing. </param>
        /// <param name="mode">Stress testing mode: can be manual or scheduled. </param>
        public void StartMeasurement(int numberOfRequestsPerSec, Schedule schedule, RelativeSchedule rSchedule, RequesterMode mode)
        {
            cancellation.Token.Register(() => TimeOut());
            switch (mode)
            {
                case RequesterMode.Manual:
                    StartSingularMeasurement(numberOfRequestsPerSec);
                    break;
                case RequesterMode.PlannedAbsolute:
                    plannedCancellation.Token.Register(() => TimeOut());
                    StartPlannedMeasurement(schedule);
                    break;
                case RequesterMode.PlannedRelative:
                    plannedCancellation.Token.Register(() => TimeOut());
                    StartRelativePlannedMeasurement(rSchedule);
                    break;
            }
        }

        /// <summary>
        /// Starts a simple measurement with given number of requests per second.
        /// </summary>
        /// <param name="numberOfRequestsPerSec">Number of requests per second. </param>
        /// <param name="prefix">Prefix of the title for the measurement. </param>
        private void StartSingularMeasurement(int numberOfRequestsPerSec, string prefix = "Manual ")
        {
            sessionMetrics.StartMetric(address, prefix + DateTime.Now.ToString("yyyy.MM.dd. hh:mm:ss.ff"));
            this.numberOfRequestsPerSec = numberOfRequestsPerSec;
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += (async (s, e) =>
            {
                if (!isPaused)
                {
                    await GetResponseParallel(this.numberOfRequestsPerSec);
                }
                else
                {
                    timer.Stop();
                    cancellation.Cancel();
                }
                Tick?.Invoke(tickCount);
                AddressedTick?.Invoke(tickCount, address);
                tickCount++;
            });
            timer.Start();
        }

        /// <summary>
        /// Starts an absolute scheduled measurement.
        ///
        /// The planned timer keeps track of the scheduling. The timer is set for each steps duration or the time until the first ones start.
        /// After the time has passed we use a cancellation token to close the current measurement.
        /// 
        /// We use a singular measurement for each scheduled steps. (row 174)
        /// </summary>
        /// <param name="schedule">Schedule of the measurement. </param>
        private void StartPlannedMeasurement(Schedule schedule)
        {
            plannedTimer = new System.Timers.Timer();
            plannedTimer.Interval = (schedule.NextStep().StartTime - DateTime.Now).TotalMilliseconds;
            plannedTimer.Elapsed += ((s, e) =>
            {
                DateTime currentTime = DateTime.Now;
                ScheduleStep nextStep = schedule.NextStep();
                if (isMeasuring)
                {
                    Stop();
                    cancellation = new CancellationTokenSource();
                    cancellation.Token.Register(() => TimeOut());
                    isMeasuring = false;
                }
                if (nextStep != null)
                {
                    if (nextStep.StartTime > currentTime)
                    {
                        double interval = (schedule.NextStep().StartTime - currentTime).TotalMilliseconds;
                        plannedTimer.Interval = interval;
                        Status = "Waiting";
                        timeSinceLastTick = (int)(interval / 1000.0);
                    }
                    else
                    {
                        schedule.Step();
                        tickCount += timeSinceLastTick;
                        timeSinceLastTick = 0;
                        plannedTimer.Interval = (schedule.CurrentStep().EndTime - currentTime).TotalMilliseconds;
                        StartSingularMeasurement(schedule.CurrentStep().Requests);
                        isMeasuring = true;
                        Status = "Running";
                    }
                }
                else
                {
                    if (schedule.CurrentStep() != null)
                    {
                        schedule.Step();
                    }
                    plannedTimer.Stop();
                    OnPlanFinish.Invoke(tickCount);
                }
                PlanTick?.Invoke(tickCount);
            });
            plannedTimer.Start();
        }

        /// <summary>
        /// Starts a relative scheduled measurement.
        ///
        /// The planned timer keeps track of the scheduling. The timer is set for each steps duration or the time until the first ones start.
        /// After the time has passed we use a cancellation token to close the current measurement.
        /// 
        /// We use a singular measurement for each scheduled steps. (row 221)
        /// </summary>
        /// <param name="schedule">Schedule of the measurement. </param>
        private void StartRelativePlannedMeasurement(RelativeSchedule schedule)
        {
            plannedTimer = new System.Timers.Timer();
            plannedTimer.Interval = schedule.NextStep().Duration.TotalMilliseconds;
            plannedTimer.Elapsed += ((s, e) =>
            {
                DateTime currentTime = DateTime.Now;
                RelativeScheduleStep nextStep = schedule.NextStep();
                if (isMeasuring)
                {
                    Stop();
                    cancellation = new CancellationTokenSource();
                    cancellation.Token.Register(() => TimeOut());
                    isMeasuring = false;
                }
                if (nextStep != null)
                {
                    schedule.Step();
                    plannedTimer.Interval = schedule.CurrentStep().Duration.TotalMilliseconds;
                    StartSingularMeasurement(schedule.CurrentStep().Requests);
                    isMeasuring = true;
                    Status = "Running";
                }
                else
                {
                    if (schedule.CurrentStep() != null)
                    {
                        schedule.Step();
                    }
                    plannedTimer.Stop();
                    OnPlanFinish.Invoke(tickCount);
                }
                PlanTick?.Invoke(tickCount);
            });
            schedule.Step();
            plannedTimer.Interval = schedule.CurrentStep().Duration.TotalMilliseconds;
            StartSingularMeasurement(schedule.CurrentStep().Requests);
            isMeasuring = true;
            Status = "Running";
            plannedTimer.Start();
        }

        /// <summary>
        /// We distribute the requests into batches for more optimal use.
        /// 
        /// We use batch sizes for numbers respectively:
        /// 1000+ = 300
        /// 500+ = 200
        /// 200+ = 100
        /// Default is 50.
        /// 
        /// We distribute the requests into partitions with given batch sizes and start them in parallel.
        /// This reduces stress on the CPU which would otherwise start all the processes in their very own thread and eventually freeze.
        /// </summary>
        /// <param name="numberOfRequestsPerSec">Number of requests per second. </param>
        private async Task GetResponseParallel(int numberOfRequestsPerSec)
        {
            int batchSize = 50;
            if (numberOfRequestsPerSec > 1000)
            {
                batchSize = 300;
            }
            else if (numberOfRequestsPerSec > 500)
            {
                batchSize = 200;
            }
            else if (numberOfRequestsPerSec > 200)
            {
                batchSize = 100;
            }

            var tasks = new List<Task>();
            for (int i = 0; i < numberOfRequestsPerSec; i++)
            {
                tasks.Add(GetResponse());
            }

            var partitions = Partitioner.Create(tasks).GetPartitions(batchSize);

            Parallel.ForEach(partitions, async partition =>
            {
                using (partition)
                {
                    while (partition.MoveNext())
                    {
                        var task = partition.Current;
                        await Task.WhenAll(task);
                    }
                }
            });

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// If the session is not paused, it closes the session.
        /// </summary>
        private void TimeOut()
        {
            if (!isPaused)
            {
                sessionMetrics.CloseMetric(address);
                Logger.Log(LogPriority.INFO, "Session interval ended. Address: " + address);
            }
        }

        /// <summary>
        /// Pauses the metric session.
        /// </summary>
        public void Pause()
        {
            isPaused = true;
            sessionMetrics.Pause(address);
            pauseTime = DateTime.Now;
        }

        /// <summary>
        /// Resumes the metric session.
        /// </summary>
        public async void Resume()
        {
            isPaused = false;
            cancellation = new CancellationTokenSource();
            cancellation.Token.Register(() => TimeOut());
            timer.Start();
            int delay = (int)(DateTime.Now - pauseTime).TotalSeconds;
            tickCount += delay;
            sessionMetrics.UnPause(delay, address);
            await GetResponseParallel(numberOfRequestsPerSec);
        }

        /// <summary>
        /// Stops the metric session.
        /// </summary>
        public void Stop()
        {
            timer.Stop();
            cancellation.Cancel();
        }

        /// <summary>
        /// Stops the absolute scheduled metric session.
        /// </summary>
        public void PlannedStop()
        {
            plannedTimer.Stop();
            plannedCancellation.Cancel();
        }

        /// <summary>
        /// Stops the relative scheduled metric session.
        /// </summary>
        public void RelativePlannedStop()
        {
            plannedTimer.Stop();
            plannedCancellation.Cancel();
        }
    }
}
