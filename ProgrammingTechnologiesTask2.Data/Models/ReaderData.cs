namespace ProgrammingTechnologiesTask2.Data.Models
{
    public abstract class ReaderData
    {
        public abstract int ReaderId { get; set; }
        public abstract string Name { get; set; }
        public abstract string Email { get; set; }
    }
}