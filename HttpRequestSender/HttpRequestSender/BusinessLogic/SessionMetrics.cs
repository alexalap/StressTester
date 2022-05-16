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

        public int GetOKResponseCount(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].OKResponseCount;
            }
            return -1;
        }

        public int GetErrorResponseCount(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].ErrorResponseCount;
            }
            return -1;
        }

        public string GetAddress(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                return activeMetrics[address].Address;
            }
            return "";
        }

        public void StartMetric(string address)
        {
            SiteMetricData siteMetricData = new SiteMetricData(address);
            siteMetrics.Add(siteMetricData);
            activeMetrics.Add(address, siteMetricData);
        }

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

        public void CloseMetric(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                activeMetrics[address].Close();
                Logger.Log(LogPriority.INFO, "Metric closed with result: " + activeMetrics[address].ResponseTimeRate() + " response / sec");
                activeMetrics.Remove(address);
            }
        }

        public void Pause(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                activeMetrics[address].Pause();
            }
        }

        public void UnPause(string address)
        {
            if (activeMetrics.ContainsKey(address))
            {
                activeMetrics[address].UnPause();
            }
        }

        public void GenerateReport()
        {
            foreach (SiteMetricData item in siteMetrics)
            {
                item.GenerateReport();
            }
            ReportGenerator.Generate();
        }
    }
}
