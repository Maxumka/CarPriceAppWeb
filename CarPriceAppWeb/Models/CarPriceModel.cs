using System.ComponentModel.DataAnnotations;

namespace CarPriceAppWeb.Models
{
    public class CarPriceModel
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum length 3 characters")]
        public string Company { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Minimum length 3 characters")]
        public string Model { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Mileage can be only positive and natural number")]
        public int Mileage { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Engine power can be only between 0 and 1000")]
        public int EnginePower { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Engine volume cannot be negative number")]
        public double EngineVolume { get; set; }

        [Required]
        [Range(1900, 2021, ErrorMessage = "Year can be only between 1990 and 2021")]
        public int Year { get; set; }

        public bool Transmission { get; set; }
    }
}
