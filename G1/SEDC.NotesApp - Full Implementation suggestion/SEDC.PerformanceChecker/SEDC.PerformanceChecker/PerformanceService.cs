namespace SEDC.PerformanceChecker
{
    public static class PerformanceService
    {
        private static string _notesAddress;
        public static void SetAddress(string address) => _notesAddress = address;
        public static void CheckPerformance()
        {
            HttpClient client = new HttpClient();
            string address = _notesAddress;
            int limit = 2500;

            HttpResponseMessage response = client.GetAsync(address).Result;
            string responseBody = response.Content.ReadAsStringAsync().Result;
            Console.ForegroundColor = ConsoleColor.Green;
            if (int.Parse(responseBody) > limit)
                Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Performance: {responseBody} [Limit: {limit}]");
        }
    }
}
