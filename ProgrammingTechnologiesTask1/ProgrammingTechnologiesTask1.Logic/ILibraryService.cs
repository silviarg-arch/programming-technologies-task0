using ProgrammingTechnologiesTask1.Data;

namespace ProgrammingTechnologiesTask1.Logic;

public interface ILibraryService
{
    void RegisterReader(string readerId, string name);
    void AddBook(string bookId, string title, string author);
    void BorrowBook(string readerId, string bookId);
    void ReturnBook(string readerId, string bookId);
    bool IsBookAvailable(string bookId);
    int GetEventCount();
}