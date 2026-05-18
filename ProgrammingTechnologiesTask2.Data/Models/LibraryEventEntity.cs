using System;
using System.Data.Linq.Mapping;

namespace ProgrammingTechnologiesTask2.Data.Models
{
    [Table(Name = "LibraryEvents")]
    public class LibraryEventEntity : LibraryEventData
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public override int EventId { get; set; }

        [Column(CanBeNull = false)]
        public override string EventType { get; set; }

        [Column]
        public override int BookId { get; set; }

        [Column(CanBeNull = true)]
        public override int? ReaderId { get; set; }

        [Column]
        public override DateTime EventDate { get; set; }
    }
}