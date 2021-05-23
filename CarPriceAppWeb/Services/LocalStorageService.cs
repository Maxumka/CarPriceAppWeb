using CarPriceAppWeb.Models;
using System.Collections.Generic;

namespace CarPriceAppWeb.Services
{
    public class LocalStorageService
    {
        public IEnumerable<CarBestDealDataModel> CarBestDealModels { get; set; }

        public string Token { get; set; } = null;

        public List<CarMarkModel> CarMarkModels { get; set; } = null;
    }
}
