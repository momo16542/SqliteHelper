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
    class SqliteHelper
    {

        private readonly string dataBaseName;
        private readonly string tableName;
        private readonly string directory = @"C:\Temps\Pivot\";
        private string insertColumnString;
        private string insertParameterString;
        public List<string> ParameterList { get; set; }
        public string ConnectionString { get { return "Data source=" + directory + dataBaseName + ".db"; } }
        private string FullPath { get { return directory + dataBaseName + ".db"; } }

        public SqliteHelper(string tableName)
        {
            this.tableName = tableName;
            CheckDirectory(directory);
            dataBaseName = CheckFile(tableName);
        }
        public void InsertOne(OdbcDataReader reader)
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
            //if (dataColumnType == "tinyint" || dataColumnType == "int" || dataColumnType == "smallint")
            //{
            //    colType = "INTEGER";
            //}
            //else if (dataColumnType == "decimal" || dataColumnType == "money")
            //{

            //    colType = $"INTEGER";
            //}
            //else if (dataColumnType == "char" || dataColumnType == "varchar" || dataColumnType == "nvarchar")
            //{
            //    colType = $"{dataColumnType}({columnSize})";
            //}
            //else if (dataColumnType == "datetime")
            //{
            //    colType = "Text";
            //}
            if (dataColumnType == "System.Int16" || dataColumnType == "System.Int32")
            {
                colType = "INTEGER";
            }
            else if (dataColumnType == "System.Decimal")
            {

                colType = $"INTEGER";
            }
            else if (dataColumnType == "System.String")
            {
                //colType = $"{dataColumnType}({columnSize})";   
                colType = "Text";
            }
            else if (dataColumnType == "System.DateTime")
            {
                colType = "Text";
            }

            else { throw new Exception(dataColumnType + " not in list"); }
            return colType;
        }
        private string CheckFile(string tableName)
        {
            string fileName = tableName;
            string path = $"{directory}\\{fileName}.db";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            SQLiteConnection.CreateFile(fileName);
            return fileName;
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
