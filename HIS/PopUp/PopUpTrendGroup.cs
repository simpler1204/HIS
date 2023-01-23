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

namespace HIS.PopUp
{
    public partial class PopUpTrendGroup : Form
    {

        public delegate void SelectTrendGroup(string title);
        public event SelectTrendGroup selectGroup;
        DataTable dtGroupDetail = new DataTable("GroupDetail");

        public PopUpTrendGroup()
        {
            InitializeComponent();
            menu.ButtonClick += (sender, e) =>
            {
                if (cmbGroup.Text.Length > 0 && dtGroupDetail.Rows.Count > 0)
                {
                    this.WindowState = FormWindowState.Minimized;
                    selectGroup(cmbGroup.Text);
                }

                this.Close();
               
            };

            Load += PopUpTrendGroup_Load;
                                   

            cmbPart.SelectedIndexChanged += (sender, e) =>
            {
                ComboBox combo = sender as ComboBox;
                string partName = combo.Text;               
                InsertGroupToCombo(partName);
            };

            cmbGroup.SelectedIndexChanged += (sender, e) =>
            {
                ComboBox combo = sender as ComboBox;
                string groupName = combo.Text;                
                SelectTrendGroupDetail(groupName);
            };
           
        }

        private void SelectTrendGroupDetail(string groupName)
        {
            if (!Database.Open()) return;

            dtGroupDetail.Rows.Clear();

            string query = "SELECT DP_NAME, DP_DESC, MIN, MAX FROM HMI_TREND_GROUP_DETAIL " +
                "WHERE GROUP_NAME = :1 ";          
            try
            {
                using (OracleCommand cmd = new OracleCommand(query, Database.OracleConn))
                {
                    cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = groupName;
                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        int row = 0;
                        while (reader.Read())
                        {
                            DataRow dr = dtGroupDetail.NewRow();
                            dr["GROUP_NAME"] = groupName;
                            dr["DP_NAME"] = reader["DP_NAME"];
                            dr["DP_DESC"] = reader["DP_DESC"];
                            dr["MIN"] = reader["MIN"];
                            dr["MAX"] = reader["MAX"];
                            dtGroupDetail.Rows.Add(dr);
                            dataGridView1.Rows[row].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                            row++;
                        }
                    }

                    InitDataGridView.AutoSettingDatagridView(dataGridView1, new List<int>() { 1, 2 }, new List<int>());
                }              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Database.Close();
            }
        }

        private void PopUpTrendGroup_Load(object sender, EventArgs e)
        {
            //DataTabele 초기하
            InitDatatable.Init(dtGroupDetail);
            dataGridView1.DataSource = dtGroupDetail;

            //DataGridview 초기화
            InitDataGridView.dataGridViewInit(dataGridView1);

            //part 가져와서 콤보박스에 추가
            InsertPartToCombo();


        }

        private void InsertGroupToCombo(string partName)
        {
            if (!Database.Open()) return;
            string query = "SELECT GROUP_NAME FROM HMI_TREND_GROUP WHERE PART_NAME = :1  ";
            cmbGroup.Items.Clear();
            try
            {
                using (OracleCommand cmd = new OracleCommand(query, Database.OracleConn))
                {
                    cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = partName;
                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cmbGroup.Items.Add(reader["GROUP_NAME"].ToString());
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Database.Close();
            }
        }

        private void InsertPartToCombo()
        {
            if (!Database.Open()) return;
            string query = "SELECT DISTINCT PART_NAME FROM HMI_TREND_GROUP  ";

            try
            {
                using (OracleCommand cmd = new OracleCommand(query, Database.OracleConn))
                {
                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            cmbPart.Items.Add(reader["PART_NAME"].ToString());
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Database.Close();
            }
        }

        
    }
}
