using DatabaseManagementSystem.BL.Fields;
using System.Collections.ObjectModel;

namespace DatabaseManagementSystem.BL.DatabaseEntities
{
    public class Table
    {
        public string Name { get; set; }

        public List<Field> Fields { get; set; }
        
        public List<Row> Rows { get; private set; }

        public Table(string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                throw new Exception("Table name must not be empty");
            }
            Name = name;
            Fields = new List<Field>();
            Rows = new List<Row>();
        }

        public Table UpdateRow(List<string> values, int row)
        {
            if(row < 0)
            {
                throw new Exception("Row index can not be less than 0");
            }
            if(values.Count != Fields.Count)
            {
                throw new Exception("Invalid data");
            }
            Row newrow;
            for(int i = 0; i < values.Count; i++)
            {
                if(!Fields[i].IsValid(values[i]) && (!string.IsNullOrEmpty(values[i]) && Fields[i].Type != "String"))
                {
                    throw new Exception($"Invalid value {values[i]} for type {Fields[i].Type}");
                }
            }
            if(row >= Rows.Count)
            {
                newrow = new Row();
                newrow.Values = values;
                for(int i = 0; i < newrow.Values.Count; ++i)
                {
                    if(string.IsNullOrEmpty(newrow.Values[i]))
                    {
                        newrow.Values[i] = Fields[i].DefaultValue;
                    }
                }
                Rows.Add(newrow);
            }
            else
            {
                Rows[row].Values = values;
            }
            return this;
        }

        public void AddField(Field field)
        {
            if(Fields.Any(f => f.Name.ToLower() == field.Name.ToLower()))
            {
                throw new Exception($"Field {field.Name} already exists");
            }
            Fields.Add(field);
            foreach(var row in Rows)
            {
                row.Values.Add(field.DefaultValue);
            }
        }

        public void DeleteField(string fieldName)
        {
            Field field = Fields.FirstOrDefault(f => f.Name == fieldName);
            if(field == null)
            {
                throw new Exception($"No field with name {fieldName}");
            }
            int ind = Fields.IndexOf(field);
            Fields.RemoveAt(ind);
            foreach(var row in Rows)
            {
                row.Values.RemoveAt(ind);
            }
        }

        public void DeleteRow(int indx)
        {
            if(indx < 0 || indx >= Rows.Count)
            {
                throw new Exception("Invalid index");
            }
            Rows.RemoveAt(indx);
        }
    }
}
