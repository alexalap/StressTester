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
        private HttpClient client = new HttpClient();
        private string address;
        private readonly CancellationTokenSource cancellation = new CancellationTokenSource();
        private SessionMetrics sessionMetrics;

        public SiteRequester(string address, SessionMetrics sessionMetrics)
        {
            this.address = address;
            this.sessionMetrics = sessionMetrics;
        }

        public async Task GetResponse()
        {
            Stopwatch stopper = new Stopwatch();
            stopper.Start();
            try
            {
                HttpResponseMessage response = await client.GetAsync(address, cancellation.Token).ConfigureAwait(false);

                stopper.Stop();
                sessionMetrics.AddResponse(address);
                //Logger.Log(LogPriority.INFO, "Response received.\n" + stopper.ElapsedMilliseconds);
            }
            catch (TaskCanceledException t)
            {
                //TODO: Timestamp
                //Logger.Log(LogPriority.WARNING, "Request has been cancelled due to timeout.");
            }
            catch (Exception e)
            {
                Logger.Log(LogPriority.ERROR, "A problem occured while asking for response.\n" + e.Message);
            }
        }

        public async Task GetResponseParallel(int timeOutMillisec, int numberOfRequests)
        {
            SetTimer(timeOutMillisec);
            var tasks = new List<Task>();
            for (int i = 0; i < numberOfRequests; i++)
            {
                tasks.Add(GetResponse());
            }
            await Task.WhenAll(tasks);
        }

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
            sessionMetrics.CloseMetric(address);
            Logger.Log(LogPriority.INFO, "Session interval ended. Address: " + address);
        }
    }
}
