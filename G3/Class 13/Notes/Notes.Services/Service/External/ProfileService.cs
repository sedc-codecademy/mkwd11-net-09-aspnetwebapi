using Notes.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notes.Services.Service.External
{
    public class ProfileService
        : IProfileService
    {
        private HttpClient client;

        public ProfileService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient(HttpClients.Profiles);
        }
        public async Task<IEnumerable<ProfileModel>> GetProfiles(string authToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/profiles")
            {
                Method = HttpMethod.Get
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            var result = await client.SendAsync(request);
            result.EnsureSuccessStatusCode();
            var json = await result.Content.ReadAsStringAsync();
            var models = JsonSerializer.Deserialize<IEnumerable<ProfileModel>>(json);
            return models;
        }
    }
}
