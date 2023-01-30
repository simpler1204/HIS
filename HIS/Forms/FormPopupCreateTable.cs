using HIS.Class;
using HIS.Popup;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HIS.Forms
{
    public partial class FormPopupCreateTable : Form
    {
        public static FormPopupCreateTable createdForm = null;

        //한번 select후 데이터 보관
        private DataTable dtTableList = new DataTable("TableList");
        
        public FormPopupCreateTable()
        {
            InitializeComponent();
            createdForm = this;

            //Logging Cycle rdo button event
            this.rdo1Sec.Click += Rdo_Click_logging;
            this.rdo3Sec.Click += Rdo_Click_logging;
            this.rdo10Sec.Click += Rdo_Click_logging;
            this.rdo1Min.Click += Rdo_Click_logging;

            //saving peroid radio button event
            this.rdoA.Click += Rdo_Click_saving;
            this.rdoB.Click += Rdo_Click_saving;
            this.rdoC.Click += Rdo_Click_saving;

            this.FormClosing += (sender, e) =>
            {
                this.rdo1Sec.Click -= Rdo_Click_logging;
                this.rdo3Sec.Click -= Rdo_Click_logging;
                this.rdo10Sec.Click -= Rdo_Click_logging;
                this.rdo1Min.Click -= Rdo_Click_logging;
                this.rdoA.Click -= Rdo_Click_saving;
                this.rdoB.Click -= Rdo_Click_saving;
                this.rdoC.Click -= Rdo_Click_saving;
            };

            
        }

        private void Rdo_Click_logging(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 1) return;
            int row = dataGridView1.CurrentCell.RowIndex;
            string tableName = dataGridView1[1, row].Value.ToString();           
            string logging = string.Empty;
            DataRow selectdDtRow = null;

            foreach (DataRow dtRow in dtTableList.Rows)
            {
                if (dtRow["TABLE_NAME"].ToString() == tableName)
                {
                    logging = dtRow["LOGGING_CYCLE"].ToString();
                    selectdDtRow = dtRow;
                }
            }

            RadioButton rdo = (RadioButton)(sender);

            if (logging != rdo.Text)
            {
                DialogResult result = MessageBox.Show("Do you really want to change logging cycle?",
                   "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    if (logging == "1SEC") rdo1Sec.Checked = true;
                    if (logging == "3SEC") rdo3Sec.Checked = true;
                    if (logging == "10SEC") rdo10Sec.Checked = true;
                    if (logging == "1MIN") rdo1Min.Checked = true;

                    return;
                }

                if (selectdDtRow != null)
                {
                    selectdDtRow["LOGGING_CYCLE"] = rdo.Text;
                    selectdDtRow["MODIFIED"] = "U";
                    dataGridView1[4, row].Value = rdo.Text;
                }

            }

        }

        private void Rdo_Click_saving(object sender, EventArgs e)
        {
            int row = dataGridView1.CurrentCell.RowIndex;
            string tableName = dataGridView1[1, row].Value.ToString();           
            string saving = string.Empty;
            DataRow selectdDtRow = null;

            foreach (DataRow dtRow in dtTableList.Rows)
            {
                if (dtRow["TABLE_NAME"].ToString() == tableName)
                {
                    saving = dtRow["SAVING_PERIOD"].ToString();
                    selectdDtRow = dtRow;
                }
            }

            RadioButton rdo = (RadioButton)(sender);

           

            if (saving != rdo.Text.Substring(0,1))
            {
                DialogResult result = MessageBox.Show("Do you really want to change logging cycle?",
                   "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.No)
                {
                    if (saving == "A") rdoA.Checked = true;
                    if (saving == "B") rdoB.Checked = true;
                    if (saving == "C") rdoC.Checked = true;
                  

                    return;
                }

                selectdDtRow["SAVING_PERIOD"] = rdo.Text.Substring(0,1);
                selectdDtRow["MODIFIED"] = "U";
                dataGridView1[5, row].Value = rdo.Text.Substring(0,1);

            }
        }

        private void FormCreateTable_Load(object sender, EventArgs e)
        {
            //버튼 컬러
            tableMenu.ForeColor = Colors.buttonForeColor;
            searchMenu.ForeColor = Colors.buttonForeColor;
            detailMenu.ForeColor = Colors.buttonForeColor;

            //grid 
            InitDataGridView.dataGridViewInit(dataGridView1);

            //DataTable init
            InitDatatable.Init(dtTableList);

            //테이블 리스트 가져오기
            GetTablesList();

            DataGridViewBufferExtension();

            //sorting 막기
            SetDoNotSort(dataGridView1);
        }

        private void GetTablesList()
        {
            string query = string.Empty;
            if(!Database.Open()) return;

            query = "SELECT TABLE_NAME, TABLE_DESC, SYSTEM, LOGGING_CYCLE, SAVING_GRADE ";
            query += "FROM C2_TREND_TABLE_INFO_MASTER ORDER BY TABLE_NAME ASC ";            

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);

            try
            {               
                OracleDataReader reader = cmd.ExecuteReader();
                int row = 0;
                if (reader.HasRows == false) return;
                while (reader.Read())
                {
                    List<string> tableData = new List<string>();
                    tableData.Add(reader[0].ToString());
                    tableData.Add(reader[1].ToString());
                    tableData.Add(reader[2].ToString());
                    tableData.Add(reader[3].ToString());
                    tableData.Add(reader[4].ToString());                                       

                    InsertDataGridView(tableData, true);
                    dataGridView1.Rows[row].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                    row++;
                }

                InitDataGridView.AutoSettingDatagridView(dataGridView1, new List<int>() { 1, 2 }, new List<int>());
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - Fail to select table list", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }
          
            GetCurrentTableValue();

        }

    

        private void FormCreateTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            createdForm = null;
        }

        private void FormCreateTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            createdForm = null;
        }

        private void tableMenu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string menuName = e.Button.Properties.Caption;

            if(menuName == "Create")
            {
                PopUpCreateOneTable form = PopUpCreateOneTable.createdForm == null ? new PopUpCreateOneTable() : PopUpCreateOneTable.createdForm;
               
                form.CreateTrendEvent += InitializeTable;
                form.ShowDialog();
            }
            else if(menuName == "Remove")
            {
                if (dataGridView1.Rows.Count < 1) return;

                int row = dataGridView1.CurrentCell.RowIndex;
                string tableName = dataGridView1[1, row].Value.ToString();

                DialogResult result = MessageBox.Show("Do you really want to delete " + tableName + "?\n" +
                                    "All data are going to deleted..", "Warning", MessageBoxButtons.YesNo, 
                                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                if (result == DialogResult.No) return;

                try
                {
                    //해당 테이블에 로깅 데이타가 있는지 확인, 있으면  삭제 못함
                    DropTrendTable(tableName);

                    //C2_TREND_TABLE_INFO_MASTER에서 해당 테이블 이름 삭제
                    FailInsertTrendInfoMaster(tableName);

                    //C2_TREND_TABLE_INFO_DEATIL에서 해당 테이블 이름 삭제
                    FailInsertTrendInfoDetail(tableName);
                   
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error - Fail to delete logging table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    Database.Close();
                }

                dataGridView1.Rows.RemoveAt(row);
                
                foreach(DataRow dtRow in dtTableList.Rows)
                {
                    if(dtRow["TABLE_NAME"].ToString() == tableName)
                    {
                        dtRow["MODIFIED"] = "D";
                    }
                }

                GetCurrentTableValue();
            }
        }

        private void DropTrendTable(string tableName)
        {
            if (!Database.Open()) return;

            string selectQuery = "SELECT COUNT(SYSTEM) FROM " + tableName;
            string dropQuery = "DROP TABLE " + tableName;           
            int selectedCount = 0;

            try
            {
                OracleCommand cmd1 = new OracleCommand(selectQuery, Database.OracleConn);
                OracleDataReader reader = cmd1.ExecuteReader();
                while (reader.Read())
                {
                    int.TryParse(reader[0].ToString(), out selectedCount);
                }

                if (selectedCount > 0)
                {                   
                    throw new ApplicationException($"{tableName} Have logging data, so you can't delete it!!");                    
                }

                // 해당 테이블 Drop

                OracleCommand cmd2 = new OracleCommand(dropQuery, Database.OracleConn);
                cmd2.ExecuteNonQuery();

                cmd1.Dispose();
                cmd2.Dispose();
            }
            catch(Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            finally
            {
                Database.Close();
            }
        }

        private void InitializeTable(string[] val)
        {
            string tableName = val[0];
            string tableDesc = val[1];
            string system = val[2];
            string loggingCycle = val[3];
            string savingPeroid = val[4];
            string query = string.Empty;

            //Trend Info Master 인서트
            if (!InsertTrendTableInfoMaster(val))
            {
                return;
            }

            //Trend Info Detail 인서트
            if (!InsertTrendInfoDetail(tableName))
            {
                FailInsertTrendInfoMaster(tableName);
            }

            //Trend Table 생성
            if(!CreateTrendTable(tableName))
            {
                FailInsertTrendInfoMaster(tableName);
                FailInsertTrendInfoDetail(tableName);
            }
            

            //오른쪽 Detail정보 표시
            List<string> tableData = new List<string>();
            tableData.Add(tableName);
            tableData.Add(tableDesc);
            tableData.Add(system);
            tableData.Add(loggingCycle);
            tableData.Add(savingPeroid);
            tableData.Add(""); //CreatedAt
            tableData.Add(""); //UpdatedAt

            InsertDataGridView(tableData, false);

            //우측 detail 화면 표시
            txtTableName.Text = tableName;
            txtTableDesc.Text = tableDesc;
            cmbSystem.Text = system;
            txtCreatedAt.Text = "";
            txtUpdatedAt.Text = "";

            if (loggingCycle == "1SEC") rdo1Sec.Checked = true;
            if (loggingCycle == "3SEC") rdo3Sec.Checked = true;
            if (loggingCycle == "10SEC") rdo10Sec.Checked = true;
            if (loggingCycle == "1MIN") rdo1Min.Checked = true;

            if (savingPeroid == "A") rdoA.Checked = true;
            if (savingPeroid == "B") rdoB.Checked = true;
            if (savingPeroid == "C") rdoC.Checked = true;

        }

        private void FailInsertTrendInfoDetail(string tableName)
        {
            if (!Database.Open()) return;
            string query;
            query = "DELETE C2_TREND_TABLE_INFO_DETAIL WHERE TABLE_NAME = :1 ";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);

            try
            {                
                cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = tableName;
                cmd.ExecuteNonQuery();               
            }
            catch
            {
                throw new ApplicationException($"Fail to delete {tableName}");
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }

            return;
        }

        private void FailInsertTrendInfoMaster(string tableName)
        {
            if (!Database.Open()) return;
            string query;
            query = "DELETE C2_TREND_TABLE_INFO_MASTER WHERE TABLE_NAME = :1 ";

            try
            {
                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = tableName;
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            catch
            {
                throw new ApplicationException($"Fail to delete {tableName}");
            }
            finally
            {
                Database.Close();
            }

            return;

        }

        private void searchMenu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;
            string name = "%" + txtSearchName.Text + "%";
            string desc = "%" + txtSearchDesc.Text + "%";

            string logging = cmbSearchLogging.Text == "ALL" ? "%" : "%" + cmbSearchLogging.Text + "%";
            string saving = cmbSavingPeriod.Text == "ALL" ? "%" : "%" + cmbSavingPeriod.Text + "%";
            string system = cmbSearchSystem.Text == "ALL" ? "%" : "%" + cmbSearchSystem.Text + "%";

            if (buttonName == "Search")
            {
                if(!Database.Open()) return;

                dataGridView1.Rows.Clear();
                dtTableList.Rows.Clear();

                string query;
                query = "SELECT TABLE_NAME, TABLE_DESC, SYSTEM, LOGGING_CYCLE, SAVING_GRADE, CREATED_AT, UPDATED_AT ";
                query += "FROM C2_TREND_TABLE_INFO_MASTER WHERE TABLE_NAME LIKE :1 AND NVL(TABLE_DESC, '%') LIKE :2 ";
                query += "AND SYSTEM LIKE :3 AND LOGGING_CYCLE LIKE :4 AND SAVING_GRADE LIKE :5";
                query += "ORDER BY CREATED_AT ASC";

                try
                {
                    OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                    cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = name;
                    cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = desc;
                    cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = system;
                    cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = logging;
                    cmd.Parameters.Add(":5", OracleDbType.Varchar2).Value = saving;

                    OracleDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        List<string> tableData = new List<string>();
                        tableData.Add(reader[0].ToString());
                        tableData.Add(reader[1].ToString());
                        tableData.Add(reader[2].ToString());
                        tableData.Add(reader[3].ToString());
                        tableData.Add(reader[4].ToString());

                        if (reader[5].ToString() != "")
                            tableData.Add(DateTime.Parse(reader[5].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                        else
                            tableData.Add("");

                        if (reader[6].ToString() != "")
                            tableData.Add(DateTime.Parse(reader[6].ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                        else
                            tableData.Add("");

                        InsertDataGridView(tableData, true);
                    }                    
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MessageBox.Show(ex.Message, "Error - Fail to select searching result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {                   
                    Database.Close();
                }

                GetCurrentTableValue();
            }
        }



       

        private void GetTableData(DataTable dt, string tableName)
        {
            foreach(DataRow row in dt.Rows)
            {
                if(row["TABLE_NAME"].ToString() == tableName)
                {
                    txtTableName.Text = row["TABLE_NAME"].ToString();
                    txtTableDesc.Text = row["TABLE_DESC"].ToString();
                    //txtCreatedAt.Text = row["CREATED_AT"].ToString();
                    cmbSystem.Text = row["SYSTEM"].ToString();
                    //txtUpdatedAt.Text = row["UPDATED_AT"].ToString();

                    if (row["LOGGING_CYCLE"].ToString() == "1SEC") rdo1Sec.Checked = true;
                    if (row["LOGGING_CYCLE"].ToString() == "3SEC") rdo3Sec.Checked = true;
                    if (row["LOGGING_CYCLE"].ToString() == "10SEC") rdo10Sec.Checked = true;
                    if (row["LOGGING_CYCLE"].ToString() == "1MIN") rdo1Min.Checked = true;

                    if (row["SAVING_PERIOD"].ToString() == "A") rdoA.Checked = true;
                    if (row["SAVING_PERIOD"].ToString() == "B") rdoB.Checked = true;
                    if (row["SAVING_PERIOD"].ToString() == "C") rdoC.Checked = true;
                }
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            grb_logging.Enabled = true;
            grb_saving.Enabled = true;

            int row = e.RowIndex;           
            if (dataGridView1[1, row].Value == null) return;
            string tableName = dataGridView1[1, row].Value.ToString();
            GetTableData(dtTableList, tableName);           
        }

        private void GetCurrentTableValue()
        {
            if (dataGridView1.Rows.Count > 0)
            {
                int rowIndex = dataGridView1.CurrentCell.RowIndex;
                string tableName = dataGridView1[1, rowIndex].Value.ToString();
                GetTableData(dtTableList, tableName);
            }
        }

        private void InsertDataGridView(List<string> data, bool first)
        {         
            int row = dataGridView1.Rows.Add();
            dataGridView1[0, row].Value = (row+1).ToString();
            dataGridView1[1, row].Value = data[0];
            dataGridView1[2, row].Value = data[1];
            dataGridView1[3, row].Value = data[2];
            dataGridView1[4, row].Value = data[3];
            dataGridView1[5, row].Value = data[4]; 

            dtTableList.Rows.Add(data[0], data[1], data[2], data[3], data[4]);
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;

            if(first == true)
                dataGridView1.Rows[0].Selected = true;
            else
                dataGridView1.Rows[row].Selected = true;
        }

        private void detailMenu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string name = e.Button.Properties.Caption;
            if(name == "Save")
            {
                if (dtTableList.Rows.Count < 1) return;

                //DialogResult result = MessageBox.Show("Do you want to update table?", "Warning",
                //    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

                //if (result == DialogResult.No) return;

                string query = "UPDATE C2_TREND_TABLE_INFO_MASTER SET LOGGING_CYCLE = :1, SAVING_GRADE = :2, UPDATED_AT = SYSDATE ";
                query += "WHERE TABLE_NAME = :3";

                int count = 0;
                if(!Database.Open()) return;
                foreach (DataRow row in dtTableList.Rows)
                {
                    if(row["MODIFIED"].ToString() == "U")
                    {
                        string tableName = row["TABLE_NAME"].ToString();
                        string logging = row["LOGGING_CYCLE"].ToString();
                        string saving = row["SAVING_PERIOD"].ToString();

                        OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = logging;
                        cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = saving;
                        cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = tableName;
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        row["MODIFIED"] = "";

                        count++;
                    }
                }
                Database.Close();

                if(count>0)
                {
                    MessageBox.Show("Updating completed", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
            else if(name == "Restart")
            {
                string tableName = txtTableName.Text;
                if (tableName == "") return;
                string sendMsg = $"restart;{tableName}";
                if(SockClient.sendMsg(cmbSystem.Text, sendMsg) == true)
                {
                    MessageBox.Show($"Sucess to restart {txtTableName.Text}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show($"Fali to restart {txtTableName.Text}", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private bool InsertTrendTableInfoMaster(string[] val)
        {
            string tableName = val[0];
            string tableDesc = val[1];
            string system = val[2];
            string loggingCycle = val[3];
            string savingPeroid = val[4];
            string query = string.Empty;
            int result = 0;

            if (!Database.Open()) return false;

            // C2_TREND_TABLE_INFO_MASTER Insert..
            query = "INSERT INTO C2_TREND_TABLE_INFO_MASTER(TABLE_NAME, TABLE_DESC, LOGGING_CYCLE, SAVING_GRADE, CREATED_AT, SYSTEM) ";
            query += "VALUES(:1, :2, :3, :4, SYSDATE, :5)";

            DateTime now = DateTime.Now;

            OracleCommand cmd1 = new OracleCommand(query, Database.OracleConn);
            cmd1.Parameters.Add(":1", OracleDbType.Varchar2).Value = tableName;
            cmd1.Parameters.Add(":2", OracleDbType.Varchar2).Value = tableDesc;
            cmd1.Parameters.Add(":3", OracleDbType.Varchar2).Value = loggingCycle;
            cmd1.Parameters.Add(":4", OracleDbType.Varchar2).Value = savingPeroid;
            cmd1.Parameters.Add(":5", OracleDbType.Varchar2).Value = system;

            try
            {
                result = cmd1.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - Fail to insert table info", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Database.Close();
                return false;
            }
            finally
            {
                cmd1.Dispose();
            }

            if (result == -1)
                return false;
            else
                return true;
        }

        private bool InsertTrendInfoDetail(string tableName)
        {
            if (!Database.Open()) return false;

            string query;
            query = "INSERT INTO C2_TREND_TABLE_INFO_DETAIL(TABLE_NAME) ";
            query += "VALUES(:1)";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = tableName;

            int result = cmd.ExecuteNonQuery();

            if (result == -1)
                return false;
            else
                return true;
        }

        private bool CreateTrendTable(string tableName)
        {
            if(!Database.Open()) return false;

            string query;

            query = "";
            query += "CREATE TABLE " + tableName + "( ";
            query += "SYSTEM VARCHAR2(30), INSERT_TIME TIMESTAMP(3) PRIMARY KEY, VAL_0001 NUMERIC(20, 6), ";
            query += "VAL_0002 NUMERIC(20, 6), VAL_0003 NUMERIC(20, 6), VAL_0004 NUMERIC(20, 6), VAL_0005 NUMERIC(20, 6), ";
            query += "VAL_0006 NUMERIC(20, 6), VAL_0007 NUMERIC(20, 6), VAL_0008 NUMERIC(20, 6), VAL_0009 NUMERIC(20, 6), ";
            query += "VAL_0010 NUMERIC(20, 6), VAL_0011 NUMERIC(20, 6), VAL_0012 NUMERIC(20, 6), VAL_0013 NUMERIC(20, 6), ";
            query += "VAL_0014 NUMERIC(20, 6), VAL_0015 NUMERIC(20, 6), VAL_0016 NUMERIC(20, 6), VAL_0017 NUMERIC(20, 6), ";
            query += "VAL_0018 NUMERIC(20, 6), VAL_0019 NUMERIC(20, 6), VAL_0020 NUMERIC(20, 6), VAL_0021 NUMERIC(20, 6), ";
            query += "VAL_0022 NUMERIC(20, 6), VAL_0023 NUMERIC(20, 6), VAL_0024 NUMERIC(20, 6), VAL_0025 NUMERIC(20, 6), ";
            query += "VAL_0026 NUMERIC(20, 6), VAL_0027 NUMERIC(20, 6), VAL_0028 NUMERIC(20, 6), VAL_0029 NUMERIC(20, 6), ";
            query += "VAL_0030 NUMERIC(20, 6), VAL_0031 NUMERIC(20, 6), VAL_0032 NUMERIC(20, 6), VAL_0033 NUMERIC(20, 6), ";
            query += "VAL_0034 NUMERIC(20, 6), VAL_0035 NUMERIC(20, 6), VAL_0036 NUMERIC(20, 6), VAL_0037 NUMERIC(20, 6), ";
            query += "VAL_0038 NUMERIC(20, 6), VAL_0039 NUMERIC(20, 6), VAL_0040 NUMERIC(20, 6), VAL_0041 NUMERIC(20, 6), ";
            query += "VAL_0042 NUMERIC(20, 6), VAL_0043 NUMERIC(20, 6), VAL_0044 NUMERIC(20, 6), VAL_0045 NUMERIC(20, 6), ";
            query += "VAL_0046 NUMERIC(20, 6), VAL_0047 NUMERIC(20, 6), VAL_0048 NUMERIC(20, 6), VAL_0049 NUMERIC(20, 6), ";
            query += "VAL_0050 NUMERIC(20, 6), VAL_0051 NUMERIC(20, 6), VAL_0052 NUMERIC(20, 6), VAL_0053 NUMERIC(20, 6), ";
            query += "VAL_0054 NUMERIC(20, 6), VAL_0055 NUMERIC(20, 6), VAL_0056 NUMERIC(20, 6), VAL_0057 NUMERIC(20, 6), ";
            query += "VAL_0058 NUMERIC(20, 6), VAL_0059 NUMERIC(20, 6), VAL_0060 NUMERIC(20, 6), VAL_0061 NUMERIC(20, 6), ";
            query += "VAL_0062 NUMERIC(20, 6), VAL_0063 NUMERIC(20, 6), VAL_0064 NUMERIC(20, 6), VAL_0065 NUMERIC(20, 6), ";
            query += "VAL_0066 NUMERIC(20, 6), VAL_0067 NUMERIC(20, 6), VAL_0068 NUMERIC(20, 6), VAL_0069 NUMERIC(20, 6), ";
            query += "VAL_0070 NUMERIC(20, 6), VAL_0071 NUMERIC(20, 6), VAL_0072 NUMERIC(20, 6), VAL_0073 NUMERIC(20, 6), ";
            query += "VAL_0074 NUMERIC(20, 6), VAL_0075 NUMERIC(20, 6), VAL_0076 NUMERIC(20, 6), VAL_0077 NUMERIC(20, 6), ";
            query += "VAL_0078 NUMERIC(20, 6), VAL_0079 NUMERIC(20, 6), VAL_0080 NUMERIC(20, 6), VAL_0081 NUMERIC(20, 6), ";
            query += "VAL_0082 NUMERIC(20, 6), VAL_0083 NUMERIC(20, 6), VAL_0084 NUMERIC(20, 6), VAL_0085 NUMERIC(20, 6), ";
            query += "VAL_0086 NUMERIC(20, 6), VAL_0087 NUMERIC(20, 6), VAL_0088 NUMERIC(20, 6), VAL_0089 NUMERIC(20, 6), ";
            query += "VAL_0090 NUMERIC(20, 6), VAL_0091 NUMERIC(20, 6), VAL_0092 NUMERIC(20, 6), VAL_0093 NUMERIC(20, 6), ";
            query += "VAL_0094 NUMERIC(20, 6), VAL_0095 NUMERIC(20, 6), VAL_0096 NUMERIC(20, 6), VAL_0097 NUMERIC(20, 6), ";
            query += "VAL_0098 NUMERIC(20, 6), VAL_0099 NUMERIC(20, 6), VAL_0100 NUMERIC(20, 6) )";


            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error - Fail to create table", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }

            return true;
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
