using System.Data.Linq;
using System.Data.SqlServerCe;
using ProgrammingTechnologiesTask2.Data.Models;

namespace ProgrammingTechnologiesTask2.Data.Database
{
    public class LibraryDataContext : DataContext
    {
        public LibraryDataContext(string connectionString)
            : base(new SqlCeConnection(connectionString))
        {
        }

        public Table<BookEntity> Books
        {
            get { return GetTable<BookEntity>(); }
        }

        public Table<ReaderEntity> Readers
        {
            get { return GetTable<ReaderEntity>(); }
        }

        public Table<LoanEntity> Loans
        {
            get { return GetTable<LoanEntity>(); }
        }

        public Table<LibraryEventEntity> LibraryEvents
        {
            get { return GetTable<LibraryEventEntity>(); }
        }
    }
}