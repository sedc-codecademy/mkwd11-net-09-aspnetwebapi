using System.Net.Http;

try
{
    using(HttpClient httpClient = new HttpClient())
    {
        HttpResponseMessage response =
                         httpClient.GetAsync("http://localhost:5207/api/Test/testUser").Result;
        string responseBodyContent = response.Content.ReadAsStringAsync().Result;

        Console.WriteLine(responseBodyContent);
    }
}
catch(Exception e)
{
    Console.WriteLine("An error ocurred");
    Console.WriteLine(e.Message);
}
