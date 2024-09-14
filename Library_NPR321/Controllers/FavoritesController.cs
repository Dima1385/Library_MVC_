using Library_NPR321.Models;
using Library_NPR321.Repositories.Books;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Library_NPR321.Controllers
{
    public class FavoritesController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FavoritesController(IBookRepository bookRepository, IHttpContextAccessor httpContextAccessor)
        {
            _bookRepository = bookRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        // Відображення списку улюблених книг
        public IActionResult Index()
        {
            var favoriteBooks = GetFavoriteBooks();
            return View(favoriteBooks);
        }

        // Додавання книги до улюблених
        public async Task<IActionResult> AddToFavorites(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            var favoriteBooks = GetFavoriteBooks();
            if (!favoriteBooks.Any(b => b.Id == id))
            {
                favoriteBooks.Add(book);
                SaveFavoriteBooks(favoriteBooks);
            }

            return RedirectToAction("Index", "Books");
        }

        // Видалення книги з улюблених
        public IActionResult RemoveFromFavorites(int id)
        {
            var favoriteBooks = GetFavoriteBooks();
            var bookToRemove = favoriteBooks.FirstOrDefault(b => b.Id == id);
            if (bookToRemove != null)
            {
                favoriteBooks.Remove(bookToRemove);
                SaveFavoriteBooks(favoriteBooks);
            }

            return RedirectToAction("Index");
        }

        // Метод для отримання списку улюблених книг
        private List<Book> GetFavoriteBooks()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var favoriteBooksJson = session.GetString(Settings.FavoriteSessionKey);
            if (string.IsNullOrEmpty(favoriteBooksJson))
            {
                return new List<Book>();
            }

            return JsonConvert.DeserializeObject<List<Book>>(favoriteBooksJson) ?? new List<Book>();
        }

        // Метод для збереження списку улюблених книг у сесію
        private void SaveFavoriteBooks(List<Book> favoriteBooks)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var favoriteBooksJson = JsonConvert.SerializeObject(favoriteBooks);
            session.SetString(Settings.FavoriteSessionKey, favoriteBooksJson);
        }
    }
}
