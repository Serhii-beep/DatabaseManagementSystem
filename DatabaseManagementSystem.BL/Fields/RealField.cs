namespace DatabaseManagementSystem.BL.Fields
{
    public class RealField : Field
    {
        public RealField(string name) : base(name)
        {
            Type = "Real";
            DefaultValue = "0.0";
        }

        public override bool IsValid(string value)
        {
            return double.TryParse(value, out _);
        }
    }
}
