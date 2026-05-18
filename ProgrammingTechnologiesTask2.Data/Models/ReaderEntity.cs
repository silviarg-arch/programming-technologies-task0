using System.Data.Linq.Mapping;

namespace ProgrammingTechnologiesTask2.Data.Models
{
    [Table(Name = "Readers")]
    public class ReaderEntity : ReaderData
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public override int ReaderId { get; set; }

        [Column(CanBeNull = false)]
        public override string Name { get; set; }

        [Column(CanBeNull = false)]
        public override string Email { get; set; }
    }
}