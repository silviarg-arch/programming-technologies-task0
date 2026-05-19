namespace ProgrammingTechnologiesTask2.Presentation.Model.Models
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublicationYear { get; set; }
        public bool IsAvailable { get; set; }

        public string AvailabilityText
        {
            get
            {
                return IsAvailable ? "Available" : "Borrowed";
            }
        }
    }
}