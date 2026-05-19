using System;

namespace ProgrammingTechnologiesTask2.Presentation.Model.Models
{
    public class LibraryEventModel
    {
        public int EventId { get; set; }
        public string EventType { get; set; }
        public int BookId { get; set; }
        public int? ReaderId { get; set; }
        public DateTime EventDate { get; set; }
    }
}