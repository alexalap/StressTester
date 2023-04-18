using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HttpRequestSender.Reports
{
    static class ReportGenerator
    {
        private static Dictionary<string, Report> reports = new Dictionary<string, Report>();

        public static void CreateReport(string title, string address, DateTime start, DateTime lastUpdateTime)
        {
            reports.Add(title, new Report());
            reports[title].Title = title;
            reports[title].Address = address;
            reports[title].Start = start;
            reports[title].End = lastUpdateTime;
        }

        public static void SetGraphData(string title, List<Dictionary<string, int>> graphData)
        {
            reports[title].GraphData = graphData;
        }

        public static void AddMetricData(string title, string key, string value)
        {
            reports[title].MetricData.Add(key, value);
        }

        public static void Generate(string selectedPath)
        {
            for(int i=0; i<reports.Keys.Count; i++)
            {
                HTMLGenerator hTMLGenerator = new HTMLGenerator(reports.Values.ToList());
                string report = hTMLGenerator.Generate();
                string key = reports.Keys.ElementAt(i);
                IOHandler.WriteToFile(Path.Combine(selectedPath, DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".html"), report);
            }
            reports.Clear();
        }
    }
}
