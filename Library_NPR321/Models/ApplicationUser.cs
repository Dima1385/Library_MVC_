using Microsoft.AspNetCore.Identity;

namespace Library_NPR321.Models
{
    public class ApplicationUser : IdentityUser
    {
        // Додаткові властивості, якщо потрібно
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AvatarUrl { get; set; }  // Для зберігання шляху до аватара
    }
}
