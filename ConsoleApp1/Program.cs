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
            List<string> list = new List<string>();
            list.Add("Sqlresult");
            list.Add("Sqlresult1");
            list.Add("Sqlresult2");
            list.Add("Sqlresult3");
            list.Add("Sqlresult4");
            list.Add("Sqlresult5");
            list.Add("Sqlresult6");
            list.Add("Sqlresult7");
            list.Add("Sqlresult8");
            DbfToSqlite dbfToSqlite = new DbfToSqlite();
            foreach (var item in list)
            {
                dbfToSqlite.ConvertDbfToSqlite(@"C:\temps\dbf\", item, @"C:\temps\dbf\unittest\", "UnitTest");
            }
            Console.Read();
        }
        private static void Normal()
        {
            //Console.WriteLine("Type: 1:Normal 2:Pivot");
            //var type = Console.ReadLine();
            //Console.WriteLine("DBF Name");
            //var name = Console.ReadLine();
            //if (type.StartsWith("1"))
            //{
            //    DbfToSqlite dbfToSqlite = new DbfToSqlite();
            //    var qq = dbfToSqlite.ConvertDbfToSqlite(name, "UnitTest");
            //    Console.Write(qq);
            //}
            //else if (type.StartsWith("2"))
            //{
            //    DbfToSqlite dbfToSqlite = new DbfToSqlite();
            //    var qq = dbfToSqlite.ConvertDbfToSqlite("sal_qry1", "PivotSource");
            //    Console.Write(qq);
            //}
            //Console.Read();
        }
    }
}
