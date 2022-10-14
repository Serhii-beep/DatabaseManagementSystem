namespace DatabaseManagementSystem.BL.Fields
{
    public class CharField : Field
    {
        public CharField(string name) : base(name)
        {
            Type = "Char";
            DefaultValue = String.Empty;
        }

        public override bool IsValid(string value)
        {
            return char.TryParse(value, out _);
        }
    }
}
