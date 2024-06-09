using MobileTeaApp.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MobileTeaApp.Services
{
    public class ApiConnectionService<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ApiConnectionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                //PropertyNameCaseInsensitive = true
            };
        }

        public async Task<T> GetResource(string address)
        {
            
                var response = await _httpClient.GetStringAsync(address);
                var resource = JsonSerializer.Deserialize<T>(response, _jsonOptions);
                return resource;
            
           
        }

        public async Task<List<T>> GetResources(string address)
        {
            var response = await _httpClient.GetStringAsync(address);
            var resources = JsonSerializer.Deserialize<List<T>>(response, _jsonOptions);
            return resources;

        }

        public async Task PostResource(string address, T resource)
        {
            var jsonResource = JsonSerializer.Serialize(resource, _jsonOptions);
            var content = new StringContent(jsonResource, Encoding.UTF8, "application/json");
            await _httpClient.PostAsync(address, content);
        }

        public async Task PutResource(string address, T resource)
        {
            var jsonResource = JsonSerializer.Serialize(resource, _jsonOptions);
            var content = new StringContent(jsonResource, Encoding.UTF8, "application/json");
            await _httpClient.PutAsync(address, content);
        }

        public async Task DeleteResource(string address)
        {
            await _httpClient.DeleteAsync(address);
        }

        public async Task<(bool success, int userId)> Login(Profile profile)
        {
            string jsonProfile = JsonSerializer.Serialize(profile, _jsonOptions);
            var content = new StringContent(jsonProfile, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Profile/login")
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                int userId = JsonSerializer.Deserialize<int>(responseBody, _jsonOptions);
                return (true, userId);
            }
            return (false, default);
        }

        public async Task<bool> Register(Profile profile)
        {
            string jsonProfile = JsonSerializer.Serialize(profile, _jsonOptions);
            var content = new StringContent(jsonProfile, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Profile/register")
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);

            return response.IsSuccessStatusCode;
        }
    }
}
