namespace ProgrammingTechnologiesTask2.Data.Models
{
    public abstract class BookData
    {
        public abstract int BookId { get; set; }
        public abstract string Title { get; set; }
        public abstract string Author { get; set; }
        public abstract int PublicationYear { get; set; }
        public abstract bool IsAvailable { get; set; }
    }
}