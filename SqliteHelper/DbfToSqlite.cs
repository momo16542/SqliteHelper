using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SqliteHelper
{
    
    [ClassInterface(ClassInterfaceType.AutoDual)] //雖然不明白這行，但查詢文件都要加上此行
    [ProgId("SqliteHelper.DbfToSqlite")]
    [ComVisible(true)]
    public class DbfToSqlite
    {
        public bool ConvertDbfToSqlite(string dbfName, string sqliteName)
        {
            var result = false;
            try
            {
                DbfHelper dbfHelper = new DbfHelper($@"C:\Temps\DBF\{dbfName}", dbfName);
                dbfHelper.ToSqlite(sqliteName);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed, {ex.Message}");
            }
            Console.WriteLine("Success");
            return result;
        }
        public bool ConvertDbfToSqlitePivot(string dbfName, string sqliteName)
        {
            var result = false;
            try
            {
                DbfHelper dbfHelper = new DbfHelper($@"C:\Temps\Pivot\{dbfName}", dbfName);
                dbfHelper.ToSqlite(sqliteName);
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed, {ex.Message}");
            }
            Console.WriteLine("Success");
            return result;
        }
    }
}
