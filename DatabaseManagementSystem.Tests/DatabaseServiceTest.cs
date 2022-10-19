using DatabaseManagementSystem.BL.DatabaseEntities;
using DatabaseManagementSystem.BL.DataServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseManagementSystem.BL.Fields;
using Newtonsoft.Json;

namespace DatabaseManagementSystem.Tests
{
    [TestFixture]
    public class DatabaseServiceTest
    {
        private DatabaseService databaseService;
        [SetUp]
        public void SetUp()
        {
            databaseService = new DatabaseService();
            databaseService.Database = new Database("Database");
        }

        [Test, TestCaseSource(nameof(AddTable_CorrectData))]
        public void DatabaseServiceTest_AddTable_AddsTable(string tableName)
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => databaseService.AddTable(tableName));
                Assert.That(databaseService.Database.Tables, Has.Count.EqualTo(1));
            });
        }

        [Test, TestCaseSource(nameof(AddTable_InvalidData))]
        public void DatabaseServiceTest_AddTable_InvalidData(string tableName)
        {
            databaseService.AddTable("Table");
            Assert.Multiple(() =>
            {
                Assert.Throws<Exception>(() => databaseService.AddTable(tableName));
                Assert.That(databaseService.Database.Tables, Has.Count.EqualTo(1));
            });
        }

        [Test]
        public void DatabaseServiceTest_DeleteTable_DeletesTable()
        {
            var tableName = "Table";
            databaseService.AddTable(tableName);
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => databaseService.DeleteTable(tableName));
                Assert.That(databaseService.Database.Tables, Is.Empty);
            });
        }

        [Test]
        public void DatabaseServiceTest_DeleteTable_DoesNotDeleteTable()
        {
            var tableName = "Table";
            Assert.Multiple(() =>
            {
                Assert.Throws<Exception>(() => databaseService.DeleteTable(tableName));
                Assert.That(databaseService.Database.Tables, Is.Empty);
            });
        }

        [Test, TestCaseSource(nameof(TableIntersection_CorrectData))]
        public void DatabaseServiceTest_GetTablesIntersection_ReturnsCorrectData(Table t1, Table t2, List<string> fields, Table expected)
        {
            databaseService.Database.Tables.Add(t1);
            databaseService.Database.Tables.Add(t2);

            var actual = databaseService.GetTablesIntersection(t1.Name, t2.Name, fields);
            var actualSerialized = JsonConvert.SerializeObject(actual);
            var expectedSerialized = JsonConvert.SerializeObject(expected);
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => databaseService.GetTablesIntersection(t1.Name, t2.Name, fields));
                Assert.That(actualSerialized, Is.EqualTo(expectedSerialized));
            });
        }

        [Test, TestCaseSource(nameof(TableIntersection_CorrectData))]
        public void DatabaseServiceTest_GetTablesIntersection_NotExistingTable(Table t1, Table t2, List<string> fields, Table expected)
        {
            databaseService.Database.Tables.Add(t1);

            Assert.Throws<Exception>(() => databaseService.GetTablesIntersection(t1.Name, t2.Name, fields));
        }

        [Test, TestCaseSource(nameof(TableIntersection_InvalidFields))]
        public void DatabaseServiceTest_GetTablesIntersection_InvalidFields(Table t1, Table t2, List<string> fields)
        {
            databaseService.Database.Tables.Add(t1);
            databaseService.Database.Tables.Add(t2);

            Assert.Throws<Exception>(() => databaseService.GetTablesIntersection(t1.Name, t2.Name, fields));
        }

        #region data
        private static IEnumerable<TestCaseData> AddTable_CorrectData()
        {
            yield return new TestCaseData("Table1");
            yield return new TestCaseData("Table2");
            yield return new TestCaseData("Table3");
        }

        private static IEnumerable<TestCaseData> AddTable_InvalidData()
        {
            yield return new TestCaseData("");
            yield return new TestCaseData("Table");
        }

        private static IEnumerable<TestCaseData> TableIntersection_CorrectData()
        {
            yield return new TestCaseData(
                new Table("Table1")
                {
                    Fields = new List<Field>()
                    {
                        new IntegerField("Id"),
                        new StringField("Name"),
                        new CharField("Ch"),
                        new MoneyIntervalField("Balance")
                    }
                }.UpdateRow(new List<string> { "0", "Serhii", "s", "($4.00,$5.68)" }, 0)
                 .UpdateRow(new List<string> { "1", "Olha", "o", "($3.55,$2.86)" }, 1),
                new Table("Table2")
                {
                    Fields = new List<Field>()
                    {
                        new IntegerField("Id"),
                        new StringField("Name"),
                        new RealField("Prop"),
                        new MoneyIntervalField("Balance")
                    }
                }.UpdateRow(new List<string> { "0", "Serhii", "4.5", "($4.00,$5.68)" }, 0),
                new List<string> { "Id", "Name", "Balance" },
                new Table("Intersection of Table1 and Table2")
                {
                    Fields = new List<Field>()
                    {
                        new IntegerField("Id"),
                        new StringField("Name"),
                        new MoneyIntervalField("Balance")
                    }
                }.UpdateRow(new List<string> { "0", "Serhii", "($4.00,$5.68)" }, 0));
        }

        private static IEnumerable<TestCaseData> TableIntersection_InvalidFields()
        {
            yield return new TestCaseData(
                new Table("Table1")
                {
                    Fields = new List<Field>()
                    {
                        new IntegerField("Id"),
                        new StringField("Name"),
                        new CharField("Ch"),
                        new MoneyIntervalField("Balance")
                    }
                }.UpdateRow(new List<string> { "0", "Serhii", "s", "($4.00,$5.68)" }, 0)
                 .UpdateRow(new List<string> { "1", "Olha", "o", "($3.55,$2.86)" }, 1),
                new Table("Table2")
                {
                    Fields = new List<Field>()
                    {
                        new IntegerField("Id"),
                        new StringField("Name"),
                        new RealField("Prop"),
                        new MoneyIntervalField("Balance")
                    }
                }.UpdateRow(new List<string> { "0", "Serhii", "4.5", "($4.00,$5.68)" }, 0),
                new List<string> { "Id", "Ch", "Password" });
        }

        #endregion
    }
}
