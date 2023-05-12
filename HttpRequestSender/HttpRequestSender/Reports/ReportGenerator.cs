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

        /// <summary>
        /// Creates a new report.
        /// </summary>
        /// <param name="title"> Title of the report. </param>
        /// <param name="address"> Address / website link the report was made of. </param>
        /// <param name="start"> Start time of the stress testing. </param>
        /// <param name="lastUpdateTime"> The time the stress testing was last checked (ended). </param>
        public static void CreateReport(string title, string address, DateTime start, DateTime lastUpdateTime)
        {
            reports.Add((title, address), new Report());
            reports[(title, address)].Title = title;
            reports[(title, address)].Address = address;
            reports[(title, address)].Start = start;
            reports[(title, address)].End = lastUpdateTime;
        }

        /// <summary>
        /// Sets the data of the graphs in the report selected by its title and address.
        /// </summary>
        /// <param name="title"> Report's title. </param>
        /// <param name="address"> Report's address. </param>
        /// <param name="graphData"> List of graph data dictionaries. </param>
        public static void SetGraphData(string title, string address, List<Dictionary<string, (int, double)>> graphData)
        {
            reports[(title, address)].GraphData = graphData;
        }

        /// <summary>
        /// Adds metric data to a report selected by its title and address.
        /// </summary>
        /// <param name="title"> Report's title. </param>
        /// <param name="address"> Report's address. </param>
        /// <param name="key"> Name of the data that is measured. </param>
        /// <param name="value"> Value of the measured data. </param>
        public static void AddMetricData(string title, string address, string key, string value)
        {
            reports[(title, address)].MetricData.Add(key, value);
        }

        /// <summary>
        /// Generates a report in .html format. Saves it in a file to a given path.
        /// </summary>
        /// <param name="selectedPath"> Path to the saving location. </param>
        public static void Generate(string selectedPath)
        {
            for (int i = 0; i < reports.Keys.Count; i++)
            {
                HTMLGenerator hTMLGenerator = new HTMLGenerator(reports.Values.ToList());
                string report = hTMLGenerator.Generate();
                // Combines the path to the saving location with the current date time and the file format.
                IOHandler.WriteToFile(Path.Combine(selectedPath, DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".html"), report);
            }
            reports.Clear();
        }
    }
}
