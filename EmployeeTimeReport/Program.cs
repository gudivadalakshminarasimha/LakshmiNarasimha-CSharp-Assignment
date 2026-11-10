
using System;
using System.IO;
using System.Threading.Tasks;
using EmployeeTimeReport.Services;

namespace EmployeeTimeReport
{
    internal class Program
    {
        private const string ApiUrl = "https://rc-vault-fap-live-1.azurewebsites.net/api/gettimeentries?code=vO17RnE8vuzXzPJo5eaLLjXjmRW07law99QTD90zat9FfOQJKKUcgQ==";

        static async Task Main(string[] args)
        {
            try
            {
                string outputFolder = Path.Combine(AppContext.BaseDirectory, "Output");

                string htmlPath = Path.Combine(outputFolder, "EmployeeReport.html");
                string pngPath = Path.Combine(outputFolder, "EmployeePieChart.png");

                Directory.CreateDirectory(outputFolder);
                var apiService = new ApiService();
                var reportCalculator = new ReportCalculator();
                var htmlGenerator = new HtmlReportGenerator();
                var pieGenerator = new PieChartGenerator();
            Console.WriteLine("Fetching employee data...");

                var entries = await apiService.GetTimeEntryAsync(ApiUrl);

                Console.WriteLine("Total entries received: " + entries.Count);
                var employees = reportCalculator.CalculateTotal(entries);
                Console.WriteLine("Total employees: " + employees.Count);

                htmlGenerator.Generate(employees, htmlPath);
                pieGenerator.Generate(employees, pngPath);
                Console.WriteLine();
                Console.WriteLine("Reports generated successfully!");
                Console.WriteLine("Output folder: " + outputFolder);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("API Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unexpected error: " + ex.Message);
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
