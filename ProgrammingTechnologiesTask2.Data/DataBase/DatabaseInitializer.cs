using System;
using System.Data.SqlServerCe;
using System.IO;

namespace ProgrammingTechnologiesTask2.Data.Database
{
    public static class DatabaseInitializer
    {
        public static string GetDefaultDatabasePath()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LibraryDatabase.sdf");
        }

        public static string GetConnectionString(string databasePath)
        {
            return "Data Source=" + databasePath + ";Persist Security Info=False;";
        }

        public static string EnsureDatabase()
        {
            string databasePath = GetDefaultDatabasePath();
            EnsureDatabase(databasePath);
            return GetConnectionString(databasePath);
        }

        public static void EnsureDatabase(string databasePath)
        {
            if (File.Exists(databasePath))
            {
                return;
            }

            string directory = Path.GetDirectoryName(databasePath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            string connectionString = GetConnectionString(databasePath);

            SqlCeEngine engine = new SqlCeEngine(connectionString);
            engine.CreateDatabase();

            using (SqlCeConnection connection = new SqlCeConnection(connectionString))
            {
                connection.Open();

                ExecuteNonQuery(connection,
                    "CREATE TABLE Readers (" +
                    "ReaderId int IDENTITY(1,1) NOT NULL PRIMARY KEY, " +
                    "Name nvarchar(100) NOT NULL, " +
                    "Email nvarchar(100) NOT NULL)");

                ExecuteNonQuery(connection,
                    "CREATE TABLE Books (" +
                    "BookId int IDENTITY(1,1) NOT NULL PRIMARY KEY, " +
                    "Title nvarchar(150) NOT NULL, " +
                    "Author nvarchar(100) NOT NULL, " +
                    "PublicationYear int NOT NULL, " +
                    "IsAvailable bit NOT NULL)");

                ExecuteNonQuery(connection,
                    "CREATE TABLE Loans (" +
                    "LoanId int IDENTITY(1,1) NOT NULL PRIMARY KEY, " +
                    "BookId int NOT NULL, " +
                    "ReaderId int NOT NULL, " +
                    "BorrowDate datetime NOT NULL, " +
                    "ReturnDate datetime NULL)");

                ExecuteNonQuery(connection,
                    "CREATE TABLE LibraryEvents (" +
                    "EventId int IDENTITY(1,1) NOT NULL PRIMARY KEY, " +
                    "EventType nvarchar(50) NOT NULL, " +
                    "BookId int NOT NULL, " +
                    "ReaderId int NULL, " +
                    "EventDate datetime NOT NULL)");

                ExecuteNonQuery(connection,
                    "INSERT INTO Readers (Name, Email) VALUES ('Alice Johnson', 'alice@example.com')");

                ExecuteNonQuery(connection,
                    "INSERT INTO Readers (Name, Email) VALUES ('Bob Smith', 'bob@example.com')");

                ExecuteNonQuery(connection,
                    "INSERT INTO Readers (Name, Email) VALUES ('Charlie Brown', 'charlie@example.com')");

                ExecuteNonQuery(connection,
                    "INSERT INTO Books (Title, Author, PublicationYear, IsAvailable) VALUES ('1984', 'George Orwell', 1949, 1)");

                ExecuteNonQuery(connection,
                    "INSERT INTO Books (Title, Author, PublicationYear, IsAvailable) VALUES ('Dune', 'Frank Herbert', 1965, 1)");

                ExecuteNonQuery(connection,
                    "INSERT INTO Books (Title, Author, PublicationYear, IsAvailable) VALUES ('The Hobbit', 'J.R.R. Tolkien', 1937, 1)");

                ExecuteNonQuery(connection,
                    "INSERT INTO Books (Title, Author, PublicationYear, IsAvailable) VALUES ('Pride and Prejudice', 'Jane Austen', 1813, 1)");
            }
        }

        private static void ExecuteNonQuery(SqlCeConnection connection, string commandText)
        {
            using (SqlCeCommand command = new SqlCeCommand(commandText, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }
}