using Notes.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Notes.Services.Service.External
{
    public class PlaceholderService : IPlaceholderService
    {
        private HttpClient httpClient;
        private static JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        public PlaceholderService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient(HttpClients.PlaceholderApi);
        }

        public async Task<IEnumerable<UserModel>> GetUsers()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "users");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<IEnumerable<UserModel>>(json);
            return result;
        }

        public async Task<PostModel> CreatePost(CreatePostModel createPostModel)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "posts");
            var modelJson = JsonSerializer.Serialize(createPostModel, options);
            request.Content = new StringContent(modelJson, Encoding.UTF8, "application/json");
            var response = await httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<PostModel>(json, options);
        }
    }
}
