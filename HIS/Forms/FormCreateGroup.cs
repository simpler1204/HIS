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
using HIS.PopUp;
using System.Reflection;

namespace HIS.Forms
{
    public partial class FormCreateGroup : Form
    {
        DataTable dtGroupMaster = new DataTable("GroupMaster");
      
        public FormCreateGroup()
        {
            InitializeComponent();
            Load += FormCreateGroup_Load;
            menu.ButtonClick += Menu_ButtonClick;

            this.FormClosing += (sender, e) =>
            {
                Load -= FormCreateGroup_Load;
                menu.ButtonClick -= Menu_ButtonClick;
            };
        }

        private void RemoveTrendGroup()
        {
            int row = dgvMaster.CurrentCell.RowIndex;
            if (row < 0) return;
            
            string group = dgvMaster[2, row].Value.ToString();

            DialogResult rtn =  MessageBox.Show($"Do you want to remove {group} ?", "Warning", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rtn == DialogResult.No) return;

            if (!Database.Open()) return;

            string queryMaster = "DELETE HMI_TREND_GROUP WHERE GROUP_NAME = :1 ";
            string queryDetail = "DELETE HMI_TREND_GROUP_DETAIL WHERE GROUP_NAME = :1";

            OracleCommand cmdMaster = new OracleCommand(queryMaster, Database.OracleConn);
            OracleCommand cmdDetail = new OracleCommand(queryDetail, Database.OracleConn);
            cmdMaster.Parameters.Add(":1", OracleDbType.Varchar2).Value = group;
            cmdDetail.Parameters.Add(":1", OracleDbType.Varchar2).Value = group;

            try
            {
                cmdMaster.ExecuteNonQuery();
                cmdDetail.ExecuteNonQuery();
                dgvMaster.Rows.RemoveAt(row);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmdMaster.Dispose();
                cmdDetail.Dispose();
                Database.Close();
            }

        }

        private void Menu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;

            switch(buttonName)
            {
                case "Search":                  
                    SearchGroupMaster();                    
                    break;
                case "Save":
                    SaveTrendGroup();
                    break;
                case "Create":
                    CreateNewGroup();                    
                    break;
                case "Remove":
                    RemoveTrendGroup();
                    break;
            }
        }

        private void SaveTrendGroup()
        {
            DialogResult rtn = MessageBox.Show("Do you want creat new trend group?", "Infomation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            if (rtn == DialogResult.No) return;               

            string query = "INSERT INTO HMI_TREND_GROUP(PART_NAME, GROUP_NAME, GROUP_DESC) " +
                        "VALUES(:1, :2, :3)";
            

            foreach (DataRow item in dtGroupMaster.Rows)
            {
                if (!Database.Open()) break;

                if (item["PART"].ToString().Trim() == "") continue;
                if (item["GROUP"].ToString().Trim() == "") continue;

                try
                {
                    if (item["MODIFIED"].ToString() == "M")
                    {
                        string part = item["PART"].ToString();
                        string group = item["GROUP"].ToString();
                        string desc = item["DESC"].ToString();

                        OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = part;
                        cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = group;
                        cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = desc;
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new ApplicationException(ex.Message, ex);
                        }
                        finally
                        {
                            cmd.Dispose();
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    Database.Close();
                    dgvMaster.ReadOnly = true;
                }

                int row = int.Parse(item["SEQ"].ToString());
                dgvMaster.Rows[row-1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                item["MODIFIED"] = "N";
            }
        }

        private void CreateNewGroup()
        {
            DataRow row = dtGroupMaster.NewRow();
            row["SEQ"] = dtGroupMaster.Rows.Count + 1;
           
            row["MODIFIED"] = "M";
            dtGroupMaster.Rows.Add(row);
            dgvMaster.Rows[dgvMaster.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;

            dgvMaster.ReadOnly = false;

        }

       
        private void SearchGroupMaster()
        {
            if (!Database.Open()) return;

            dtGroupMaster.Clear();

            string groupName = "%" + txtGroup.Text + "%";
            string groupDesc = "%" + txtDesc.Text + "%";

            string query = "SELECT PART_NAME, GROUP_NAME, GROUP_DESC " +
                "FROM HMI_TREND_GROUP WHERE GROUP_NAME LIKE :1 AND NVL(GROUP_DESC, '%') LIKE :2";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = groupName;
            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = groupDesc;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int count = 1;
                    while (reader.Read())
                    {
                        DataRow row = dtGroupMaster.NewRow();
                        row["SEQ"] = count.ToString();
                        row["PART"] = reader["PART_NAME"].ToString();
                        row["GROUP"] = reader["GROUP_NAME"].ToString();
                        row["DESC"] = reader["GROUP_DESC"].ToString();
                        row["MODIFIED"] = "N";
                        dtGroupMaster.Rows.Add(row);
                        dgvMaster.Rows[count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                        count++;
                    }
                }

                InitDataGridView.AutoSettingDatagridView(dgvMaster, new List<int>() { 2, 3 }, new List<int>() { 4 });
                dgvMaster.Columns["SEQ"].Width = 50;
                dgvMaster.Columns["SEQ"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
               
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

        }

        private void FormCreateGroup_Load(object sender, EventArgs e)
        {
            InitDatatable.Init(dtGroupMaster);          

            InitDataGridView.dataGridViewInit(dgvMaster);          

            dgvMaster.DataSource = dtGroupMaster;

            SearchGroupMaster();

            DataGridViewBufferExtension();

            SetDoNotSort(dgvMaster);

        }

        private void DataGridViewBufferExtension()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
            null, this.dgvMaster, new object[] { true });
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
