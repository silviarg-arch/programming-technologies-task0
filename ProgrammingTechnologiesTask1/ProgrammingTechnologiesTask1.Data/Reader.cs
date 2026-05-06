namespace ProgrammingTechnologiesTask1.Data;

public class Reader : User
{
    public override string UserId { get; }
    public override string Name { get; }

    public Reader(string userId, string name)
    {
        UserId = userId;
        Name = name;
    }
}