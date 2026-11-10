
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using EmployeeTimeReport.Models;

namespace EmployeeTimeReport.Services
{
    public class HtmlReportGenerator
    {
        public void Generate(List<EmployeeSummary> employees, string filePath)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html>");
            sb.AppendLine("<head>");
            sb.AppendLine("<title>Employee Working Hours</title>");
            sb.AppendLine("<style>");
            sb.AppendLine("body { font-family: Arial; margin: 30px; }");
            sb.AppendLine("h2 { text-align: center; }");
            sb.AppendLine("table { width: 80%; margin: auto; border-collapse: collapse; }");
            sb.AppendLine("th, td { border: 1px solid black; padding: 10px; text-align: center; }");
            sb.AppendLine("th { background-color: #333; color: white; }");
            sb.AppendLine(".low { background-color: #ffcccc; }");
            sb.AppendLine("</style>");

            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine("<h2>Employee Working Hours Report</h2>");
            sb.AppendLine("<table>");
            sb.AppendLine("<tr>");
            sb.AppendLine("<th>Name</th>");
            sb.AppendLine("<th>Total Time Worked</th>");
            sb.AppendLine("</tr>");


            foreach (var employee in employees)
            {
                string rowClass = "";

                if (employee.TotalHours < 100)
                {
                    rowClass = "low";
                }

                string name = WebUtility.HtmlEncode(employee.Name ?? "");
                string hours = employee.TotalHours.ToString("F2", CultureInfo.InvariantCulture);

                sb.AppendLine("<tr class='" + rowClass + "'>");
                sb.AppendLine("<td>" + name + "</td>");
                sb.AppendLine("<td>" + hours + "</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");


            string? folder = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            File.WriteAllText(filePath, sb.ToString(), Encoding.UTF8);

            Console.WriteLine("HTML report generated successfully.");
        }
    }
}
