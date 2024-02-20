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
            var list = GetVendorInspect(out string sqliteName);
            DbfToSqlite dbfToSqlite = new DbfToSqlite();
            foreach (var item in list)
            {
                dbfToSqlite.ConvertDbfToSqlite(@"C:\temps\dbf\vendordefect", item, @"C:\temps\dbf\unittest\", sqliteName);
            }
            Console.Read();
        }
        private static List<string> GetVendorInspect(out string sqliteName)
        {
            sqliteName = "VendorDefect";
            List<string> list = new List<string>();
            list.Add("tmp");
            list.Add("tmp1");
            list.Add("tmp2");
            list.Add("PUQRY2日期");
            list.Add("PUQRY2延交");
            list.Add("PUQRY2廠商");
            list.Add("PUQRY2不良");
            return list;
        }
        private static List<string> GetVendorFillRate(out string sqliteName)
        {
            sqliteName = "VendorFillRate";
            List<string> list = new List<string>();
            list.Add("tmp");
            list.Add("tmp1");
            list.Add("PUQRY3延交");
            list.Add("PUQRY3廠商");

            return list;
        }
        private static List<string> GetBOLPrint(out string sqliteName)
        {
            sqliteName = "BOLPrint";
            List<string> list = new List<string>();
            list.Add("EDICUSTLIST");
            list.Add("lable_result");
            list.Add("ASNLABELSTXT");        

            return list;
        }
        private static List<string> GetBalance(out string sqliteName)
        {
            sqliteName = "Balance";
            List<string> list = new List<string>();
            list.Add("balance");
            list.Add("tmp");
            list.Add("tmp1");
            list.Add("tmp2");
            list.Add("tmp3");
            list.Add("右帳戶");
            list.Add("左帳戶");
            list.Add("帳戶表");
            list.Add("資產負債表");
        
            return list;
        }
        private static List<string> GetParameters(out string sqliteName)
        {
            sqliteName = "NogiParameters";
            List<string> list = new List<string>();
            list.Add("lcl系統參數表");
            return list;
        }
        private static List<string> Getmrp(out string sqliteName)
        {
            sqliteName = "Mrp";
            List<string> list = new List<string>();
            list.Add("tmpmoverec");
            list.Add("tmpmoverec1");
            list.Add("tmpmoverec2");
            list.Add("tmpmoverec3");
            list.Add("tmpmoverec4");
            list.Add("tmpmoverec5");
            list.Add("tmpmoverec6");
            list.Add("tmpmoverec7");
            list.Add("tmpmoverec8");
            list.Add("tmpmoverec9");
            list.Add("tmpmoverec10");
            list.Add("tmpmoverec11");
            list.Add("tmpmoverec12");
            list.Add("tmpmoverec13");
            list.Add("tmpmoverec14");
            list.Add("獨立需求表");
            list.Add("獨立彙總表");
            list.Add("相依需求表");
            list.Add("相依彙總表");
            list.Add("MRP在庫量");
            list.Add("MRP在庫量彙總表");
            list.Add("MRP待驗量");
            list.Add("MRP待驗量彙總表");
            list.Add("MRP訂購未交表");
            list.Add("MRP訂購彙總表");
            list.Add("MRP生產未交表");
            list.Add("MRP生產彙總表");
            list.Add("MRP保留量表");
            list.Add("MRP保留量彙總表");
            list.Add("MRP計算品號表");
            list.Add("MRP查詢明細");
            list.Add("MRP建議表");
            return list;

        }
        private static List<string> GetBom_purp(out string sqliteName)
        {
            sqliteName = "Bom_purp";
            List<string> list = new List<string>();
            list.Add("tmpmoverec");
            list.Add("tmpmoverec1");
            list.Add("tmpmoverec2");
            list.Add("tmpmoverec3");
            list.Add("用途表主檔");
            list.Add("用途表明細");
            return list;
        }
        private static List<string> GetBom_cost(out string sqliteName)
        {
            sqliteName = "Bom_cost";
            List<string> list = new List<string>();
            list.Add("tmpmoverec");
            list.Add("tmpmoverec1");
            list.Add("tmp製程表");
            return list;
        }
        private static List<string> GetCostDetail(out string sqliteName)
        {
            sqliteName = "Cost_CostDetail";
            List<string> list = new List<string>();
            list.Add("TMP單位成本表_不分倉庫");
            list.Add("TMPINVENT1");
            list.Add("TMPSTKBAS");
            list.Add("TMP前期財務");
            list.Add("成本主檔");
            list.Add("成本明細");
            list.Add("TRADELST");
            return list;
        }
        private static List<string> GetCalculateCost()
        {
            List<string> list = new List<string>();
            list.Add("TMP倉儲表");
            list.Add("TMPINVENT");
            list.Add("TP_加工回收單");
            list.Add("TRADELST");
            list.Add("TMP單位成本表_不分倉庫");
            return list;
        }
        private static List<string> GetSearchCurrentPeriod()
        {
            List<string> list = new List<string>();
            list.Add("tmpsplace11");
            list.Add("Sqlresult");
            list.Add("tmpmoverec");
            list.Add("TRADELST1");
            list.Add("最近成本表1");
            return list;
        }
        private static List<string> GetInvbk()
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
            list.Add("invbkm");
            list.Add("invbkm2");
            list.Add("invbkd");
            list.Add("invbkd2");
            return list;
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
