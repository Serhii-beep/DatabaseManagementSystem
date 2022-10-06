namespace DatabaseManagementSystem.BL.DatabaseEntities
{
    public class Row
    {
        public List<string> Values { get; set; }

        public Row()
        {
            Values = new List<string>();
        }
    }
}
