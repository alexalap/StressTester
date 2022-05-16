using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Razor.Generator;

namespace HttpRequestSender.Reports
{
    static class ReportGenerator
    {
        private static Dictionary<string, Report> reports = new Dictionary<string, Report>();

        public static void CreateReport(string title)
        {
            reports.Add(title, new Report());
            reports[title].Title = title;
        }

        public static void SetGraphData(string title, List<Dictionary<string, int>> graphData)
        {
            reports[title].GraphData = graphData;
        }

        public static void AddMetricData(string title, string key, string value)
        {
            reports[title].MetricData.Add(key, value);
        }

        public static void Generate()
        {
            reports.Clear();
            foreach (string key in reports.Keys)
            {
                reports[key].Next = "http://google.com";
                reports[key].Previous = "http://google.com";
                reports[key].OtherReports.Add("http://google.com");
                reports[key].OtherReports.Add("http://gmail.com");
                reports[key].Generate();
            }
        }
    }
}
