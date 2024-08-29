using Library_NPR321.Models;

namespace Library_NPR321.Repositories.Authors
{
    public interface IAuthorRepository
    {
        IEnumerable<Author> GetAll();
        bool Add(Author model);
        bool Update(Author model);
        bool Delete(Author model);
        Author? GetById(int id);
    }
}
