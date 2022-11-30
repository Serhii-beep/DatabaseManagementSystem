namespace DatabaseManagementSystem.WebApi.Models
{
    public class Table
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DatabaseId { get; set; }
        public virtual ICollection<Field> Fields { get; set; }
        public virtual ICollection<Row> Rows { get; set; }
        public virtual Database Database { get; set; }
        public Table()
        {
            Fields = new List<Field>();
            Rows = new List<Row>();
        }
    }
}
