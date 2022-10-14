namespace DatabaseManagementSystem.BL.Fields
{
    public class StringField : Field
    {
        public StringField(string name) : base(name)
        {
            Type = "String";
            DefaultValue = String.Empty;
        }

        public override bool IsValid(string value)
        {
            return true;
        }
    }
}
