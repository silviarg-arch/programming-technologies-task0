namespace ProgrammingTechnologiesTask1.Data;

public class ReturnEvent : LibraryEvent
{
    public ReturnEvent(DateTime timestamp, string userId, string itemId)
        : base(timestamp, userId, itemId)
    {
    }
}