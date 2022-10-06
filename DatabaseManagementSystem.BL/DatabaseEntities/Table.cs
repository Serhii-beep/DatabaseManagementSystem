using DatabaseManagementSystem.BL.Fields;

namespace DatabaseManagementSystem.BL.DatabaseEntities
{
    public class Table
    {
        public string Name { get; private set; }

        public List<Field> Fields { get; set; }
        
        public List<Row> Rows { get; set; }

        public Table(string name)
        {
            Name = name;
            Fields = new List<Field>();
            Rows = new List<Row>();
        }
    }
}
