using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteHelper
{
    class DbfHelper
    {
        string dbfPath;
        string dbfTableName;
        public DbfHelper(string dbfPath, string dbfTableName)
        {
            this.dbfPath = dbfPath;
            this.dbfTableName = dbfTableName;
        }
        private OdbcConnection OdbcDbfOpenConn(string Database)
        {
            string cnstr = "Driver={Microsoft Visual FoxPro Driver}; SourceType=DBF; SourceDB="
                + Database + "; Exclusive=No; Collate=Machine; NULL=Yes; DELETED=Yes; BACKGROUNDFETCH=Yes;";

            OdbcConnection icn = new OdbcConnection();
            icn.ConnectionString = cnstr;
            if (icn.State == ConnectionState.Open)
            {
                icn.Close();
            }
            icn.Open();
            return icn;
        }
        private string Connection
        {
            get
            {
                return "Driver={Microsoft Visual FoxPro Driver}; SourceType=DBF; SourceDB="
                + dbfPath + "; Exclusive=No; Collate=Machine; NULL=Yes; DELETED=Yes; BACKGROUNDFETCH=Yes;";
            }
        }
        public void ToSqlite(string sqliteFileName)
        {
            string odbcString = $"select * from {dbfPath}";
            using (OdbcConnection conn = new OdbcConnection(Connection))
            {
                using (OdbcCommand cmd = new OdbcCommand(odbcString, conn))
                {
                    conn.Open();
                    OdbcDataReader reader = cmd.ExecuteReader();
                    var schema = reader.GetSchemaTable();
                    SqliteHelper sqliteHelper = new SqliteHelper(sqliteFileName);
                    sqliteHelper.InsertOne(reader, dbfTableName);
                    reader.Close();
                }
            }
        }

        private DataTable CheckDBFGarbled(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = 0; i < dr.ItemArray.Length; i++)
                {
                    char[] cs = dr[i].ToString().ToCharArray();
                    foreach (var c in cs)
                    {
                        int ss = (int)c;
                        if (((ss >= 0) && (ss <= 8)) || ((ss >= 11) && (ss <= 12)) || ((ss >= 14) && (ss <= 32)))
                        {
                            dr[i] = dr[i].ToString().Replace(c, ' ');
                        }
                    }
                }
            }
            return dt;
        }
    }
}
