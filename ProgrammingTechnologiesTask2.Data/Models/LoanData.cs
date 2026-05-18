using System;

namespace ProgrammingTechnologiesTask2.Data.Models
{
    public abstract class LoanData
    {
        public abstract int LoanId { get; set; }
        public abstract int BookId { get; set; }
        public abstract int ReaderId { get; set; }
        public abstract DateTime BorrowDate { get; set; }
        public abstract DateTime? ReturnDate { get; set; }
    }
}