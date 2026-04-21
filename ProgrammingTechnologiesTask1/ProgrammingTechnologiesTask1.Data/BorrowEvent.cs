namespace ProgrammingTechnologiesTask1.Data;

public class BorrowEvent : LibraryEvent
{
    public BorrowEvent(DateTime timestamp, string readerId, string bookId)
        : base(timestamp, readerId, bookId)
    {
    }
}