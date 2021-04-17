using System.ComponentModel.DataAnnotations;

namespace CarPriceAppWeb.Models
{
    public class CarPriceModel
    {
        [Required(ErrorMessage = "Компания не может быть пустой")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        public string Company { get; set; }

        [Required(ErrorMessage = "Модель не может быть пустой")]
        [MinLength(3, ErrorMessage = "Минимальная длина 3 символа")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Пробег не может быть пустым")]
        [Range(0, int.MaxValue, ErrorMessage = "Пробег натуральное число")]
        public int? Mileage { get; set; }

        [Required(ErrorMessage = "Мощность не может быть пустой")]
        [Range(1, 1000, ErrorMessage = "Мощность натуральное число")]
        public int? EnginePower { get; set; }

        [Required(ErrorMessage = "Объем не может быть пустым")]
        public double? EngineVolume { get; set; }

        [Required(ErrorMessage = "Год не может быть пустым")]
        public int? Year { get; set; }

        public bool Transmission { get; set; }
    }
}
