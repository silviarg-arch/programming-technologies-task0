using System;

namespace ProgrammingTechnologiesTask2.Data.Models
{
    public abstract class LibraryEventData
    {
        public abstract int EventId { get; set; }
        public abstract string EventType { get; set; }
        public abstract int BookId { get; set; }
        public abstract int? ReaderId { get; set; }
        public abstract DateTime EventDate { get; set; }
    }
}