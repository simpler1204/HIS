using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using HIS.Class;
using Oracle.ManagedDataAccess.Client;
using System.Reflection;
using System.Threading;

namespace HIS.Forms
{
    public partial class FormsTrendGroupSetting : Form
    {
        private readonly string _dpName;
        private DataTable _dtTrendDpList = new DataTable("TrendDpList");
        private DataTable _dtFirstTrendGroup = new DataTable("FirstTrendGroup");
        private DataTable _dtSecondTrendGroup = new DataTable("SecondTrendGroup");
        private string _pageName = string.Empty;
        private string _partName = string.Empty;
        private string _groupName = string.Empty;
        private bool dg1AllSelected = false;
        private bool dg2AllSelected = false;
        private bool dg3AllSelected = false;
        private bool isNew = false;


        public FormsTrendGroupSetting(string dpName)
        {
            InitializeComponent();
            this._dpName = dpName;

            Database.CreateDatabase();

            this.Resize += FormsTrendGroupSetting_Resize;
            btnFirst.ButtonClick += BtnFirst_ButtonClick;
            btnSecond.ButtonClick += BtnSecond_ButtonClick;
            btnSave.ButtonClick += BtnSave_ButtonClick;

            cmbPart.SelectedValueChanged += CmbPart_SelectedValueChanged;
            cmbGroup.SelectedIndexChanged += CmbGroup_SelectedIndexChanged;

            dataGridView1.ColumnHeaderMouseClick += DataGridView1_ColumnHeaderMouseClick;
            dataGridView2.ColumnHeaderMouseClick += DataGridView2_ColumnHeaderMouseClick;
            dataGridView3.ColumnHeaderMouseClick += DataGridView3_ColumnHeaderMouseClick;

            DataGridViewBufferExtension();

            DefineDataTable();
            FileCopy();
            MakeComboPart();
            //SearchFirstDpList();
            //SearchSecondDpList();
            //ReadDataPoint();
            //SearchTrendDpList();

        }

        private void DataGridView3_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_dtSecondTrendGroup.Rows.Count < 1) return;
            if (dg3AllSelected == false)
            {
                foreach (DataRow dr in _dtSecondTrendGroup.Rows)
                {
                    dr["CHK"] = true;
                }
                dg3AllSelected = true;
            }
            else
            {
                foreach (DataRow dr in _dtSecondTrendGroup.Rows)
                {
                    dr["CHK"] = false;
                }
                dg3AllSelected = false;
            }
        }

        private void DataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_dtFirstTrendGroup.Rows.Count < 1) return;
            if (dg2AllSelected == false)
            {
                foreach (DataRow dr in _dtFirstTrendGroup.Rows)
                {
                    dr["CHK"] = true;
                }
                dg2AllSelected = true;
            }
            else
            {
                foreach (DataRow dr in _dtFirstTrendGroup.Rows)
                {
                    dr["CHK"] = false;
                }
                dg2AllSelected = false;
            }
        }

        private void DataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_dtTrendDpList.Rows.Count < 1) return;
            if (dg1AllSelected == false)
            {
                foreach (DataRow dr in _dtTrendDpList.Rows)
                {
                    dr["CHK"] = true;
                }
                dg1AllSelected = true;
            }
            else
            {
                foreach (DataRow dr in _dtTrendDpList.Rows)
                {
                    dr["CHK"] = false;
                }
                dg1AllSelected = false;
            }
        }



        private void CmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            _groupName = cmbGroup.Text;

            SearchFirstDpList();
            SearchSecondDpList();
            ReadDataPoint();
            SearchTrendDpList();
        }

        private void CmbPart_SelectedValueChanged(object sender, EventArgs e)
        {
            _partName = cmbPart.Text;

            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT DISTINCT GROUP_NAME FROM C2_TREND_GROUP WHERE PAGE_NAME = :1 AND PART_NAME = :2";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = _pageName;
            cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = _partName;
            OracleDataReader reader = cmd.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    cmbGroup.Items.Clear();
                    while (reader.Read())
                    {
                        cmbGroup.Items.Add(reader[0].ToString());
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

        private void MakeComboPart()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = "SELECT DISTINCT PART_NAME FROM C2_TREND_GROUP WHERE PAGE_NAME = :1";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", _pageName);
            OracleDataReader reader = cmd.ExecuteReader();
            try
            {

                if (reader.HasRows)
                {
                    cmbPart.Items.Clear();
                    while (reader.Read())
                    {
                        cmbPart.Items.Add(reader[0].ToString());
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

        private void BtnSecond_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Move")
            {
                MoveToSecond();
            }
            else if (e.Button.Properties.Caption == "Back")
            {
                BackToListFromSecond();
            }
        }

        private void SearchFirstDpList()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            string query = @"SELECT PAGE_NAME, DP_NAME1 FROM C2_TREND_GROUP WHERE PAGE_NAME = :1 
                             AND PART_NAME = :2 AND GROUP_NAME = :3 AND DP_NAME1 IS NOT NULL";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = _pageName;
            cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = _partName;
            cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = _groupName;
            OracleDataReader reader = cmd.ExecuteReader();
            int count = 1;
            try
            {
                if (_dtFirstTrendGroup.Rows.Count > 0) _dtFirstTrendGroup.Rows.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DataRow dr = _dtFirstTrendGroup.NewRow();
                        dr["CHK"] = false;
                        dr["SEQ"] = count;
                        dr["PAGE_NAME"] = reader["PAGE_NAME"].ToString();
                        dr["DP_NAME"] = reader["DP_NAME1"].ToString();
                        _dtFirstTrendGroup.Rows.Add(dr);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                reader.Close();
                reader.Dispose();
                Database.Close();
            }

            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.EnableHeadersVisualStyles = false;

            dataGridView2.Columns["CHK"].Width = 35; //CHK
            dataGridView2.Columns["CHK"].ReadOnly = false;
            dataGridView2.Columns["CHK"].ValueType = typeof(bool);
            dataGridView2.Columns["CHK"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView2.Columns["CHK"].DefaultCellStyle.ForeColor = Color.White;


            dataGridView2.Columns["SEQ"].Width = 40;
            dataGridView2.Columns["SEQ"].ReadOnly = true;
            dataGridView2.Columns["SEQ"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView2.Columns["SEQ"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView2.Columns["PAGE_NAME"].Width = 100;
            dataGridView2.Columns["PAGE_NAME"].ReadOnly = true;
            dataGridView2.Columns["PAGE_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView2.Columns["PAGE_NAME"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView2.Columns["DP_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns["DP_NAME"].ReadOnly = true;
            dataGridView2.Columns["DP_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView2.Columns["DP_NAME"].DefaultCellStyle.ForeColor = Color.White;
        }

        private void SearchSecondDpList()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"SELECT PAGE_NAME, DP_NAME2 FROM C2_TREND_GROUP WHERE PAGE_NAME = :1
                             AND PART_NAME = :2 AND GROUP_NAME = :3 AND DP_NAME2 IS NOT NULL";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = _pageName;
            cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = _partName;
            cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = _groupName;
            OracleDataReader reader = cmd.ExecuteReader();
            int count = 1;
            try
            {
                if (_dtSecondTrendGroup.Rows.Count > 0) _dtSecondTrendGroup.Rows.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        DataRow dr = _dtSecondTrendGroup.NewRow();
                        dr["CHK"] = false;
                        dr["SEQ"] = count;
                        dr["PAGE_NAME"] = reader["PAGE_NAME"].ToString();
                        dr["DP_NAME"] = reader["DP_NAME2"].ToString();
                        _dtSecondTrendGroup.Rows.Add(dr);
                        count++;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                cmd.Dispose();
                reader.Close();
                reader.Dispose();
                Database.Close();
            }

            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView3.EnableHeadersVisualStyles = false;

            dataGridView3.Columns["CHK"].Width = 35; //CHK
            dataGridView3.Columns["CHK"].ReadOnly = false;
            dataGridView3.Columns["CHK"].ValueType = typeof(bool);
            dataGridView3.Columns["CHK"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView3.Columns["CHK"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView3.Columns["SEQ"].Width = 40;
            dataGridView3.Columns["SEQ"].ReadOnly = true;
            dataGridView3.Columns["SEQ"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView3.Columns["SEQ"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView3.Columns["PAGE_NAME"].Width = 100;
            dataGridView3.Columns["PAGE_NAME"].ReadOnly = true;
            dataGridView3.Columns["PAGE_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView3.Columns["PAGE_NAME"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView3.Columns["DP_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.Columns["DP_NAME"].ReadOnly = true;
            dataGridView3.Columns["DP_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView3.Columns["DP_NAME"].DefaultCellStyle.ForeColor = Color.White;
        }

        private void BtnSave_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Save")
            {
                if (SaveGroup() == true)
                {
                    txtGroup.Visible = false;
                    txtPart.Visible = false;
                    cmbPart.Visible = true;
                    cmbGroup.Visible = true;
                    MakeComboPart();

                    cmbPart.Text = _partName;
                    cmbGroup.Text = _groupName;

                    isNew = false;
                }
            }
            else if (e.Button.Properties.Caption == "New")
            {
                _dtFirstTrendGroup.Rows.Clear();
                _dtSecondTrendGroup.Rows.Clear();
                txtGroup.Text = "";
                txtPart.Text = "";

                ReadDataPoint();
                SearchTrendDpList();

                txtGroup.Visible = true;
                txtPart.Visible = true;
                cmbPart.Visible = false;
                cmbGroup.Visible = false;
                isNew = true;
            }
            else if (e.Button.Properties.Caption == "Cancel")
            {
                if (isNew)
                {
                    txtGroup.Visible = false;
                    txtPart.Visible = false;
                    cmbPart.Visible = true;
                    cmbGroup.Visible = true;
                    isNew = false;
                }
            }
            else if (e.Button.Properties.Caption == "Remove")
            {
                if (isNew) return;
                if (_pageName == string.Empty) return;
                if (_partName == string.Empty) return;
                if (_groupName == string.Empty) return;

                if (!Database.Open())
                {
                    MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE C2_TREND_GROUP WHERE PAGE_NAME = :1 AND PART_NAME = :2 AND GROUP_NAME = :3";
                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = _pageName;
                cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = _partName;
                cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = _groupName;

                try
                {
                    cmd.ExecuteNonQuery();
                    _dtFirstTrendGroup.Rows.Clear();
                    _dtSecondTrendGroup.Rows.Clear();
                    _dtTrendDpList.Rows.Clear();
                    MakeComboPart();
                    cmbGroup.Items.Clear();
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

        private bool SaveGroup()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int firstCount = _dtFirstTrendGroup.Rows.Count;
            int secondCount = _dtSecondTrendGroup.Rows.Count;
            int totalCount = firstCount + secondCount;
            int maxCount = firstCount > secondCount ? firstCount : secondCount;

            if (totalCount < 1) return false;
            if (firstCount > 60)
            {
                MessageBox.Show("You can not exceed 60.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (secondCount > 60)
            {
                MessageBox.Show("You can not exceed 60.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (isNew == true)
            {
                if (txtPart.Text.Trim() == "")
                {
                    MessageBox.Show("Fill the part name");
                    txtPart.Focus();
                    return false;
                }

                if (txtGroup.Text.Trim() == "")
                {
                    MessageBox.Show("Fill the group name");
                    txtGroup.Focus();
                    return false;
                }

                _partName = txtPart.Text;
                _groupName = txtGroup.Text;
            }
            else
            {
                _partName = cmbPart.Text;
                _groupName = cmbGroup.Text;
            }


            string deleteQuery = "DELETE C2_TREND_GROUP WHERE PAGE_NAME = :1 AND PART_NAME = :2 AND GROUP_NAME = :3";

            OracleCommand cmd = new OracleCommand(deleteQuery, Database.OracleConn);

            cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = _pageName;
            cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = _partName;
            cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = _groupName;

            cmd.ExecuteNonQuery();
            cmd.CommandText = null;
            cmd.Parameters.Clear();


            string InsertQuery = @"INSERT INTO c2_trend_group(page_name, part_name, group_name, dp_name1, sys_name1, dp_name2, sys_name2)
                                   VALUES(:1, :2, :3, :4, :5, :6, :7)";

            cmd.CommandText = InsertQuery;
            

            try
            {
                for (int i = 1; i <= maxCount; i++)
                {
                    cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = _pageName;
                    cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = _partName;
                    cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = _groupName;
                    if (dataGridView2.Rows.Count >= i)
                    {
                        cmd.Parameters.Add(":4", OracleDbType.NVarchar2).Value = dataGridView2[3, i - 1].Value.ToString();
                        cmd.Parameters.Add(":6", OracleDbType.NVarchar2).Value = GetSystemName(dataGridView2[3, i - 1].Value.ToString());
                    }
                    else
                    {
                        cmd.Parameters.Add(":4", OracleDbType.NVarchar2).Value = null;
                        cmd.Parameters.Add(":6", OracleDbType.NVarchar2).Value = null;
                    }

                    if (dataGridView3.Rows.Count >= i)
                    {
                        cmd.Parameters.Add(":5", OracleDbType.NVarchar2).Value = dataGridView3[3, i - 1].Value.ToString();
                        cmd.Parameters.Add(":7", OracleDbType.NVarchar2).Value = GetSystemName(dataGridView3[3, i - 1].Value.ToString());
                    }
                    else
                    {
                        cmd.Parameters.Add(":5", OracleDbType.NVarchar2).Value = null;
                        cmd.Parameters.Add(":7", OracleDbType.NVarchar2).Value = null;
                    }

                    cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                cmd.Dispose();
                Database.Close();
            }
        }

        private string GetSystemName(string dp)
        {           
            string query = "SELECT SYSTEM FROM C2_TREND_INFO WHERE DP_NAME = :1";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dp;

            string systemName = null;
            try
            {
                OracleDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        systemName = reader[0].ToString();
                    }
                }
                return systemName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
           

        }

        private void BtnFirst_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Move")
            {
                MoveToFirst();
            }
            else if (e.Button.Properties.Caption == "Back")
            {
                BackToListFromFirst();
            }
        }

        private void MoveToFirst()
        {
            if (_dtTrendDpList.Rows.Count < 1) return;

            List<DataRow> rows = new List<DataRow>();

            //이동
            foreach (DataRow dr in _dtTrendDpList.Rows)
            {
                if (Convert.ToBoolean(dr["CHK"]) == true)
                {
                    DataRow firstdr = _dtFirstTrendGroup.NewRow();
                    firstdr["CHK"] = false;
                    firstdr["SEQ"] = _dtFirstTrendGroup.Rows.Count + 1;
                    firstdr["PAGE_NAME"] = dr["PAGE_NAME"].ToString();
                    firstdr["DP_NAME"] = dr["DP_NAME"].ToString();

                    rows.Add(dr);
                    _dtFirstTrendGroup.Rows.Add(firstdr);
                }
            }


            dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView2.EnableHeadersVisualStyles = false;

            dataGridView2.Columns["CHK"].Width = 35; //CHK
            dataGridView2.Columns["CHK"].ReadOnly = false;
            dataGridView2.Columns["CHK"].ValueType = typeof(bool);
            dataGridView2.Columns["CHK"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView2.Columns["CHK"].DefaultCellStyle.ForeColor = Color.White;


            dataGridView2.Columns["SEQ"].Width = 40;
            dataGridView2.Columns["SEQ"].ReadOnly = true;
            dataGridView2.Columns["SEQ"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView2.Columns["SEQ"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView2.Columns["PAGE_NAME"].Width = 100;
            dataGridView2.Columns["PAGE_NAME"].ReadOnly = true;
            dataGridView2.Columns["PAGE_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView2.Columns["PAGE_NAME"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView2.Columns["DP_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.Columns["DP_NAME"].ReadOnly = true;
            dataGridView2.Columns["DP_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView2.Columns["DP_NAME"].DefaultCellStyle.ForeColor = Color.White;

            //이동 후 삭제
            foreach (DataRow r in rows)
            {
                _dtTrendDpList.Rows.Remove(r);
            }

            //삭제 후 seq 다시 채번
            int seq = 1;
            foreach (DataRow r in _dtTrendDpList.Rows)
            {
                r["SEQ"] = seq++;
            }

            if (dataGridView2.Rows.Count > 0)
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.Rows.Count - 1;
        }

        private void BackToListFromFirst()
        {
            if (_dtFirstTrendGroup.Rows.Count < 1) return;

            List<DataRow> rows = new List<DataRow>();

            //이동
            foreach (DataRow dr in _dtFirstTrendGroup.Rows)
            {
                if (Convert.ToBoolean(dr["CHK"]) == true)
                {
                    DataRow listdr = _dtTrendDpList.NewRow();
                    listdr["CHK"] = false;
                    listdr["SEQ"] = _dtFirstTrendGroup.Rows.Count + 1;
                    listdr["PAGE_NAME"] = dr["PAGE_NAME"].ToString();
                    listdr["DP_NAME"] = dr["DP_NAME"].ToString();

                    rows.Add(dr);
                    _dtTrendDpList.Rows.Add(listdr);
                }
            }

            //이동 후 삭제
            foreach (DataRow r in rows)
            {
                _dtFirstTrendGroup.Rows.Remove(r);
            }

            //삭제 후 seq 다시 채번
            int seq = 1;
            foreach (DataRow r in _dtTrendDpList.Rows)
            {
                r["SEQ"] = seq++;
            }

            seq = 1;
            foreach (DataRow r in _dtFirstTrendGroup.Rows)
            {
                r["SEQ"] = seq++;
            }

            if (dataGridView1.Rows.Count > 0)
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
        }

        private void MoveToSecond()
        {
            if (_dtTrendDpList.Rows.Count < 1) return;

            List<DataRow> rows = new List<DataRow>();

            //이동
            foreach (DataRow dr in _dtTrendDpList.Rows)
            {
                if (Convert.ToBoolean(dr["CHK"]) == true)
                {
                    DataRow seconddr = _dtSecondTrendGroup.NewRow();
                    seconddr["CHK"] = false;
                    seconddr["SEQ"] = _dtSecondTrendGroup.Rows.Count + 1;
                    seconddr["PAGE_NAME"] = dr["PAGE_NAME"].ToString();
                    seconddr["DP_NAME"] = dr["DP_NAME"].ToString();

                    rows.Add(dr);
                    _dtSecondTrendGroup.Rows.Add(seconddr);
                }
            }

            dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
            dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dataGridView3.EnableHeadersVisualStyles = false;

            dataGridView3.Columns["CHK"].Width = 35; //CHK
            dataGridView3.Columns["CHK"].ReadOnly = false;
            dataGridView3.Columns["CHK"].ValueType = typeof(bool);
            dataGridView3.Columns["CHK"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView3.Columns["CHK"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView3.Columns["SEQ"].Width = 40;
            dataGridView3.Columns["SEQ"].ReadOnly = true;
            dataGridView3.Columns["SEQ"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView3.Columns["SEQ"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView3.Columns["PAGE_NAME"].Width = 100;
            dataGridView3.Columns["PAGE_NAME"].ReadOnly = true;
            dataGridView3.Columns["PAGE_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView3.Columns["PAGE_NAME"].DefaultCellStyle.ForeColor = Color.White;

            dataGridView3.Columns["DP_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.Columns["DP_NAME"].ReadOnly = true;
            dataGridView3.Columns["DP_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dataGridView3.Columns["DP_NAME"].DefaultCellStyle.ForeColor = Color.White;

            //이동 후 삭제
            foreach (DataRow r in rows)
            {
                _dtTrendDpList.Rows.Remove(r);
            }

            //삭제 후 seq 다시 채번
            int seq = 1;
            foreach (DataRow r in _dtTrendDpList.Rows)
            {
                r["SEQ"] = seq++;
            }

            if (dataGridView3.Rows.Count > 0)
                dataGridView3.FirstDisplayedScrollingRowIndex = dataGridView3.Rows.Count - 1;
        }

        private void BackToListFromSecond()
        {
            if (_dtSecondTrendGroup.Rows.Count < 1) return;

            List<DataRow> rows = new List<DataRow>();

            //이동
            foreach (DataRow dr in _dtSecondTrendGroup.Rows)
            {
                if (Convert.ToBoolean(dr["CHK"]) == true)
                {
                    DataRow listdr = _dtTrendDpList.NewRow();
                    listdr["CHK"] = false;
                    listdr["SEQ"] = _dtSecondTrendGroup.Rows.Count + 1;
                    listdr["PAGE_NAME"] = dr["PAGE_NAME"].ToString();
                    listdr["DP_NAME"] = dr["DP_NAME"].ToString();

                    rows.Add(dr);
                    _dtTrendDpList.Rows.Add(listdr);
                }
            }

            //이동 후 삭제
            foreach (DataRow r in rows)
            {
                _dtSecondTrendGroup.Rows.Remove(r);
            }

            //삭제 후 seq 다시 채번
            int seq = 1;
            foreach (DataRow r in _dtTrendDpList.Rows)
            {
                r["SEQ"] = seq++;
            }

            seq = 1;
            foreach (DataRow r in _dtSecondTrendGroup.Rows)
            {
                r["SEQ"] = seq++;
            }

            if (dataGridView1.Rows.Count > 0)
                dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows.Count - 1;
        }

        private void FormsTrendGroupSetting_Resize(object sender, EventArgs e)
        {
            Point dg2 = dataGridView2.Location;
            Point dg3 = dataGridView3.Location;

            Point bf = btnFirst.Location;
            Point bs = btnSecond.Location;

            Point newDg2 = new Point(bf.X, dg2.Y + 20);
            btnFirst.Location = newDg2;

            Point newDg3 = new Point(bs.X, dg3.Y + 20);
            btnSecond.Location = newDg3;

        }

        private void DefineDataTable()
        {
            InitDatatable.Init(_dtTrendDpList);
            InitDatatable.Init(_dtFirstTrendGroup);
            InitDatatable.Init(_dtSecondTrendGroup);

            dataGridView1.DataSource = _dtTrendDpList;
            dataGridView2.DataSource = _dtFirstTrendGroup;
            dataGridView3.DataSource = _dtSecondTrendGroup;
        }

        private void FileCopy()
        {
            // XML 파일 생성
            string path = FindPath(_dpName);
            string xmlFileName = _dpName.Replace(".pnl", ".xml");
            ConvertToXml(path);
            int repeatCount = 0;

            //XML파일 생성하는데 시간이 조금 소요되어 기다림
            while (!File.Exists(xmlFileName) || repeatCount > 10) //xml 파일이 생성될때 까지
            {               
                Thread.Sleep(500);
                repeatCount++;
            }

            //TempFile폴더가 없으면 생성
            if (Directory.Exists("TempFile") == false)
            {
                Directory.CreateDirectory("TempFile");
            }
            
            //기존 temp.xml파일이 있으면 삭제
            if(File.Exists(@"TempFile\temp.xml"))
            {
                File.Delete(@"TempFile\temp.xml");
            }

            //WinccOA panel/screen 폴더에서 이동
            File.Move(xmlFileName, @"TempFile\temp.xml");           

            string[] str1 = _dpName.Split('/');
            _partName = str1[str1.Length - 2];
            string[] str2 = str1.Last().Split('.');
            _pageName = str2.First();
            this.lblPageName.Text = _pageName;            
        }

        private string FindPath(string path)
        {
            string[] paths = path.Split('/');
            string splitPath = string.Empty;
            bool start = false;
            int startNum = 0;
            for (int i = 0; i < paths.Length; i++)
            {
                if (start)
                {
                    if (startNum == 0)
                        splitPath += paths[i];
                    else
                        splitPath += "/" +paths[i];

                    startNum++;
                }

                if (paths[i] == "panels")
                {
                    start = true;
                }
            }

            return splitPath;
        }

        public bool ConvertToXml(string filePath)
        {
            try
            {
                string command = "/C -xmlConvert -p " + filePath + " -o -currentproj";
                System.Diagnostics.Process.Start("WCCOAui.exe", command);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

            return true;
        }


        private void DataGridViewBufferExtension()
        {
            typeof(DataGridView).InvokeMember("DoubleBuffered",
            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
            null, this.dataGridView1, new object[] { true });
        }


        private void ReadDataPoint()
        {
            XmlDocument XmlDoc = new XmlDocument();
            XmlDoc.Load(@"TempFile\temp.xml"); // XML문서 로딩     
            XmlDoc.PreserveWhitespace = true; //공백제거

            XmlNode FirstNode = XmlDoc.DocumentElement;
            XmlNodeList nodes = XmlDoc.SelectNodes("panel/shapes/reference/properties/prop/prop/prop");

            string insertQuery = @"INSERT INTO c2_temp_group (page_name,sys_name,dp_name) VALUES (:v0, :v1, :v2)";
            string deleteQuery = @"DELETE C2_TEMP_GROUP";           

            if (Database.Open())
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = Database.OracleConn;
                try
                {
                    cmd.CommandText = deleteQuery;
                    cmd.ExecuteNonQuery();


                    foreach (XmlNode node in nodes)
                    {
                        if (node.InnerText != null)
                        {
                            string[] temp = node.InnerText.Split(':');
                            if (temp.Length == 2)
                            {
                                cmd.CommandText = insertQuery;
                                cmd.Parameters.Add(":v0", OracleDbType.Varchar2).Value = _pageName;
                                cmd.Parameters.Add(":v1", OracleDbType.Varchar2).Value = temp[0];
                                cmd.Parameters.Add(":v2", OracleDbType.Varchar2).Value = temp[1] + "%";
                                cmd.ExecuteNonQuery();
                                cmd.Parameters.Clear();                               
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    cmd.Dispose();
                    Database.Close();
                }
            }
            else
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

        }

        private void SearchTrendDpList()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string query = @"SELECT DISTINCT B.PAGE_NAME, A.DP_NAME FROM C2_TREND_INFO A JOIN C2_TEMP_GROUP B
                              ON A.DP_NAME LIKE B.DP_NAME ORDER BY A.DP_NAME ASC";

            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            OracleDataReader reader = cmd.ExecuteReader();
            int count = 1;
            try
            {
                if (_dtTrendDpList.Rows.Count > 0) _dtTrendDpList.Rows.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        bool isFirstExists = false;
                        bool isSecondExists = false;

                        foreach (DataRow datarow in _dtFirstTrendGroup.Select())
                        {
                            if (datarow["DP_NAME"].ToString() == reader[1].ToString())
                            {
                                isFirstExists = true;
                                break;
                            }
                        }
                        foreach (DataRow datarow in _dtSecondTrendGroup.Select())
                        {
                            if (datarow["DP_NAME"].ToString() == reader[1].ToString())
                            {
                                isSecondExists = true;
                                break;
                            }
                        }


                        if (isFirstExists == false && isSecondExists == false)
                        {
                            DataRow dr = _dtTrendDpList.NewRow();
                            dr["CHK"] = false;
                            dr["SEQ"] = count;
                            dr["PAGE_NAME"] = reader[0].ToString();
                            dr["DP_NAME"] = reader[1].ToString();
                            _dtTrendDpList.Rows.Add(dr);
                            count++;
                        }
                    }
                }


                dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(80, 80, 80);
                dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dataGridView1.EnableHeadersVisualStyles = false;

                dataGridView1.Columns["CHK"].Width = 35; //CHK
                dataGridView1.Columns["CHK"].ReadOnly = false;
                dataGridView1.Columns["CHK"].ValueType = typeof(bool);
                dataGridView1.Columns["CHK"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                dataGridView1.Columns["CHK"].DefaultCellStyle.ForeColor = Color.White;


                dataGridView1.Columns["SEQ"].Width = 40;
                dataGridView1.Columns["SEQ"].ReadOnly = true;
                dataGridView1.Columns["SEQ"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                dataGridView1.Columns["SEQ"].DefaultCellStyle.ForeColor = Color.White;

                dataGridView1.Columns["PAGE_NAME"].Width = 100;
                dataGridView1.Columns["PAGE_NAME"].ReadOnly = true;
                dataGridView1.Columns["PAGE_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                dataGridView1.Columns["PAGE_NAME"].DefaultCellStyle.ForeColor = Color.White;

                dataGridView1.Columns["DP_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dataGridView1.Columns["DP_NAME"].ReadOnly = true;
                dataGridView1.Columns["DP_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
                dataGridView1.Columns["DP_NAME"].DefaultCellStyle.ForeColor = Color.White;

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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                bool value = Convert.ToBoolean(_dtTrendDpList.Rows[e.RowIndex][e.ColumnIndex].ToString());
                _dtTrendDpList.Rows[e.RowIndex][e.ColumnIndex] = !value;
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                bool value = Convert.ToBoolean(_dtFirstTrendGroup.Rows[e.RowIndex][e.ColumnIndex].ToString());
                _dtFirstTrendGroup.Rows[e.RowIndex][e.ColumnIndex] = !value;
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                bool value = Convert.ToBoolean(_dtSecondTrendGroup.Rows[e.RowIndex][e.ColumnIndex].ToString());
                _dtSecondTrendGroup.Rows[e.RowIndex][e.ColumnIndex] = !value;
            }
        }

        
    }
}
