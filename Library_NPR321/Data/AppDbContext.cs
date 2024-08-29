using Library_NPR321.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_NPR321.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Authors
            modelBuilder.Entity<Author>().HasData(
                new Author
                {
                    Id = 1,
                    FirstName = "Джоан",
                    LastName = "Роулінг",
                    Birthday = new DateTime(1965, 7, 31)
                },
                new Author
                {
                    Id = 2,
                    FirstName = "Джон Рональд Руел",
                    LastName = "Толкін",
                    Birthday = new DateTime(1892, 1, 3)
                },
                new Author
                {
                    Id = 3,
                    FirstName = "Стівен",
                    LastName = "Кінг",
                    Birthday = new DateTime(1947, 9, 21)
                }
                );

            // Books
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Title = "Гаррі Поттер і філософський камінь",
                    Genre = "Фентезі",
                    Language = "Українська",
                    PageCount = 320,
                    Year = (short)1997,
                    Publisher = " ",
                    Image = "E:\\програмування\\Git_C#(бази)\\Library_NPR321_MVC\\images\\Гаррі Поттер і філософський камінь.jpg",
                    AuthorId = 1
                },
                new Book
                {
                    Id = 2,
                    Title = "Володар Перснів",
                    Genre = "Фентезі",
                    Language = "Українська",
                    PageCount = 1216,
                    Year = (short)1954,
                    Publisher = " ",
                    Image = "E:\\програмування\\Git_C#(бази)\\Library_NPR321_MVC\\images\\The_Fellowship_Of_The_Ring.jpg",
                    AuthorId = 2
                },
                new Book
                {
                    Id = 3,
                    Title = "Сяйво",
                    Genre = "Жахи",
                    Language = "Українська",
                    PageCount = 447,
                    Year = (short)1977,
                    Publisher = "Клуб Сімейного Дозвілля",
                    Image = "E:\\програмування\\Git_C#(бази)\\Library_NPR321_MVC\\images\\Сяйво.jpg",
                    AuthorId = 3
                }
            );
        }
    }
}
