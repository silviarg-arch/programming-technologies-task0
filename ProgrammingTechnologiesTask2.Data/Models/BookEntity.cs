using System.Data.Linq.Mapping;

namespace ProgrammingTechnologiesTask2.Data.Models
{
    [Table(Name = "Books")]
    public class BookEntity : BookData
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public override int BookId { get; set; }

        [Column(CanBeNull = false)]
        public override string Title { get; set; }

        [Column(CanBeNull = false)]
        public override string Author { get; set; }

        [Column]
        public override int PublicationYear { get; set; }

        [Column]
        public override bool IsAvailable { get; set; }
    }
}