using CarPriceAppWeb.Models;

namespace CarPriceAppWeb.Services
{
    public class LocalStorageService
    {
        public CarBestDealDataModel[] CarBestDealModels { get; set; }

        public string Token { get; set; } = null;
    }
}
