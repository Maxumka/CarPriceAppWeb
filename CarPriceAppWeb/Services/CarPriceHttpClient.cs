using System.Net.Http;
using System.Net.Http.Json;
using CarPriceAppWeb.Models;
using System.Threading.Tasks;
using System.Linq;

namespace CarPriceAppWeb.Services
{
    public class CarPriceHttpClient
    {
        private readonly HttpClient _client;

        static public bool IsSignIned { get; private set; } = false;

        static private string Token { get; set; } = "";

        public CarPriceHttpClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<bool> SignInAsync(UserModel user)
        {
            if (user is null) return false;

            var token = await GetTokenAsync(user);

            if (token is null) return false;

            return true;
        }

        public async Task<bool> SignUpAsync(UserModel user)
        {
            var response = await _client.PostAsJsonAsync("/signup", user);

            if (!response.IsSuccessStatusCode) return false;

            var token = await GetTokenAsync(user);

            if (token is null) return false;

            return true;
        }

        public async Task<int> GetPriceAsync(CarModel car)
        {
            SetToken();

            var response = await _client.PostAsJsonAsync("/carprice", car);

            if (!response.IsSuccessStatusCode) return -1;

            var price = await response.Content.ReadFromJsonAsync<int>();

            return price;
        }

        private async Task<string> GetTokenAsync(UserModel user)
        {
            var response = await _client.PostAsJsonAsync("/identity", user);

            if (!response.IsSuccessStatusCode) return null;

            var token = await response.Content.ReadFromJsonAsync<string>();

            Token = token;
            IsSignIned = true;

            return token;
        }

        private void SetToken()
        {
            _client.DefaultRequestHeaders.Authorization = new("Bearer", Token);
        }
    }
}
