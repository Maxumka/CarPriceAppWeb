using System.Net.Http;
using System.Net.Http.Json;
using CarPriceAppWeb.Models;
using System.Threading.Tasks;

namespace CarPriceAppWeb.Services
{
    public class CarPriceHttpClient
    {
        private readonly HttpClient _client;

        static public bool IsSignIned { get; private set; } = false;

        public CarPriceHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> SignIn(UserModel user)
        {
            if (user is null) return false;

            var token = await GetToken(user);

            if (token is null) return false;

            SetToken(token);

            return true;
        }

        public async Task<bool> SignUp(UserModel user)
        {
            var response = await _client.PostAsJsonAsync("/signup", user);

            if (!response.IsSuccessStatusCode) return false;

            var token = await GetToken(user);

            if (token is null) return false;

            SetToken(token);

            return true;
        }

        private async Task<string> GetToken(UserModel user)
        {
            var response = await _client.PostAsJsonAsync("/identity", user);

            if (!response.IsSuccessStatusCode) return null;

            var token = await response.Content.ReadAsStringAsync();

            return token;
        }

        private void SetToken(string token)
        {
            IsSignIned = true;
            _client.DefaultRequestHeaders.Authorization = new("Bearer", token);
        }
    }
}
