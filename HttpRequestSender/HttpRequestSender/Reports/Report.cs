using System;
using System.Collections.Generic;

namespace HttpRequestSender.Reports
{
    class Report
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public DateTime Start { get; internal set; }
        public DateTime End { get; internal set; }

        public List<Dictionary<string, int>> GraphData { get; set; }
        public Dictionary<string, string> MetricData { get; } = new Dictionary<string, string>();
    }
}
