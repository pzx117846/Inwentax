using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inwentax.Models
{
    public class Assignment
    {
        public int Id { get; set; }

        [Display(Name = "Użytkownik")]
        [Required(ErrorMessage = "Proszę podać użytkownika")]
        public string UserId { get; set; }

        [Display(Name = "Laptop")]
        public int? LaptopId { get; set; }

        [ForeignKey("LaptopId")]
        public Laptop Laptop { get; set; }

        [Display(Name = "Telefon")]
        public int? PhoneId { get; set; }

        [ForeignKey("PhoneId")]
        public Phone Phone { get; set; }

        [Display(Name = "Data wydania")]
        [Required(ErrorMessage = "Proszę podać datę wydania")]
        [DataType(DataType.Date)]
        public DateTime date_from { get; set; }

        [Display(Name = "Data zwrotu")]
        [DataType(DataType.Date)]
        public DateTime? date_to { get; set; }

        public string? Status { get; set; }
    }
}
