namespace IleriWebProject.Models
{
    public class Top10Books
    {
        public int BookId { get; set; }

        public int StatusId { get; set; }

        public string BookName { get; set; } = null!;

        public string Author { get; set; } = null!;

        public int PublicationYear { get; set; }

        public int CategoryId { get; set; }

        public int StockCount { get; set; }
    }
}
