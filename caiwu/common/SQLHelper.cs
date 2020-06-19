
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caiwu.common
{
    class SQLHelper
    {
        #region 通用方法
        // 数据连接池 
 
        private static void CreateFile(byte[] fileBuffer, string newFilePath)
        {
            if (File.Exists(newFilePath))
            {
                File.Delete(newFilePath);
            }
            FileStream fs = new FileStream(newFilePath, FileMode.CreateNew);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(fileBuffer, 0, fileBuffer.Length); //用文件流生成一个文件
            bw.Close();
            fs.Close();
        }

        public static int Index = 0;


        public static String GetSqlConn()
        {


            String ip = "106.14.6.64";
            //String ip = "getdata.feifeifgo.cn";
            String db = "feifeigo";
            String user = "root";
            String pwd = "ANbRfDBhdi8UsR0XOq";
            String port = "3306";

            //String ip = "127.0.0.1";
            //String db = "betaskdata";
            //String user = "root";
            //String pwd = "root123";
            //String port = "3306";
            return "Database=" + db + ";Data Source=" + ip + ";User Id=" + user + ";Password=" + pwd + ";pooling=false;CharSet=utf8;port=" + port + "";
        }
 
        static string Decrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();   // 实例化加/解密类对象    

            byte[] key = Encoding.Unicode.GetBytes("Llwy"); // 定义字节数组，用来存储密钥

            byte[] data = Convert.FromBase64String(str);    // 定义字节数组，用来存储要解密的字符串

            MemoryStream MStream = new MemoryStream();      // 实例化内存流对象      

            // 使用内存流实例化解密流对象
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);
            //    Image = Properties.Resources.sub,
            //    Name = "someName",
            //    HeaderText = "Some Text"
            //});//    Image = Properties.Resources.sub,
            //    Name = "someName",
            //    HeaderText = "Some Text"
            //});
            CStream.Write(data, 0, data.Length);            // 向解密流中写入数据

            CStream.FlushFinalBlock();                      // 释放解密流

            return Encoding.Unicode.GetString(MStream.ToArray()); // 返回解密后的字符串  
        }
        
        
        /// <summary> 
        /// 执行SQL语句并返回数据表 
        /// </summary> 
        /// <param name="Sqlstr">SQL语句</param> 
        /// <returns></returns> 
        public static DataTable ExecuteDt(String Sqlstr)
        {
            using (MySqlConnection conn = new MySqlConnection(Setting.ConnStr))
            {
                MySqlDataAdapter da = new MySqlDataAdapter(Sqlstr, conn);
                DataTable dt = new DataTable();
                conn.Open();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
        }

        public static DataTable NewExecuteDt(String Sqlstr, String connStr)
        {
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                MySqlDataAdapter da = new MySqlDataAdapter(Sqlstr, conn);
                DataTable dt = new DataTable();
                conn.Open();
                da.Fill(dt);
                conn.Close();
                return dt;
            }
        }


        public static int ExecuteNonQuery(string cmdText)
        {


            MySqlCommand cmd = new MySqlCommand();


            using (MySqlConnection conn = new MySqlConnection(Setting.ConnStr))
            {
                cmd.Connection = conn;
                conn.Open();
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
                conn.Close();
            }
        }

        /// <summary> 
        /// 执行SQL语句并返回DataSet 
        /// </summary> 
        /// <param name="Sqlstr">SQL语句</param> 
        /// <returns></returns> 
 
         
        #endregion
        /// <summary>
        /// 检测数据库连接状态
        /// </summary>
        /// <returns></returns>
        public static bool CheckDBConnection()
        {
            MySqlConnection conn = new MySqlConnection(Setting.ConnStr);

            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            try
            {
                conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
        }
       

 


        public static void updateCgd(String Query)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(Setting.ConnStr);

                MySqlCommand MyCommand2 = new MySqlCommand(Query, conn);

                MySqlDataReader MyReader2;
                conn.Open();
                MyReader2 = MyCommand2.ExecuteReader();
                conn.Close();
                MessageBox.Show("添加成功");
            }
            catch (Exception w)
            {
                MessageBox.Show("添加失败");
                LogHelper.WriteLog("执行SQL出现异常" + w.Message );
                LogHelper.WriteLog("执行SQL出现异常" + Query);
               
            }


        }




        private static byte[] FileToBytes(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            byte[] buffer = new byte[fi.Length];
            FileStream fs = fi.OpenRead();
            fs.Read(buffer, 0, Convert.ToInt32(fi.Length));
            fs.Close();
            return buffer;
        }
        private static void SendFileBytesToDatabase(string title, byte[] fileArr, string username)
        {
            MySqlConnection sendDataConnection = new MySqlConnection(GetSqlConn());
            string sendFileSql = "insert into t_cs_tb_file(c_title,c_file,c_user) values(?title,?file,?user);";
            MySqlCommand sendCmd = new MySqlCommand(sendFileSql, sendDataConnection);
            sendCmd.Parameters.Add("?title", MySqlDbType.VarChar).Value = title;
            sendCmd.Parameters.Add("?file", MySqlDbType.MediumBlob).Value = fileArr;
            sendCmd.Parameters.Add("?user", MySqlDbType.VarChar).Value = username;
            sendDataConnection.Open();
            try
            {
                sendCmd.ExecuteNonQuery();
                Console.WriteLine("向数据库储存数据完成");
            }
            catch (Exception e)
            {
                Console.WriteLine("向数据库存储数据失败：" + e.Message);
            }
            finally
            {
                sendDataConnection.Close();
            }
        }
        private static void GetFileFromDatabase(string searchTitle, string newFilePath)
        {

            MySqlConnection getFileConnection = new MySqlConnection(GetSqlConn());
            string getFileSql = "select * from t_cs_tb_file where c_title='" + searchTitle + "';";
            MySqlCommand getCmd = new MySqlCommand(getFileSql, getFileConnection);
            getFileConnection.Open();
            MySqlDataReader reader = getCmd.ExecuteReader();
            byte[] newFileBuffer = null;
            try
            {
                while (reader.Read())
                {
                    if (reader.HasRows)
                    {
                        long len = reader.GetBytes(1, 0, newFileBuffer, 0, 0);
                        newFileBuffer = new byte[len];
                        len = reader.GetBytes(1, 0, newFileBuffer, 0, (int)len);
                        CreateFile(newFileBuffer, newFilePath);
                        Console.WriteLine("已成功创建文件：" + newFilePath);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("查找数据库信息失败：" + e.Message);
            }
            finally
            {
                getFileConnection.Close();
            }
        }
        public static void SendImgTest()
        {
            string imgPath = @"C:\Users\ \Pictures\fds.jpg";
            byte[] imgBuffer = FileToBytes(imgPath);
            SendFileBytesToDatabase("cloud", imgBuffer, "test");
        }
        public static void GetImgTest()
        {
            string imgPath = @"C:\Users\ \Pictures\fds_testr.jpgf";
            GetFileFromDatabase("cloud", imgPath);
        }
         
    }
}
