using System;
using System.Data.Linq.Mapping;

namespace ProgrammingTechnologiesTask2.Data.Models
{
    [Table(Name = "Loans")]
    public class LoanEntity : LoanData
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public override int LoanId { get; set; }

        [Column]
        public override int BookId { get; set; }

        [Column]
        public override int ReaderId { get; set; }

        [Column]
        public override DateTime BorrowDate { get; set; }

        [Column(CanBeNull = true)]
        public override DateTime? ReturnDate { get; set; }
    }
}