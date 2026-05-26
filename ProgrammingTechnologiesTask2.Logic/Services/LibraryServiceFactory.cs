using ProgrammingTechnologiesTask2.Data.Repositories;

namespace ProgrammingTechnologiesTask2.Logic.Services
{
    public static class LibraryServiceFactory
    {
        public static LibraryService CreateDefault()
        {
            LibraryRepository repository = LibraryRepositoryFactory.CreateDefault();
            return new LibraryServiceImplementation(repository);
        }
    }
}