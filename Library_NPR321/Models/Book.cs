namespace Library_NPR321.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Language { get; set; }
        public int PageCount { get; set; }
        public short Year { get; set; }
        public string Publisher { get; set; }
        public string Image { get; set; }
        public int AuthorId { get; set; }
        public Author Author { get; set; }  
    }
}
