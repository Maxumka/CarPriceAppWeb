using System.Collections.Generic;

namespace CarPriceAppWeb.Models
{
    public class CarTypeModel 
    {
        public string Name { get; set; }

        public string Link { get; set; }
    }

    public class CarMarkModel 
    {
        public string Name { get; set; }

        public List<CarTypeModel> TypeModels { get; set; }
    }
}