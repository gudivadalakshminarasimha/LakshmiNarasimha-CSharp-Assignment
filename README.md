# Employee Time Report – C# Assignment

A C# console application that calls the provided API, retrieves employee time entries, calculates the total working hours for each employee, and generates an HTML report and a PNG pie chart.

## How to run

1. Download `Executable/LakshmiNarasimha_C# Assignment.exe`.
2. Double-click it (an internet connection is required to call the API).
3. An `Output` folder is created next to the exe with both generated files.

## Output

![Pie chart](Output/EmployeePieChart.png)

- `Output/EmployeeReport.html` – a table of employee names and total working hours, sorted in descending order (highest hours first). Employees who worked less than 100 hours are highlighted with a light red background.
- `Output/EmployeePieChart.png` – a pie chart showing each employee's percentage of the total working hours.

## How it works

- **ApiService** – calls the API and retrieves the employee time-entry data.
- **ReportCalculator** – groups entries by employee and calculates their total working hours.
- **HtmlReportGenerator** – creates the HTML table and highlights employees below 100 hours.
- **PieChartGenerator** – creates the PNG pie chart of each employee's share of total hours.

## Data handling decisions

- Entries with an empty or whitespace employee name are skipped, because they cannot be assigned to an employee.
- Entries where the end time is earlier than or equal to the start time are skipped, because they do not represent a valid working duration.
- Entries with a `DeletedOn` value are still included, because the assignment asks for total time worked and does not mention deletions. This can be changed with one extra condition in ReportCalculator.
- The API field is named `StarTimeUtc` (missing a "t"). I mapped it to the C# property `StartTimeUtc` using the `[JsonPropertyName("StarTimeUtc")]` attribute so the value is deserialized correctly.

## Tech used

C#, .NET 8, System.Text.Json, ScottPlot
