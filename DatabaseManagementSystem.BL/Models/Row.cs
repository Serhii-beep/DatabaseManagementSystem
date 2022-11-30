namespace DatabaseManagementSystem.WebApi.Models
{
    public class Row
    {
        public int Id { get; set; }
        public string Values { get; set; }
        public int TableId { get; set; }
        public virtual Table Table { get; set; }
    }
}
