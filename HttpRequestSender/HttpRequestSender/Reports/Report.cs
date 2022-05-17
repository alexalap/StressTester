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

        public List<string> OtherReports { get; } = new List<string>();

        public string Previous { get; set; }
        public string Next { get; set; }
        public List<Dictionary<string, int>> GraphData { get; set; }
        public Dictionary<string, string> MetricData { get; } = new Dictionary<string, string>();

        public string Generate()
        {
            string res = $@"<!DOCTYPE html>

            <html lang=""en"" xmlns=""http://www.w3.org/1999/xhtml"">
            <head>
            <meta charset = ""utf-8""/>
 
            <title>{Title}</title>
            </head>";

            res += $@"<body onload=""autoSizeCanvas(); "">
            <table width = ""100%"" border = ""1"">
    
            <tr>
    
            <td align = ""center"" colspan = ""3"">
       
            <h1 style = ""color: black""> {Title} </h1>
            
            </td>
            
            </tr> ";

            res += $@"<tr>
            <td align=""center"">
            <Button onclick = ""location.href=`{Previous}`""> &#8592;</Button>
            </td> ";

            res += @"<td align=""center"" width=""40 % "">
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

            res += @"
            <tr>
            <td align=""center"" colspan=""3"">
            <canvas id = ""Chart Canvas"" style = ""width:100%; background-color:black"" height = ""150""></canvas>
     
            </td>
     
            </tr> ";

            foreach (string item in MetricData.Keys)
            {
                res += $@"<tr>
                <td align=""left"" width=""30 % "">
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
    }
}
