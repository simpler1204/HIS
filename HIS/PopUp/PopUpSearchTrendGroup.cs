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

namespace HIS.PopUp
{
    public partial class PopUpSearchTrendGroup : Form
    { 
        private DataTable _dtGroup = new DataTable("TrendGroup");
        private MainForm _mainForm = null;
        private string _distinct;

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

            if(isHIS == true)
            {
                
            }
        }
    }
}
