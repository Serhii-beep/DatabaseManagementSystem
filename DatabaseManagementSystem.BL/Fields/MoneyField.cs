namespace DatabaseManagementSystem.BL.Fields
{
    public class MoneyField : Field
    {
        public MoneyField(string name) : base(name)
        {
            Type = "Money";
            DefaultValue = "$0.00";
        }

        public override bool IsValid(string value)
        {
            if(string.IsNullOrEmpty(value) || value[0] != '$')
                return false;
            if(value.Contains('.'))
            {
                string[] parts = value[1..].Split('.');
                if(parts.Length != 2 || !long.TryParse(parts[0], out _) || !int.TryParse(parts[1], out _) || parts[1].Length > 2 ||
                    parts[1].Length < 1)
                    return false;
            }
            else
            {
                return long.TryParse(value[1..], out _);
            }
            return true;
        }
    }
}
