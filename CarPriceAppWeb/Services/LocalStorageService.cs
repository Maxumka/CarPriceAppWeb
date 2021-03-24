using CarPriceAppWeb.Models;

namespace CarPriceAppWeb.Services
{
    public class LocalStorageService
    {
        public CarBestDealModel[] CarBestDealModels { get; set; }

        public string Token { get; set; } = null;
    }
}
