using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqliteHelper;

namespace SqliteHelperUnitTest
{
    [TestClass]
    public class DbfHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string tableName = "Sqlresult";
            DbfHelper dbfHelper = new DbfHelper($@"C:\temps\dbf\dbfhelperUnitTest\{tableName}", tableName);
            var dt = dbfHelper.GetTable();
            SqliteHelper.SqliteHelper sqliteHelper = new SqliteHelper.SqliteHelper($@"C:\temps\dbf\dbfhelperUnitTest\", "UnitTest");
          var dt1=  sqliteHelper.GetTable(tableName);
            Assert.AreEqual(dt.Rows.Count, dt1.Rows.Count);
        }
    }
}
