using EmployeeTimeReport.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeTimeReport.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient= new HttpClient();

        public async  Task<List<TimeEntry>> GetTimeEntryAsync(string url)
        {
            try
            {

                string json = await _httpClient.GetStringAsync(url);

                var options = new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                List<TimeEntry>? timeEntries = System.Text.Json.JsonSerializer.Deserialize<List<TimeEntry>>(json, options);

                return timeEntries ?? new List<TimeEntry>();
            }
            catch (HttpRequestException ex)
            {
                throw new InvalidOperationException("Failed to retrieve employee time entries from the API.", ex);
            }
            catch(JsonException ex)
            {
                throw new InvalidOperationException( "Failed to deserialize the API JSON response.", ex);
            }

        }

    }
}
