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
using HIS.Forms;
using Oracle.ManagedDataAccess.Client;

namespace HIS.PopUp
{
    public partial class PopUpSearchTrendGroup : Form
    { 
        private DataTable _dtGroup = new DataTable("TrendGroup");
        private MainForm _mainForm = null;
        private string _distinct;
        private FormTrend1 _frmTrend1 = null;
        private FormTrend2 _frmTrend2 = null;
        private FormTrend3 _frmTrend3 = null;

        public PopUpSearchTrendGroup(MainForm mainForm, string distinct)
        {
            InitializeComponent();

            this._mainForm = mainForm;
            _distinct = distinct;
            InitDataTable(_dtGroup);
            dataGridView1.DataSource = _dtGroup;

            DecorateDataGridView(dataGridView1);
            btnTrend.ButtonClick += BtnTrend_ButtonClick;

            if (_distinct == "hmi")
                lblDistinct.Text = "HMI TREND";
            else if (_distinct == "his")
                lblDistinct.Text = "HIS TREND";


        }

        private void BtnTrend_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string btnName = e.Button.Properties.Caption;

            switch(btnName)
            {
                case "Search":
                    GetGroup();
                    break;
                case "Show Trend":
                    ShowTrend();
                    break;
            }

          
        } 

      

        private void InitDataTable(DataTable dt)
        {
            if (dt.TableName == "TrendGroup")
            {
                dt.Columns.Add("PART_NAME", typeof(string));
                dt.Columns.Add("GROUP_NAME", typeof(string));
                dt.Columns.Add("OA", typeof(bool));
                dt.Columns.Add("HIS", typeof(bool));
            }
        }

        private void GetGroup()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dtGroup.Rows.Clear();
                        
            string partName = txtPart.Text.Trim() == "" ? "%" : "%" + txtPart.Text + "%";
            string groupName = txtGroup.Text.Trim() == "" ? "%" : "%" + txtGroup.Text + "%";

            string query = @"SELECT DISTINCT PART_NAME, GROUP_NAME FROM C2_TREND_GROUP WHERE PART_NAME LIKE :1 AND GROUP_NAME LIKE :2 
                             ORDER BY PART_NAME ASC, GROUP_NAME ASC";
            OracleCommand cmd = null;
            OracleDataReader reader = null;

            try
            {
                cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = partName;
                cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = groupName;

                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DataRow dr = _dtGroup.NewRow();
                        dr["PART_NAME"] = reader["PART_NAME"];
                        dr["GROUP_NAME"] = reader["GROUP_NAME"];
                        if (_distinct == "hmi")
                        {
                            dr["OA"] = true;
                            dr["HIS"] = false;
                        }
                        else if((_distinct == "his"))
                        {
                            dr["OA"] = false;
                            dr["HIS"] = true;
                        }
                       _dtGroup.Rows.Add(dr);
                    }
                }              
            }
            catch (Exception ex)
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

        private void DecorateDataGridView(DataGridView dg)
        {
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dg.EnableHeadersVisualStyles = false;

            dg.Columns["PART_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg.Columns["PART_NAME"].ReadOnly = true;
            dg.Columns["PART_NAME"].ValueType = typeof(bool);
            dg.Columns["PART_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["PART_NAME"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["GROUP_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg.Columns["GROUP_NAME"].ReadOnly = true;
            dg.Columns["GROUP_NAME"].ValueType = typeof(bool);
            dg.Columns["GROUP_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["GROUP_NAME"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["OA"].Width = 40;
            dg.Columns["OA"].ReadOnly = false;
            dg.Columns["OA"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["OA"].DefaultCellStyle.ForeColor = Color.White;
            dg.Columns["OA"].Visible = false;

            dg.Columns["HIS"].Width = 40;
            dg.Columns["HIS"].ReadOnly = false;
            dg.Columns["HIS"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["HIS"].DefaultCellStyle.ForeColor = Color.White;
            dg.Columns["HIS"].Visible = false;


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            

            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {              
                bool value = Convert.ToBoolean(_dtGroup.Rows[e.RowIndex][e.ColumnIndex]);
                _dtGroup.Rows[e.RowIndex][e.ColumnIndex] = !value;
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            ShowTrend();
        }

        private void ShowTrend()
        {
            if (_dtGroup.Rows.Count < 1) return;
            if (dataGridView1.CurrentRow.Index < 0) return;

            int row = dataGridView1.CurrentRow.Index;

            bool isOA = false;
            bool isHIS = false;

            bool.TryParse(_dtGroup.Rows[row]["OA"].ToString(), out isOA);
            bool.TryParse(_dtGroup.Rows[row]["HIS"].ToString(), out isHIS);


            if (isOA == true)
            {
                string page = "ALL";
                string part = dataGridView1["PART_NAME", row].Value.ToString();
                string group = dataGridView1["GROUP_NAME", row].Value.ToString();
                string sendMsg = "find;TrendGroupFromHIS|" + page + "|" + part + "|" + group;
                _mainForm.sendMsgToOA("find;TrendGroupFromHIS;" + page + ";" + part + ";" + group);
            }

            if (isHIS == true)
            {
                bool isLeftOn = false;
                bool isRightOn = false;


                string part = dataGridView1["PART_NAME", row].Value.ToString();
                string group = dataGridView1["GROUP_NAME", row].Value.ToString();

                int count = IsBoth(part, group);
                if (count == -1) return;

                if (count == 0)
                {
                    OpenTrend1(part, group);
                }
                else
                {
                    OpenTrend2(part, group);
                    OpenTrend3(part, group);



                    //foreach (Form form in Application.OpenForms)
                    //{
                    //    if (form.GetType() == typeof(FormTrend3))
                    //    {
                    //        form.Activate();
                    //        form.WindowState = FormWindowState.Normal;
                    //        _frmTrend3.CreateSeriesByTrendGroup(part, group, "right");
                    //        isRightOn = true;
                    //    }
                    //}                                    

                    //if (isRightOn == false)
                    //{
                    //    _frmTrend3 = new FormTrend3(_mainForm);
                    //    _frmTrend3.Show();
                    //    _frmTrend3.Size = new Size(900, 1000);
                    //    _frmTrend3.Location = new Point(910, 0);
                    //    _frmTrend3.CreateSeriesByTrendGroup(part, group, "right");
                    //}

                }
            }
        }

        private void OpenTrend1(string part, string group)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormTrend1))
                {
                    form.Activate();
                    form.WindowState = FormWindowState.Normal;
                    _frmTrend1.CreateSeriesByTrendGroup(part, group, "left");
                    return;
                }
            }
            _frmTrend1 = new FormTrend1(_mainForm);
            _frmTrend1.Show();
            _frmTrend1.CreateSeriesByTrendGroup(part, group, "left");
        }

        private void OpenTrend2(string part, string group)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormTrend2))
                {
                    form.Activate();
                    form.WindowState = FormWindowState.Normal;
                    _frmTrend2.CreateSeriesByTrendGroup(part, group, "left");
                    return;
                }
            }
            _frmTrend2 = new FormTrend2(_mainForm);
           
            _frmTrend2.Size = new Size(955, 1000);          
            _frmTrend2.Show();           
            _frmTrend2.Location = new Point(0, 10);
            _frmTrend2.CreateSeriesByTrendGroup(part, group, "left");           

        }

        private void OpenTrend3(string part, string group)
        {
            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(FormTrend3))
                {
                    form.Activate();
                    form.WindowState = FormWindowState.Normal;
                    _frmTrend3.CreateSeriesByTrendGroup(part, group, "right");
                    return;
                }
            }
            _frmTrend3 = new FormTrend3(_mainForm);          
            _frmTrend3.Show();        
            _frmTrend3.Size = new Size(955, 1000);
            _frmTrend3.Location = new Point(956, 10);
            _frmTrend3.CreateSeriesByTrendGroup(part, group, "right");     
        }

        private int IsBoth(string part, string group)
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return -1;
            }

            string query = "SELECT COUNT(DP_NAME2) FROM C2_TREND_GROUP WHERE PART_NAME = :1 AND GROUP_NAME = :2";

            OracleCommand cmd = null;
            OracleDataReader reader = null;
            int count = 0;
            try
            {
                cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = part;
                cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = group;

                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        count = int.Parse(reader[0].ToString());
                    }
                }

                return count;
            }
            catch(Exception ex)
            {

                MessageBox.Show(ex.ToString());
                return -1;
            }
            finally
            {
                reader.Close();
                reader.Dispose();
                cmd.Dispose();
                Database.Close();
            }


            
           

           
        }
    }
}
