using Library_NPR321.Models;
using Library_NPR321.Repositories.Authors;
using Microsoft.AspNetCore.Mvc;

namespace Library_NPR321.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IActionResult Index()
        {
            var authors = _authorRepository.GetAll();

            return View(authors);
        }

        // GET
        public IActionResult Create()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author model)
        {
            _authorRepository.Add(model);

            return RedirectToAction("Index");
        }

        // GET
        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _authorRepository.GetById((int)id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Author model)
        {
            _authorRepository.Update(model);

            return RedirectToAction("index");
        }

        // GET
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _authorRepository.GetById((int)id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Author model)
        {
            _authorRepository.Delete(model);

            return RedirectToAction("index");
        }
    }
}
