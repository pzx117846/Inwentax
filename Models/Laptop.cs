using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.ComponentModel.DataAnnotations;

namespace Inwentax.Models
{
    public class Laptop
    {
        public int Id { get; set; }

        [Display(Name = "Marka laptopa")]
        [Required(ErrorMessage = "Proszę podać markę laptopa")]
        [StringLength(40, ErrorMessage = "Maksymalna długość to 40 znaków")]
        public string Brand { get; set; }

        [Display(Name = "Model laptopa")]
        [Required(ErrorMessage = "Proszę podać model laptopa")]
        [StringLength(50, ErrorMessage = "Maksymalna długość to 50 znaków")]
        public string Model { get; set; }

        [Display(Name = "Rok produkcji laptopa")]
        [Required(ErrorMessage = "Proszę podać rok produkcji")]
        [Range(2010, 2026, ErrorMessage = "Rok produkcji musi być z zakresu 2010 - 2026")]
        public int Year { get; set; }

        [Display(Name = "Numer seryjny laptopa")]
        [Required(ErrorMessage = "Proszę podać numer seryjny laptopa")]
        [StringLength(70, ErrorMessage = "Maksymalna długość to 70 znaków")]
        public string serial_number { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Proszę wybrać status")]
        public string Status { get; set; } = "Bufor";

    }
}
