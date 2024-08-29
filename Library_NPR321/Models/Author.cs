using System.ComponentModel.DataAnnotations;

namespace Library_NPR321.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public required string FirstName { get; set; }
        [Required]
        [MaxLength(100)]
        public required string LastName { get; set; }
        public DateTime Birthday { get; set; }

        public virtual IEnumerable<Book> Books { get; set; } = new List<Book>();
    }
}
