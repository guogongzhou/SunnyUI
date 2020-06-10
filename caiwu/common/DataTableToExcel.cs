using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caiwu.common
{
    class DataTableToExcel
    {
        public static void DataToExcel(DataTable m_DataTable)

        {

            SaveFileDialog kk = new SaveFileDialog();

            kk.Title = "保存EXECL文件";

            kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";

            kk.FilterIndex = 1;

            if (kk.ShowDialog() == DialogResult.OK)

            {

                string FileName = kk.FileName ;

                if (File.Exists(FileName))

                    File.Delete(FileName);

                FileStream objFileStream;

                StreamWriter objStreamWriter;

                string strLine = "";

                objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);

                objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);

                for (int i = 0; i < m_DataTable.Columns.Count ; i++)

                {

                    strLine = strLine + m_DataTable.Columns[i].Caption.ToString() + Convert.ToChar(9);

                }

                objStreamWriter.WriteLine(strLine);

                strLine = "";



                for (int i = 0; i < m_DataTable.Rows.Count; i++)

                {

                    for (int j = 0; j < m_DataTable.Columns.Count ; j++)

                    {

                        if (m_DataTable.Rows[i].ItemArray[j] == null)

                            strLine = strLine + " " + Convert.ToChar(9);

                        else

                        {

                            string rowstr = "";

                            rowstr = m_DataTable.Rows[i].ItemArray[j].ToString();

                            if (rowstr.IndexOf("\r\n") > 0)

                                rowstr = rowstr.Replace("\r\n", " ");

                            if (rowstr.IndexOf("\t") > 0)

                                rowstr = rowstr.Replace("\t", " ");

                            strLine = strLine + (j == 4 ? "" : "") + (j == 2 ? "" : "") + Convert.ToString(rowstr) + "\t";

                        }

                    }

                    objStreamWriter.WriteLine(strLine);

                    strLine = "";

                }

                objStreamWriter.Close();

                objFileStream.Close();

                MessageBox.Show("保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

        }


    }
}
