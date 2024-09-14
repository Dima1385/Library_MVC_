using Library_MVC_.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Threading.Tasks;

namespace Library_MVC_.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public AccountController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Обробка аватара
                if (model.Avatar != null && model.Avatar.Length > 0)
                {
                    var fileName = Path.GetFileName(model.Avatar.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Avatar.CopyToAsync(stream);
                    }

                    // Збереження URL аватара в базі даних або інші дії
                }

                // Логіка для збереження користувача в базі даних
                // Наприклад, використання UserManager для реєстрації

                // Якщо реєстрація успішна, перенаправити користувача
                return RedirectToAction("Index", "Home");
            }

            // Якщо модель не валідна, повернути назад до форми реєстрації
            return View(model);
        }
    }
}
