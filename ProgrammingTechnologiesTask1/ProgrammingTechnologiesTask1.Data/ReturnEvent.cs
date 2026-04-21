namespace ProgrammingTechnologiesTask1.Data;

public class ReturnEvent : LibraryEvent
{
    public ReturnEvent(DateTime timestamp, string readerId, string bookId)
        : base(timestamp, readerId, bookId)
    {
    }
}