using Newtonsoft.Json;
using System.Text.Json;
using System.Text;

namespace SchoolManagementSystem.Sevices
{
    public class GenericApiClient
    {
        private readonly HttpClient _httpClient;
        public GenericApiClient(IHttpClientFactory httpClientFactory, HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");

            var baseUrl = configuration["ApiSettings:BaseUrl"];
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<T> PostAsync<T>(string url, object requestBody)
        {
            var jsonContent = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);

            if(!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error : {response.StatusCode}, response :{errorContent}");

                throw new Exception($"Error: {response.StatusCode}, Content: {errorContent}");
            }

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(responseContent);
        }
        public async Task<T> GetAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public async Task<T> PutAsync<T>(string url, object data)
        {

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public async Task DeleteAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();
        }
    }
}
