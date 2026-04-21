namespace ProgrammingTechnologiesTask1.Data;

public abstract class LibraryEvent
{
    public DateTime Timestamp { get; }
    public string ReaderId { get; }
    public string BookId { get; }

    protected LibraryEvent(DateTime timestamp, string readerId, string bookId)
    {
        Timestamp = timestamp;
        ReaderId = readerId;
        BookId = bookId;
    }
}