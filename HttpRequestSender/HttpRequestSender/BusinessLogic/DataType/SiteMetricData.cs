using HttpRequestSender.ErrorHandling;
using HttpRequestSender.Reports;
using System;
using System.Collections.Generic;
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
        private DateTime lastUpdateTime;
        private double duration = 0;

        /// <summary>
        /// Responses of HTTP requests.
        /// </summary>
        private List<Dictionary<string, int>> responses = new List<Dictionary<string, int>>();

        /// <summary>
        /// Measures the time duration of the session.
        /// </summary>
        public double Duration
        {
            get
            {
                RefreshDuration();
                return duration;
            }
        }

        /// <summary>
        /// Counts and returns the total number of OK responses.
        /// </summary>
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

        /// <summary>
        /// Counts and returns the total number of responses that do not include "OK".
        /// </summary>
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

        public string Title { get; private set; }

        /// <summary>
        /// Dictionary of current responses.
        /// </summary>
        private Dictionary<string, int> currentResponses
        {
            get
            {
                return responses.Last();
            }
        }

        public SiteMetricData(string address, string title)
        {
            Title = title;
            this.address = address;
            start = DateTime.Now;
            responses.Add(new Dictionary<string, int>());
            lastUpdateTime = DateTime.Now;
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

        /// <summary>
        /// Adds a response to the dictionary of current responses.
        /// </summary>
        /// <param name="statusCode">Status code of response. </param>
        /// <exception cref="InvalidMethodCallException">Exception for when a method call in invalid. </exception>
        public void AddResponse(string statusCode)
        {
            if (closed || paused)
            {
                throw new InvalidMethodCallException(nameof(AddResponse), address);
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

        /// <summary>
        /// Calculates the OK response time rate.
        /// </summary>
        /// <returns>Returns the OK responses time rate.</returns>
        public float ResponseTimeRate()
        {
            return ((float)Duration / 1000 / OKResponseCount);
        }

        /// <summary>
        /// Calculates the OK response time rate.
        /// </summary>
        /// <returns>Returns the not OK responses time rate.</returns>
        public float ErrorTimeRate()
        {
            return ((float)Duration / 1000 / ErrorResponseCount);
        }

        /// <summary>
        /// Gets the OK responses time rate of the last second.
        /// </summary>
        /// <returns>Returns the time rate of OK responses of the last second. </returns>
        public float ResponseTimeRateLastSec()
        {
            if (currentResponses.ContainsKey("OK"))
            {
                return 1000 / (float)currentResponses["OK"];
            }
            return 0;
        }

        /// <summary>
        /// Generates a report.
        /// </summary>
        internal void GenerateReport()
        {
            ReportGenerator.CreateReport(Title, Address, start, lastUpdateTime);
            ReportGenerator.SetGraphData(Title, Address, responses);
            ReportGenerator.AddMetricData(Title, Address, "Number of OK responses: ", OKResponseCount.ToString());
            ReportGenerator.AddMetricData(Title, Address, "Average response time: ", (Duration / OKResponseCount).ToString());
        }

        /// <summary>
        /// Gets the not OK responses time rate of the last second.
        /// </summary>
        /// <returns>Returns the time rate of not OK responses of the last second. </returns>
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

        /// <summary>
        /// Refreshes the duration by setting the current DateTime.
        /// </summary>
        private void RefreshDuration()
        {
            duration += (DateTime.Now - lastUpdateTime).TotalMilliseconds;
            lastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        public void Close()
        {
            if (!closed)
            {
                closed = true;
                timer.Stop();
            }
        }

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        /// <exception cref="InvalidMethodCallException"> Exception for when a method call in invalid. </exception>
        public void Pause()
        {
            if (closed || paused)
            {
                throw new InvalidMethodCallException(nameof(Pause), address);
            }

            paused = true;
            RefreshDuration();
        }

        /// <summary>
        /// Resumes the timer.
        /// </summary>
        /// <exception cref="InvalidMethodCallException"> Exception for when a method call in invalid. </exception>
        public void UnPause()
        {
            if (closed || !paused)
            {
                throw new InvalidMethodCallException(nameof(UnPause), address);
            }

            paused = false;
            timer.Start();
            responses.Add(new Dictionary<string, int>());
            lastUpdateTime = DateTime.Now;
        }
    }
}
