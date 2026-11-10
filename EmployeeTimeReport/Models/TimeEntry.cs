
using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
namespace EmployeeTimeReport.Models
{
    public class TimeEntry
    {
        public string Id { get; set; } = string.Empty;
        public string? EmployeeName { get; set; }
        [JsonPropertyName("StarTimeUtc")]
        public DateTime StartTimeUtc { get; set; }
        public DateTime EndTimeUtc { get; set; }
        public string? EntryNotes { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
