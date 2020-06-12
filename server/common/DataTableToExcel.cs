using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace caiwu.common
{
    class DataTableToExcel
    {

        public static void ExportDataToExcel(DataGridView myDGV)
        {
            string path = "";
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "请选择要导出的位置";
            saveDialog.Filter = "Excel文件| *.xlsx;*.xls";
            saveDialog.ShowDialog();
            path = saveDialog.FileName;
            if (path.IndexOf(":") < 0) return; //判断是否点击取消
            try
            {
                Thread.Sleep(1000);
                StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("gb2312"));
                StringBuilder sb = new StringBuilder();
                //写入标题
                for (int k = 0; k < myDGV.Columns.Count; k++)
                {
                    if (myDGV.Columns[k].Visible)//导出可见的标题
                    {
                        //"\t"就等于键盘上的Tab,加个"\t"的意思是: 填充完后进入下一个单元格.
                        sb.Append(myDGV.Columns[k].HeaderText.ToString().Trim() + "\t");
                    }
                }
                sb.Append(Environment.NewLine);//换行
                                               //写入每行数值
                for (int i = 0; i < myDGV.Rows.Count ; i++)
                {
                    System.Windows.Forms.Application.DoEvents();
                    for (int j = 0; j < myDGV.Columns.Count; j++)
                    {
                        if (myDGV.Columns[j].Visible)//导出可见的单元格
                        {
                            //注意单元格有一定的字节数量限制,如果超出,就会出现两个单元格的内容是一模一样的.
                            //具体限制是多少字节,没有作深入研究.
                            sb.Append(myDGV.Rows[i].Cells[j].Value.ToString().Trim() + "\t");
                        }
                    }
                    sb.Append(Environment.NewLine); //换行
                }
                sw.Write(sb.ToString());
                sw.Flush();
                sw.Close();
                MessageBox.Show(path + "，导出成功", "系统提示", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        } 

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
