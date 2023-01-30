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
using System.Reflection;

namespace HIS.Forms
{
    public partial class FormSettingDpToTable: Form
    {
        public static FormSettingDpToTable createdForm = null;
        DataTable dtTrendInfo = new DataTable("TrendInfo");
        DataTable dtMaster = new DataTable("Master");
        DataTable dtDetail = new DataTable("Detail");

        private SplashScreenManager splashScreenManager1;

        List<int> emptyColumnNumbers = new List<int>(); //Trend Table에서 빈 컬럼
        List<int> checkedColumnNumbers = new List<int>();

        private string clickedButtonName = string.Empty;

        public FormSettingDpToTable()
        {
            InitializeComponent();
            createdForm = this;

            this.Load += FormPopupTrendInfo_Load;
            

            //폼 클로즈때
            this.FormClosing += delegate (object sender, FormClosingEventArgs e) { createdForm = null; };
            this.FormClosed += delegate (object sender, FormClosedEventArgs e) { createdForm = null; };

            //메뉴 버튼 클릭 이벤트
            this.menu.ButtonClick += Menu_ButtonClick;


            this.FormClosing += (sender, e) =>
            {
                this.Load -= FormPopupTrendInfo_Load;
                this.menu.ButtonClick -= Menu_ButtonClick;
            };
        }
        

        private void Menu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string menu = e.Button.Properties.Caption;

            switch (menu)
            {
                case "Select":
                    SelectTrendInfo();
                    if (cmbSystem.Text != "ALL") SelectTrendInfoMaster(cmbSystem.Text);
                    MenuButtonClicked("Select");
                    break;
                case "Mapping":
                    int mappingCount = MappingDpToTable();
                    if(mappingCount > 0)
                        MenuButtonClicked("Mapping");
                    break;
                case "Clear":
                    ClearMappingData();
                    MenuButtonClicked("Clear");
                    break;
                case "Remove":
                    int removedCount = RemoveTable();
                    if(removedCount > 0)
                        MenuButtonClicked("Remove");
                    break;
                case "Save":
                    DialogResult rtn = MessageBox.Show("Do you want to save trend info?", "Infomation",
                       MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    if (rtn == DialogResult.Yes)
                    {
                        SaveTrendData();
                        dataGridView1.Refresh();
                        MenuButtonClicked("Save");
                    }
                    break;
            }
        }

        private void SaveTrendData()
        {
            splashScreenManager1.ShowWaitForm();
            try
            {
                SaveTrendInfo();
                SaveMasterInfo();
                SaveDetailInfo(txtSystem.Text);
                splashScreenManager1.CloseWaitForm();
                MessageBox.Show("Success to save trend data ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                splashScreenManager1.CloseWaitForm();
                if (ex.Message == "Socket Error")
                {
                    MessageBox.Show($"Sucess to save trend data.\nBut {txtSystem.Text} server is not restarted\nYou need to restart WCCOADataLogging",  
                        "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
            

        }

        private void SaveTrendMoveHistory(string dp, string table, string column)
        {
            string query = string.Empty;
            if (clickedButtonName == "Mapping")
            {
                 query = @"INSERT INTO C2_DP_TREND_MOVE_HISTORY(SEQ, DP_NAME, TB_NAME, COL_NAME, START_AT) 
                             VALUES(DP_MOVE.NEXTVAL, :1, :2, :3, SYSDATE)";

                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dp;
                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = table;
                cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = column;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            else if(clickedButtonName == "Remove")
            {                
                query = @"UPDATE C2_DP_TREND_MOVE_HISTORY SET END_AT = SYSDATE WHERE DP_NAME = :1 AND END_AT IS NULL";
                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dp;               
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }

                     
        }

        private void SaveDetailInfo(string system)
        {
            if (!Database.Open()) return;
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = Database.OracleConn;

            var searched = dtDetail.AsEnumerable().Where(row => row.Field<string>("MODIFIED") == "M");
            try
            {
                foreach (DataRow item in searched)
                {
                    string col_name = item["COL_NAME"].ToString();
                    string tb_name = item["TB_NAME"].ToString();
                    string dp_name = item["DP_NAME"].ToString();
                    item["MODIFIED"] = "N";

                    string query = $"UPDATE C2_TREND_TABLE_INFO_DETAIL SET {col_name} = :1 WHERE TABLE_NAME = :2";

                    cmd.CommandText = query;
                    cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dp_name;
                    cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = tb_name;

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
                Thread.Sleep(200);
                if(SockClient.sendMsg(system, $"restart;{txtTable.Text}") == false)
                {
                    throw new ApplicationException("Socket Error");
                }

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }



        }

        private void SaveMasterInfo()
        {
            if (!Database.Open()) return;
            string query = @"UPDATE C2_TREND_TABLE_INFO_MASTER SET EMPTY = :1, 
                             UPDATED_AT = SYSDATE WHERE TABLE_NAME = :2 ";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);

            var searched = dtMaster.AsEnumerable().Where(row => row.Field<string>("MODIFIED") == "M");

            try
            {
                foreach (DataRow item in searched)
                {
                    item["MODIFIED"] = "N";                   
                    string tb_name = item["TB_NAME"].ToString();
                    int empty = int.Parse(item["EMPTY"].ToString());

                    cmd.Parameters.Add(":1", OracleDbType.Int32).Value = empty;
                    cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = tb_name;                   

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }
        }

        private void SaveTrendInfo()
        {
            if (!Database.Open()) return;
            string query = @"UPDATE C2_TREND_INFO SET TB_NAME = :1, COL_NAME = :2, UPDATED_AT = NULL WHERE DP_NAME = :3 ";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);

            var searched = dtTrendInfo.AsEnumerable().Where(row => row.Field<string>("MODIFIED") == "M");

            try
            {
                foreach (DataRow item in searched)
                {
                    item["MODIFIED"] = "N";
                    string dp_name = item["DP_NAME"].ToString();
                    string tb_name = item["TB_NAME"].ToString();
                    string col_name = item["COL_NAME"].ToString();


                    cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = tb_name;
                    cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = col_name;
                    cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = dp_name;
                    cmd.ExecuteNonQuery();

                    //DP 이동내역 Insert                   
                     SaveTrendMoveHistory(dp_name, tb_name, col_name);

                    cmd.Parameters.Clear();
                }
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message, ex);
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }            
        }

        private void ClearMappingData()
        {
            var trend = dtTrendInfo.AsEnumerable()
                .Where(row => row.Field<string>("MODIFIED") == "M");
            foreach (var item in trend)
            {
                item["TB_NAME"] = "";
                item["COL_NAME"] = "";
                item["MODIFIED"] = "N";
            }

            var detail = dtDetail.AsEnumerable()
                .Where(row => row.Field<string>("MODIFIED") == "M");
            int count = 0;
            foreach (var item in detail)
            {
                item["DP_NAME"] = "";
                item["MODIFIED"] = "N";
                count++;
            }

            if(count != 0)
            {
                var master = dtMaster.AsEnumerable()
               .Where(row => row.Field<string>("MODIFIED") == "M");
                foreach (var item in master)
                {
                    int emptyCount = int.Parse(item["EMPTY"].ToString());
                    item["EMPTY"] = emptyCount + count;
                    item["MODIFIED"] = "N";
                }
            }


        }

        private int RemoveTable()
        {           
            if (dtDetail.Rows.Count < 1) return 0;

            //삭제한 갯수
            int removedCount = 0;

            Int32 selectedCellCount = dataGridView1.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {

                if (dataGridView1.AreAllCellsSelected(true))
                {
                    MessageBox.Show("All cells are selected", "Selected Cells");
                }
                else
                {
                    for (int i = selectedCellCount - 1; i >= 0; i--)
                    {
                        if (dataGridView1.SelectedCells[i].ColumnIndex.ToString() == "2")
                        {
                            int row = dataGridView1.SelectedCells[i].RowIndex;
                            string dpName = dataGridView1.SelectedCells[i].Value.ToString();
                            string tableName = dataGridView1[6, row].Value.ToString();

                            if (tableName == "") continue;
                            if (tableName != txtTable.Text) continue; // TrendInfo 에서 선택된 DP의 매핑테이블이 현재 작업 테이블과 다르면

                            
                            var searchedTrend = dtTrendInfo.AsEnumerable().Where(r => r.Field<string>("DP_NAME") == dpName);
                           
                            foreach (DataRow item in searchedTrend)
                            {
                                item["TB_NAME"] = "";
                                item["COL_NAME"] = "";

                                if (item["MODIFIED"].ToString() == "N")
                                {
                                    item["MODIFIED"] = "M";
                                    removedCount++;
                                }
                                else
                                    item["MODIFIED"] = "N";                                
                            }
                            
                            // Detail Table에서 DP 삭제
                            var searched = dtDetail
                                .AsEnumerable().Where(r => r.Field<string>("DP_NAME") == dpName);                           
                           
                            foreach (DataRow item in searched)
                            {
                                item["DP_NAME"] = "";

                                if(item["MODIFIED"].ToString() == "N")
                                    item["MODIFIED"] = "M";
                                else
                                    item["MODIFIED"] = "N";

                               
                            }

                            // Master Table의 empty 컬럼 업데이트
                            int emptyCount = 0;

                            var searchedDetail = dtDetail.AsEnumerable()
                                 .Where(r => r.Field<string>("DP_NAME") == "");
                            foreach (var item in searchedDetail)
                            {
                                emptyCount++;
                            }

                            var searchedMaster = dtMaster
                                .AsEnumerable().Where(r => r.Field<string>("TB_NAME") == txtTable.Text);
                            foreach (DataRow item in searchedMaster)
                            {
                                item["EMPTY"] = emptyCount.ToString();
                                item["MODIFIED"] = "M";
                            }

                        }
                    }
                }
            }

            return removedCount;
        }

        private int MappingDpToTable()
        {   
            if (dtDetail.Rows.Count < 1) return 0;

            //매핑된 총 갯수
            int mappingCount = 0;
                
            Int32 selectedCellCount =  dataGridView1.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {

                if (dataGridView1.AreAllCellsSelected(true))
                {
                    MessageBox.Show("Can not select all cells", "Selected Cells", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    for (int i = selectedCellCount - 1; i >= 0; i--)
                    {
                        if (dataGridView1.SelectedCells[i].ColumnIndex.ToString() == "2")
                        {
                            int row = dataGridView1.SelectedCells[i].RowIndex;
                            string dpName = dataGridView1.SelectedCells[i].Value.ToString();
                            string tableName = txtTable.Text;
                            string insertedTableName = dataGridView1[6, row].Value.ToString();

                            if (insertedTableName != "") continue;

                            foreach (DataRow item in dtDetail.Rows)
                            {
                                string detailTableName = item["TB_NAME"].ToString();
                                string detailDpName = item["DP_NAME"].ToString();
                                string detailColumnName = item["COL_NAME"].ToString();
                                if (detailTableName == tableName && detailDpName == "")
                                {
                                    item["DP_NAME"] = dpName;
                                    item["MODIFIED"] = "M";
                                    var searchedTrend = dtTrendInfo
                                        .AsEnumerable().Where(r => r.Field<string>("DP_NAME") == dpName);

                                    foreach (DataRow dtRow in searchedTrend)
                                    {
                                        dtRow["TB_NAME"] = tableName;
                                        dtRow["COL_NAME"] = detailColumnName;
                                        dtRow["MODIFIED"] = "M";
                                    }

                                    mappingCount++;
                                    break;
                                }
                            }                         
                        }
                    }
                }
            }


            int emptyCount = 0;

            var searchedDetail = dtDetail.AsEnumerable()
                 .Where(row => row.Field<string>("DP_NAME") == "");
            foreach (var item in searchedDetail)
            {
                emptyCount++;
            }

            var searchedMaster = dtMaster
                .AsEnumerable().Where(row => row.Field<string>("TB_NAME") == txtTable.Text);
            foreach (DataRow item in searchedMaster)
            {
                item["EMPTY"] = emptyCount.ToString();
                item["MODIFIED"] = "M";
            }

            return mappingCount;
        }

        private void SelectTrendInfo()
        {
            if (!Database.Open()) return;
            dtTrendInfo.Clear();

            splashScreenManager1.ShowWaitForm();

            string dpName = "%" + txtDpName.Text + "%";
            string dpDesc = "%" + txtDesc.Text + "%"; ;
            string tableName = "%" + txtTableName.Text + "%"; ;
            string system = cmbSystem.Text == "ALL" ? "%" : "%" + cmbSystem.Text + "%";


            string query = @"SELECT SYSTEM, 
                                    DP_NAME, 
                                    DP_DESC, 
                                    Y_MIN, 
                                    Y_MAX, 
                                    TB_NAME, 
                                    COL_NAME,
                                    UPDATED_AT
                             FROM C2_TREND_INFO
                             WHERE DP_NAME LIKE :1 
                             AND NVL(DP_DESC, '%') LIKE :2 
                             AND NVL(TB_NAME, '%') LIKE :3 
                             AND NVL(SYSTEM, '%') LIKE :4";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dpName;
            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dpDesc;
            cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = tableName;
            cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = system;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                int seq = 0;
                while (reader.Read())
                {
                    DataRow dr = dtTrendInfo.NewRow();
                    dr["SEQ"] = (seq + 1);
                    dr["SYSTEM"] = reader[0].ToString();
                    dr["DP_NAME"] = reader[1].ToString();
                    dr["DP_DESC"] = reader[2].ToString();
                    dr["Y_MIN"] = reader[3].ToString();
                    dr["Y_MAX"] = reader[4].ToString();
                    dr["TB_NAME"] = reader[5].ToString();
                    dr["COL_NAME"] = reader[6].ToString();

                    if (reader[7].ToString() == "")
                    {
                        dr["UPDATED"] = reader[7].ToString(); //column name
                    }
                    else
                    {
                        DateTime dt;
                        DateTime.TryParse(reader[7].ToString(), out dt);
                        dr["UPDATED"] = dt.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                   
                    dr["MODIFIED"] = "N";
                    dtTrendInfo.Rows.Add(dr);
                    dataGridView1.Rows[seq].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                    seq++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }

            
            AutoSettingDatagridView(dataGridView1, new List<int>() { 2, 3 }, new List<int>() { });
           

            if (system == "")
            {
                dtMaster.Clear();
                dtDetail.Clear();
            }

            if(dtMaster.Rows.Count > 0)
                dataGridView2.Rows[0].Selected = true;

             splashScreenManager1.CloseWaitForm();
        }
       

        private void FormPopupTrendInfo_Load(object sender, EventArgs e)
        {
            menu.ForeColor = Colors.buttonForeColor;         
         

            cmbSystem.Text = "ALL";
            InitDatatable.Init(dtTrendInfo);
            InitDatatable.Init(dtMaster);
            InitDatatable.Init(dtDetail);

            InitDataGridView.dataGridViewInit(dataGridView1);
            InitDataGridView.dataGridViewInit(dataGridView2);
            InitDataGridView.dataGridViewInit(dataGridView3);            

            dataGridView1.DataSource = dtTrendInfo;
            dataGridView2.DataSource = dtMaster;
            dataGridView3.DataSource = dtDetail;

            splashScreenManager1 = new SplashScreenManager(this, typeof(global::HIS.Forms.WaitForm1), true, true);
            splashScreenManager1.ClosingDelay = 500;

            DataGridViewBufferExtension();

            SetDoNotSort(dataGridView1);
            SetDoNotSort(dataGridView2);
            SetDoNotSort(dataGridView3);
        }

       
       

        private void SelectTrendInfoMaster(string system)
        {
            dtMaster.Clear();

            if (!Database.Open()) return;

            string query = @"SELECT TABLE_NAME,
                                        TABLE_DESC,
                                        SYSTEM,
                                        LOGGING_CYCLE,
                                        SAVING_GRADE,
                                        EMPTY
                                        FROM C2_TREND_TABLE_INFO_MASTER WHERE SYSTEM = :1
                                        ORDER BY TABLE_NAME";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = system;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                int row = 0;
                while (reader.Read())
                {
                    DataRow dr = dtMaster.NewRow();
                    dr["TB_NAME"] = reader[0].ToString();
                    dr["TB_DESC"] = reader[1].ToString();
                    dr["SYSTEM"] = reader[2].ToString();
                    dr["LOGGING"] = reader[3].ToString();
                    dr["SAVING"] = reader[4].ToString();
                    dr["EMPTY"] = reader[5].ToString();
                    dr["MODIFIED"] = "N";

                    dtMaster.Rows.Add(dr);
                    dataGridView2.Rows[row].DefaultCellStyle.BackColor = Color.FromArgb(60, 60, 60);
                    row++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }

            
            AutoSettingDatagridView(dataGridView2, new List<int>() { 1 }, new List<int>() { 6 });

            txtSystem.Text = system;

            if (dataGridView2.Rows.Count > 0)
            {
                string tableName = dataGridView2[0, 0].Value.ToString();

                if (tableName == "") return;
                SelectTrendTableDetail(tableName);
            }
        }

        private void SelectTrendTableDetail(string tableName)
        {
            if (!Database.Open()) return;

            dtDetail.Clear();
            txtTable.Text = tableName;       

            string query = @"SELECT TABLE_NAME, ";

            for (int i = 1; i <= 99; i++)
            {
                query += "COL_" + i.ToString("0000") + ", ";
            }
            query += "COL_0100 FROM C2_TREND_TABLE_INFO_DETAIL WHERE TABLE_NAME = :1 ";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = tableName;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                int row = 0;
                while (reader.Read())
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        DataRow dr = dtDetail.NewRow();
                        dr["TB_NAME"] = tableName;
                        dr["COL_NAME"] = "COL_" + i.ToString("0000");
                        dr["DP_NAME"] = reader[i].ToString();
                        dr["MODIFIED"] = "N";
                        dtDetail.Rows.Add(dr);
                        dataGridView3.Rows[row].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                        row++;
                        if (reader[i].ToString() != "")
                            emptyColumnNumbers.Add(i); // 비어있는 컬럼 번호
                    }

                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }

           
            AutoSettingDatagridView(dataGridView3, new List<int>() { 2 }, new List<int>() {0,3});
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string system = dataGridView2[0, e.RowIndex].Value.ToString();
            txtTable.Text = system;
            SelectTrendTableDetail(txtTable.Text);
        }    


        private void UpdateTableMaster(string table, string count)
        {
            var searchedDataTable = dtMaster
                    .AsEnumerable()
                    .Where(row => row.Field<string>("TABLE_NAME") == table);

            foreach (DataRow dtRow in searchedDataTable)
            {
                dtRow["EMPTY"] = count;
            }

        }


        private void UpdateTableDetail(string dp, string table, string column)
        {
            var searchedDataTable = dtDetail
                    .AsEnumerable()
                    .Where(row => row.Field<string>("TABLE_NAME") == table);

            foreach (DataRow dtRow in searchedDataTable)
            {
                dtRow[column] = dp;
                dtRow["MODIFIED"] = "";
            }

        }

        private void AutoSettingDatagridView(DataGridView dgv, List<int> fillColumns, List<int> hideColumns)
        {
            InitDataGridView.dataGridViewInit(dgv);

            for(int i=0; i<dgv.ColumnCount; i++)
            {
                dgv.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            }

            if (fillColumns.Count > 0)
            {
                foreach (int item in fillColumns)
                {
                    dgv.Columns[item].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            if(hideColumns.Count > 0)
            {
                foreach (int item in hideColumns)
                {
                    dgv.Columns[item].Visible = false;
                }
            }
        }

        private void MenuButtonClicked(string buttonName)
        {
            if (buttonName == "Select")
            {
                menu.Buttons[0].Properties.Enabled = true;
                menu.Buttons[1].Properties.Enabled = true;
                menu.Buttons[2].Properties.Enabled = false;
                menu.Buttons[3].Properties.Enabled = true;
                menu.Buttons[4].Properties.Enabled = false;
                dataGridView2.Enabled = true;
                clickedButtonName = "Select";
            }
            else if (buttonName == "Mapping")
            {
                menu.Buttons[0].Properties.Enabled = false;
                menu.Buttons[1].Properties.Enabled = false;
                menu.Buttons[2].Properties.Enabled = true;
                menu.Buttons[3].Properties.Enabled = false;
                menu.Buttons[4].Properties.Enabled = true;
                dataGridView2.Enabled = false;
                clickedButtonName = "Mapping";
            }
            else if (buttonName == "Clear")
            {
                menu.Buttons[0].Properties.Enabled = true;
                menu.Buttons[1].Properties.Enabled = true;
                menu.Buttons[2].Properties.Enabled = false;
                menu.Buttons[3].Properties.Enabled = true;
                menu.Buttons[4].Properties.Enabled = true;
                dataGridView2.Enabled = true;
                clickedButtonName = "Clear";
            }
            else if (buttonName == "Remove")
            {
                menu.Buttons[0].Properties.Enabled = false;
                menu.Buttons[1].Properties.Enabled = false;
                menu.Buttons[2].Properties.Enabled = false;
                menu.Buttons[3].Properties.Enabled = false;
                menu.Buttons[4].Properties.Enabled = true;
                dataGridView2.Enabled = false;
                clickedButtonName = "Remove";
            }
            else if (buttonName == "Save")
            {
                menu.Buttons[0].Properties.Enabled = true;
                menu.Buttons[1].Properties.Enabled = true;
                menu.Buttons[2].Properties.Enabled = true;
                menu.Buttons[3].Properties.Enabled = true;
                menu.Buttons[4].Properties.Enabled = false;
                dataGridView2.Enabled = true;
                clickedButtonName = "Save";
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
