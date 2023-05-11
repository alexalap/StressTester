using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.Reports;
using HttpRequestSender.Utilities;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;

namespace HttpRequestSender.BusinessLogic
{
    class SessionMetrics
    {
        private List<SiteMetricData> siteMetrics = new List<SiteMetricData>();
        private Dictionary<string, SiteMetricData> activeMetrics = new Dictionary<string, SiteMetricData>();

        public double GetDuration(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].Duration;
            }
            return -1;
        }

        /// <summary>
        /// Gets the number of OK responses.
        /// </summary>
        /// <param name="address">Website's address. </param>
        /// <returns>Returns the number of OK responses. </returns>
        public int GetOKResponseCount(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].OKResponseCount;
            }
            return -1;
        }

        /// <summary>
        /// Gets the number of error responses.
        /// </summary>
        /// <param name="address">Website's address. </param>
        /// <returns>Returns the number of error responses. </returns>
        public int GetErrorResponseCount(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].ErrorResponseCount;
            }
            return -1;
        }

        /// <summary>
        /// Gets the address of a website.
        /// </summary>
        /// <param name="address">Website's address. </param>
        /// <returns>Returns the website's address if there is any. Otherwise returns an empty string. </returns>
        public string GetAddress(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].Address;
            }
            return "";
        }

        /// <summary>
        /// Starts the metric session and logs a log.
        /// </summary>
        /// <param name="address"> Website's address. </param>
        /// <param name="title"> ??? </param>
        public void StartMetric(string address, string title)
        {
            SiteMetricData siteMetricData = new SiteMetricData(address, title);
            siteMetrics.Add(siteMetricData);
            activeMetrics.Add(address, siteMetricData);
        }

        /// <summary>
        /// Adds the response to the dictionary of active metrics.
        /// </summary>
        /// <param name="address">Wesbite's address. </param>
        /// <param name="statusCode">Status code of response. </param>
        public void AddResponse(string address, string statusCode)
        {
            if (activeMetrics.ContainsKey(address))
            {
                activeMetrics[address].AddResponse(statusCode);
            }
        }

        public float ResponseTimeRate(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].ResponseTimeRate();
            }
            return -1;
        }

        public float ErrorTimeRate(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].ErrorTimeRate();
            }
            return -1;
        }

        /// <summary>
        /// Adds the response time rate of the last second to the dictionary of active metrics.
        /// </summary>
        /// <param name="address">Website's address. </param>
        /// <returns></returns>
        public float ResponseTimeRateLastSec(string address)
        {
            if (activeMetrics.ContainsKey(address)) {
                return activeMetrics[address].ResponseTimeRateLastSec();
            }
            return -1;
        }

        public float ErrorTimeRateLastSec(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].ErrorTimeRateLastSec();
            }
            return -1;
        }

        /// <summary>
        /// Closes the metric session and logs a log.
        /// </summary>
        /// <param name="address">Website's address. </param>
        public void CloseMetric(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                activeMetrics[address].Close();
                Logger.Log(LogPriority.INFO, "Metric closed with result: " + activeMetrics[address].ResponseTimeRate() + " response / sec");
                activeMetrics.Remove(address);
            }
        }

        /// <summary>
        /// Pauses the metric session.
        /// </summary>
        /// <param name="address"></param>
        public void Pause(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                activeMetrics[address].Pause();
            }
        }

        /// <summary>
        /// Resumes the metric session.
        /// </summary>
        /// <param name="address"></param>
        public void UnPause(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                activeMetrics[address].UnPause();
            }
        }

        /// <summary>
        /// Generates a report of the session.
        /// </summary>
        /// <param name="selectedPath">Path to the saving location. </param>
        public void GenerateReport(string selectedPath)
        {
            foreach (SiteMetricData item in siteMetrics)
            {
                item.GenerateReport();
            }
            ReportGenerator.Generate(selectedPath);
        }
    }
}
