using CarPriceAppWeb.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using CarPriceAppWeb.Infrastructure.Error;
using CarPriceAppWeb.Infrastructure.Helpers;
using System.Net.Http;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;

namespace CarPriceAppWeb.Services
{
    public interface ICarPriceService
    {
        Task SignInAsync(UserModel user);
        Task<bool> SignUpAsync(UserModel user);
        void SignOut();
        Task<int> GetPriceAsync(CarModel car);
        Task GetCarBestDeals(CarModel car);
        Task<CarHistoryModel[]> GetHistortAsync();
    }

    public class CarPriceService : ICarPriceService
    {
        private readonly HttpClient _client;

        private readonly LocalStorageService _localStorage;

        private readonly NavigationManager _navigationManager;

        public CarPriceService(HttpClient client, LocalStorageService localStorage, NavigationManager navigationManager)
            => (_client, _localStorage, _navigationManager) = (client, localStorage, navigationManager);

        public async Task GetCarBestDeals(CarModel car)
        {
            var res = await PostAsync<CarModel, Either<CarBestDealModel[], Error>>("/carbestdeals", car);

            if (res.Error is not null) return;

            _localStorage.CarBestDealModels = res.Result;
        }

        public async Task<CarHistoryModel[]> GetHistortAsync()
        {
            var res = await GetAsync<Either<CarHistoryModel[], Error>>("/history");

            return res.Error is null ? res.Result : null;
        }

        public async Task<int> GetPriceAsync(CarModel car)
        {
            var res = await PostAsync<CarModel, Either<int, Error>>("/carprice", car);

            return res.Error is null ? res.Result : 0;
        }

        public async Task SignInAsync(UserModel user)
        {
            var res = await PostAsync<UserModel, Either<string, Error>>("/identity", user);

            if (res.Error is not null) return;

            _localStorage.Token = res.Result;
        }

        public void SignOut()
        {
            _localStorage.Token = null;
            _localStorage.CarBestDealModels = null;
            _navigationManager.NavigateTo("account/signin");
        }

        public async Task<bool> SignUpAsync(UserModel user)
        {
            var res = await PostAsync<UserModel, Either<bool, Error>>("/signup", user);

            return res.Error is null ? res.Result : false;
        }

        private async Task<T> GetAsync<T>(string uri)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, uri);

            return await SendRequestAsync<T>(request);
        }

        private async Task<U> PostAsync<T, U>(string uri, T value)
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
