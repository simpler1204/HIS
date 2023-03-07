using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using HIS.Class;
using System.Threading;

namespace HIS.Forms
{
    public partial class FormSmsHIST : Form
    {
        private DataTable _dtSmsHist = new DataTable("SMS_HIST");
        private event EventHandler<int[]> ProgressBarEvent;

        public FormSmsHIST()
        {
            InitializeComponent();
            Database.CreateDatabase();
            this.Load += FormSmsHIST_Load;
            btnMenu.ButtonClick += BtnMenu_ButtonClick;
            ProgressBarEvent += FormPopupTrendInfo_ProgressBarEvent;
            this.FormClosed += FormSmsHIST_FormClosed;
        }

        private void FormSmsHIST_FormClosed(object sender, FormClosedEventArgs e)
        {
            ProgressBarEvent -= FormPopupTrendInfo_ProgressBarEvent;
        }

        private void BtnMenu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string btnName = e.Button.Properties.Caption;
            
            switch(btnName)
            {
                case "Search":
                    GetHistory();
                    break;
                case "Export":
                    ExportSmsHistory();
                    break;
            }
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
                panelProgress.Location = new Point((dgHist.Width - panelProgress.Width) / 2, (dgHist.Height - panelProgress.Height) / 2);
                lblNowCount.Text = e[0].ToString("#,##0");
                lblTotalCount.Text = e[1].ToString("#,##0");
                progressBar1.Value = e[0];
                if (e[0] == e[1]) panelProgress.Visible = false;

            }
        }

        private void FormSmsHIST_Load(object sender, EventArgs e)
        {
            startDt.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");
            endDt.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            InitDatatable.Init(_dtSmsHist);
            dgHist.DataSource = _dtSmsHist;
            DecorateDataGridView(dgHist);

            cmbSuccess.Text = "ALL";

           
        }

        private void GetHistory()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dtSmsHist.Rows.Clear();

            DateTime start = DateTime.Parse(startDt.Text);
            DateTime end = DateTime.Parse(endDt.Text);
            string name = txtUser.Text == "" ? "%" : "%" + txtUser.Text + "%";
            string success = cmbSuccess.Text == "ALL" ? "%"  : "%" + cmbSuccess.Text + "%";
            string dsnt = cmbDsnt.Text == "ALL" ? "%" : "%" + cmbDsnt.Text + "%";

            string query = @"SELECT TO_CHAR(SENT_TIME, 'YYYY.MM.DD HH24:MI:SS.FF3') AS SENT_TIME, NAME, PHONE, MESSAGE, SUCCESS, 
                             TO_CHAR(ALARM_TIME, 'YYYY.MM.DD HH24:MI:SS.FF3') AS ALARM_TIME FROM  HMI_SMS_LIST
                             WHERE SENT_TIME BETWEEN: 1 AND: 2 AND NAME LIKE: 3 AND SUCCESS LIKE: 4 AND NVL(DSNT, '%') LIKE :5";

            OracleCommand cmd = null;
            OracleDataReader reader = null;
            try
            {
                cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Date).Value = start;
                cmd.Parameters.Add(":2", OracleDbType.Date).Value = end;
                cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = name;
                cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = success;
                cmd.Parameters.Add(":5", OracleDbType.NVarchar2).Value = dsnt;

                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        DataRow dr = _dtSmsHist.NewRow();
                        dr["SENT_TIME"] = reader["SENT_TIME"].ToString();
                        dr["NAME"] = reader["NAME"].ToString();
                        dr["PHONE"] = reader["PHONE"].ToString();
                        dr["MESSAGE"] = reader["MESSAGE"].ToString();
                        dr["SUCCESS"] = reader["SUCCESS"].ToString();
                        dr["ALARM_TIME"] = reader["ALARM_TIME"].ToString();
                       // dr["DSNT"] = reader["DSNT"].ToString();

                        _dtSmsHist.Rows.Add(dr);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Database.Close();
            }
                



        }

        private void ExportSmsHistory()
        {
            if (_dtSmsHist.Rows.Count < 1) return;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveDialog.FilterIndex = 1;

            DataTable selectedDt = _dtSmsHist.AsEnumerable()
                .Where(row => row.Field<string>("SENT_TIME") != "").CopyToDataTable();

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
                var t = new Thread(() => ex.ExportToExcel(selectedDt, saveDialog.FileName, "SMS History"));
                t.Start();

            }
        }


        private void DecorateDataGridView(DataGridView dg)
        {
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dg.EnableHeadersVisualStyles = false;

            dg.Columns["SENT_TIME"].Width = 150; 
            dg.Columns["SENT_TIME"].ReadOnly = true;
            dg.Columns["SENT_TIME"].ValueType = typeof(bool);
            dg.Columns["SENT_TIME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["SENT_TIME"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["NAME"].Width = 100;
            dg.Columns["NAME"].ReadOnly = true;
            dg.Columns["NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["NAME"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["PHONE"].Width = 100;
            dg.Columns["PHONE"].ReadOnly = true;
            dg.Columns["PHONE"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["PHONE"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["SUCCESS"].Width = 100;
            dg.Columns["SUCCESS"].ReadOnly = true;
            dg.Columns["SUCCESS"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["SUCCESS"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["MESSAGE"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg.Columns["MESSAGE"].ReadOnly = true;
            dg.Columns["MESSAGE"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["MESSAGE"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["ALARM_TIME"].Width = 150;
            dg.Columns["ALARM_TIME"].ReadOnly = true;
            dg.Columns["ALARM_TIME"].ValueType = typeof(bool);
            dg.Columns["ALARM_TIME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["ALARM_TIME"].DefaultCellStyle.ForeColor = Color.White;

           // dg.Columns["DSNT"].Width = 100;
            //dg.Columns["DSNT"].ReadOnly = true;
            //dg.Columns["DSNT"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            //dg.Columns["DSNT"].DefaultCellStyle.ForeColor = Color.White;

        }
    }
}
