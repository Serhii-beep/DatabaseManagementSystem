using DatabaseManagementSystem.BL.DatabaseEntities;

namespace DatabaseManagementSystem.BL.FileManagers
{
    public interface IDatabaseFileManager
    {
        void SaveDatabase(Database database);

        void DeleteDatabase(string databaseName);

        Database LoadDatabase(string databaseName);

        List<string> GetAllDatabaseNames();
    }
}
