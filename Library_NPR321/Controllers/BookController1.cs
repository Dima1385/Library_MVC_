using Library_NPR321.Models;
using Library_NPR321.Repositories.Books;
using Microsoft.AspNetCore.Mvc;

namespace Library_NPR321.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var books = _bookRepository.GetAll();

            return View(books);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book model)
        {
            _bookRepository.Add(model);

            return RedirectToAction("Index");
        }


        public IActionResult Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _bookRepository.GetById((int)id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Book model)
        {
            _bookRepository.Update(model);

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = _bookRepository.GetById((int)id);

            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Book model)
        {
            _bookRepository.Delete(model);

            return RedirectToAction("Index");
        }
    }
}
