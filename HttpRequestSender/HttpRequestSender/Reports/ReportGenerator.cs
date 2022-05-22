using HttpRequestSender.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Razor.Generator;
using System.Windows.Forms;

namespace HttpRequestSender.Reports
{
    static class ReportGenerator
    {
        private static Dictionary<string, Report> reports = new Dictionary<string, Report>();

        public static void CreateReport(string title, string address)
        {
            reports.Add(title, new Report());
            reports[title].Title = title;
            reports[title].Address = address;
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
                string key = reports.Keys.ElementAt(i);
                string nextPath = Path.Combine(selectedPath, reports[reports.Keys.ElementAt(i == reports.Keys.Count - 1 ? 0 : i + 1)].Title + ".html");
                string previousPath = Path.Combine(selectedPath, reports[reports.Keys.ElementAt(i == 0 ? reports.Keys.Count - 1 : i - 1)].Title + ".html");
                reports[key].Next = nextPath.Replace(Path.DirectorySeparatorChar.ToString(), Path.DirectorySeparatorChar.ToString() + Path.DirectorySeparatorChar.ToString());
                reports[key].Previous = previousPath.Replace(Path.DirectorySeparatorChar.ToString(), Path.DirectorySeparatorChar.ToString() + Path.DirectorySeparatorChar.ToString());
                foreach (string item in reports.Keys)
                {
                    reports[key].OtherReports.Add(Path.Combine(selectedPath, reports[item].Title + ".html"));
                    reports[key].OtherReports.Add(Path.Combine(selectedPath, reports[item].Title + ".html"));
                }
                string report = reports[key].Generate();
                IOHandler.WriteToFile(Path.Combine(selectedPath, reports[key].Title + ".html"), report);
            }
            reports.Clear();
        }
    }
}
