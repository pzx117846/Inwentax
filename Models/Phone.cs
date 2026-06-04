using System.ComponentModel.DataAnnotations;

namespace Inwentax.Models
{
    public class Phone
    {
        public int Id { get; set; }

        [Display(Name = "Marka telefonu")]
        [Required(ErrorMessage = "Proszę podać markę telefonu")]
        [StringLength(40, ErrorMessage = "Maksymalna długość to 40 znaków")]
        public string Brand { get; set; }

        [Display(Name = "Model telefonu")]
        [Required(ErrorMessage = "Proszę podać model telefonu")]
        [StringLength(50, ErrorMessage = "Maksymalna długość to 50 znaków")]
        public string Model { get; set; }

        [Display(Name = "Rok produkcji telefonu")]
        [Required(ErrorMessage = "Proszę podać rok telefonu")]
        [Range(2010, 2026, ErrorMessage = "Rok produkcji musi być z zakresu 2010 - 2026")]
        public int Year { get; set; }

        [Display(Name = "IMEI telefonu")]
        [Required(ErrorMessage = "Proszę podać IMEI telefonu")]
        [MinLength(15, ErrorMessage = "Numer IMEI powinien mieć 15 cyfr")]
        [MaxLength(15, ErrorMessage = "Numer IMEI powinien mieć 15 cyfr")]
        public string Imei { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "Proszę wybrać status")]
        public string Status { get; set; } = "Bufor";
    }
}
