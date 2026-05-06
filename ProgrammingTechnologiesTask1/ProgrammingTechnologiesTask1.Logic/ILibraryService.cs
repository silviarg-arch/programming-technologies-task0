namespace ProgrammingTechnologiesTask1.Logic;

public interface ILibraryService
{
    void RegisterReader(string userId, string name);
    void AddBook(string itemId, string title, string author);
    void BorrowBook(string userId, string itemId);
    void ReturnBook(string userId, string itemId);
    bool IsBookAvailable(string itemId);
    int GetEventCount();
}