using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpRequestSender.Reports
{
    class Report
    {
        public string Title { get; set; }
        public string Address { get; set; }

        public List<string> OtherReports { get; } = new List<string>();

        public string Previous { get; set; }
        public string Next { get; set; }
        public List<Dictionary<string, int>> GraphData { get; set; }
        public Dictionary<string, string> MetricData { get; } = new Dictionary<string, string>();

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
            <title>{Title}</title>
            </head>";

            res += $@"<body onload=""autoSizeCanvas(); "">
            <table width = ""100%"" border = ""1"">
    
            <tr>
            <td align = ""center"" colspan = ""3"">
            <h1 style = ""color: black""> {Title} </h1>
            </td>
            </tr>

            <tr>
            <td align = ""center"" colspan = ""3"">
            <h3 style = ""color: black""> Address: {Address} </h3>
            </td>
            </tr> ";

            res += $@"<tr>
            <td align=""center"">
            <Button onclick = ""location.href=`{Previous}`""> &#8592;</Button>
            </td> ";

            res += @"<td align=""center"" width=""40%"">
            <select>";

            foreach (string item in OtherReports)
            {
                res += $@"
                <option value = ""{item}"" selected> {item} </option>";

            }
        
            res+= @"
            </select>
            </td> ";

            res += $@"
            <td align=""center"">
            <Button onclick = ""location.href=`{Next}`""> &#8594;</Button>
            </td>
            </tr>";

            res += $@"
            <tr>
            <td align=""center"" colspan=""3"">
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
            <canvas id = ""Chart Canvas"" style = ""width:100%; max-width:500px""></canvas>
                <script>
                    var xValues = [{GetTimeValuesOfReport()}];
                    var yValues = [{GetResponseValuesOfReport()}];";

            res += @"
            new Chart(""Chart Canvas"", {
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
                        yAxes:[{ GetValueScaleOfReport() }],";
            res += @"
                    }
                }
            });
            </script>
            </td>
            <td align=""center"">
            <canvas id = ""Chart Canvas 2"" style = ""width:100%; max-width:500px""></canvas>";
            
            res += $@"
                <script>
                    var xValues = [{GetTimeValuesOfReport()}];
                    var yValues = [{GetResponseTimeValuesOfReport()}];";

            res += @"
            new Chart(""Chart Canvas 2"", {
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
                        yAxes:[{ GetValueTimeScaleOfReport() }],";
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

            foreach (string item in MetricData.Keys)
            {
                res += $@"<tr>
                <td align=""left"" width=""30%"">
                <p><b>{item}</b></p>
                </td>
     
                <td align = ""left"" colspan = ""2"">
                <p> {MetricData[item]} </p>
                </td>
                </tr> ";
            }

            res += @"</table>
            </body>
            </html>";

            return res;
        }

        private string GetValueScaleOfReport()
        {
            string res = "{ticks: {min: -1, max: ";
            int maxValue = 0;
            for (int i = 0; i < GraphData.Count; i++)
            {
                if (GraphData[i].ContainsKey("OK"))
                {
                    if (GraphData[i]["OK"] > maxValue)
                    {
                        maxValue = GraphData[i]["OK"];
                    }
                }
            }
            res += Math.Min(maxValue * 1.1, maxValue + 1);
            res += "}}";
            return res;
        }

        private string GetValueTimeScaleOfReport()
        {
            string res = "{ticks: {min: -1, max: ";
            int maxValue = 0;
            for (int i = 0; i < GraphData.Count; i++)
            {
                if (GraphData[i].ContainsKey("OK"))
                {
                    if (1000 / GraphData[i]["OK"] > maxValue)
                    {
                        maxValue = 1000 / GraphData[i]["OK"];
                    }
                }
            }
            res += Math.Min(maxValue * 1.1, maxValue + 1);
            res += "}}";
            return res;
        }

        private string GetResponseValuesOfReport()
        {
            string res = "";
            for (int i = 0; i <GraphData.Count; i++)
            {
                if (GraphData[i].ContainsKey("OK"))
                {
                    res += GraphData[i]["OK"] + (i != GraphData.Count - 1 ? "," : "");
                }
                else
                {
                    res += 0 + (i != GraphData.Count - 1 ? "," : "");
                }
            }
            return res;
        }

        private string GetResponseTimeValuesOfReport()
        {
            string res = "";
            for (int i = 0; i < GraphData.Count; i++)
            {
                if (GraphData[i].ContainsKey("OK"))
                {
                    res += (1000.0 / GraphData[i]["OK"]).ToString("n2") + (i != GraphData.Count - 1 ? "," : "");
                }
                else
                {
                    res += 0 + (i != GraphData.Count - 1 ? "," : "");
                }
            }
            return res;
        }

        private string GetTimeValuesOfReport()
        {
            string res = "";
            for (int i = 0; i <GraphData.Count; i++)
            {
                res += i + (i != GraphData.Count - 1 ? "," : "");
            }
            return res;
        }
    }
}
