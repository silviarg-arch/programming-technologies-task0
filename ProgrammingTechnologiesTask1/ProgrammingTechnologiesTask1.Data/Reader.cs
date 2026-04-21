namespace ProgrammingTechnologiesTask1.Data;

public class Reader
{
    public string ReaderId { get; }
    public string Name { get; }

    public Reader(string readerId, string name)
    {
        ReaderId = readerId;
        Name = name;
    }
}
