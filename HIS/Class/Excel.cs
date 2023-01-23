using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Runtime.InteropServices;
using System.Data.OleDb;
using System.Diagnostics;
using System.Windows.Forms;
using System.Data;
using System.Collections;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;

namespace HIS.Class
{
    class Excel : IEnumerable
    {
        [DllImport("user32.dll")]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        List<List<string>> dpList = new List<List<string>>();
        public event EventHandler<int[]> ExportEvent;


        public List<List<string>> ExcelToList()
        {
            string sFile = string.Empty;
            int i = 0;
            int j = 0;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            OleDbConnection cnCSV = null;
            OleDbCommand cmdSelect = null;
            OleDbDataAdapter daCSV = null;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    sFile = openFileDialog.FileName;
                    Cursor.Current = Cursors.WaitCursor;

                    string sheet = "Sheet1";
                    string strFilePath = sFile;//file path name
                    String strConnectionString = "";

                    strConnectionString = $"Provider= Microsoft.ACE.OLEDB.12.0;Data Source={strFilePath};" +
                        $"Extended Properties='Excel 8.0;HDR=Yes'";


                    cnCSV = new OleDbConnection(strConnectionString);
                    cnCSV.Open();
                    cmdSelect = new OleDbCommand(@"SELECT * FROM [" + sheet + "$]", cnCSV);
                    daCSV = new OleDbDataAdapter(cmdSelect);
                    //  System.Data. DataTable dtCSV = new System.Data. DataTable();

                    DataSet ds = new DataSet();
                    daCSV.Fill(ds);                    

                    for ( i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<string> list = new List<string>();
                        for ( j = 0; j < ds.Tables[0].Columns.Count; j++)
                        {                            
                            list.Add(ds.Tables[0].Rows[i][j].ToString());  
                        }                        
                        dpList.Add(list);
                    }                
                    Cursor.Current = Cursors.Default;                   
                }
                catch(Exception ex)
                {
                   // MessageBox.Show(ex.Message);
                    throw new ApplicationException(ex.Message, ex);
                }
                finally
                {
                    if(cmdSelect != null) cmdSelect.Dispose();
                    if(daCSV != null) daCSV.Dispose();
                    if (cnCSV != null) cnCSV.Close();
                }

            }

            return dpList;     
        }

        public void ExportToExcel(DataTable dt, string path, string sheetName)
        {           
            // Creating a Excel object. 
            Microsoft.Office.Interop.Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;

            int rowCount = dt.Rows.Count + 1; //제목을 포함
            int colCount = dt.Columns.Count - 2; //마지막 MODOFIED, CHECKED항목은 제외                      

            try
            {
                worksheet = workbook.ActiveSheet;
                worksheet.Name = sheetName;

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                for(int i=0; i<rowCount; i++)
                {
                    for (int j = 0; j < colCount; j++)
                    {
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dt.Columns[j].ColumnName;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dt.Rows[i - 1][j].ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;

                    ExportEvent(this, new int[] { cellRowIndex, rowCount });
                    cellRowIndex++;
                }
               
                workbook.SaveAs(path);             

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {                
                int hWnd = excel.Application.Hwnd;
                uint processID;

                GetWindowThreadProcessId((IntPtr)hWnd, out processID);
                Process.GetProcessById((int)processID).Kill();

                workbook = null;
                excel = null;
                worksheet = null;

                Excel.excelOpen(path);
            }


        }

        public static void excelOpen(string path)
        {
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start(path);
            }
            else
            {
                MessageBox.Show("Succeed to export trend data, but can't open the file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }        


        public IEnumerator GetEnumerator()
        {
            return dpList.GetEnumerator();
        }
    }
}
