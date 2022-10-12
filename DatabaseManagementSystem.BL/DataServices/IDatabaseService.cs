using DatabaseManagementSystem.BL.DatabaseEntities;
using DatabaseManagementSystem.BL.Fields;

namespace DatabaseManagementSystem.BL.DataServices
{
    public interface IDatabaseService
    {
        Database Database { get; set; }
        void AddTable(string tableName);
        void DeleteTable(string tableName);
        void AddField(string tableName, Field field);
        void DeleteField(string tableName, string fieldName);
        Table GetTablesIntersection(string tableName1, string tableName2, List<string> fieldNames);
    }
}
