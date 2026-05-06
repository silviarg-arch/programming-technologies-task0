namespace ProgrammingTechnologiesTask1.Data;

public class LibraryState : ProcessState
{
    // BookId -> ReaderId
    public Dictionary<string, string> BorrowedBooks { get; } = new();
}