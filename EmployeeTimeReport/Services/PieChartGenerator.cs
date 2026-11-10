
using System;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using EmployeeTimeReport.Models;
using ScottPlot;
using System.Globalization;

namespace EmployeeTimeReport.Services
{
    public class PieChartGenerator
    {
        public void Generate(List<EmployeeSummary> employees, string filePath)
        {
            double grandTotal = employees.Sum(e => e.TotalHours);

            if (grandTotal <= 0)
            {
                Console.WriteLine("No working hours available to create pie chart.");
                return;
            }

            var palette = new ScottPlot.Palettes.Category20();
            var slices = new List<PieSlice>();

            for (int i = 0; i < employees.Count; i++)
            {
                var employee = employees[i];
                double percentage = (employee.TotalHours / grandTotal) * 100;

                string percentText = percentage.ToString(
                    "F2", CultureInfo.InvariantCulture) + "%";
                var slice = new PieSlice
                {
                    Value = employee.TotalHours,
                    Label = percentText,
                    LegendText = employee.Name + " (" + percentText + ")",
                    FillColor = palette.GetColor(i)
                };

                slices.Add(slice);
            }
            var plot = new Plot();

            var pie = plot.Add.Pie(slices);

            pie.SliceLabelDistance = 0.7;

            plot.Title("Employee Working Hours Percentage");

            plot.ShowLegend();
            plot.Axes.Frameless();
            plot.HideGrid();
            string? folder = Path.GetDirectoryName(filePath);

            if (!string.IsNullOrEmpty(folder))
            {
                Directory.CreateDirectory(folder);
            }

            plot.SavePng(filePath, 800, 600);
            Console.WriteLine("Pie chart generated successfully.");
        }
    }
}
