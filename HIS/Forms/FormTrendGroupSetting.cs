using HIS.Class;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using System.Reflection;

namespace HIS.Forms
{
    public partial class FormTrendGroupSetting : Form
    {
        private DataTable dtTrendSimpleInfo = new DataTable("TrendSimpleInfo");
        private DataTable dtTrendDetail = new DataTable("GroupDetail");

        private SplashScreenManager splashScreenManager1;

        public FormTrendGroupSetting()
        {
            InitializeComponent();
            Load += FormTrendGroupSetting_Load;
            menu1.ButtonClick += Menu1_ButtonClick;
            cmbPart.SelectedIndexChanged += CmbPart_SelectedIndexChanged;
            cmbGroup.SelectedIndexChanged += CmbGroup_SelectedIndexChanged;
            dgvTrendGroup.CellValueChanged += DgvTrendGroup_CellValueChanged;            
            menu2.ButtonClick += Menu2_ButtonClick;

            this.FormClosing += (sender, e) =>
            {
                Load += FormTrendGroupSetting_Load;
                menu1.ButtonClick -= Menu1_ButtonClick;
                cmbPart.SelectedIndexChanged -= CmbPart_SelectedIndexChanged;
                cmbGroup.SelectedIndexChanged -= CmbGroup_SelectedIndexChanged;
                menu2.ButtonClick -= Menu2_ButtonClick;
            };
            
        }

        private void DgvTrendGroup_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex < 3) return;

            string dpName = dgvTrendGroup[1, e.RowIndex].Value.ToString();
            float fValue = 0f;

            
            if(e.ColumnIndex == 3 && float.TryParse(dgvTrendGroup[3, e.RowIndex].Value.ToString(), out fValue))
            {
                var searched = dtTrendDetail.AsEnumerable()
                                  .Where(r => r.Field<string>("DP_NAME") == dpName);

                foreach (var s in searched)
                {
                    s["MIN"] = fValue;
                }
            }
            else if (e.ColumnIndex == 4 && float.TryParse(dgvTrendGroup[4, e.RowIndex].Value.ToString(), out fValue))
            {
                var searched = dtTrendDetail.AsEnumerable()
                                  .Where(r => r.Field<string>("DP_NAME") == dpName);

                foreach (var s in searched)
                {
                    s["MAX"] = fValue;
                }
            }
            else
            {
                MessageBox.Show("Information", "It is not number", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

        }

        private void Menu2_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;
            switch (buttonName)
            {
                case "Remove":
                    RemoveTrendGroupData();
                    break;
                case "Save":
                    deleteTrendDetail();
                    InsertTrendDetail();
                    MessageBox.Show("Saving Complete");
                    break;
            }
            
        }

        private void deleteTrendDetail()
        {
            if (!Database.Open()) return;
            string deleteQuery = "DELETE HMI_TREND_GROUP_DETAIL WHERE GROUP_NAME = :1";
            
            string groupName = cmbGroup.Text;
            try
            {
                using (OracleCommand cmd = new OracleCommand(deleteQuery, Database.OracleConn))
                {
                    cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = groupName;
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Database.Close();
            }
        }

        private void InsertTrendDetail()
        {
            if (!Database.Open()) return;           
            string insertQuery = "INSERT INTO HMI_TREND_GROUP_DETAIL(GROUP_NAME, DP_NAME, DP_DESC, MIN, MAX) " +
                                "VALUES(:1, :2, :3, :4, :5) ";
            string updateQuery = " UPDATE C2_TREND_INFO SET Y_MIN = :1, Y_MAX = :2 WHERE DP_NAME = :3";
            string groupName = cmbGroup.Text;
            try
            {
                using (OracleCommand cmd = new OracleCommand(insertQuery, Database.OracleConn))
                {
                    foreach (DataRow item in dtTrendDetail.Rows)
                    {
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = groupName;
                        cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = item["DP_NAME"];
                        cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = item["DP_DESC"];
                        cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = item["MIN"];
                        cmd.Parameters.Add(":5", OracleDbType.Varchar2).Value = item["MAX"];
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                }

                using (OracleCommand cmd = new OracleCommand(updateQuery, Database.OracleConn))
                {
                    foreach (DataRow item in dtTrendDetail.Rows)
                    {                        
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = item["MIN"];
                        cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = item["MAX"];
                        cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = item["DP_NAME"];
                        cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
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

        private void CmbPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            string partName = combo.SelectedItem.ToString();

            cmbGroup.Items.Clear();
            InsertTrendGroupToCmbGroup(partName);

        }

        private void CmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            string group = combo.SelectedItem.ToString();

            if (!Database.Open()) return;

            dtTrendDetail.Rows.Clear();

            string query = "SELECT GROUP_NAME, DP_NAME, DP_DESC, MIN, MAX FROM HMI_TREND_GROUP_DETAIL " +
                "WHERE GROUP_NAME = :1";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = group;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    int row = 0;
                    while(reader.Read())
                    {
                        DataRow dr = dtTrendDetail.NewRow();                
                        dr["GROUP_NAME"] = reader["GROUP_NAME"].ToString();                       
                        dr["DP_NAME"] = reader["DP_NAME"].ToString();
                        dr["DP_DESC"] = reader["DP_DESC"].ToString();
                        dr["MIN"] = reader["MIN"].ToString();
                        dr["MAX"] = reader["MAX"].ToString();                      

                        dtTrendDetail.Rows.Add(dr);
                        dgvTrendGroup.Rows[row].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                        row++;
                    }
                }

                InitDataGridView.AutoSettingDatagridView(dgvTrendGroup, new List<int>() { 1, 2 }, new List<int>());
                dgvTrendGroup.ReadOnly = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }

        }

        private void Menu1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;

            switch (buttonName)
            {
                case "Search":
                    SearchTrendInfo();
                    break;
                case "Move":
                    MappingTrendGroup();
                    break;
            }
        }

        private void SearchTrendInfo()
        {
            if (!Database.Open()) return;
            dtTrendSimpleInfo.Clear();
            splashScreenManager1.ShowWaitForm();

            string system = cmbSystem.Text == "ALL" ? "%" : cmbSystem.Text;
            string dpName = "%" + txtDpName.Text + "%";
            string dpDesc = "%" + txtDesc.Text + "%";
            string query = "SELECT SYSTEM, DP_NAME, DP_DESC, Y_MIN, Y_MAX, TB_NAME, COL_NAME " +
                "FROM C2_TREND_INFO  " +
                "WHERE SYSTEM LIKE :1 AND DP_NAME LIKE :2 AND NVL(DP_DESC, '%') LIKE :3 " +
                "ORDER BY SYSTEM ASC, DP_NAME ASC";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = system;
            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dpName;
            cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = dpDesc;

            try
            {
                OracleDataReader reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    int count = 0;
                    while(reader.Read())
                    {
                        DataRow row = dtTrendSimpleInfo.NewRow();
                        row["SYSTEM"] = reader["SYSTEM"];
                        row["DP_NAME"] = reader["DP_NAME"];
                        row["DP_DESC"] = reader["DP_DESC"];
                        row["Y_MIN"] = reader["Y_MIN"];
                        row["Y_MAX"] = reader["Y_MAX"];
                        row["TB_NAME"] = reader["TB_NAME"];
                        row["COL_NAME"] = reader["COL_NAME"];
                        dtTrendSimpleInfo.Rows.Add(row);

                        dgvTrendInfo.Rows[count].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                        count++;    
                    }
                }

                InitDataGridView.AutoSettingDatagridView(dgvTrendInfo, new List<int>() { 1, 2 }, new List<int>());
                
            }
            catch(Exception ex)
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

        private void FormTrendGroupSetting_Load(object sender, EventArgs e)
        {
            InitDatatable.Init(dtTrendSimpleInfo);
            InitDatatable.Init(dtTrendDetail);

            InitDataGridView.dataGridViewInit(dgvTrendInfo);
            InitDataGridView.dataGridViewInit(dgvTrendGroup);

            dgvTrendInfo.DataSource = dtTrendSimpleInfo;
            dgvTrendGroup.DataSource = dtTrendDetail;

            cmbSystem.Text = "ALL";

            splashScreenManager1 = new SplashScreenManager(this, typeof(global::HIS.Forms.WaitForm1), true, true);
            splashScreenManager1.ClosingDelay = 500;

            InsertPartNameToCmbPart();

            DataGridViewBufferExtension();

            SetDoNotSort(dgvTrendInfo);
            SetDoNotSort(dgvTrendGroup);

        }

        private void InsertPartNameToCmbPart()
        {
            if (!Database.Open()) return;

            string query = "SELECT DISTINCT PART_NAME FROM HMI_TREND_GROUP ";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);

            try
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

        private void InsertTrendGroupToCmbGroup(string partName)
        {
            if (partName == "") dgvTrendGroup.Rows.Clear();

            if (!Database.Open()) return;

            string query = "SELECT GROUP_NAME FROM HMI_TREND_GROUP  WHERE PART_NAME = :1 ";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = partName;


            try
            {
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        cmbGroup.Items.Add(reader["GROUP_NAME"].ToString());
                    }
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }
        }

        private void MappingTrendGroup()
        {
            if (dgvTrendInfo.Rows.Count < 1) return;
            string trendGroup = string.Empty;
            if (cmbGroup.Text == "")
            {
                MessageBox.Show("Select trend group");               
                cmbGroup.Focus();
                return;
            }

            trendGroup = cmbGroup.Text;
            if (trendGroup == "") return;           

            Int32 selectedCellCount = dgvTrendInfo.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                if (dgvTrendInfo.AreAllCellsSelected(true))
                {
                    MessageBox.Show("Can not select all cells", "Selected Cells", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {                    
                    for (int i = selectedCellCount - 1; i >= 0; i--)
                    {
                        if (dgvTrendInfo.SelectedCells[i].ColumnIndex.ToString() == "1")
                        {                          

                            int row = dgvTrendInfo.SelectedCells[i].RowIndex;
                            string dpName = dgvTrendInfo["DP_NAME", row].Value.ToString();
                            string dpDesc = dgvTrendInfo["DP_DESC", row].Value.ToString();
                            string yMin = dgvTrendInfo["Y_MIN", row].Value.ToString();
                            string yMax = dgvTrendInfo["Y_MAX", row].Value.ToString();

                            var searched = dtTrendDetail.AsEnumerable()
                                .Where(r => r.Field<string>("DP_NAME") == dpName);
                            
                            if(searched.Count() == 0)
                            {
                                DataRow dr = dtTrendDetail.NewRow();
                                dr["GROUP_NAME"] = trendGroup;
                                dr["DP_NAME"] = dpName;
                                dr["DP_DESC"] = dpDesc;
                                dr["MIN"] = yMin;
                                dr["MAX"] = yMax;
                                dtTrendDetail.Rows.Add(dr);
                                dgvTrendGroup.Rows[dgvTrendGroup.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;                              
                            }
                                                        
                        }
                    }
                }
            }
        }

        private void RemoveTrendGroupData()
        {
            DialogResult rtn = MessageBox.Show("Do You want to remove dp ?", "Warning", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (rtn == DialogResult.No) return;

            if (dgvTrendGroup.Rows.Count < 1) return;
            string trendGroup = string.Empty;
            if (cmbGroup.Text == "")
            {
                MessageBox.Show("Select trend group");
                cmbGroup.Focus();
                return;
            }

            trendGroup = cmbGroup.Text;
            if (trendGroup == "") return;

            Int32 selectedCellCount = dgvTrendGroup.GetCellCount(DataGridViewElementStates.Selected);

            if (selectedCellCount > 0)
            {
                if (dgvTrendGroup.AreAllCellsSelected(true))
                {
                    MessageBox.Show("Can not select all cells", "Selected Cells", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    for (int i = selectedCellCount - 1; i >= 0; i--)
                    {
                        dgvTrendGroup.Refresh();

                        if (dgvTrendGroup.SelectedCells[i].ColumnIndex.ToString() == "1")
                        {  
                            int row = dgvTrendGroup.SelectedCells[i].RowIndex;
                            string dpName = dgvTrendGroup["DP_NAME", row].Value.ToString();

                            dgvTrendGroup.Rows.RemoveAt(row);                          
                        }
                    }
                }
            }
        }

        private void DataGridViewBufferExtension()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
            null, this.dgvTrendGroup, new object[] { true });
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
