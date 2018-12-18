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
    public class DbfToSqlite
    {
        public bool ConvertDbfToSqlite(string name)
        {
            var result = false; DbfHelper dbfHelper = new DbfHelper($@"C:\Temps\Pivot\{name}", name);
            dbfHelper.ToSqlite();
            result = true;
            try
            {
                
            }
            catch (Exception ex)
            {

            }
            return result;
        }
    }
}
