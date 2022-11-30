namespace DatabaseManagementSystem.WebApi.Models
{
    public class Field
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string DefaultValue { get; set; }
        public int TableId { get; set; }
        public virtual Table Table { get; set; }
    }
}
