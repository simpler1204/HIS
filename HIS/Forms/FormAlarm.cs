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
using Timer = System.Windows.Forms.Timer;
using System.Reflection;
using DevExpress.XtraSplashScreen;

namespace HIS.Forms
{
    public partial class FormAlarm :Form //HIS.Forms.FormSuper
    {
        DataTable dtAlarmHistory = new DataTable("AlarmHistory");
        private event EventHandler<int[]> ProgressBarEvent;
        private bool bChkRealTime = false;
        private bool bChkRecent = true;
        private DataGridViewImageAnimator dataGridImageAnimator;
        Timer fastTimer, slowTimer;
        bool fastOdd = false;
        bool slowOdd = false;

        int totalCount = 0;
        int unAckCount = 0;

        SplashScreenManager splashScreenManager1;

        MainForm mainForm;

        List<DataGridViewImageCell> fastRedBlink;
        List<DataGridViewImageCell> slowRedBlink;
        List<DataGridViewImageCell> fastOrangeBlink;
        List<DataGridViewImageCell> slowOrangeBlink;
        List<DataGridViewImageCell> fastYellowBlink;
        List<DataGridViewImageCell> slowYellowBlink;
        List<DataGridViewImageCell> fastGreenBlink;
        List<DataGridViewImageCell> slowGreenBlink;



        public FormAlarm(MainForm mainForm)
        {
            InitializeComponent();
            Load += FormAlarm_Load;
            this.mainForm = mainForm;
            menu.ButtonClick += Menu_ButtonClick;
            ProgressBarEvent += FormPopupTrendInfo_ProgressBarEvent;
            this.mainForm.MsgFromOa += MainForm_MsgFromOa;
            
            dataGridImageAnimator = new DataGridViewImageAnimator(dataGridView1);

            this.splashScreenManager1 = new SplashScreenManager(this, typeof(global::HIS.Forms.WaitForm1), true, true);
            this.splashScreenManager1.ClosingDelay = 500;

            fastRedBlink = new List<DataGridViewImageCell>();
            slowRedBlink = new List<DataGridViewImageCell>();
            fastOrangeBlink = new List<DataGridViewImageCell>();
            slowOrangeBlink = new List<DataGridViewImageCell>();
            fastYellowBlink = new List<DataGridViewImageCell>();
            slowYellowBlink = new List<DataGridViewImageCell>();
            fastGreenBlink = new List<DataGridViewImageCell>();
            slowGreenBlink = new List<DataGridViewImageCell>();

            fastTimer = new Timer();
            fastTimer.Interval = 300;
            fastTimer.Tick += fastTimer_Tick;
            fastTimer.Start();

            slowTimer = new Timer();
            slowTimer.Interval = 800;
            slowTimer.Tick += slowTimer_Tick;
            slowTimer.Start();

            FormClosing += FormAlarm_FormClosing;

            SetDoNotSort(dataGridView1);

        }

        private void FormAlarm_FormClosing(object sender, FormClosingEventArgs e)
        {
            fastTimer.Stop();
           // fastTimer.Dispose();

            slowTimer.Stop();
            // slowTimer.Dispose();

            menu.ButtonClick -= Menu_ButtonClick;
            ProgressBarEvent -= FormPopupTrendInfo_ProgressBarEvent;
            this.mainForm.MsgFromOa -= MainForm_MsgFromOa;
        }

        private void slowTimer_Tick(object sender, EventArgs e)
        {
            slowTimer.Stop();
            if (slowOdd == false)
            {
                foreach (DataGridViewImageCell item in slowRedBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.RED;
                }
                foreach (DataGridViewImageCell item in slowOrangeBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.ORANGE;
                }
                foreach (DataGridViewImageCell item in slowYellowBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.YELLOW;
                }
                foreach (DataGridViewImageCell item in slowGreenBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.GREEN;
                }
            }
            else
            {
                foreach (DataGridViewImageCell item in slowRedBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.normal2;
                }
                foreach (DataGridViewImageCell item in slowOrangeBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.normal2;
                }
                foreach (DataGridViewImageCell item in slowYellowBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.normal2;
                }
                foreach (DataGridViewImageCell item in slowGreenBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.normal2;
                }
            }
            slowOdd = !slowOdd;
            slowTimer.Start();
        }

        private void fastTimer_Tick(object sender, EventArgs e)
        {
            fastTimer.Stop();
            if (fastOdd == false)
            {
                foreach (DataGridViewImageCell item in fastRedBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.RED;
                }
                foreach (DataGridViewImageCell item in fastOrangeBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.ORANGE;
                }
                foreach (DataGridViewImageCell item in fastYellowBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.YELLOW;
                }
                foreach (DataGridViewImageCell item in fastGreenBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.GREEN;
                }
            }
            else
            {
                foreach (DataGridViewImageCell item in fastRedBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.normal2;
                }
                foreach (DataGridViewImageCell item in fastOrangeBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.normal2;
                }
                foreach (DataGridViewImageCell item in fastYellowBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.normal2;
                }
                foreach (DataGridViewImageCell item in fastGreenBlink)
                {
                    item.Value = (System.Drawing.Image)Properties.Resources.normal2;
                }
            }
            fastOdd = !fastOdd;
            fastTimer.Start();
        }

        private void  ColorChange(DataGridViewImageCell cell,  string grade, string color)
        {
            fastRedBlink.Remove(cell);
            slowRedBlink.Remove(cell);
            fastOrangeBlink.Remove(cell);
            slowOrangeBlink.Remove(cell);
            fastYellowBlink.Remove(cell);
            slowYellowBlink.Remove(cell);
            fastGreenBlink.Remove(cell);
            slowGreenBlink.Remove(cell);

            if(grade == "fast")
            {
                switch(color)
                {
                    case "red":
                        fastRedBlink.Add(cell);
                        break;                       
                    case "orange":
                        fastOrangeBlink.Add(cell);
                        break;
                    case "yellow":
                        fastYellowBlink.Add(cell);
                        break;
                    case "green":
                        fastGreenBlink.Add(cell);
                        break;
                }
            }
            else if (grade == "slow")
            {
                switch (color)
                {
                    case "red":
                        slowRedBlink.Add(cell);
                        break;
                    case "orange":
                        slowOrangeBlink.Add(cell);
                        break;
                    case "yellow":
                        slowYellowBlink.Add(cell);
                        break;
                    case "green":
                        slowGreenBlink.Add(cell);
                        break;
                }
            }

        }

        private void MainForm_MsgFromOa(string receiveMessage)
        {
            string[] msg = receiveMessage.Split(';');
            if (msg.Length < 3) return;

            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        if (msg[0] == "alarm")
                        {
                           
                            if (msg[1] == "insert")
                                InsertAlarm(ref msg);
                            else if (msg[1] == "ack")
                                AckAlarm(ref msg);
                            else if (msg[1] == "reset")
                                ResetAlarm(ref msg);
                          //  else if (msg[1] == "select")
                          //  {
                           //     string[] temp = msg[2].Split('.');
                          //      txtDpName.Text = temp[0];
                               // GetAlarmHistory();
                          //      if (dataGridView1.Rows.Count > 0)
                          //          dataGridView1.Visible = true;
                          //      else
                           //         dataGridView1.Visible = false;
                          //  }
                            else
                                return;
                        }


                    }));
                }
                catch 
                {
                    //MessageBox.Show(ex.ToString());
                }
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
                    SearhAlarmHistory();
                    break;
                case "Export":
                    ExportAlarmHistory();
                    break;
            }
        }

        private void ExportAlarmHistory()
        {
            if (dtAlarmHistory.Rows.Count < 1) return;

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
            saveDialog.FilterIndex = 1;

            DataTable selectedDt = dtAlarmHistory.AsEnumerable()
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

        public void SearhAlarmHistory()
        {
            fastRedBlink.Clear();
            slowRedBlink.Clear();
            fastOrangeBlink.Clear();
            slowOrangeBlink.Clear();
            fastYellowBlink.Clear();
            slowYellowBlink.Clear();
            fastGreenBlink.Clear();
            slowGreenBlink.Clear();

            splashScreenManager1.ShowWaitForm();
            //startDt.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
            DateTime start = DateTime.Parse(startDt.Text);
            DateTime end;
            if (bChkRecent == true)
            {
                end = DateTime.Now;
                endDt.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                end = DateTime.Parse(endDt.Text);
            }

             
            string ackUser = "%" + txtAckUser.Text + "%";
            string alarmLevel = "%" + cmbAlarmLevel.Text + "%";
            string dpName = "%" + txtDpName.Text + "%";
            string alarmMsg = "%" + txtAlarmMsg.Text + "%";

            if (!Database.Open()) return;

            dtAlarmHistory.Clear();

            string query = string.Empty;
            query = " SELECT TO_CHAR(INSERT_TIME, 'YYYY.MM.DD HH24:MI:SS.FF3') AS INSERT_TIME, ALM_PRIO, DP, ALM_MSG, PV_VAL, SP_VAL, ";
            query += " TO_CHAR(ACK_TIME, 'YYYY.MM.DD HH24:MI:SS.FF3') AS ACK_TIME, DUR_ACK, TO_CHAR(RESET_TIME, 'YYYY.MM.DD HH24:MI:SS.FF3') AS RESET_TIME, DUR_RST, ";
            query += " ACK_FULLNAME, PANEL FROM HMI_HIST_ALARM  ";
            query += " WHERE INSERT_TIME BETWEEN :1 AND :2 ";

            if (alarmLevel != "%%")            
                query += " AND ALM_PRIO LIKE :3 ";
            
            if (dpName != "%%")            
                query += " AND DP LIKE :4 ";
            
            if (ackUser != "%%")            
                query += " AND ACK_FULLNAME LIKE :5 ";
            
            if (alarmMsg != "%%")            
                query += " AND ALM_MSG LIKE :6 ";

            query += "  ORDER BY INSERT_TIME DESC";


            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            try
            {               
                cmd.Parameters.Add(":1", OracleDbType.Date).Value = start;
                cmd.Parameters.Add(":2", OracleDbType.Date).Value = end;

                if (alarmLevel != "%%")
                    cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = alarmLevel;

                if (dpName != "%%")
                    cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = dpName;

                if (ackUser != "%%")
                    cmd.Parameters.Add(":5", OracleDbType.Varchar2).Value = ackUser;

                if (alarmMsg != "%%")
                    cmd.Parameters.Add(":6", OracleDbType.Varchar2).Value = alarmMsg;

                OracleDataReader reader = cmd.ExecuteReader();
                if (!reader.HasRows) return;                
                while(reader.Read())
                {
                    totalCount++;

                    DataRow row         = dtAlarmHistory.NewRow();
                    //row["SEQ"] = totalCount.ToString("#,##0");
                    row["INSERT_TIME"]  = reader[0].ToString();
                    row["PRIO"]     = reader[1].ToString();
                    row["DP"]           = reader[2].ToString();
                    row["ALM_MSG"]      = reader[3].ToString();
                    row["PV"]       = reader[4].ToString();
                    row["SV"]       = reader[5].ToString();
                    row["ACK_TIME"]     = reader[6].ToString();
                    row["ACK"]      = reader[7].ToString();
                    row["RESET_TIME"]   = reader[8].ToString();
                    row["RESET"]      = reader[9].ToString();
                    row["ACK_NAME"] = reader[10].ToString();
                    row["PANEL"]        = reader[11].ToString();
                    dtAlarmHistory.Rows.Add(row);

                    dataGridView1.Rows[totalCount - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                    DataGridViewImageCell cell = dataGridView1.Rows[totalCount - 1].Cells[0] as DataGridViewImageCell;
                    cell.Value = (System.Drawing.Image)Properties.Resources.normal2;

                    if (reader[6].ToString() == "")
                        unAckCount++;
                }

                lblTotal.Text = totalCount.ToString("#,##0");
                lblUnAck.Text = unAckCount.ToString("#,##0");

                foreach (DataRow item in dtAlarmHistory.Rows)
                {
                    item["SEQ"] = totalCount--;
                }

                InitDataGridView.AutoSettingDatagridView(dataGridView1, new List<int>() { 4, 5 }, new List<int>());
                dataGridView1.Columns["SEQ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["PRIO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView1.Columns["PV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["SV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["ACK"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["RESET"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dataGridView1.Columns["ACK_NAME"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                DataGridViewCell c =  dataGridView1.Rows[1].Cells[1]; 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
                splashScreenManager1.CloseWaitForm();
            }
            

        }

        private void FormAlarm_Load(object sender, EventArgs e)
        {
            // dataGridView1 초기화
            InitDataGridView.dataGridViewInit(dataGridView1);

            //dtAlarmHistory 초기화
            InitDatatable.Init(dtAlarmHistory);

            // dtAlarmHistory와 dataGridView1 연결
            dataGridView1.DataSource = dtAlarmHistory;

            //메뉴 색깔
            menu.ForeColor = Colors.buttonForeColor;

            //초기 날짜 설정
            startDt.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");

            //DataGridView 버퍼를 늘림.
            DataGridViewBufferExtension();

            //sorting 막기
            SetDoNotSort(dataGridView1);

        }

        public void InsertAlarm(ref string[] receiveMessage)
        {
            if (receiveMessage.Length != 16) return;

            //receiveMessage[0]  구분 : alarm, operation...
            //receiveMessage[1]  구분 : insert, ack, reset
            //receiveMessage[2]  dp name
            //receiveMessage[3]  alarm time or reset time
            //receiveMessage[4]  alarm message
            //receiveMessage[5]  value
            //receiveMessage[6]  partner datetime
            //receiveMessage[7]  level
            //receiveMessage[8]  ack_user
            //receiveMessage[9]  ack time
            //receiveMessage[10]  ack state
            //receiveMessage[11]  act state
            //receiveMessage[12] ackable
            //receiveMessage[13] alert color;
            //receiveMessage[14] pv
            //receiveMessage[15] sv

            if (bChkRealTime == false) return;

           // dataGridView1.Visible = true;

            string sAlarmTime = receiveMessage[3].Substring(0, 23);
            string sAlarmMessage = receiveMessage[4];
            string sPv, sSv;
            if (receiveMessage[14] == "TRUE")
            {
                sPv = receiveMessage[14];
                sSv = receiveMessage[15];
            }
            else
            {
                float fPv = 0f, fSv = 0f;
                float.TryParse(receiveMessage[14], out fPv);
                float.TryParse(receiveMessage[15], out fSv);

                sPv = fPv.ToString("#,##0.##0");
                sSv = fSv.ToString("#,##0.##0");
            }
          

            string[] temp = receiveMessage[7].Split(':'); //  RND_CLIENT1:Lev_1. 이런 식으로 수신됨
            if (temp.Length < 2) return;            

            temp = temp[1].Split('.');// Lev_1. 
            temp = temp[0].Split('_');// Lev_1
            string sLevel = temp[1]; // 1 이런식으로 
            string sEmpy = string.Empty;


            temp = receiveMessage[2].Split(':');
            string sDpName = temp.Last(); // 순수 dp.

            DataRow dr = dtAlarmHistory.NewRow();
            dr["SEQ"] = dtAlarmHistory.Rows.Count + 1;
            dr["INSERT_TIME"] = sAlarmTime; //alarm time
            dr["PRIO"] = sLevel; //level;
            dr["DP"] = sDpName;  //dp name;
            dr["ALM_MSG"] = sAlarmMessage; //message;
            dr["PV"] = sPv; //pv
            dr["SV"] = sSv; //sv
            dr["ACK_TIME"] = sEmpy; //ack time
            dr["ACK"] = sEmpy; //ack duration
            dr["RESET_TIME"] = sEmpy; //reset time;
            dr["RESET"] = sEmpy; //reset 
            dr["PANEL"] = sEmpy;

            dtAlarmHistory.Rows.InsertAt(dr, 0);
            dataGridView1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(60,60,60);
                       
            int unAck = unAckCount; // int.Parse(lblUnAck.Text);
            int total = totalCount;
            lblUnAck.Text = (unAck + 1).ToString();
            lblTotal.Text = (total + 1).ToString();


            if (sLevel == "1")
            {
                DataGridViewImageCell cell = dataGridView1.Rows[0].Cells[0] as DataGridViewImageCell;
                cell.Value = (System.Drawing.Image)Properties.Resources.normal2;
                ColorChange(cell, "fast", "red");
            }
            else if (sLevel == "2")
            {
                DataGridViewImageCell cell = dataGridView1.Rows[0].Cells[0] as DataGridViewImageCell;
                cell.Value = (System.Drawing.Image)Properties.Resources.normal2;
                ColorChange(cell, "fast", "orange");
            }
            else if (sLevel == "3")
            {
                DataGridViewImageCell cell = dataGridView1.Rows[0].Cells[0] as DataGridViewImageCell;
                cell.Value = (System.Drawing.Image)Properties.Resources.normal2;
                // cell.Value = (System.Drawing.Image)Properties.Resources.YELLOW_FAST;
                ColorChange(cell, "fast", "yellow");
            }
            else if (sLevel == "4")
            {
                DataGridViewImageCell cell = dataGridView1.Rows[0].Cells[0] as DataGridViewImageCell;
                cell.Value = (System.Drawing.Image)Properties.Resources.normal2;
                //cell.Value = (System.Drawing.Image)Properties.Resources.GREEN_FAST;
                ColorChange(cell, "fast", "green");
            }
            else
            {

            }
        }

        public void AckAlarm(ref string[] receiveMessage)
        {
            if (receiveMessage.Length != 16) return;           

            if (bChkRealTime == false) return;

            string sAlarmTime = receiveMessage[3].Substring(0, 23);
            string sPartnerTime = receiveMessage[6].Substring(0, 23);
            string sAckUser = "";//receiveMessage[8];
            string sAckTime = receiveMessage[9].Substring(0, 23); ;


            string[] temp = receiveMessage[7].Split(':'); //  RND_CLIENT1:Lev_1. 이런 식으로 수신됨           
            temp = temp[1].Split('.');// Lev_1. 
            temp = temp[0].Split('_');// Lev_1
            string sLevel = temp[1]; // 1 이런식으로 
            string sEmpy = string.Empty;

            temp = receiveMessage[2].Split(':');
            string sDpName = temp.Last(); // 순수 dp.

            string sAckDuration = DateDiff(ref sAlarmTime, ref sPartnerTime, ref sAckTime, "ack"); //ack duration           

            int rowCount = dataGridView1.RowCount;
            if (rowCount < 1) return;

            int row = 0;
            foreach (DataRow item in dtAlarmHistory.Rows)
            {
                if(item["DP"].ToString() == sDpName && item["ACK_TIME"].ToString().Length == 0)
                {
                    item["ACK_TIME"] = sAckTime;
                    item["ACK"] = sAckDuration;
                    item["ACK_NAME"] = sAckUser;

                    if(item["RESET_TIME"].ToString().Length == 0)
                    {
                        AckSetColor(ref sLevel, row, 0);
                    }
                    else
                    {
                        AckSetColor(ref sLevel, row, 1);
                    }

                    break;                   
                }

                row++;

            }

            int unAck = unAckCount; //int.Parse(lblUnAck.Text);
            int total = totalCount;// int.Parse(lblTotal.Text);
            lblUnAck.Text = (unAck - 1).ToString("#,##0");
            
        }

        public void ResetAlarm(ref string[] receiveMessage)
        {
            if (receiveMessage.Length != 16) return;

            if (bChkRealTime == false) return;

            string sAlarmTime = receiveMessage[3].Substring(0, 23); // alarm_time, reset_time 둘다 됨. duration 계산을 위해
            string sResetTime = receiveMessage[3].Substring(0, 23);
            string sPartnerTime = receiveMessage[6].Substring(0, 23);

            string[] temp = receiveMessage[7].Split(':'); //  RND_CLIENT1:Lev_1. 이런 식으로 수신됨           
            temp = temp[1].Split('.');// Lev_1. 
            temp = temp[0].Split('_');// Lev_1
            string sLevel = temp[1]; // 1 이런식으로 
            string sEmpy = string.Empty;

            temp = receiveMessage[2].Split(':');
            string sDpName = temp.Last(); // 순수 dp.
            

            string sResetDuration = DateDiff(ref sAlarmTime, ref sPartnerTime, ref sResetTime, "reset"); //ack duration

            int rowCount = dtAlarmHistory.Rows.Count;
            if (rowCount < 1) return;

            int row = 0;
            foreach (DataRow item in dtAlarmHistory.Rows)
            {
                if(item["RESET_TIME"].ToString().Length == 0 && item["DP"].ToString() == sDpName)
                {
                    item["RESET_TIME"] = sResetTime;
                    item["RESET"] = sResetDuration;
                    if(item["ACK_TIME"].ToString().Length == 0)
                    {
                        ResetSetColor(ref sLevel, row, 0);
                    }
                    else
                    {
                        ResetSetColor(ref sLevel, row, 1);
                    }

                    break;
                }
                row++;
            }
           
        }

        private void chkRealTime_CheckStateChanged(object sender, EventArgs e)
        {
            bChkRealTime = chkRealTime.Checked;
        }

        private string DateDiff(ref string alarmTime, ref string partnerTime, ref string ackTime, string section)
        {           
            TimeSpan dateDiff;
            int seconds = 0;

            if (section == "ack")
            {
                if (partnerTime == "1970.01.01 08:00:00.000")
                {
                    dateDiff = Convert.ToDateTime(ackTime) - Convert.ToDateTime(alarmTime);
                    seconds = (int)dateDiff.TotalSeconds;
                    return seconds.ToString();
                }
                else
                {
                    dateDiff = Convert.ToDateTime(ackTime) - Convert.ToDateTime(partnerTime);
                    seconds = (int)dateDiff.TotalSeconds;
                    return seconds.ToString();
                }
            }
            else if (section == "reset")
            {
                dateDiff = Convert.ToDateTime(alarmTime) - Convert.ToDateTime(partnerTime);
                seconds = (int)dateDiff.TotalSeconds;
                return seconds.ToString();
            }
            else
            {
                return "Error";
            }

        }

        private void AckSetColor(ref string level, int row, int checkResetTime)  //checkResetTime =>  0->reset없음, 1->reset 됨. 
        {
            DataGridViewImageCell cell = dataGridView1.Rows[row].Cells[0] as DataGridViewImageCell;
            RemoveCellFromBlink(cell);

            if (checkResetTime == 0)
            {
                switch (level)
                {
                    case "1":
                        cell.Value = (System.Drawing.Image)Properties.Resources.RED;
                        break;
                    case "2":
                        cell.Value = (System.Drawing.Image)Properties.Resources.ORANGE;
                        break;
                    case "3":
                        cell.Value = (System.Drawing.Image)Properties.Resources.YELLOW;
                        break;
                    case "4":
                        cell.Value = (System.Drawing.Image)Properties.Resources.GREEN;
                        break;
                    default:
                        break;
                }
            }
            else if (checkResetTime == 1)
            {
                cell.Value = (System.Drawing.Image)Properties.Resources.normal2;
            }

        }

        public void ResetSetColor(ref string level, int row, int checkAckTime)  // 0->ack 없음, ack 됨. 
        {

            if (checkAckTime == 0)
            {
                DataGridViewImageCell cell = dataGridView1.Rows[row].Cells[0] as DataGridViewImageCell;
                switch (level)
                {
                    case "1":
                        //cell.Value = (System.Drawing.Image)Properties.Resources.RED_SLOWLY;
                        ColorChange(cell, "slow", "red");
                        break;
                    case "2":
                        //cell.Value = (System.Drawing.Image)Properties.Resources.ORANGE_SLOWLY;
                        ColorChange(cell, "slow", "orange");
                        break;
                    case "3":
                        //cell.Value = (System.Drawing.Image)Properties.Resources.YELLOW_SLOWLY;
                        ColorChange(cell, "slow", "yellow");
                        break;
                    case "4":
                        //cell.Value = (System.Drawing.Image)Properties.Resources.GREEN_SLOWLY;
                        ColorChange(cell, "slow", "green");
                        break;
                    default:
                        break;
                }
            }
            else if (checkAckTime == 1)
            {
                DataGridViewImageCell cell = dataGridView1.Rows[row].Cells[0] as DataGridViewImageCell;
                RemoveCellFromBlink(cell);
                cell.Value = (System.Drawing.Image)Properties.Resources.normal2;
            }

        }

        private void chkRecent_CheckStateChanged(object sender, EventArgs e)
        {
            bChkRecent = chkRecent.Checked;
        }

        private void RemoveCellFromBlink(DataGridViewImageCell cell)
        {
            fastRedBlink.Remove(cell);
            slowRedBlink.Remove(cell);
            fastOrangeBlink.Remove(cell);
            slowOrangeBlink.Remove(cell);
            fastYellowBlink.Remove(cell);
            slowYellowBlink.Remove(cell);
            fastGreenBlink.Remove(cell);
            slowGreenBlink.Remove(cell);
        }
               
        private void DataGridViewBufferExtension()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
            null, this.dataGridView1, new object[] { true });
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            string dpName = dataGridView1[4, e.RowIndex].Value.ToString();
            string[] temp = dpName.Split('.');
            dpName = temp[0];
            mainForm.sendMsgToOA("find;" + dpName);
            //SendMessageEvent("find;" + dpName);
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
