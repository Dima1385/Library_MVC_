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

        public IEnumerable<Book> Books => _context.Books.AsNoTracking().ToList();

        public async Task<Book?> GetByIdAsync(int? id)
        {
            if (id == null)
            {
                return null;
            }
            return await _context.Books.FindAsync(id);
        }

        public async Task<bool> AddAsync(Book book)
        {
            _context.Books.Add(book);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> RemoveAsync(Book book)
        {
            _context.Books.Remove(book);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
