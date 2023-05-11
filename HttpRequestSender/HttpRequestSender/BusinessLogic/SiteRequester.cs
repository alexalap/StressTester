using HttpRequestSender.Utilities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpRequestSender.BusinessLogic
{
    public enum RequesterMode
    {
        Manual,
        Planned
    }

    class SiteRequester
    {
        public delegate void OnTickDelegate();
        public delegate void OnAddressedTickDelegate(string address);
        public event OnTickDelegate Tick;
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

        public SiteRequester(string address, SessionMetrics sessionMetrics, float timeoutSeconds = 100)
        {
            this.address = address;
            this.sessionMetrics = sessionMetrics;
            this.client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
        }

        /// <summary>
        /// Gets a HTTP request's response and creates a log.
        /// </summary>
        /// <returns></returns>
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
                    sessionMetrics.AddResponse(address, response.StatusCode.ToString());
                }
                Logger.Log(LogPriority.INFO, "Response received.\n" + stopper.ElapsedMilliseconds);
            }
            catch (TaskCanceledException t)
            {
                //TODO: Timestamp
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
        public void StartMeasurement(int numberOfRequestsPerSec, Schedule schedule, RequesterMode mode)
        {
            cancellation.Token.Register(() => TimeOut());
            switch (mode)
            {
                case RequesterMode.Manual:
                    StartSingularMeasurement(numberOfRequestsPerSec);
                    break;
                case RequesterMode.Planned:
                    plannedCancellation.Token.Register(() => TimeOut());
                    StartPlannedMeasurement(schedule);
                    break;
            }
        }

        /// <summary>
        /// Starts a singular, manual measurement.
        /// </summary>
        /// <param name="numberOfRequestsPerSec">Number of requests per second. </param>
        /// <param name="prefix">Measurement's mode. </param>
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
                Tick?.Invoke();
                AddressedTick?.Invoke(address);
            });
            timer.Start();
        }

        /// <summary>
        /// Starts a scheduled measurement.
        /// </summary>
        /// <param name="schedule">Schedule of the measurement. </param>
        private void StartPlannedMeasurement(Schedule schedule)
        {
            plannedTimer = new System.Timers.Timer();
            plannedTimer.Interval = (schedule.NextStep().StartTime - DateTime.Now).TotalMilliseconds;
            plannedTimer.Elapsed += (async (s, e) =>
            {
                if (isMeasuring)
                {
                    Stop();
                    cancellation = new CancellationTokenSource();
                    cancellation.Token.Register(() => TimeOut());
                    isMeasuring = false;
                }
                if (schedule.NextStep() != null)
                {
                    if (schedule.NextStep().StartTime > DateTime.Now)
                    {
                        plannedTimer.Interval = (schedule.NextStep().StartTime - DateTime.Now).TotalMilliseconds;
                    }
                    else
                    {
                        schedule.Step();
                        plannedTimer.Interval = (schedule.CurrentStep().EndTime - DateTime.Now).TotalMilliseconds;
                        StartSingularMeasurement(schedule.CurrentStep().Requests);
                        isMeasuring = true;
                    }
                }
                else
                {
                    if (schedule.CurrentStep() != null)
                    {
                        schedule.Step();
                    }
                    plannedTimer.Stop();
                    OnPlanFinish.Invoke();
                }
            });
            plannedTimer.Start();
        }

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

        #region deprecated
        public async Task GetResponseParallelBatched(int timeOutMillisec, int numberOfRequests, int batchSize = 100)
        {
            SetTimer(timeOutMillisec);
            int batchCount = (int)Math.Ceiling((double)numberOfRequests / batchSize);
            var tasks = new List<Task>();

            for (int i = 0; i < numberOfRequests; i++)
            {
                tasks.Add(GetResponse());
            }

            for (int i = 0; i < batchCount; i++)
            {
                var currentTasks = tasks.Skip(i * batchSize).Take(batchSize);
                await Task.WhenAll(currentTasks);
            }
            await Task.WhenAll(tasks);
            cancellation.Cancel();
        }

        private void SetTimer(int millisec)
        {
            cancellation.CancelAfter(millisec);
            cancellation.Token.Register(() => TimeOut());
            //sessionMetrics.StartMetric(address);
            Logger.Log(LogPriority.INFO, "New session interval started. Timeout: " + millisec + "ms, Address: " + address);
        }

        /// <summary>
        /// If the session is not paused, it closes the sesison.
        /// </summary>
        private void TimeOut()
        {
            if (!isPaused)
            {
                sessionMetrics.CloseMetric(address);
                Logger.Log(LogPriority.INFO, "Session interval ended. Address: " + address);
            }
        }

        #endregion

        /// <summary>
        /// Pauses the metric session.
        /// </summary>
        public void Pause()
        {
            isPaused = true;
            sessionMetrics.Pause(address);
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
            sessionMetrics.UnPause(address);
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
        /// Stops the scheduled metric session.
        /// </summary>
        public void PlannedStop()
        {
            plannedTimer.Stop();
            plannedCancellation.Cancel();
        }
    }
}
