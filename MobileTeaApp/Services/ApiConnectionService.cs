using MobileTeaApp.Models;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;


namespace MobileTeaApp.Services
{
    public class ApiConnectionService<T> where T : class
    {
        private readonly HttpClient _httpClient;

        public ApiConnectionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T> GetResource(string adress)
        {
            return await _httpClient.GetFromJsonAsync<T>(adress);
        }

        public async Task<List<T>> GetResources(string adress)
        {
            return await _httpClient.GetFromJsonAsync<List<T>>(adress);
        }

        public async Task PostResource(string adress, T resource)
        {
            await _httpClient.PostAsJsonAsync(adress, resource);
        }

        public async Task PutResource(string adress, T resource)
        {
            await _httpClient.PutAsJsonAsync(adress, resource);
        }

        public async Task DeleteResource(string adress)
        {
            await _httpClient.DeleteAsync(adress);
        }




        public async Task<(bool success, int userId)> Login(Profile profile)
        {
            string jsonProfile = JsonSerializer.Serialize(profile);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Profile/login")
            {
                Content = new StringContent(jsonProfile, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                int userId = JsonSerializer.Deserialize<int>(responseBody);

                return (true, userId);
            }
            return (false, default);
        }



        public async Task<bool> Register(Profile profile)
        {
            string jsonProfile = JsonSerializer.Serialize(profile);

            var request = new HttpRequestMessage(HttpMethod.Post, "api/Profile/register")
            {
                Content = new StringContent(jsonProfile, Encoding.UTF8, "application/json")
            };

            var response = await _httpClient.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
