using System.ComponentModel.DataAnnotations;

namespace Inwentax.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required(ErrorMessage = "Proszę wybrać rodzaj zgłoszenia")]
        [Display(Name = "Rodzaj zgłoszenia")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Proszę opisać problem (max. 1000 znaków)")]
        [StringLength(1000)]
        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Data zgłoszenia")]
        [DataType(DataType.Date)]
        public DateTime Created { get; set; }

        [Display(Name = "Staus")]
        public string Status { get; set; }
    }
}
