using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace NotesAppConsoleClient
{
    public class NotesAppService
    {
        private HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7071";
        private string? _token;

        public NotesAppService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_baseUrl);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task UserLoginAsync(string username, string password)
        {
            // Create login dto
            var loginRequestDto = new LoginRequest(username, password);

            // Serialize the login request object to JSON
            // Send the POST request to the login endpoint
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("/api/Users/login", loginRequestDto);

            if (response.IsSuccessStatusCode)
            {
                // Read the response content as a string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize the response content into a dynamic object
                dynamic jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseBody);

                // Access the desired property from the dynamic object
                _token = jsonResponse?.token;
            }
            else
            {
                throw new Exception("\n\n\tInvalid login attempt!");
            }
        }

        public async Task GetNotesAsync()
        {
            // Ensure that a token is available
            if (string.IsNullOrEmpty(_token))
            {
                throw new Exception("Authentication token is missing. Please log in first.");
            }

            // Add the Authorization header with the token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            // Send the GET request to the notes endpoint
            HttpResponseMessage response = await _httpClient.GetAsync("/api/notes");

            if (response.IsSuccessStatusCode)
            {
                // Read and deserialize the response content into a list of notes
                string responseBody = await response.Content.ReadAsStringAsync();
                List<NoteResponse> notes = JsonConvert.DeserializeObject<List<NoteResponse>>(responseBody);

                // Print notes if any
                notes?.PrintNotes();
            }
            else
            {
                throw new Exception("Notes request failed!");
            }
        }
    }


    // Models that our Console Client uses (similar/same to those from our api)

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class NoteResponse
    {
        public string Text { get; set; }
        public Priority Priority { get; set; }
        public Tag Tag { get; set; }
        public string UserFullName { get; set; }

        public override string ToString()
        {
            return $"\n*Note text: {Text}. *Priority: {Priority}. *Tag: {Tag}. *User: {UserFullName}";
            // return base.ToString(); // will return the Type of the object
        }
    }

    public enum Priority
    {
        Low = 1,
        Medium,
        High
    }

    public enum Tag
    {
        Work = 1,
        Health,
        SocialLife
    }
}
