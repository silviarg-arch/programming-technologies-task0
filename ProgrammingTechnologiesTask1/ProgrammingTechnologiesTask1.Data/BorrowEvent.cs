namespace ProgrammingTechnologiesTask1.Data;

public class BorrowEvent : LibraryEvent
{
    public BorrowEvent(DateTime timestamp, string userId, string itemId)
        : base(timestamp, userId, itemId)
    {
    }
}
