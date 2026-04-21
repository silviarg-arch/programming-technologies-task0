namespace ProgrammingTechnologiesTask1.Data;

public class LibraryState
{
    // BookId -> ReaderId
    public Dictionary<string, string> BorrowedBooks { get; } = new();
}