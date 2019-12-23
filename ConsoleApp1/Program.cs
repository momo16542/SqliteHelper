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
            DbfToSqlite dbfToSqlite = new DbfToSqlite();
           var qq= dbfToSqlite.ConvertDbfToSqlite("sal_qry1");
            Console.Write(qq);
            Console.Read();

        }
    }
}
