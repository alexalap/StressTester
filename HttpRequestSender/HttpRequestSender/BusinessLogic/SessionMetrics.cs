using HttpRequestSender.BusinessLogic.DataType;
using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSender.BusinessLogic
{
    class SessionMetrics
    {
        private List<SiteMetricData> siteMetrics = new List<SiteMetricData>();
        private Dictionary<string, SiteMetricData> activeMetrics = new Dictionary<string, SiteMetricData>();

        public void StartMetric(string address)
        {
            SiteMetricData siteMetricData = new SiteMetricData(address);
            siteMetrics.Add(siteMetricData);
            activeMetrics.Add(address, siteMetricData);
        }

        public void CloseMetric(string address)
        {
            activeMetrics[address].Close();
            Logger.Log(LogPriority.INFO, "Metric closed with result: " + activeMetrics[address].ResponseTimeRate() + " response / sec");
            activeMetrics.Remove(address);
        }

        public void AddResponse(string address)
        {
            activeMetrics[address].AddResponse();
        }
    }
}
