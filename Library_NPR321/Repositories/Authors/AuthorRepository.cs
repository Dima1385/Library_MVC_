using Library_NPR321.Data;
using Library_NPR321.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_NPR321.Repositories.Authors
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool Add(Author model)
        {
            _context.Authors.Add(model);
            var res = _context.SaveChanges();
            return res != 0;
        }

        public bool Delete(Author model)
        {
            _context.Remove(model);
            var res = _context.SaveChanges();
            return res != 0;
        }

        public IEnumerable<Author> GetAll()
        {
            return _context.Authors
                .Include(a => a.Books)
                .AsNoTracking();
        }

        public Author? GetById(int id)
        {
            var model = _context.Authors.FirstOrDefault(a => a.Id == id);
            return model;
        }

        public bool Update(Author model)
        {
            _context.Update(model);
            var res = _context.SaveChanges();
            return res != 0;
        }
    }
}
