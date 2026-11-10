using EmployeeTimeReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTimeReport.Services
{
    public class ReportCalculator
    {

        public List<EmployeeSummary> CalculateTotal(List<TimeEntry> timeEntries)
        {
            return timeEntries .Where(e=>!string.IsNullOrWhiteSpace(e.EmployeeName) && e.EndTimeUtc > e.StartTimeUtc)
                .GroupBy(e=>e.EmployeeName.Trim())
                  .Select(g => new EmployeeSummary
                  {
                      Name = g.Key,
                      TotalHours = g.Sum(e =>
                          (e.EndTimeUtc - e.StartTimeUtc).TotalHours)
                  })
                  .OrderByDescending(e => e.TotalHours)
                    .ToList();
        }
    }
}
