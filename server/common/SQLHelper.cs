using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server.common
{
    class SQLHelper
    {
        #region 通用方法
        // 数据连接池 
        private SqlConnection con;

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


            // String ip = "hk-cdb-gzcavcg3.sql.tencentcdb.com";
            String ip = "39.100.137.4";
            String db = "feifeigo";
            String user = "root";
            String pwd = "123456";
            String port = "1522";

            //String ip = "127.0.0.1";
            //String db = "betaskdata";
            //String user = "root";
            //String pwd = "root123";
            //String port = "3306";
            return "Database=" + db + ";Data Source=" + ip + ";User Id=" + user + ";Password=" + pwd + ";pooling=false;CharSet=utf8;port=" + port + "";
        }

        #region 解密字符串   
        /// <summary>  
        /// 解密字符串   
        /// </summary>  
        /// <param name="str">要解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
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
        #endregion

        #endregion
        #region 执行sql字符串
        /// <summary> 
        /// 执行不带参数的SQL语句 
        /// </summary> 
        /// <param name="Sqlstr"></param> 
        /// <returns></returns> 
        public static int ExecuteSql(String Sqlstr)
        {
            int ret = 0;
            using (SqlConnection conn = new SqlConnection(Setting.ConnStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                conn.Open();
                try
                {
                    ret = cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    return 0;
                }
                conn.Close();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return ret;
            }
        }
        public static object ExecuteScalar(String Sqlstr)
        {
            object obj;
            using (SqlConnection conn = new SqlConnection(Setting.ConnStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                conn.Open();
                try
                {
                    obj = cmd.ExecuteScalar();
                }
                catch (Exception)
                {
                    return 0;
                }
                conn.Close();
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                return obj;
            }
        }
        /// <summary> 
        /// 执行带参数的SQL语句 
        /// </summary> 
        /// <param name="Sqlstr">SQL语句</param> 
        /// <param name="param">参数对象数组</param> 
        /// <returns></returns> 
        public static int ExecuteSql(String Sqlstr, SqlParameter[] param)
        {
            using (SqlConnection conn = new SqlConnection(Setting.ConnStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                cmd.Parameters.AddRange(param);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
                return 1;
            }
        }
        /// <summary> 
        /// 返回DataReader 
        /// </summary> 
        /// <param name="Sqlstr"></param> 
        /// <returns></returns> 
        public static SqlDataReader ExecuteReader(String Sqlstr)
        {
            SqlConnection conn = new SqlConnection(Setting.ConnStr);//返回DataReader时,是不可以用using()的 
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = Sqlstr;
                conn.Open();
                return cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);//关闭关联的Connection 
            }
            catch (Exception e)
            {
                return null;
            }
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
        #region 操作存储过程
        /// <summary> 
        /// 运行存储过程(已重载) 
        /// </summary> 
        /// <param name="procName">存储过程的名字</param> 
        /// <returns>存储过程的返回值</returns> 
        public void RunProc(string procName)
        {
            SqlCommand cmd = CreateCommand(procName, null);
            cmd.ExecuteNonQuery();
            this.Close();
            //return (int)cmd.Parameters[0].Value;
        }
        /// <summary> 
        /// 运行存储过程(已重载) 
        /// </summary> 
        /// <param name="procName">存储过程的名字</param> 
        /// <param name="prams">存储过程的输入参数列表</param> 
        /// <returns>存储过程的返回值</returns> 
        public object RunProc(string procName, SqlParameter[] prams)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            cmd.ExecuteNonQuery();
            this.Close();
            return cmd.Parameters[0].Value;
        }
        /// <summary> 
        /// 运行存储过程(已重载) 
        /// </summary> 
        /// <param name="procName">存储过程的名字</param> 
        /// <param name="dataReader">结果集</param> 
        public void RunProc(string procName, out SqlDataReader dataReader)
        {
            SqlCommand cmd = CreateCommand(procName, null);
            dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
        /// <summary> 
        /// 运行存储过程(已重载) 
        /// </summary> 
        /// <param name="procName">存储过程的名字</param> 
        /// <param name="prams">存储过程的输入参数列表</param> 
        /// <param name="dataReader">结果集</param> 
        public void RunProc(string procName, SqlParameter[] prams, out SqlDataReader dataReader)
        {
            SqlCommand cmd = CreateCommand(procName, prams);
            dataReader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
        }
        /// <summary> 
        /// 创建Command对象用于访问存储过程 
        /// </summary> 
        /// <param name="procName">存储过程的名字</param> 
        /// <param name="prams">存储过程的输入参数列表</param> 
        /// <returns>Command对象</returns> 
        private SqlCommand CreateCommand(string procName, SqlParameter[] prams)
        {
            // 确定连接是打开的 
            Open();
            //command = new SqlCommand( sprocName, new SqlConnection( ConfigManager.DALConnectionString ) ); 
            SqlCommand cmd = new SqlCommand(procName, con);
            cmd.CommandType = CommandType.StoredProcedure;
            // 添加存储过程的输入参数列表 
            if (prams != null)
            {
                foreach (SqlParameter parameter in prams)
                    cmd.Parameters.Add(parameter);
            }
            // 返回Command对象 
            return cmd;
        }
        //增删改
        public bool addpr(string prname, SqlParameter[] paraname)
        {

            Open();

            SqlCommand mycmd = new SqlCommand(prname, con);

            mycmd.CommandType = CommandType.StoredProcedure;

            if (paraname != null)
            {

                foreach (SqlParameter patem in paraname)
                {
                    mycmd.Parameters.Add(patem);
                }
            }

            return Convert.ToBoolean(mycmd.ExecuteNonQuery());
        }

        /// <summary> 
        /// 创建输入参数 
        /// </summary> 
        /// <param name="ParamName">参数名</param> 
        /// <param name="DbType">参数类型</param> 
        /// <param name="Size">参数大小</param> 
        /// <param name="Value">参数值</param> 
        /// <returns>新参数对象</returns> 
        public SqlParameter MakeInParam(string ParamName, SqlDbType DbType, int Size, object Value)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Input, Value);
        }
        /// <summary> 
        /// 创建输出参数 
        /// </summary> 
        /// <param name="ParamName">参数名</param> 
        /// <param name="DbType">参数类型</param> 
        /// <param name="Size">参数大小</param> 
        /// <returns>新参数对象</returns> 
        public SqlParameter MakeOutParam(string ParamName, SqlDbType DbType, int Size)
        {
            return MakeParam(ParamName, DbType, Size, ParameterDirection.Output, null);
        }
        /// <summary> 
        /// 创建存储过程参数 
        /// </summary> 
        /// <param name="ParamName">参数名</param> 
        /// <param name="DbType">参数类型</param> 
        /// <param name="Size">参数大小</param> 
        /// <param name="Direction">参数的方向(输入/输出)</param> 
        /// <param name="Value">参数值</param> 
        /// <returns>新参数对象</returns> 
        public SqlParameter MakeParam(string ParamName, SqlDbType DbType, Int32 Size, ParameterDirection Direction, object Value)
        {
            SqlParameter param;
            if (Size > 0)
            {
                param = new SqlParameter(ParamName, DbType, Size);
            }
            else
            {
                param = new SqlParameter(ParamName, DbType);
            }
            param.Direction = Direction;
            if (!(Direction == ParameterDirection.Output && Value == null))
            {
                param.Value = Value;
            }
            return param;
        }
        #endregion
        #region 数据库连接和关闭
        /// <summary> 
        /// 打开连接池 
        /// </summary> 
        private void Open()
        {

            // 打开连接池 
            if (con == null)
            {
                //这里不仅需要using System.Configuration;还要在引用目录里添加 
                con = new SqlConnection(Setting.ConnStr);
                con.Open();
            }

        }
        /// <summary> 
        /// 关闭连接池 
        /// </summary> 
        public void Close()
        {
            if (con != null)
                con.Close();
        }
        /// <summary> 
        /// 释放连接池 
        /// </summary> 
        public void Dispose()
        {
            // 确定连接已关闭 
            if (con != null)
            {
                con.Dispose();
                con = null;
            }
        }
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
        /// <summary>
        /// 向数据库导出图片
        /// </summary>
        /// <param name="photoName">导出图片的名称</param>
        /// <returns>返回导出的图片，如果数据库没有该文件，返回一个16*16的bitmap文件</returns>
        public static Image ExecuteOutPhoto(string photoName)
        {
            byte[] imagebytes = null;
            //打开数据库
            SqlConnection con = new SqlConnection(Setting.ConnStr);
            con.Open();

            SqlCommand com = new SqlCommand("select bmp from speedBtn where ncaption = '" + photoName + "'", con);
            SqlDataReader dr = com.ExecuteReader();

            while (dr.Read())
            {
                imagebytes = (byte[])dr.GetValue(0);
            }

            dr.Close();
            com.Clone();
            con.Close();
            if (imagebytes != null)
            {
                MemoryStream ms = new MemoryStream(imagebytes);
                return new Bitmap(ms);
            }
            else
            {
                return null;
            }
        }


        public static SqlDataAdapter getSqlDataAdapter(string selectStr)
        {
            SqlDataAdapter sda;
            using (SqlConnection conn = new SqlConnection(Setting.ConnStr))
            {
                sda = new SqlDataAdapter(selectStr, conn);
                //DataTable dt = new DataTable();
                conn.Open();
                //da.Fill(dt);
                //conn.Close();
                return sda;
            }
        }



        /// <summary>
        /// 批量向数据库写入数据
        /// </summary>
        /// <param DataTable="dt">DataTable</param>
        /// <param tablename>tablename</param>
        /// <returns></returns>
        public static void BulkToDB(DataTable dt, string tablename)
        {
            SqlConnection conn = new SqlConnection(Setting.ConnStr);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(conn);
            bulkCopy.DestinationTableName = tablename;
            bulkCopy.BatchSize = dt.Rows.Count;

            try
            {
                if (dt != null && dt.Rows.Count != 0)
                {
                    conn.Open();
                    foreach (DataColumn column in dt.Columns)
                        bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    bulkCopy.WriteToServer(dt);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                if (bulkCopy != null)
                    bulkCopy.Close();
            }
        }



        public static void updateCgd(String Query)
        {

            MySqlConnection conn = new MySqlConnection(Setting.ConnStr);

            MySqlCommand MyCommand2 = new MySqlCommand(Query, conn);

            MySqlDataReader MyReader2;

            conn.Open();

            MyReader2 = MyCommand2.ExecuteReader();





            conn.Close();

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
