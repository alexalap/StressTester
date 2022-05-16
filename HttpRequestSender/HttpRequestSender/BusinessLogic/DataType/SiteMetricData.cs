using HttpRequestSender.ErrorHandling;
using HttpRequestSender.Reports;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Timers;

namespace HttpRequestSender.BusinessLogic.DataType
{
    class SiteMetricData
    {
        private static int ID = 0;
        
        private readonly int id = ID++;
        private readonly string address;
        private DateTime start;
        private bool closed = false;
        private bool paused = false;
        private Timer timer;
        private DateTime lastDurationGetTime;
        private double duration = 0;

        private List<Dictionary<string, int>> responses = new List<Dictionary<string, int>>();

        public double Duration {
            get
            {
                RefreshDuration();
                return duration;
            }
        }

        private void RefreshDuration()
        {
            duration += (DateTime.Now - lastDurationGetTime).TotalMilliseconds;
            lastDurationGetTime = DateTime.Now;
        }

        public int OKResponseCount
        {
            get
            {
                int res = 0;
                foreach (Dictionary<string, int> responsesInLastSec in responses)
                {
                    if (responsesInLastSec.ContainsKey("OK"))
                    {
                        res += responsesInLastSec["OK"];
                    }
                }
                return res;
            }
        }

        public int ErrorResponseCount
        {
            get
            {
                int errorCount = 0;
                foreach (Dictionary<string, int> responsesInLastSec in responses)
                {
                    foreach (string key in responsesInLastSec.Keys)
                    {
                        if (key != "OK")
                        {
                            errorCount += responsesInLastSec[key];
                        }
                    }
                }
                return errorCount;
            }
        }

        public string Address => address;

        private Dictionary<string, int> currentResponses
        {
            get
            {
                return responses.Last();
            }
        }

        public SiteMetricData(string address)
        {
            this.address = address;
            start = DateTime.Now;
            responses.Add(new Dictionary<string, int>());
            lastDurationGetTime = DateTime.Now;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += (o, e) =>
            {
                if (!paused)
                {
                    responses.Add(new Dictionary<string, int>());
                }
                else
                {
                    timer.Stop();
                }
            };
            timer.Start();
        }

        public void AddResponse(string statusCode)
        {
            if(closed || paused)
            {
                throw new IncorrectMetricCallException(nameof(AddResponse), address);
            }
            if (currentResponses.ContainsKey(statusCode))
            {
                currentResponses[statusCode]++;
            }
            else
            {
                currentResponses.Add(statusCode, 1);
            }
        }

        public float ResponseTimeRate()
        {
            return ((float)Duration / 1000 / OKResponseCount);
        }

        public float ErrorTimeRate()
        {
            return ((float)Duration / 1000 / ErrorResponseCount);
        }

        public float ResponseTimeRateLastSec()
        {
            if (currentResponses.ContainsKey("OK"))
            {
                return 1000 / (float)currentResponses["OK"];
            }
            return 0;
        }

        internal void GenerateReport()
        {
            ReportGenerator.CreateReport(address);
            //TODO: add title
            ReportGenerator.SetGraphData(address, responses);
            ReportGenerator.AddMetricData(address, "Number of OK responses: ", OKResponseCount.ToString());
            ReportGenerator.AddMetricData(address, "Average response time: ", (Duration / OKResponseCount).ToString());
        }

        public float ErrorTimeRateLastSec()
        {
            int errorCount = 0;
            foreach (string key in currentResponses.Keys)
            {
                if (key != "OK")
                {
                    errorCount += currentResponses[key];
                }
            }
            return errorCount / 1000;
        }

        public void Close()
        {
            if (!closed)
            {
                closed = true;
                timer.Stop();
            }
        }

        public void Pause()
        {
            if (closed || paused)
            {
                throw new IncorrectMetricCallException(nameof(Pause), address);
            }

            paused = true;
            RefreshDuration();
        }

        public void UnPause()
        {
            if (closed || !paused)
            {
                throw new IncorrectMetricCallException(nameof(UnPause), address);
            }

            paused = false;
            timer.Start();
            responses.Add(new Dictionary<string, int>());
            lastDurationGetTime = DateTime.Now;
        }
    }
}
