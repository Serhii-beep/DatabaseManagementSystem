namespace DatabaseManagementSystem.WebApi.Models
{
    public class Database
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Table> Tables { get; set; }
        public Database()
        {
            Tables = new List<Table>();
        }
    }
}
