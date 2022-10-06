﻿using DatabaseManagementSystem.BL.DatabaseEntities;
using DatabaseManagementSystem.BL.Fields;
using System.Linq;

namespace DatabaseManagementSystem.BL.DataServices
{
    public class DatabaseService : IDatabaseService
    {
        public Database Database { get; set; }

        public void AddField(string tableName, Field field)
        {
            Table t = Database.Tables.FirstOrDefault(t => t.Name == tableName);
            if(t == null)
            {
                throw new Exception($"Table {tableName} does not exist");
            }
            if(t.Fields.Any(f => f.Name == field.Name))
            {
                throw new Exception($"Field {field.Name} already exists");
            }
            t.Fields.Add(field);
            foreach(var row in t.Rows)
            {
                row.Values.Add(field.DefaultValue);
            }
        }

        public void AddTable(string tableName)
        {
            if(Database.Tables.Any(t => t.Name == tableName))
            {
                throw new Exception($"Table {tableName} already exists");
            }
            Database.Tables.Add(new Table(tableName));
        }

        public void DeleteField(string tableName, string fieldName)
        {
            Table table = Database.Tables.FirstOrDefault(t => t.Name == tableName);
            if(table == null)
            {
                throw new Exception($"Table {tableName} does not exist");
            }
            Field field= table.Fields.FirstOrDefault(f => f.Name == fieldName);
            if(field == null)
            {
                throw new Exception($"Field {fieldName} does not exist");
            }
            int ind = table.Fields.IndexOf(field);
            table.Fields.Remove(field);
            foreach(var row in table.Rows)
            {
                row.Values.RemoveAt(ind);
            }
        }

        public void DeleteTable(string tableName)
        {
            Table table = Database.Tables.FirstOrDefault(t => t.Name == tableName);
            if(table == null)
            {
                throw new Exception($"Table {tableName} does not exist");
            }
            Database.Tables.Remove(table);
        }

        public Table GetTablesIntersection(string tableName1, string tableName2, List<string> fieldNames)
        {
            Table table1 = Database.Tables.FirstOrDefault(t => t.Name == tableName1);
            Table table2 = Database.Tables.FirstOrDefault(t => t.Name == tableName2);
            if(table1 == null)
            {
                throw new Exception($"Table {tableName1} does not exist");
            }
            if(table2 == null)
            {
                throw new Exception($"Table {tableName2} does not exist");
            }
            if(!fieldNames.All(fn => table1.Fields.Any(f => f.Name == fn)))
            {
                throw new Exception($"{tableName1} does not contain one or more fields from list");
            }
            if(!fieldNames.All(fn => table2.Fields.Any(f => f.Name == fn)))
            {
                throw new Exception($"{tableName2} does not contain one or more fields from list");
            }
            Table result = new Table($"Intersection of {tableName1} and {tableName2}");
            result.Fields = table1.Fields.Where(f => fieldNames.Contains(f.Name)).ToList();
            var inxs1 = table1.Fields.Where(f => fieldNames.Contains(f.Name)).Select(f => table1.Fields.IndexOf(f)).ToArray();
            var inxs2 = table2.Fields.Where(f => fieldNames.Contains(f.Name)).Select(f => table2.Fields.IndexOf(f)).ToArray();
            foreach(var row1 in table1.Rows)
            {
                var values1 = row1.Values.Where((v, i) => inxs1.Contains(i)).ToList();
                foreach(var row2 in table2.Rows)
                {
                    var values2 = row2.Values.Where((v, i) => inxs2.Contains(i)).ToList();
                    if(values1.SequenceEqual(values2))
                    {
                        result.Rows.Add(new Row { Values = values1 });
                    }
                }
            }
            return result;
        }
    }
}
