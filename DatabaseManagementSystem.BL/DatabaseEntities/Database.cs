using System.Collections.ObjectModel;

namespace DatabaseManagementSystem.BL.DatabaseEntities
{
    public class Database
    {
        public string Name { get; private set; }

        public ObservableCollection<Table> Tables { get; set; }

        public Database(string name)
        {
            Name = name;
            Tables = new ObservableCollection<Table>();
        }
    }
}
