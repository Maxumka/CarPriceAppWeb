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
        Task<int> GetPriceAsync(CarPriceModel car);
        Task GetCarBestDeals(CarBestDealFormModel car);
        Task<CarHistoryModel[]> GetHistortAsync();
    }

    public class CarPriceService : ICarPriceService
    {
        private readonly HttpClient _client;

        private readonly LocalStorageService _localStorage;

        private readonly NavigationManager _navigationManager;

        private readonly AlertService _alertService;

        public CarPriceService(HttpClient client, 
                               LocalStorageService localStorage, 
                               NavigationManager navigationManager, 
                               AlertService alertService)
            => (_client, _localStorage, _navigationManager, _alertService) 
            = (client, localStorage, navigationManager, alertService);

        public async Task GetCarBestDeals(CarBestDealFormModel car)
        {
            var res = await PostAsync<CarBestDealFormModel, Either<CarBestDealDataModel[], Error>>("/carbestdeals", car);

            if (res.HasError) return;

            _localStorage.CarBestDealModels = res.Result;
        }

        public async Task<CarHistoryModel[]> GetHistortAsync()
        {
            return await GetAsync<CarHistoryModel[]>("/history");
        }

        public async Task<int> GetPriceAsync(CarPriceModel car)
        {
            return await PostAsync<CarPriceModel, int>("/carprice", car);
        }

        public async Task SignInAsync(UserModel user)
        {
            _localStorage.Token = await PostAsync<UserModel, string>("/identity", user); ;
        }

        public void SignOut()
        {
            _localStorage.Token = null;
            _localStorage.CarBestDealModels = null;
            _navigationManager.NavigateTo("account/signin");
        }

        public async Task<bool> SignUpAsync(UserModel user)
        {
            return await PostAsync<UserModel, bool>("/signup", user);
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

            var result =  await response.Content.ReadFromJsonAsync<Either<T, Error>>();

            return HandleErrors(result);
        }

        private T HandleErrors<T>(Either<T, Error> either)
        {
            if (either.HasError)
            {
                _alertService.ShowAlert(either.Error.Message);
                return default;
            }
            else
            {
                return either.Result;
            }
        }
    }
}
