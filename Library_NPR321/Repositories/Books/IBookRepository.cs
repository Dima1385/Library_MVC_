using Library_NPR321.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Library_NPR321.Repositories.Books
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(int id);
        Task<bool> AddAsync(Book book);
        Task<bool> UpdateAsync(Book book);
        Task<bool> DeleteAsync(Book book); 
        IEnumerable<Book> Books { get; }
    }
}
