using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace HttpRequestSender.BusinessLogic
{
    class SessionMetrics
    {
        private List<SiteMetricData> siteMetrics = new List<SiteMetricData>();
        private Dictionary<string, SiteMetricData> activeMetrics = new Dictionary<string, SiteMetricData>();

        public double GetDuration(string address)
        {
            return activeMetrics[address].Duration;
        }

        public int GetOKResponseCount(string address)
        {
            return activeMetrics[address].OKResponseCount;
        }

        public int GetErrorResponseCount(string address)
        {
            return activeMetrics[address].ErrorResponseCount;
        }

        public string GetAddress(string address)
        {
            return activeMetrics[address].Address;
        }

        public void StartMetric(string address)
        {
            SiteMetricData siteMetricData = new SiteMetricData(address);
            siteMetrics.Add(siteMetricData);
            activeMetrics.Add(address, siteMetricData);
        }

        public void AddResponse(string address, string statusCode)
        {
            activeMetrics[address].AddResponse(statusCode);
        }

        public float ResponseTimeRate(string address)
        {
            return activeMetrics[address].ResponseTimeRate();
        }

        public float ErrorTimeRate(string address)
        {
            return activeMetrics[address].ErrorTimeRate();
        }

        public float ResponseTimeRateLastSec(string address)
        {
            return activeMetrics[address].ResponseTimeRateLastSec();
        }

        public float ErrorTimeRateLastSec(string address)
        {
            return activeMetrics[address].ErrorTimeRateLastSec();
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
    }
}
