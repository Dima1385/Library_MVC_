using Library_NPR321.Models;

namespace Library_NPR321.Repositories.Books
{
    public interface IBookRepository
    {
        IEnumerable<Book> GetAll();
        bool Add(Book model);
        bool Update(Book model);
        bool Delete(Book model);
        Book? GetById(int id);
    }
}
