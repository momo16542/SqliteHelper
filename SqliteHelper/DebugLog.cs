using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqliteHelper
{
    public class DebugLog
    {
        private static string DebugLog儲存位置 = @"C:\temps\debuglog\";
        private static string LanguangeConvert儲存位置 = @"C:\temps\debuglog\langauage";

        public void WriteLog(string message)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                if (!Directory.Exists(DebugLog儲存位置))
                {
                    Directory.CreateDirectory(DebugLog儲存位置);
                }
                DateTime Date = DateTime.Now;
                string TodyMillisecond = Date.ToString("yyyy-MM-dd HH:mm:ss");
                string Today = Date.ToString("yyyy-MM-dd");
                File.AppendAllText(DebugLog儲存位置 + Today + ".txt", "\r\n" + TodyMillisecond + "：" + message);
            }
            catch
            {

            }
            finally
            {
                if (bw != null)
                {
                    bw.Flush();
                    bw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        public void WriteLanguangeConvert(string programClass, string key)
        {
            FileStream fs = null;
            BinaryWriter bw = null;
            try
            {
                if (!Directory.Exists(DebugLog儲存位置))
                {
                    Directory.CreateDirectory(DebugLog儲存位置);
                }
                DateTime Date = DateTime.Now;
                File.AppendAllText(DebugLog儲存位置 + programClass + ".txt", "\r\n" + key + "");
            }
            catch
            {

            }
            finally
            {
                if (bw != null)
                {
                    bw.Flush();
                    bw.Close();
                }
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
    }
}
