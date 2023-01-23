using DevExpress.XtraSplashScreen;
using HIS.Class;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace HIS.Forms
{
    public partial class FormOperation : Form
    {
        DataTable dtOperationHistory;
        private event EventHandler<int[]> ProgressBarEvent;
        SplashScreenManager splashScreenManager1;

        public FormOperation()
        {
            InitializeComponent();
            Load += FormOperation_Load;

            this.FormClosed += (sender, e) =>
            {
                Load -= FormOperation_Load;
            };
        }

        private void FormOperation_Load(object sender, EventArgs e)
        {
            //menu 초기화
            menu.ForeColor = Colors.buttonForeColor;
            menu.ButtonClick += Menu_ButtonClick;

            this.splashScreenManager1 = new SplashScreenManager(this, typeof(global::HIS.Forms.WaitForm1), true, true);
            this.splashScreenManager1.ClosingDelay = 500;

            //DataTable 정의 및 초기화
            dtOperationHistory = new DataTable("OperationHistory");
            InitDatatable.Init(dtOperationHistory);

            // dataGridView1 초기화
            InitDataGridView.dataGridViewInit(dataGridView1);            

            //DataGridView 버퍼 늘리기
            DataGridViewBufferExtension();

            //dataGridView1과 dtOperationHistory 연결
            dataGridView1.DataSource = dtOperationHistory;

            //ProgressBarr  이벤트 연결
            ProgressBarEvent += FormOperation_ProgressBarEvent;

            startDt.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");

            //sorting 막기
            SetDoNotSort(dataGridView1);
        }

        private void FormOperation_ProgressBarEvent(object sender, int[] e)
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
                lblNowCount.Text = e[0].ToString("#,##0");
                lblTotalCount.Text = e[1].ToString("#,##0");
                progressBar1.Value = e[0];
                if (e[0] == e[1]) panelProgress.Visible = false;

            }
        }

        private void Menu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;

            switch(buttonName)
            {
                case "Search":
                    SearchOperationHistory();
                    break;
                case "Export":
                    ExportAlarmHistory();
                    break;
            }
        }

        public void SearchOperationHistory()
        {
            dtOperationHistory.Clear();

            splashScreenManager1.ShowWaitForm();

            DateTime start = DateTime.Parse(startDt.Text);
            DateTime end = DateTime.Parse(endDt.Text);

            string user = "%" + txtUser.Text + "%";
            string dp = "%" + txtDp.Text + "%";
            string desc = "%" + txtDesc.Text + "%";
            string ele = "%" + txtEle.Text + "%";
            string eleDesc = "%" + txtEleDesc.Text + "%";

            Cursor.Current = Cursors.WaitCursor;

            if (!Database.Open()) return;


            string query = "SELECT TO_CHAR(INSERT_TIME, 'YYYY.MM.DD HH24:MI:SS') AS INSERT_TIME, ACTION, DP, DP_DESC, ELE, ELE_DESC, PRE_VAL, CUR_VAL, ";
            query += " MODULE_NM, OBJECT_NM, INSERT_USER_ID FROM HMI_HIST_HANDLE ";
            query += " WHERE INSERT_TIME BETWEEN :1 AND :2 ";
            query += " AND NVL(DP, '%') LIKE :3 AND NVL(INSERT_USER_ID, '%') LIKE :4 AND NVL(DP_DESC, '%') LIKE :5 ";
            query += " AND NVL(ELE,'%') LIKE :6 AND NVL(ELE_DESC,'%') LIKE :7 ORDER BY INSERT_TIME DESC";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);           
            cmd.Parameters.Add(":1", OracleDbType.Date).Value = start;
            cmd.Parameters.Add(":2", OracleDbType.Date).Value = end;
            cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = dp;
            cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = user;
            cmd.Parameters.Add(":5", OracleDbType.Varchar2).Value = desc;
            cmd.Parameters.Add(":6", OracleDbType.Varchar2).Value = ele;
            cmd.Parameters.Add(":7", OracleDbType.Varchar2).Value = eleDesc;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();

                int totalCount = 0;
                while (reader.Read())
                {
                    totalCount++;
                    DataRow row = dtOperationHistory.NewRow();
                    //row["SEQ"] = totalCount; 
                    row["INSERT_TIME"]    = reader[0].ToString();
                    row["ACTION"] = reader[1].ToString();
                    row["DP"] = reader[2].ToString();
                    row["DP_DESC"] = reader[3].ToString();
                    row["ELE"] = reader[4].ToString();
                    row["ELE_DESC"] = reader[5].ToString();
                    row["PRE_VAL"] = reader[6].ToString();
                    row["CUR_VAL"] = reader[7].ToString();
                    row["MODULE"] = reader[8].ToString();
                    row["OBJECT"] = reader[9].ToString();
                    row["USER_ID"] = reader[10].ToString();

                    dtOperationHistory.Rows.Add(row);

                    dataGridView1.Rows[totalCount-1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                    // dataGridView1.Rows[totalCount - 1].DefaultCellStyle.ForeColor = Color.White;
                }               

                foreach (DataRow item in dtOperationHistory.Rows)
                {
                    item["SEQ"] = totalCount--;
                }

                InitDataGridView.AutoSettingDatagridView(dataGridView1, new List<int>() { 3, 4 }, new List<int>());
                dataGridView1.Columns["SEQ"].Width = 50;
                dataGridView1.Columns["PRE_VAL"].Width = 100;
                dataGridView1.Columns["PRE_VAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["CUR_VAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["USER_ID"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
                splashScreenManager1.CloseWaitForm();
            }
        }

        private void ExportAlarmHistory()
        {
            if (dtOperationHistory.Rows.Count < 1) return;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveDialog.FilterIndex = 1;

            DataTable selectedDt = dtOperationHistory.AsEnumerable()
                .Where(row => row.Field<string>("INSERT_TIME") != "").CopyToDataTable();

            Excel ex = new Excel();
            ex.ExportEvent += delegate (object sender, int[] e)
            {
                if (InvokeRequired)
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
                var t = new Thread(() => ex.ExportToExcel(selectedDt, saveDialog.FileName, "Alarm History"));
                t.Start();
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
