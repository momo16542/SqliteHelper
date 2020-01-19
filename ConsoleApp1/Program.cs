using SqliteHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type: 1:Normal 2:Pivot");
            var type = Console.ReadLine();
            Console.WriteLine("DBF Name");
            var name = Console.ReadLine();
            if (type.StartsWith("1"))
            {
                DbfToSqlite dbfToSqlite = new DbfToSqlite();
                var qq = dbfToSqlite.ConvertDbfToSqlite(name, "UnitTest");
                Console.Write(qq);
            }
            else if (type.StartsWith("2"))
            {
                DbfToSqlite dbfToSqlite = new DbfToSqlite();
                var qq = dbfToSqlite.ConvertDbfToSqlite("sal_qry1", "PivotSource");
                Console.Write(qq);
            }
            Console.Read();

        }
    }
}
