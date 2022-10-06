namespace DatabaseManagementSystem.BL.DatabaseEntities
{
    public class Database
    {
        public string Name { get; private set; }

        public List<Table> Tables { get; set; }

        public Database(string name)
        {
            Name = name;
            Tables = new List<Table>();
        }
    }
}
