using System.Net;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;

namespace CarPriceAppWeb.Services
{
    public interface ICarPriceHttpClient
    {
        Task<T> GetAsync<T>(string uri);
        Task<U> PostAsync<T, U>(string uri, T value);
    }

    public class CarPriceHttpClient : ICarPriceHttpClient
    {
        private readonly HttpClient _client;

        private readonly LocalStorageService _localStorage;

        private readonly NavigationManager _navigationManager;

        //public CarPriceHttpClient(HttpClient client, LocalStorageService localStorage, NavigationManager navigationManager)
        //    => (_client, _localStorage, _navigationManager) = (client, localStorage, navigationManager);

        public async Task<T> GetAsync<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            return await SendRequestAsync<T>(request);
        }

        public async Task<U> PostAsync<T, U>(string uri, T value)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json")
            };

            return await SendRequestAsync<U>(request);
        }

        private void SetToken(ref HttpRequestMessage requestMessage)
        {
            var token = _localStorage.Token;

            if (token is null) return;

            requestMessage.Headers.Authorization = new("Bearer", token);
        }

        private async Task<T> SendRequestAsync<T>(HttpRequestMessage requestMessage)
        {
            SetToken(ref requestMessage);

            var response = await _client.SendAsync(requestMessage);

            if (response.StatusCode is HttpStatusCode.Unauthorized)
            {
                _navigationManager.NavigateTo("account/signin");
                return default;
            }

            return await response.Content.ReadFromJsonAsync<T>();
        }
    }
}
