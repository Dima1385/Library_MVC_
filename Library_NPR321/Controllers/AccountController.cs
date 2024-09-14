using Library_MVC_.Models;
using Library_NPR321.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace Library_MVC_.Controllers
{
    public class AccountController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IWebHostEnvironment env, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _env = env;
            _userManager = userManager;
            _signInManager = signInManager;
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
                // Створення нового користувача
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                // Обробка аватара
                if (model.Avatar != null && model.Avatar.Length > 0)
                {
                    // Якщо потрібно зберегти аватар як файл:
                    var fileName = Path.GetFileName(model.Avatar.FileName);
                    var filePath = Path.Combine(_env.WebRootPath, "images", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Avatar.CopyToAsync(stream);
                    }

                    // Зберегти URL аватара в базі даних (як варіант)
                    user.AvatarUrl = "/images/" + fileName;
                }

                // Реєстрація користувача в системі
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Успішна реєстрація, автоматичний вхід
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                // Додати помилки у випадку невдалої реєстрації
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Якщо модель не валідна, повернути назад до форми реєстрації
            return View(model);
        }
    }
}
