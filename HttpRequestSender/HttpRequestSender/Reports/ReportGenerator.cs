using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HttpRequestSender.Reports
{
    static class ReportGenerator
    {
        private static Dictionary<(string, string), Report> reports = new Dictionary<(string, string), Report>();

        public static void CreateReport(string title, string address, DateTime start, DateTime lastUpdateTime)
        {
            reports.Add((title, address), new Report());
            reports[(title, address)].Title = title;
            reports[(title, address)].Address = address;
            reports[(title, address)].Start = start;
            reports[(title, address)].End = lastUpdateTime;
        }

        public static void SetGraphData(string title, string address, List<Dictionary<string, int>> graphData)
        {
            reports[(title, address)].GraphData = graphData;
        }

        public static void AddMetricData(string title, string address, string key, string value)
        {
            reports[(title, address)].MetricData.Add(key, value);
        }

        public static void Generate(string selectedPath)
        {
            for (int i = 0; i < reports.Keys.Count; i++)
            {
                HTMLGenerator hTMLGenerator = new HTMLGenerator(reports.Values.ToList());
                string report = hTMLGenerator.Generate();
                IOHandler.WriteToFile(Path.Combine(selectedPath, DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".html"), report);
            }
            reports.Clear();
        }
    }
}
