using SEDC.PerformanceChecker;

var port = "5043"; // Add your port here
var address = $"http://localhost:{port}/api/external/performance/getnote";

Console.WriteLine("Performance checks started...");
Console.WriteLine("-----------------------------");
PerformanceService.SetAddress(address);
PerformanceService.CheckPerformance();
Console.ReadLine();