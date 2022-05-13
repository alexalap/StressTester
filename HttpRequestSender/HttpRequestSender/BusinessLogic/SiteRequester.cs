using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HttpRequestSender.BusinessLogic
{
    class SiteRequester
    {
        public delegate void OnTickDelegate();
        public event OnTickDelegate Tick;

        private HttpClient client = new HttpClient();
        private string address;
        private CancellationTokenSource cancellation = new CancellationTokenSource();
        private SessionMetrics sessionMetrics;
        private System.Timers.Timer timer;
        private bool paused = false;
        private int numberOfRequestsPerSec;

        public SiteRequester(string address, SessionMetrics sessionMetrics, float timeoutSeconds = 100)
        {
            this.address = address;
            this.sessionMetrics = sessionMetrics;
            this.client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
        }

        public async Task GetResponse()
        {
            Stopwatch stopper = new Stopwatch();
            stopper.Start();
            try
            {
                HttpResponseMessage response = await client.GetAsync(address, cancellation.Token).ConfigureAwait(false);

                stopper.Stop();
                sessionMetrics.AddResponse(address, response.StatusCode.ToString());
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

        public void GetResponseParallelPeriodic(int numberOfRequestsPerSec)
        {
            cancellation.Token.Register(() => TimeOut());
            sessionMetrics.StartMetric(address);
            this.numberOfRequestsPerSec = numberOfRequestsPerSec;
            timer = new System.Timers.Timer();
            timer.Interval = 1000;
            timer.Elapsed += (async (s, e) => 
            {
                if (!paused)
                {
                    await GetResponseParallel(this.numberOfRequestsPerSec);
                }
                else
                {
                    timer.Stop();
                    cancellation.Cancel();
                }
                Tick?.Invoke();
            });
            timer.Start();
        }

        private async Task GetResponseParallel(int numberOfRequestsPerSec)
        {
            var tasks = new List<Task>();
            for (int i = 0; i < numberOfRequestsPerSec; i++)
            {
                tasks.Add(GetResponse());
            }
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
            sessionMetrics.StartMetric(address);
            Logger.Log(LogPriority.INFO, "New session interval started. Timeout: " + millisec + "ms, Address: " + address);
        }

        private void TimeOut()
        {
            if (!paused)
            {
                sessionMetrics.CloseMetric(address);
                Logger.Log(LogPriority.INFO, "Session interval ended. Address: " + address);
            }
        }

#endregion

        public void Pause()
        {
            paused = true;
            sessionMetrics.Pause(address);
        }

        public async void Resume()
        {
            paused = false;
            cancellation = new CancellationTokenSource();
            cancellation.Token.Register(() => TimeOut());
            timer.Start();
            sessionMetrics.UnPause(address);
            await GetResponseParallel(numberOfRequestsPerSec);
        }
        
        public void Stop()
        {
            timer.Stop();
            cancellation.Cancel();
        }
    }
}
