using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteHelper
{
    public class SqliteHelper
    {
        private readonly string databaseName;

        private readonly string directory;/*= @"C:\Temps\Pivot\";*/
        private string insertColumnString;
        private string insertParameterString;
        public List<string> ParameterList { get; set; }
        public string ConnectionString { get { return "Data source=" + directory + databaseName + ".db"; } }
        private string FullPath { get { return directory + databaseName + ".db"; } }

        public SqliteHelper(string directory, string databaseName)
        {
            this.directory = directory;
            CheckDirectory(directory);
            this.databaseName = databaseName;
            CheckFile(databaseName);
        }
        public DataTable GetTable(string tableName)
        {
            using (var cn = new SQLiteConnection(ConnectionString))
            {             
                using (var cmd = cn.CreateCommand())
                {
                    cn.Open();
                    cmd.CommandText = $"Select * from {tableName}";
                    SQLiteDataAdapter adp= new SQLiteDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adp.Fill(dt);
                    return dt;
                }

            }

           
        }
        public void InsertOne(OdbcDataReader reader, string tableName)
        {
            DataTable dt = reader.GetSchemaTable();
            if (dt != null)
            {
                //string colstr = helper.CreateColumnStringBySqlReaderSchema(資料表名稱[no], dt);
                string colstr = CreateColumnStringBySqlReaderSchema(dt);
                CreateTable(tableName, colstr);
            }
            bool first = true;
            using (var cn = new SQLiteConnection(ConnectionString))
            {
                cn.Open();
                using (var transaction = cn.BeginTransaction())
                {
                    using (var cmd = cn.CreateCommand())
                    {
                        while (reader.Read())
                        {
                            Object[] values = new Object[reader.FieldCount];
                            int fieldCounts = reader.GetValues(values);

                            if (first)
                            {
                                cmd.CommandText = GetInsertString(tableName);
                                for (int i = 0; i < fieldCounts; i++)
                                {
                                    cmd.Parameters.AddWithValue(ParameterList[i], values[i]);
                                }
                                first = false;
                            }
                            else
                            {
                                for (int i = 0; i < fieldCounts; i++)
                                {
                                    cmd.Parameters[i].Value = values[i];
                                }
                            }
                            cmd.ExecuteNonQuery();
                            values = null;
                        }
                    }
                    transaction.Commit();
                }
            }
        }
        private void CreateTable(string tableName, string columnSring)
        {
            using (SQLiteConnection sqlite_conn = new SQLiteConnection(ConnectionString))
            {
                using (SQLiteCommand sqlite_cmd = sqlite_conn.CreateCommand())
                {
                    sqlite_conn.Open();
                    sqlite_cmd.CommandText = $"DROP TABLE IF EXISTS {tableName};" +
                        $"CREATE TABLE IF NOT EXISTS {tableName} " +
                        $"({   columnSring.TrimEnd(',') });";
                    sqlite_cmd.ExecuteNonQuery();
                    sqlite_conn.Close();
                }
            }
        }
        private string GetInsertString(string name)
        {
            return $"INSERT INTO {name} ({insertColumnString}) VALUES({ insertParameterString});";
        }
        private string CreateColumnStringBySqlReaderSchema(DataTable dt)
        {
            string column = "";
            insertColumnString = "";
            insertParameterString = "";
            ParameterList = new List<string>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string columnName = dt.Rows[i]["ColumnName"].ToString();
                column += $"[{columnName}]  {ConvertSqlColumnType(dt.Rows[i])} ,";
                insertColumnString += $"[{columnName}],";

                string param = columnName.Replace(" ", "_");
                insertParameterString += $"@{param},";
                ParameterList.Add($"@{param}");
            }
            insertColumnString = insertColumnString.TrimEnd(',');
            insertParameterString = insertParameterString.TrimEnd(',');
            return column;
        }
        private string ConvertSqlColumnType(DataRow dataRow)
        {
            var dataColumnType = dataRow["DataType"].ToString().Trim();
            var columnSize = dataRow["ColumnSize"].ToString().Trim();
            var numericPrecision = dataRow["NumericPrecision"].ToString().Trim();
            var numericScale = dataRow["NumericScale"].ToString().Trim();
            string colType = "";
            if (dataColumnType == "tinyint" || dataColumnType == "int" || dataColumnType == "smallint" || dataColumnType == "bit" ||
                dataColumnType == "System.Int32" || dataColumnType == "System.Int16")
            {
                colType = "INTEGER";
            }
            else if (dataColumnType == "decimal" || dataColumnType == "System.Decimal")
            {
                colType = "Real";
            }
            else if (dataColumnType == "money" || dataColumnType == "smallmoney")
            {
                colType = "Real";
            }
            else if (dataColumnType == "char" || dataColumnType == "varchar" || dataColumnType == "nvarchar" || dataColumnType == "nchar" ||
                dataColumnType == "System.String")
            {
                colType = "Text";
                //colType = $"{dataColumnType}({columnSize})";
            }
            else if (dataColumnType == "datetime" || dataColumnType == "System.DateTime")
            {
                //colType = "Text";
                colType = "datetime";
            }
            else if (dataColumnType == "System.Boolean")
            {
                
                colType = "boolean";
            }
            else { throw new Exception(dataColumnType + " not in list"); }
            return colType;
        }
        private void CheckFile(string databaseName, bool needDeelte = false)
        {
            string fileName = databaseName;
            string path = $"{directory}\\{fileName}.db";
            if (File.Exists(path))
            {
                if (needDeelte)
                {
                    File.Delete(path);
                    SQLiteConnection.CreateFile(fileName);
                }
            }
            else
            {
                SQLiteConnection.CreateFile(fileName);
            }


        }
        private void CheckDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
