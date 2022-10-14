namespace DatabaseManagementSystem.BL.Fields
{
    public class MoneyIntervalField : Field
    {
        public MoneyIntervalField(string name) : base(name)
        {
            Type = "MoneyInterval";
            DefaultValue = "($0, $1)";
        }

        public override bool IsValid(string value)
        {
            if((value[0] != '(' && value[0] != '[') || (value[^1] != ')' && value[^1] != ']') || (value[0] != value[^1]))
            {
                return false;
            }
            string[] parts = value[1..^1].Replace(" ", "").Split(',');
            MoneyField mf = new MoneyField("t");
            return mf.IsValid(parts[0]) && mf.IsValid(parts[1]);
        }


    }
}
