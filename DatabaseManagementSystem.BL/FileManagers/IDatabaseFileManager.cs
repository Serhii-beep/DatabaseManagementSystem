using DatabaseManagementSystem.BL.DatabaseEntities;

namespace DatabaseManagementSystem.BL.FileManagers
{
    public interface IDatabaseFileManager
    {
        void SaveDatabase(Database database);

        Database LoadDatabase(string databaseName);
    }
}
