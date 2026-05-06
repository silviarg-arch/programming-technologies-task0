namespace ProgrammingTechnologiesTask1.Data;

public abstract class LibraryEvent
{
    public DateTime Timestamp { get; }
    public string UserId { get; }
    public string ItemId { get; }

    protected LibraryEvent(DateTime timestamp, string userId, string itemId)
    {
        Timestamp = timestamp;
        UserId = userId;
        ItemId = itemId;
    }
}