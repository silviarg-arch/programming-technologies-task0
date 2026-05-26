namespace ProgrammingTechnologiesTask2.Data.Repositories
{
    public static class LibraryRepositoryFactory
    {
        public static LibraryRepository CreateDefault()
        {
            return new SqlCompactLibraryRepository();
        }
    }
}