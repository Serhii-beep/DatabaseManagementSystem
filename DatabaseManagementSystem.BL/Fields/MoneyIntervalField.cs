﻿namespace DatabaseManagementSystem.BL.Fields
{
    public class MoneyIntervalField : Field
    {
        public MoneyIntervalField(string name) : base(name)
        {
            Type = "MoneyInterval";
            DefaultValue = "($0, $1)";
        }

        public static bool IsValid(string value)
        {
            if((value[0] != '(' && value[0] != '[') || (value[^1] != ')' && value[^1] != ']') || (value[0] != value[^1]))
            {
                return false;
            }
            string[] parts = value[1..^1].Replace(" ", "").Split(',');
            return MoneyField.IsValid(parts[0]) && MoneyField.IsValid(parts[1]);
        }


    }
}