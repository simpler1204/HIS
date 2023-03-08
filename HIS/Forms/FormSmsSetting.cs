using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HIS.Class;
using Oracle.ManagedDataAccess.Client;

namespace HIS.Forms
{
    public partial class FormSmsSetting : Form
    {
        private MainForm _mainForm { get; }
        private DataTable _dtSmsUser = new DataTable("SmsUser");

        public FormSmsSetting(MainForm mainForm)
        {
            InitializeComponent();
            _mainForm = mainForm;

            Database.CreateDatabase();
            InitDatatable.Init(_dtSmsUser);

            this.Load += FormSmsSetting_Load;
            btnSearch.ButtonClick += BtnSearch_ButtonClick;
            btnMenu.ButtonClick += BtnMenu_ButtonClick;
           
        }

        private void BtnMenu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            var btn = e.Button.Properties.Caption;

            switch(btn)
            {
                case "Add":
                    AddUser();
                    break;
                case "Save":
                    SaveUser();
                    break;
                case "Delete":
                    DeleteUser();
                    break;

            }
        }

        private void DeleteUser()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "DELETE HMI_SMS_USER WHERE SEQ = :1";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            List<DataRow> drs = new List<DataRow>();
            foreach (DataRow dr in _dtSmsUser.Rows)
            {
                if (Convert.ToBoolean(dr["CHK"]) == true)
                {
                    cmd.Parameters.Add(":1", OracleDbType.Int32).Value = dr["SEQ"];
                    if(cmd.ExecuteNonQuery() > 0)
                    {
                        drs.Add(dr);
                    }
                    cmd.Parameters.Clear();
                }
            }

            foreach(DataRow dr in drs)
            {
                _dtSmsUser.Rows.Remove(dr);
            }



            cmd.Dispose();
            SaveUser();
            GetSmsUser();
        }

        private void SaveUser()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (_dtSmsUser.Rows.Count < 1) return;

            dgUser.EndEdit();

            //for (int i=0; i<dgUser.Rows.Count; i++)
            //{
               
            //    for(int j = 0; j<dgUser.Columns.Count; j++)
            //    {
            //        //_dtSmsUser.Rows[i][j] = dgUser[j, i].Value;
                   

            //        MessageBox.Show(dgUser[j, i].Value.ToString());
            //    }
            //}

            string deleteQuery = "DELETE HMI_SMS_USER";
            string insertQuery = @"INSERT INTO HMI_SMS_USER(SEQ, USER_NAME, PHONE_NUMBER, WORK_NO, RECEIVE_YN)
                                   VALUES(:1, :2, :3, :4, :5)";
            
            OracleCommand cmd = new OracleCommand(deleteQuery, Database.OracleConn);

            int rtn = cmd.ExecuteNonQuery();
            int emptyCount = 0;
                       

            foreach (DataRow dr in _dtSmsUser.Rows)
            {                
                if (dr["USER_NAME"].ToString() == "" || dr["PHONE_NUMBER"].ToString().Trim() == "" || dr["WORK_NO"].ToString().Trim() == "")
                {
                    emptyCount++;
                }
            }

            if (emptyCount > 0)
            {
                MessageBox.Show("Fill the all columns..");
                return;
            }

            cmd.CommandText = insertQuery;
            int seq = 1;
            foreach (DataRow dr in _dtSmsUser.Rows)
            {
                cmd.Parameters.Add(":1", OracleDbType.Int32).Value = seq;
                cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = dr["USER_NAME"];
                cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = dr["PHONE_NUMBER"];
                cmd.Parameters.Add(":4", OracleDbType.NVarchar2).Value = dr["WORK_NO"];

                if (Convert.ToBoolean(dr["RECEIVE_YN"]) == false)
                {
                    cmd.Parameters.Add(":5", OracleDbType.NVarchar2).Value = "FALSE";
                }
                else
                {
                    cmd.Parameters.Add(":5", OracleDbType.NVarchar2).Value = "TRUE";
                }
                seq++;
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();            
            }
            cmd.Dispose();
            GetSmsUser();
        }

        private void AddUser()
        {
            DataRow dr = _dtSmsUser.NewRow();
            dr["SEQ"] = _dtSmsUser.Rows.Count + 1;
            dr["RECEIVE_YN"] = false;
            _dtSmsUser.Rows.Add(dr);
            DecorateDataGridView(dgUser);
        }

        private void FormSmsSetting_Load(object sender, EventArgs e)
        {
            cmbReceive.Text = "ALL";
            GetSmsUser();
            dgUser.DataSource = _dtSmsUser;
            DecorateDataGridView(dgUser);
            GetOnOffStatus();
        }

        private void BtnSearch_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            GetSmsUser();
        }

        private void GetSmsUser()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _dtSmsUser.Rows.Clear();
            string name = txtName.Text == "" ? "%" : "%" + txtName.Text + "%";
            string phone = txtPhone.Text == "" ? "%" : "%"+ txtPhone.Text + "%";
            string workNo = txtWork.Text == "" ? "%" : "%" + txtWork.Text + "%";
            string receive = string.Empty;

            if(cmbReceive.Text == "ALL")
                receive = "%";
            else if (cmbReceive.Text == "Y")
                receive = "TRUE";
            else if (cmbReceive.Text == "N")
                receive = "FALSE";


            string query = @"SELECT SEQ, USER_NAME, PHONE_NUMBER, WORK_NO, RECEIVE_YN FROM HMI_SMS_USER
                             WHERE USER_NAME LIKE :1 AND PHONE_NUMBER LIKE :2 AND WORK_NO LIKE :3
                             AND RECEIVE_YN LIKE :4 ORDER BY SEQ ASC";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = name;
            cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = phone;
            cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = workNo;
            cmd.Parameters.Add(":4", OracleDbType.NVarchar2).Value = receive;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {                
                    while(reader.Read())
                    {
                        DataRow dr = _dtSmsUser.NewRow();
                        dr["CHK"] = false;
                        dr["SEQ"] = reader["SEQ"];
                        dr["USER_NAME"] = reader["USER_NAME"].ToString();
                        dr["PHONE_NUMBER"] = reader["PHONE_NUMBER"].ToString();
                        dr["WORK_NO"] = reader["WORK_NO"].ToString();

                        if(reader["RECEIVE_YN"].ToString() == "TRUE")
                        {
                            dr["RECEIVE_YN"] = true;
                        }
                        else if (reader["RECEIVE_YN"].ToString() == "FALSE")
                        {
                            dr["RECEIVE_YN"] = false;
                        }

                        _dtSmsUser.Rows.Add(dr);
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

        private void DecorateDataGridView(DataGridView dg)
        {
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dg.EnableHeadersVisualStyles = false;

            dg.Columns["CHK"].Width = 35; //CHK
            dg.Columns["CHK"].ReadOnly = false;
            dg.Columns["CHK"].ValueType = typeof(bool);
            dg.Columns["CHK"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["CHK"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["SEQ"].Width = 40;
            dg.Columns["SEQ"].ReadOnly = true;
            dg.Columns["SEQ"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["SEQ"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["USER_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg.Columns["USER_NAME"].ReadOnly = false;
            dg.Columns["USER_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["USER_NAME"].DefaultCellStyle.ForeColor = Color.White;
            dg.Columns["USER_NAME"].HeaderText = "NAME";


            dg.Columns["PHONE_NUMBER"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg.Columns["PHONE_NUMBER"].ReadOnly = false;
            dg.Columns["PHONE_NUMBER"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["PHONE_NUMBER"].DefaultCellStyle.ForeColor = Color.White;
            dg.Columns["PHONE_NUMBER"].HeaderText = "PHONE";

            dg.Columns["WORK_NO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg.Columns["WORK_NO"].ReadOnly = false;
            dg.Columns["WORK_NO"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["WORK_NO"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["RECEIVE_YN"].Width = 60;
            dg.Columns["RECEIVE_YN"].ReadOnly = false;
            dg.Columns["RECEIVE_YN"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["RECEIVE_YN"].DefaultCellStyle.ForeColor = Color.White;
            dg.Columns["RECEIVE_YN"].HeaderText = "ACTIVE";


        }

        private void GetOnOffStatus()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string s1_sms = string.Empty;
            string s2_sms = string.Empty;
            string s3_sms = string.Empty;
            string s4_sms = string.Empty;
            string s1_wechat = string.Empty;
            string s2_wechat = string.Empty;
            string s3_wechat = string.Empty;
            string s4_wechat = string.Empty;
            string callback = string.Empty;
            string prefix = string.Empty;

            string query = @"SELECT NVL(svr_1_sms,    'NONE') AS svr_1_sms, 
                                    NVL(svr_2_sms,    'NONE') AS svr_2_sms,
                                    NVL(svr_3_sms,    'NONE') AS svr_3_sms,
                                    NVL(svr_4_sms,    'NONE') AS svr_4_sms,
                                    NVL(svr_1_wechat, 'NONE') AS svr_1_wechat, 
                                    NVL(svr_2_wechat, 'NONE') AS svr_2_wechat, 
                                    NVL(svr_3_wechat, 'NONE') AS svr_3_wechat,
                                    NVL(svr_4_wechat, 'NONE') AS svr_4_wechat,
                                    CALLBACK, PREFIX
                               FROM hmi_sms_on";

            OracleCommand cmd = null;
            OracleDataReader reader = null;

            try
            {
                cmd = new OracleCommand(query, Database.OracleConn);
                reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        s1_sms = reader["svr_1_sms"].ToString();
                        s2_sms = reader["svr_2_sms"].ToString();
                        s3_sms = reader["svr_3_sms"].ToString();
                        s4_sms = reader["svr_4_sms"].ToString();
                        s1_wechat = reader["svr_1_wechat"].ToString();
                        s2_wechat = reader["svr_2_wechat"].ToString();
                        s3_wechat = reader["svr_3_wechat"].ToString();
                        s4_wechat = reader["svr_4_wechat"].ToString();
                        callback = reader["CALLBACK"].ToString();
                        prefix = reader["PREFIX"].ToString();
                    }
                }

                if (s1_sms == "TRUE") s1SmsOn.Checked = true; else s1SmsOff.Checked = true;
                if (s2_sms == "TRUE") s2SmsOn.Checked = true; else s2SmsOff.Checked = true;
                if (s3_sms == "TRUE") s3SmsOn.Checked = true; else s3SmsOff.Checked = true;
                if (s4_sms == "TRUE") s4SmsOn.Checked = true; else s4SmsOff.Checked = true;

                if (s1_wechat == "TRUE") s1WechatOn.Checked = true; else s1WechatOff.Checked = true;
                if (s2_wechat == "TRUE") s2WechatOn.Checked = true; else s2WechatOff.Checked = true;
                if (s3_wechat == "TRUE") s3WechatOn.Checked = true; else s3WechatOff.Checked = true;
                if (s4_wechat == "TRUE") s4WechatOn.Checked = true; else s4WechatOff.Checked = true;

                if (s1_sms == "TRUE" && s2_sms == "TRUE" && s3_sms == "TRUE" && s4_sms == "TRUE") rdoSmsAllOn.Checked = true;
                if (s1_wechat == "TRUE" && s2_wechat == "TRUE" && s3_wechat == "TRUE" && s4_wechat == "TRUE") rdoWechatAllOn.Checked = true;

                txtCallBack.Text = callback;
                txtPrefix.Text = prefix;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                Database.Close();
            }
        }

        private void dgUser_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0 || e.ColumnIndex == 5)
            {
                bool value = Convert.ToBoolean(_dtSmsUser.Rows[e.RowIndex][e.ColumnIndex]);
                _dtSmsUser.Rows[e.RowIndex][e.ColumnIndex] = !value;
            }
        }

        private void rdoSmsAllOn_CheckedChanged(object sender, EventArgs e)
        {          

            if(rdoSmsAllOn.Checked == true)
            {
                s1SmsOn.Checked = true;
                s2SmsOn.Checked = true;
                s3SmsOn.Checked = true;
                s4SmsOn.Checked = true;
            }
            else
            {
                s1SmsOff.Checked = true;
                s2SmsOff.Checked = true;
                s3SmsOff.Checked = true;
                s4SmsOff.Checked = true;
            }
        }

        private void rdoWechatAllOn_CheckedChanged(object sender, EventArgs e)
        {
            if(rdoWechatAllOn.Checked == true)
            {
                s1WechatOn.Checked = true;
                s2WechatOn.Checked = true;
                s3WechatOn.Checked = true;
                s4WechatOn.Checked = true;
            }
            else
            {
                s1WechatOff.Checked = true;
                s2WechatOff.Checked = true;
                s3WechatOff.Checked = true;
                s4WechatOff.Checked = true;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string s1_sms = string.Empty;
            string s2_sms = string.Empty;
            string s3_sms = string.Empty;
            string s4_sms = string.Empty;
            string s1_wechat = string.Empty;
            string s2_wechat = string.Empty;
            string s3_wechat = string.Empty;
            string s4_wechat = string.Empty;
            string callback = string.Empty;
            string prefix = string.Empty;

            s1_sms = s1SmsOn.Checked == true ? "TRUE" : "FALSE";
            s2_sms = s2SmsOn.Checked == true ? "TRUE" : "FALSE";
            s3_sms = s3SmsOn.Checked == true ? "TRUE" : "FALSE";
            s4_sms = s4SmsOn.Checked == true ? "TRUE" : "FALSE";

            s1_wechat = s1WechatOn.Checked == true ? "TRUE" : "FALSE";
            s2_wechat = s2WechatOn.Checked == true ? "TRUE" : "FALSE";
            s3_wechat = s3WechatOn.Checked == true ? "TRUE" : "FALSE";
            s4_wechat = s4WechatOn.Checked == true ? "TRUE" : "FALSE";

            callback = "1234";// txtCallBack.Text;
            prefix = txtPrefix.Text;

            string query = @"UPDATE HMI_SMS_ON SET svr_1_sms = :1,
                                    svr_2_sms = :2,
                                    svr_3_sms = :3,
                                    svr_4_sms = :4,
                                    svr_1_wechat = :5,
                                    svr_2_wechat = :6,
                                    svr_3_wechat = :7,
                                    svr_4_wechat = :8,
                                    CALLBACK = :9,
                                    PREFIX = :10";

            OracleCommand cmd = null;

            try
            {
                cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = s1_sms;
                cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = s2_sms;
                cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = s3_sms;
                cmd.Parameters.Add(":4", OracleDbType.NVarchar2).Value = s4_sms;
                cmd.Parameters.Add(":5", OracleDbType.NVarchar2).Value = s1_wechat;
                cmd.Parameters.Add(":6", OracleDbType.NVarchar2).Value = s2_wechat;
                cmd.Parameters.Add(":7", OracleDbType.NVarchar2).Value = s3_wechat;
                cmd.Parameters.Add(":8", OracleDbType.NVarchar2).Value = s4_wechat;
                cmd.Parameters.Add(":9", OracleDbType.NVarchar2).Value = callback;
                cmd.Parameters.Add(":10", OracleDbType.NVarchar2).Value = prefix;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Saving completed");
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
