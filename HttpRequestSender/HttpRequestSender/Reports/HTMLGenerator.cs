using System;
using System.Collections.Generic;

namespace HttpRequestSender.Reports
{
    internal class HTMLGenerator
    {
        private List<Report> reports;

        public HTMLGenerator(List<Report> reports)
        {
            this.reports = reports;
        }

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

            res += $@"<body onload=""autoSizeCanvas(); "">";

            int reportIndex = 0;

            foreach (Report report in reports)
            {
                string title = report.Title;
                string address = report.Address;
                string start = "Measurement start: " + report.Start.ToString("yyyy.MM.dd. hh:mm:ss.ffff");
                string end = "Measurement end: " + report.End.ToString("yyyy.MM.dd. hh:mm:ss.ffff");

                List<Dictionary<string, int>> graphData = report.GraphData;
                Dictionary<string, string> metricData = report.MetricData;

                res += $@"<table style=""margin: 25px 10px 40px 10px;"" width = ""100%"" border = ""1"">
    
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
                <h5 style = ""color: black""> {start} </h3>
                </td>
                <td align = ""center"" style=""width: 50%;"">
                <h5 style = ""color: black""> {end} </h3>
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
     
                    <td align = ""left"" colspan = ""2"">
                    <p> {metricData[item]} </p>
                    </td>
                    </tr> ";
                }

                res += @"</table>";
            }

            res += @"</body>
            </html>";

            return res;
        }

        private string GetValueScaleOfReport(List<Dictionary<string, int>> graphData)
        {
            string res = "{ticks: {min: -1, max: ";
            int maxValue = 0;
            for (int i = 0; i < graphData.Count; i++)
            {
                if (graphData[i].ContainsKey("OK"))
                {
                    if (graphData[i]["OK"] > maxValue)
                    {
                        maxValue = graphData[i]["OK"];
                    }
                }
            }
            res += Math.Min(maxValue * 1.1, maxValue + 1);
            res += "}}";
            return res;
        }

        private string GetValueTimeScaleOfReport(List<Dictionary<string, int>> graphData)
        {
            string res = "{ticks: {min: -1, max: ";
            int maxValue = 0;
            for (int i = 0; i < graphData.Count; i++)
            {
                if (graphData[i].ContainsKey("OK"))
                {
                    if (1000 / graphData[i]["OK"] > maxValue)
                    {
                        maxValue = 1000 / graphData[i]["OK"];
                    }
                }
            }
            res += Math.Min(maxValue * 1.1, maxValue + 1);
            res += "}}";
            return res;
        }

        private string GetResponseValuesOfReport(List<Dictionary<string, int>> graphData)
        {
            string res = "";
            for (int i = 0; i < graphData.Count; i++)
            {
                if (graphData[i].ContainsKey("OK"))
                {
                    res += graphData[i]["OK"] + (i != graphData.Count - 1 ? "," : "");
                }
                else
                {
                    res += 0 + (i != graphData.Count - 1 ? "," : "");
                }
            }
            return res;
        }

        private string GetResponseTimeValuesOfReport(List<Dictionary<string, int>> graphData)
        {
            string res = "";
            for (int i = 0; i < graphData.Count; i++)
            {
                if (graphData[i].ContainsKey("OK"))
                {
                    res += (1000.0 / graphData[i]["OK"]).ToString("n2") + (i != graphData.Count - 1 ? "," : "");
                }
                else
                {
                    res += 0 + (i != graphData.Count - 1 ? "," : "");
                }
            }
            return res;
        }

        private string GetTimeValuesOfReport(List<Dictionary<string, int>> graphData)
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
