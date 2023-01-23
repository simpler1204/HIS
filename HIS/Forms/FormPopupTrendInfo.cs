using HIS.Class;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using DevExpress.XtraSplashScreen;
using HIS.PopUp;
using System.Reflection;

namespace HIS.Forms
{
    public partial class FormPopupTrendInfo : Form
    {
        public static FormPopupTrendInfo createdForm = null;
        DataTable dtTrendInfo = new DataTable("TrendInfo");
        private event EventHandler<int[]> ProgressBarEvent;
        Thread t1 = null;
        SplashScreenManager splashScreenManager1;


        public FormPopupTrendInfo()
        {
            InitializeComponent();
            createdForm = this;

            this.Load += FormPopupTrendInfo_Load;

            //폼 클로즈때
            this.FormClosing += delegate (object sender, FormClosingEventArgs e) { createdForm = null; };
            this.FormClosed += delegate (object sender, FormClosedEventArgs e) { createdForm = null; };
            this.ProgressBarEvent += FormPopupTrendInfo_ProgressBarEvent;

            //메뉴 버튼 클릭 이벤트
            this.menu.ButtonClick += Menu_ButtonClick;


            this.FormClosing += (sender, e) =>
            {
                this.Load -= FormPopupTrendInfo_Load;
                this.menu.ButtonClick -= Menu_ButtonClick;
            };
        }

        private void FormPopupTrendInfo_ProgressBarEvent(object sender, int[] e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<int[]>(ProgressBarEvent), sender, e);
            }
            else
            {
                if (e[0] == -1 && e[1] == -1)
                {
                    panelProgress.Visible = false; //에러 발생했을때
                    return;
                }

                progressBar1.Maximum = e[1];

                if (e[0] > 0) panelProgress.Visible = true;
                panelProgress.Location = new Point((dataGridView1.Width - panelProgress.Width) / 2, (dataGridView1.Height - panelProgress.Height) / 2);
                lblNowCount.Text = e[0].ToString("#,###");
                lblTotalCount.Text = e[1].ToString("#,###");
                progressBar1.Value = e[0];
                if (e[0] == e[1]) panelProgress.Visible = false;
               
            }
        }

        private void Menu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string menu = e.Button.Properties.Caption;
            DialogResult rtn;
            switch (menu)
            {
                case "Import":
                    ImportTrendData();
                    break;


                case "Save":
                    if (dataGridView1.Rows.Count < 1) break;
                    rtn = MessageBox.Show("Do you want to save trend info?", "Infomation", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (rtn == DialogResult.Yes)
                    {
                        t1 = new Thread(SaveTrendData);
                        t1.Start();                            
                    }
                    break;


                case "Remove":
                    if (dataGridView1.Rows.Count < 1) break;
                    string dpName = string.Empty;
                    if (dataGridView1.CurrentCell.ColumnIndex == 2)
                    {
                        dpName = dataGridView1[2, dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    }

                    rtn = MessageBox.Show($"Do you want to delete {dpName}?", "Warning",
                         MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if(rtn == DialogResult.Yes)
                        DeleteTrendData();
                    break;

                case "Select":
                    SelectTrendInfo();
                    break;

                case "New":
                    //if (dtTrendInfo.Rows.Count < 1) return;
                    NewTrendInfo();
                    break;

                case "Export":
                    ExportTrendInfo();
                    break;
            }
        }

        private void ExportTrendInfo()
        {
            if (dtTrendInfo.Rows.Count < 1) return;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveDialog.FilterIndex = 1;

            DataTable selectedDt = dtTrendInfo.AsEnumerable()
                .Where(row => row.Field<string>("MODIFIED") == "S").CopyToDataTable();
            
            Excel ex = new Excel();
            ex.ExportEvent += delegate (object sender, int[] e)
            {
                if(InvokeRequired)
                {
                    if (e[0] == -1) Excel.excelOpen(saveDialog.FileName);
                    Invoke(new EventHandler<int[]>(ProgressBarEvent), sender, e);
                }
                else
                {  
                    panelProgress.Visible = true;
                    progressBar1.Value = e[0];
                    lblNowCount.Text = e[0].ToString();
                    lblTotalCount.Text = e[1].ToString();

                    if (e[0] == e[1]) panelProgress.Visible = false;
                }
                
            };

            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var t = new Thread(() => ex.ExportToExcel(selectedDt, saveDialog.FileName, "Sheet1"));
                t.Start();
            }
          
        }
       
        private void NewTrendInfo()
        {
            PopUpInsertDp frm = new PopUpInsertDp();
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.InsertEvent += Frm_InsertEvent;
            frm.ShowDialog();
        }

        private void Frm_InsertEvent(object sender, List<string> e)
        {
            if (e.Count < 1) return;
            string system = e[0];
            string dpName = e[1];
            string dpDesc = e[2];
            string yMin = e[3];
            string yMax = e[4];

            if(InvokeRequired)
            {
                Invoke(new EventHandler<List<string>>(Frm_InsertEvent), sender, e);
            }
            else
            {
                DataRow dr = dtTrendInfo.NewRow();
                dr["SEQ"] = dtTrendInfo.Rows.Count + 1;
                dr["SYSTEM"] = system;
                dr["DP_NAME"] = dpName;
                dr["DP_DESC"] = dpDesc;
                dr["Y_MIN"] = yMin;
                dr["Y_MAX"] = yMax;

                dtTrendInfo.Rows.Add(dr);
            }

            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
            dataGridView1.CurrentCell = dataGridView1[1, dataGridView1.Rows.Count - 1];
            


        }

        private void SelectTrendInfo()
        {
            if (dtTrendInfo.Rows.Count > 0) dtTrendInfo.Clear();

            splashScreenManager1.ShowWaitForm();

            string dpName = "%"+ txtDpName.Text + "%";
            string dpDesc = "%" + txtDesc.Text + "%";
            string tableName = "%" + txtTableName.Text + "%";
            string system = cmbSystem.Text == "ALL" ? "%" : "%"+cmbSystem.Text+"%";
            string query = @"SELECT SYSTEM, DP_NAME, DP_DESC, Y_MIN, Y_MAX, TB_NAME, COL_NAME, UPDATED_AT 
                             FROM C2_TREND_INFO WHERE SYSTEM LIKE :1 AND DP_NAME LIKE :2 
                             AND NVL(DP_DESC, '%') LIKE :3 AND NVL(TB_NAME, '%') LIKE :4 ";

            if (!Database.Open()) return;

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = system;
            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dpName;
            cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = dpDesc;
            cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = tableName;


            dtTrendInfo.Rows.Clear(); //Data table 초기화

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                int i = 1;
                //int count = 0;
                while (reader.Read())
                {
                    DataRow row = dtTrendInfo.NewRow();
                    row["SEQ"]      = i.ToString(); //SEQ
                    row["SYSTEM"]   = reader[0].ToString(); //system
                    row["DP_NAME"]  = reader[1].ToString(); //dp name
                    row["DP_DESC"]  = reader[2].ToString(); //dp desc
                    row["Y_MIN"]    = reader[3].ToString(); //y_min
                    row["Y_MAX"]    = reader[4].ToString(); //y_max
                    row["TB_NAME"]  = reader[5].ToString(); //table name
                    row["COL_NAME"] = reader[6].ToString(); //column name

                    if (reader[7].ToString() == "")
                    {
                        row["UPDATED"] = reader[7].ToString(); //column name
                    }
                    else
                    {
                        DateTime dt;
                        DateTime.TryParse(reader[7].ToString(), out dt);
                        row["UPDATED"] = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    }

                    row["MODIFIED"] = "S";

                    dtTrendInfo.Rows.Add(row);
                    i++;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                   // count++;
                }

                
                InitDataGridView.AutoSettingDatagridView(dataGridView1, new List<int>() { 2, 3 }, new List<int>() { 9 });
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                splashScreenManager1.CloseWaitForm();
                cmd.Dispose();
                Database.Close();
            }
        }

        private void DeleteTrendData()
        {
            if (dtTrendInfo.Rows.Count < 1) return;
            if (!Database.Open()) return;

            Int32 selectedCellCount = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);
            
            if (selectedCellCount > 0)
            {
                if (dataGridView1.AreAllCellsSelected(true))
                {
                    MessageBox.Show("Can not select all cells", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string query = "DELETE C2_TREND_INFO WHERE DP_NAME = :1";
                    OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                    try
                    {
                        for (int i = selectedCellCount - 1; i >= 0; i--)
                        {
                            if (dataGridView1.SelectedCells[i].ColumnIndex.ToString() == "2")
                            {
                                int row = dataGridView1.SelectedCells[i].RowIndex;
                                string dpName = dataGridView1.SelectedCells[i].Value.ToString();
                                string insertedTableName = dataGridView1[6, row].Value.ToString();

                                if (insertedTableName != "") continue;

                                cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dpName;
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();                             

                                var searched = dtTrendInfo.AsEnumerable().Where(r => r.Field<string>("DP_NAME") == dpName);

                                bool deleted = false;
                                for (int j = 0; j < dtTrendInfo.Rows.Count; j++)
                                {
                                    for (int k = 0; k < dtTrendInfo.Columns.Count; k++)
                                    {
                                        if (dtTrendInfo.Rows[j][k].ToString() == dpName)
                                        {
                                            dtTrendInfo.Rows[j].Delete();
                                            deleted = true;
                                            break;
                                        }
                                    }

                                    if (deleted) dtTrendInfo.Rows[j][0] = j+1;
                                }
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        cmd.Dispose();
                        Database.Close();
                    }
                }
            }

          
        }
    

        private void SaveTrendData()
        {
            if (!Database.Open()) return;

            string insertQuery = "INSERT INTO C2_TREND_INFO(SYSTEM, DP_NAME, DP_DESC, Y_MIN, Y_MAX) VALUES(:1, :2, :3, :4, :5)";

            EnumerableRowCollection<DataRow> filteredData = GetFilterdData(TransactionType.INSERT);
            int totalCount = filteredData.Count<DataRow>();
            if (totalCount < 1) return;

            int currentCount = 0;

            OracleCommand cmd = new OracleCommand();
            cmd.CommandText = insertQuery;
            cmd.Connection = Database.OracleConn;

            try
            {
                foreach (DataRow item in filteredData)  //dtTrendData에서 필터된 Rows
                {
                    for (int j = 1; j < 6; j++) //DataRow의 각 column을 읽음. TB_NAME, COL_NAME은 제외한다.(별도의 Form에서 기능 구현)
                    {
                        cmd.Parameters.Add(":" + (j + 1).ToString(), OracleDbType.Varchar2).Value = item[j].ToString();
                    }

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                    currentCount++;

                    var rows = dtTrendInfo.AsEnumerable().Where(r => r.Field<string>("DP_NAME") == item["DP_NAME"].ToString());
                    foreach (DataRow val in rows)
                    {
                        val["MODIFIED"] = "";
                    }

                    ProgressBarEvent(this, new int[2] { currentCount, totalCount });
                }

            }
            catch (Exception ex)
            {
                ProgressBarEvent(this, new int[2] { -1, -1 }); //에러 발생
                MessageBox.Show(ex.Message);             
               
                return;
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }

        }

        private void ImportTrendData()
        {

            splashScreenManager1.ShowWaitForm();
            Excel excel = new Excel();
            int row = 1;
            try
            {
                excel.ExcelToList();
                foreach (List<string> outer in excel)
                {                 
                    int col = 1;                 

                    DataRow dr = dtTrendInfo.NewRow();
                    dr[0] = row;
                    foreach (var inner in outer)
                    {  
                        dr[col] = inner;                       
                        col++;
                    }                 
                    dr["MODIFIED"] = "I";
                    dtTrendInfo.Rows.Add(dr);
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                    row++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.Message);
            }
            finally
            {
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void FormPopupTrendInfo_Load(object sender, EventArgs e)
        {
            menu.ForeColor = Colors.buttonForeColor;

            dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.DarkGray;

            cmbSystem.Text = "ALL";
            InitDatatable.Init(dtTrendInfo);
            InitDataGridView.dataGridViewInit(dataGridView1);

            dataGridView1.DataSource = dtTrendInfo;

            this.splashScreenManager1 = new SplashScreenManager(this, typeof(global::HIS.Forms.WaitForm1), true, true);
            this.splashScreenManager1.ClosingDelay = 500;

            DataGridViewBufferExtension();

            SetDoNotSort(dataGridView1);

        }

        private EnumerableRowCollection<DataRow> GetFilterdData(TransactionType type)
        {
            string tranType = string.Empty;

            if (type == TransactionType.INSERT)
                tranType = "I";
            else if (type == TransactionType.DELETE)
                tranType = "D";
            else if (type == TransactionType.UPDATE)
                tranType = "U";
            else
                tranType = "S";

            var searchedValues = dtTrendInfo
                .AsEnumerable()
                .Where(row => row.Field<string>("MODIFIED") == tranType);

            return searchedValues;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.Rows.Count < 1) return;
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
               

                if (Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[0].EditedFormattedValue) == true)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = false;
                   
                }
                else
                {
                    dataGridView1.Rows[e.RowIndex].Cells[0].Value = true;
                   
                }
                Thread.Sleep(20);
            }
        }

        private void DataGridViewBufferExtension()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
            null, this.dataGridView1, new object[] { true });
        }

        private void SetDoNotSort(DataGridView dgv)
        {
            foreach (DataGridViewColumn i in dgv.Columns)
            {
                i.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }      
    
}
