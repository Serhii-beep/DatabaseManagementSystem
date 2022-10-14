using DatabaseManagementSystem.BL.DatabaseEntities;
using Newtonsoft.Json;

namespace DatabaseManagementSystem.BL.FileManagers
{
    public class DatabaseFileManager : IDatabaseFileManager
    {
        private string _defaultPathToStorage;

        public DatabaseFileManager()
        {
            _defaultPathToStorage = "C:\\Other\\C#\\DatabaseManagementSystem\\storage";
        }

        public Database LoadDatabase(string databaseName)
        {
            using var sr = new StreamReader(BuildPath(databaseName));
            string databaseSerialized = sr.ReadToEnd();
            Database? result = JsonConvert.DeserializeObject<Database>(databaseSerialized);
            if(result == null)
            {
                throw new Exception("Invalid database format");
            }
            return result;
        }

        public void SaveDatabase(Database database)
        {
            if(GetAllDatabaseNames().Any(n => n.ToLower() == database.Name.ToLower()))
            {
                //throw new ArgumentException("Database with such name already exists");
            }
            string databaseSerialized = JsonConvert.SerializeObject(database);
            using var sw = new StreamWriter(BuildPath(database.Name));
            sw.Write(databaseSerialized);
        }

        public List<string> GetAllDatabaseNames()
        {
            DirectoryInfo di = new DirectoryInfo(_defaultPathToStorage);
            FileInfo[] files = di.GetFiles("*.json");
            var result = files.Select(f => f.Name[..^5]).ToList();
            return result;
        }

        private string BuildPath(string databaseName)
        {
            return Path.Combine(_defaultPathToStorage, databaseName + ".json");
        }
    }
}
