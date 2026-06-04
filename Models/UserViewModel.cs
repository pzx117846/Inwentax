using System.ComponentModel.DataAnnotations;

namespace Inwentax.Models
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty;

        [Display(Name = "Imie")]
        [Required(ErrorMessage = "Proszę podać imie użytkownika")]
        [StringLength(30, ErrorMessage = "Maksymalna ilość znaków to 30")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Nazwisko")]
        [Required(ErrorMessage = "Proszę podać nazwisko użytkownika")]
        [StringLength(30, ErrorMessage = "Maksymalna ilość znaków to 30")]
        public string Surname { get; set; } = string.Empty;

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Proszę podać e-mail")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Display(Name = "Rola")]
        [Required(ErrorMessage = "Proszę wybrać rolę użytkownika")]
        public string Role { get; set; } = "User";
    }
}
