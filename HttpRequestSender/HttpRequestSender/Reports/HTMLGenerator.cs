using System;
using System.Collections.Generic;
using System.Globalization;

namespace HttpRequestSender.Reports
{
    internal class HTMLGenerator
    {
        private List<Report> reports;

        public HTMLGenerator(List<Report> reports)
        {
            this.reports = reports;
        }

        /// <summary>
        /// Generates an .HTML code from reports, incorporating their information.
        /// </summary>
        /// <returns> Returns the .HTML code. </returns>
        public string Generate()
        {
            string res = @"<!DOCTYPE html>
            
            <html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
            <script src=""https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.4/Chart.js""></script>
            <script>
                $(function () {
                    $('#DinamicSelect').on('change', function () {
                        var url = $(this).val();
                        if (url) {
                        window.location = url;
                    }
                return false;
                })
            })
            </script>";

            res += $@"<head>
            <meta charset = ""utf-8""/>
            <title>Measurement Report</title>
            </head>";

            res += $@"<body onload=""autoSizeCanvas();"" style=""width: 80%; margin-left: auto; margin-right: auto;"">";

            int reportIndex = 0;

            // Loads in the data from the list of reports.
            foreach (Report report in reports)
            {
                string title = report.Title;
                string address = report.Address;
                string start = "Measurement start: " + report.Start.ToString("yyyy.MM.dd. hh:mm:ss.ffff");
                string end = "Measurement end: " + report.End.ToString("yyyy.MM.dd. hh:mm:ss.ffff");

                List<Dictionary<string, (int, double)>> graphData = report.GraphData;
                Dictionary<string, string> metricData = report.MetricData;

                res += $@"<div style=""width: 100%; padding: 30px 10px 30px 10px;"">
                <table width = ""100%"" border = ""1"">
                <tr>
                <td align = ""center"" colspan = ""2"">
                <h1 style = ""color: black""> {title} </h1>
                </td>
                </tr>
                <tr>
                <td align = ""center"" colspan = ""2"">
                <h3 style = ""color: black""> Address: {address} </h3>
                </td>
                </tr>
                <tr>
                <td align = ""center"" style=""width: 50%;"">
                <h3 style = ""color: black""> {start} </h3>
                </td>
                <td align = ""center"" style=""width: 50%;"">
                <h3 style = ""color: black""> {end} </h3>
                </td>
                </tr>
                <tr>
                <td align=""center"" colspan = ""2"">
                <table width=""100%"">
                <tr>
                <td width=""50%"" align=""center"">
                <a><b>Responses per second</b></a>
                </td>
                <td width=""50%"" align=""center"">
                <a><b>Average response time per second</b></a>
                </td>
                </tr>
                <tr>
                <td align=""center"">
                <canvas id = ""Chart_Canvas_1_{reportIndex}"" style = ""width:100%; max-width:500px""></canvas>
                    <script>
                        var xValues = [{GetTimeValuesOfReport(graphData)}];
                        var yValues = [{GetResponseValuesOfReport(graphData)}];";

                res += $@"
                new Chart(""Chart_Canvas_1_{reportIndex}"", ";

                res += @"{
                    type: ""line"",
                    data:
                    {
                            labels: xValues,
                            datasets:
                            [{
                                fill: false,
                                lineTension: 0,
                                backgroundColor: ""rgba(0,0,255,1.0)"",
                                borderColor: ""rgba(0,0,255,0.1)"",
                                data: yValues
                            }]
                    },
                    options:
                    {
                        legend: { display: false},
                        scales:
                    {";

                res += $@"
                            yAxes:[{GetValueScaleOfReport(graphData)}],";
                res += @"
                        }
                    }
                });";

                res += $@"</script>
                </td>
                <td align=""center"">
                <canvas id = ""Chart_Canvas_2_{reportIndex}"" style = ""width:100%; max-width:500px""></canvas>";

                res += $@"
                    <script>
                        var xValues = [{GetTimeValuesOfReport(graphData)}];
                        var yValues = [{GetResponseTimeValuesOfReport(graphData)}];";

                res += $@"
                new Chart(""Chart_Canvas_2_{reportIndex++}"", ";

                res += @"{
                    type: ""line"",
                    data:
                    {
                            labels: xValues,
                            datasets:
                            [{
                                fill: false,
                                lineTension: 0,
                                backgroundColor: ""rgba(0,0,255,1.0)"",
                                borderColor: ""rgba(0,0,255,0.1)"",
                                data: yValues
                            }]
                    },
                    options:
                    {
                        legend: { display: false},
                        scales:
                    {";

                res += $@"
                            yAxes:[{GetValueTimeScaleOfReport(graphData)}],";
                res += @"
                        }
                    }
                });
                </script>
                </td>
                </td>
                </tr>
                </table>
                </td>
                </tr> ";

                foreach (string item in metricData.Keys)
                {
                    res += $@"<tr>
                    <td align=""left"" width=""30%"">
                    <p><b>{item}</b></p>
                    </td>
     
                    <td align = ""right"" colspan = ""2"">
                    <p> {metricData[item]} </p>
                    </td>
                    </tr> ";
                }

                res += @"</table>
                </div>";
            }

            res += @"</body>
            </html>";

            return res;
        }

        /// <summary>
        /// Gets the Y axis maximum from the OK response values of report.
        /// </summary>
        /// <param name="graphData"> List of graph data dictionaries. </param>
        /// <returns> Returns a string of min and max values of Y axis. </returns>
        private string GetValueScaleOfReport(List<Dictionary<string, (int, double)>> graphData)
        {
            string res = "{ticks: {min: -1, max: ";
            int maxValue = 0;
            for (int i = 0; i < graphData.Count; i++)
            {
                if (graphData[i].ContainsKey("OK"))
                {
                    if (graphData[i]["OK"].Item1 > maxValue)
                    {
                        maxValue = graphData[i]["OK"].Item1;
                    }
                }
            }
            res += Math.Max(maxValue * 1.1, maxValue + 1);
            res += "}}";
            return res;
        }

        /// <summary>
        /// Gets the Y axis maximum from the average OK response time values of report.
        /// </summary>
        /// <param name="graphData"> List of graph data dictionaries. </param>
        /// <returns> Returns a string of min and max values of Y axis. </returns>
        private string GetValueTimeScaleOfReport(List<Dictionary<string, (int, double)>> graphData)
        {
            string res = "{ticks: {min: -1, max: ";
            int maxValue = 0;
            for (int i = 0; i < graphData.Count; i++)
            {
                if (graphData[i].ContainsKey("OK"))
                {
                    if (1000 / graphData[i]["OK"].Item2 > maxValue)
                    {
                        maxValue = (int)Math.Ceiling(1000 / graphData[i]["OK"].Item2);
                    }
                }
            }
            res += Math.Max(maxValue * 1.1, maxValue + 1);
            res += "}}";
            return res;
        }

        /// <summary>
        /// Gets the "OK" response values of the report.
        /// </summary>
        /// <param name="graphData"> List of graph data dictionaries. </param>
        /// <returns> Returns the string of result. </returns>
        private string GetResponseValuesOfReport(List<Dictionary<string, (int, double)>> graphData)
        {
            string res = "";
            for (int i = 0; i < graphData.Count; i++)
            {
                if (graphData[i].ContainsKey("OK"))
                {
                    res += graphData[i]["OK"].Item1 + (i != graphData.Count - 1 ? "," : "");
                }
                else
                {
                    res += " " + (i != graphData.Count - 1 ? "," : "");
                }
            }
            return res;
        }

        /// <summary>
        /// Gets the average "OK" response time values of the report.
        /// </summary>
        /// <param name="graphData"> List of graph data dictionaries. </param>
        /// <returns>Returns the string of result. </returns>
        private string GetResponseTimeValuesOfReport(List<Dictionary<string, (int, double)>> graphData)
        {
            string res = "";
            for (int i = 0; i < graphData.Count; i++)
            {
                if (graphData[i].ContainsKey("OK"))
                {
                    res += Math.Round(1000.0 / graphData[i]["OK"].Item2, 2).ToString(CultureInfo.InvariantCulture) + (i != graphData.Count - 1 ? "," : "");
                }
                else
                {
                    res += " " + (i != graphData.Count - 1 ? "," : "");
                }
            }
            return res;
        }

        /// <summary>
        /// Gets the time values of the report. For the X axis placement.
        /// </summary>
        /// <param name="graphData"> List of graph data dictionaries. </param>
        /// <returns> Returns the string of result. </returns>
        private string GetTimeValuesOfReport(List<Dictionary<string, (int, double)>> graphData)
        {
            string res = "";
            for (int i = 0; i < graphData.Count; i++)
            {
                res += i + (i != graphData.Count - 1 ? "," : "");
            }
            return res;
        }
    }
}
