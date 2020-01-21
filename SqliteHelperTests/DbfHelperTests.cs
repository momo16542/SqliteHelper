using Microsoft.VisualStudio.TestTools.UnitTesting;
using SqliteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteHelper.Tests
{
    [TestClass()]
    public class DbfHelperTests
    {
        [TestMethod()]
        public void GetTableTest()
        {
            string tableName = "Sqlresult";
            DbfHelper dbfHelper = new DbfHelper($@"C:\temps\dbf\dbfhelperUnitTest\{tableName}", tableName);
            var dt = dbfHelper.GetTable();
            SqliteHelper sqliteHelper = new SqliteHelper($@"C:\temps\dbf\dbfhelperUnitTest\", "UnitTest");
            var dt1 = sqliteHelper.GetTable(tableName);
            Assert.AreEqual(dt.Rows.Count, dt1.Rows.Count);
        }
    }
}