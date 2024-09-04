using Library_NPR321.Models.ViewModels;
using Library_NPR321.Models;
using Library_NPR321.Repositories.Authors;
using Library_NPR321.Repositories.Books;
using Library_NPR321;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

public class BookController : Controller
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository, IWebHostEnvironment webHostEnvironment)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
        _webHostEnvironment = webHostEnvironment;
    }

    public IActionResult Index()
    {
        var books = _bookRepository.Books;
        return View(books);
    }

    // GET: Create
    public IActionResult Create()
    {
        var viewModel = new BookVM
        {
            Book = new Book { Title = "", Language = "", Genre = "" },
            ListItems = _authorRepository.Authors.Select(a =>
            {
                return new SelectListItem
                {
                    Text = $"{a.FirstName} {a.LastName}",
                    Value = a.Id.ToString()
                };
            })
        };
        return View(viewModel);
    }

    // POST: Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(BookVM model)
    {
        if (!ModelState.IsValid)
        {
            model.ListItems = _authorRepository.Authors.Select(a =>
            {
                return new SelectListItem
                {
                    Text = $"{a.FirstName} {a.LastName}",
                    Value = a.Id.ToString()
                };
            });
            return View(model);
        }

        var files = HttpContext.Request.Form.Files;
        string? imageName = null;

        if (files.Count > 0)
        {
            var imageFile = files[0];
            var types = imageFile.ContentType.Split("/");

            if (types[0] == "image")
            {
                var ext = types[1];
                imageName = Guid.NewGuid().ToString() + "." + ext;
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, Settings.BookImagePath, imageName);

                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }
            }
        }

        model.Book.Image = imageName;
        await _bookRepository.AddAsync(model.Book);


        return RedirectToAction("Index");
    }

    // GET: Edit
    public async Task<IActionResult> Edit(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        var viewModel = new BookVM
        {
            Book = book,
            ListItems = _authorRepository.Authors.Select(a =>
            {
                return new SelectListItem
                {
                    Text = $"{a.FirstName} {a.LastName}",
                    Value = a.Id.ToString()
                };
            })
        };

        return View(viewModel);
    }

    // GET: Book/Update/1
    public async Task<IActionResult> Update(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        var viewModel = new BookVM
        {
            Book = book,
            ListItems = _authorRepository.Authors.Select(a =>
            {
                return new SelectListItem
                {
                    Text = $"{a.FirstName} {a.LastName}",
                    Value = a.Id.ToString()
                };
            })
        };

        return View("Update", viewModel); 
    }


    // GET: Uptade
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(BookVM model, IFormFile? newImage)
    {
        Console.WriteLine("Метод Update викликаний"); 

        if (!ModelState.IsValid)
        {
            Console.WriteLine("Модель невалідна"); 
            model.ListItems = _authorRepository.Authors.Select(a =>
            {
                return new SelectListItem
                {
                    Text = $"{a.FirstName} {a.LastName}",
                    Value = a.Id.ToString()
                };
            });
            return View("Update", model);
        }

        var book = await _bookRepository.GetByIdAsync(model.Book.Id);

        if (book == null)
        {
            Console.WriteLine("Книгу не знайдено"); 
            return NotFound();
        }


        if (newImage != null && newImage.Length > 0)
        {
            if (!string.IsNullOrEmpty(book.Image))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, Settings.BookImagePath, book.Image);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            var newImageName = Guid.NewGuid() + Path.GetExtension(newImage.FileName);
            var newImagePath = Path.Combine(_webHostEnvironment.WebRootPath, Settings.BookImagePath, newImageName);

            using (var stream = new FileStream(newImagePath, FileMode.Create))
            {
                await newImage.CopyToAsync(stream);
            }

            book.Image = newImageName;
        }

        // Оновлення полів книги
        book.Title = model.Book.Title;
        book.Description = model.Book.Description;
        book.Language = model.Book.Language;
        book.Genre = model.Book.Genre;
        book.PageCount = model.Book.PageCount;
        book.Year = model.Book.Year;
        book.Publisher = model.Book.Publisher;
        book.AuthorId = model.Book.AuthorId;

        Console.WriteLine("Книга оновлена, збереження до бази даних"); 
        await _bookRepository.UpdateAsync(book);

        return RedirectToAction("Index");
    }

    // GET: Delete
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return View(book);
    }

    // POST: Delete
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var book = await _bookRepository.GetByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(book.Image))
        {
            var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, Settings.BookImagePath, book.Image);
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
        }

        await _bookRepository.DeleteAsync(book);
        return RedirectToAction(nameof(Index));
    }
}
