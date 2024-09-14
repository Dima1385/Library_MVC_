using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Library_MVC_.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Ім'я")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Прізвище")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Підтвердження пароля")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Паролі не збігаються.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Аватар")]
        public IFormFile Avatar { get; set; }

        // Інші поля, якщо є, наприклад, для адреси чи телефону
    }
}
