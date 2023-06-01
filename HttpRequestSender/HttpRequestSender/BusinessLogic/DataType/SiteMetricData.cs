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
        /// 
        /// We contain the responses in a list. The responses are distributed into dictionaries based on each second.
        /// The key of the dictionary is the response code ("OK").
        /// The value is an int and double Value Touple from which the int is the total count of the responses and the double is the average response time.
        /// </summary>
        private List<Dictionary<string, (int, double)>> responses = new List<Dictionary<string, (int, double)>>();

        /// <summary>
        /// Measures the time duration of the session if it's not stopped.
        /// </summary>
        public double Duration
        {
            get
            {
                if (!closed)
                {
                    RefreshDuration();
                }
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
                foreach (Dictionary<string, (int, double)> responsesInLastSec in responses)
                {
                    if (responsesInLastSec.ContainsKey("OK"))
                    {
                        res += responsesInLastSec["OK"].Item1;
                    }
                }
                return res;
            }
        }

        /// <summary>
        /// Calculates the OK response rate.
        /// </summary>
        public double OKResponseRate
        {
            get
            {
                int responseCount = 0;
                float rate = 0;
                foreach (Dictionary<string, (int, double)> key in responses)
                {
                    if (key.ContainsKey("OK"))
                    {
                        int currentCount = key["OK"].Item1;
                        // X amount * X rate = SUM(X response time)
                        // current amount * current rate = SUM(current response time)
                        // SUM(X response time) + SUM(current response time) = SUM(response time including current status code)
                        // X amount + current amount = SUM(amount of responses including status code)
                        // SUM(rticsc) / SUM (aorisc) = total response time / total amount => average response time including current status code
                        // 42,85 =     20ms * 5 response +         100ms               *       2 response             / 7 total response;
                        rate = (float)(rate * responseCount + currentCount * key["OK"].Item2) / (float)(responseCount + currentCount);
                        responseCount += currentCount;
                    }
                }
                return rate;
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
                foreach (Dictionary<string, (int, double)> responsesInLastSec in responses)
                {
                    foreach (string key in responsesInLastSec.Keys)
                    {
                        if (key != "OK")
                        {
                            errorCount += responsesInLastSec[key].Item1;
                        }
                    }
                }
                return errorCount;
            }
        }

        /// <summary>
        /// Calculates the not OK response rate.
        /// </summary>
        public double ErrorResponseRate
        {
            get
            {
                int responseCount = 0;
                float rate = 0;
                foreach (Dictionary<string, (int, double)> second in responses)
                {
                    foreach (string statusCode in second.Keys)
                    {
                        if (statusCode != "OK")
                        {
                            int currentCount = second[statusCode].Item1;
                            // X amount * X rate = SUM(X response time)
                            // current amount * current rate = SUM(current response time)
                            // SUM(X response time) + SUM(current response time) = SUM(response time including current status code)
                            // X amount + current amount = SUM(amount of responses including status code)
                            // SUM(rticsc) / SUM (aorisc) = total response time / total amount => average response time including current status code
                            // 42,85 =     20ms * 5 response +         100ms               *       2 response             / 7 total response;
                            rate = (float)(rate * responseCount + currentCount * second[statusCode].Item2) / (float)(responseCount + currentCount);
                            responseCount += currentCount;
                        }
                    }
                }
                return rate;
            }
        }

        /// <summary>
        /// Website's address.
        /// </summary>
        public string Address => address;

        /// <summary>
        /// Title of measurement and the report.
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// Dictionary of current responses.
        /// </summary>
        private Dictionary<string, (int, double)> currentResponses
        {
            get
            {
                return responses.Last();
            }
        }

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="address">Website's address. </param>
        /// <param name="title">Title of measurement. </param>
        public SiteMetricData(string address, string title)
        {
            Title = title;
            this.address = address;
            start = DateTime.Now;
            responses.Add(new Dictionary<string, (int, double)>());
            lastUpdateTime = DateTime.Now;
            timer = new Timer();
            timer.Interval = 1000;
            timer.Elapsed += (o, e) =>
            {
                if (!paused)
                {
                    responses.Add(new Dictionary<string, (int, double)>());
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
        /// <param name="elapsedMilliseconds">Elapsed milliseconds from sending out the HTTP request to receiving a response. </param>
        /// <exception cref="InvalidMethodCallException">Exception for when a method call in invalid. </exception>
        public void AddResponse(string statusCode, long elapsedMilliseconds)
        {
            if (closed || paused)
            {
                throw new InvalidMethodCallException(nameof(AddResponse), address);
            }
            if (currentResponses.ContainsKey(statusCode))
            {
                int currentCount = currentResponses[statusCode].Item1;
                double currentRate = currentResponses[statusCode].Item2 * currentCount;
                currentCount++;
                currentResponses[statusCode] = (currentCount, (currentRate + elapsedMilliseconds) / currentCount);
            }
            else
            {
                currentResponses.Add(statusCode, (1, elapsedMilliseconds));
            }
        }

        /// <summary>
        /// Gets the OK responses time rate of the last second.
        /// </summary>
        /// <returns>Returns the time rate of OK responses of the last second. </returns>
        public float ResponseTimeRateLastSec()
        {
            if (currentResponses.ContainsKey("OK"))
            {
                return (float)currentResponses["OK"].Item2;
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
            ReportGenerator.AddMetricData(Title, Address, "Average response time: ", Math.Round(OKResponseRate).ToString());
        }

        /// <summary>
        /// Gets the not OK responses time rate of the last second.
        /// </summary>
        /// <returns>Returns the time rate of not OK responses of the last second. </returns>
        public float ErrorTimeRateLastSec()
        {
            int errorCount = 0;
            float rate = 0;
            foreach (string statusCode in currentResponses.Keys)
            {
                if (statusCode != "OK")
                {
                    int currentCount = currentResponses[statusCode].Item1;
                    // X amount * X rate = SUM(X response time)
                    // current amount * current rate = SUM(current response time)
                    // SUM(X response time) + SUM(current response time) = SUM(response time including current status code)
                    // X amount + current amount = SUM(amount of responses including status code)
                    // SUM(rticsc) / SUM (aorisc) = total response time / total amount => average response time including current status code
                    // 42,85 =     20ms * 5 response +         100ms               *       2 response             / 7 total response;
                    rate = (float)(rate * errorCount + currentCount * currentResponses[statusCode].Item2) / (float)(errorCount + currentCount);
                    errorCount += currentCount;
                }
            }
            return rate;
        }

        /// <summary>
        /// Refreshes the last update time and the duration using DateTime.Now.
        /// </summary>
        private void RefreshDuration()
        {
            duration += (DateTime.Now - lastUpdateTime).TotalMilliseconds;
            lastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// Stops the measurement and the timer.
        /// </summary>
        public void Close()
        {
            if (!closed)
            {
                lastUpdateTime = DateTime.Now;
                closed = true;
                timer.Stop();
            }
        }

        /// <summary>
        /// Pauses the measurement.
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
        /// Resumes the measurement.
        /// </summary>
        /// <exception cref="InvalidMethodCallException"> Exception for when a method call in invalid. </exception>
        public void UnPause(int delay)
        {
            if (closed || !paused)
            {
                throw new InvalidMethodCallException(nameof(UnPause), address);
            }

            paused = false;
            timer.Start();
            for (int i = 0; i < delay; i++)
            {
                responses.Add(new Dictionary<string, (int, double)>());
            }
            responses.Add(new Dictionary<string, (int, double)>());
            lastUpdateTime = DateTime.Now;
        }
    }
}
