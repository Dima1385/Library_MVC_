using Library_NPR321.Data;
using Library_NPR321.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library_NPR321.Repositories.Books
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> Books => _context.Books.Include(b => b.Author);

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.Include(b => b.Author).FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<bool> AddAsync(Book book)
        {
            _context.Books.Add(book);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            _context.Books.Update(book);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
