using System;
using System.Collections.Generic;

namespace HttpRequestSender.Reports
{
    /// <summary>
    /// Report class: stores the information of a report
    /// </summary>
    class Report
    {
        /// <summary>
        /// Title of report.
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Address of the target site.
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Start time of the measurement.
        /// </summary>
        public DateTime Start { get; internal set; }
        /// <summary>
        /// End time of the measurement.
        /// </summary>
        public DateTime End { get; internal set; }

        /// <summary>
        /// A list of graph data dictionaries.
        /// </summary>
        public List<Dictionary<string, (int, double)>> GraphData { get; set; }
        /// <summary>
        /// A dictionary of metric data.
        /// </summary>
        public Dictionary<string, string> MetricData { get; } = new Dictionary<string, string>();
    }
}
