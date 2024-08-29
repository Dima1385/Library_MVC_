using Library_NPR321.Data;
using Library_NPR321.Models;
using Microsoft.EntityFrameworkCore;
using Library_NPR321.Repositories.Books;

namespace Library_NPR321.Repositories.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Book model)
        {
            _context.Books.Add(model);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public bool Delete(Book model)
        {
            _context.Books.Remove(model);
            var result = _context.SaveChanges();
            return result > 0;
        }

        public IEnumerable<Book> GetAll()
        {
            return _context.Books
                .Include(b => b.Author)  // 
                .AsNoTracking()
                .ToList();
        }

        public Book? GetById(int id)
        {
            return _context.Books
                .Include(b => b.Author)  // 
                .AsNoTracking()
                .FirstOrDefault(b => b.Id == id);
        }

        public bool Update(Book model)
        {
            _context.Books.Update(model);
            var result = _context.SaveChanges();
            return result > 0;
        }
    }
}
