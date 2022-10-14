namespace DatabaseManagementSystem.BL.Fields
{
    public class IntegerField : Field
    {
        public IntegerField(string name) : base(name)
        {
            Type = "Integer";
            DefaultValue = "0";
        }

        public override bool IsValid(string value)
        {
            return int.TryParse(value, out _);
        }
    }
}
