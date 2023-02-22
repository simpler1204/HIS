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
    public partial class PopUpPart : Form
    {
        DataTable _dtpart = new DataTable("PartTable");
        private string _pageName;
        public event Action<string> _action;

        public PopUpPart(string pageName)
        {
            InitializeComponent();

            _pageName = pageName;
            lblPage.Text = pageName;
            btnSearch.ButtonClick += BtnSearch_ButtonClick;
            InitDataTable(_dtpart);
            Database.CreateDatabase();
            dataGridView1.DataSource = _dtpart;

            GetPart();
           
        }

        private void GetPart()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dtpart.Rows.Clear();

            string partName = txtPart.Text.Trim() == "" ? "%" : "%"+ txtPart.Text +"%";
            string query = "SELECT DISTINCT PART_NAME FROM C2_TREND_GROUP WHERE PAGE_NAME LIKE :1 AND PART_NAME LIKE :2 ";
            OracleCommand cmd = null;
            OracleDataReader reader = null;

            try
            {
                cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = _pageName;
                cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = partName;

                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DataRow dr = _dtpart.NewRow();
                        dr["PART_NAME"] = reader["PART_NAME"];
                        _dtpart.Rows.Add(dr);
                    }
                }

                DecorateDataGridView(dataGridView1);
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
               

        private void BtnSearch_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            GetPart();
        }


        private void InitDataTable(DataTable dt)
        {
            if(dt.TableName == "PartTable")
            {
                dt.Columns.Add("PART_NAME", typeof(string));
            }
        }

        private void DecorateDataGridView(DataGridView dg)
        {
            dg.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            dg.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dg.EnableHeadersVisualStyles = false;

            dg.Columns["PART_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg.Columns["PART_NAME"].ReadOnly = false;
            dg.Columns["PART_NAME"].ValueType = typeof(bool);
            dg.Columns["PART_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["PART_NAME"].DefaultCellStyle.ForeColor = Color.White;

            //dg.Columns["SEQ"].Width = 40;
            //dg.Columns["SEQ"].ReadOnly = true;
            //dg.Columns["SEQ"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            //dg.Columns["SEQ"].DefaultCellStyle.ForeColor = Color.White;

            //dg.Columns["USER_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dg.Columns["USER_NAME"].ReadOnly = false;
            //dg.Columns["USER_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            //dg.Columns["USER_NAME"].DefaultCellStyle.ForeColor = Color.White;
            //dg.Columns["USER_NAME"].HeaderText = "NAME";


            //dg.Columns["PHONE_NUMBER"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dg.Columns["PHONE_NUMBER"].ReadOnly = false;
            //dg.Columns["PHONE_NUMBER"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            //dg.Columns["PHONE_NUMBER"].DefaultCellStyle.ForeColor = Color.White;
            //dg.Columns["PHONE_NUMBER"].HeaderText = "PHONE";

            //dg.Columns["WORK_NO"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //dg.Columns["WORK_NO"].ReadOnly = false;
            //dg.Columns["WORK_NO"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            //dg.Columns["WORK_NO"].DefaultCellStyle.ForeColor = Color.White;

            //dg.Columns["RECEIVE_YN"].Width = 60;
            //dg.Columns["RECEIVE_YN"].ReadOnly = false;
            //dg.Columns["RECEIVE_YN"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            //dg.Columns["RECEIVE_YN"].DefaultCellStyle.ForeColor = Color.White;
            //dg.Columns["RECEIVE_YN"].HeaderText = "ACTIVE";
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow.Index < 0) return;
            _action(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());

            this.Close();
        }
    }
}
