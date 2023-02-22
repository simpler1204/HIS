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

namespace HIS.Forms
{
    public partial class FormNewTrendGroup : Form
    {
        private DataTable _dtList     = new DataTable("TrendGroupDpList");
        private DataTable _dtFirst  = new DataTable("TrendGroupFirst");
        private DataTable _dtSecond = new DataTable("TrendGroupSecond");
        private string _partName = string.Empty;
        private string _groupName = string.Empty;
        private bool dg1AllSelected = false;
        private bool dg2AllSelected = false;
        private bool dg3AllSelected = false;
        private bool isNew = false;

        public FormNewTrendGroup()
        {
            InitializeComponent();

            InitDatatable.Init(_dtList);
            InitDatatable.Init(_dtFirst);
            InitDatatable.Init(_dtSecond);

            dgTrend.DataSource = _dtList;
            dgFirst.DataSource = _dtFirst;
            dgSecond.DataSource = _dtSecond;

            Database.CreateDatabase();
            GetPart();

            this.Resize += FormNewTrendGroup_Resize;
            btnSearch.ButtonClick += BtnSearch_ButtonClick;
            btnFirst.ButtonClick += BtnFirst_ButtonClick;
            btnSecond.ButtonClick += BtnSecond_ButtonClick;
            btnSave.ButtonClick += BtnSave_ButtonClick;
            cmbPart.SelectedValueChanged += CmbPart_SelectedValueChanged;
            cmbGroup.SelectedValueChanged += CmbGroup_SelectedValueChanged;
            dgTrend.ColumnHeaderMouseClick += DgTrend_ColumnHeaderMouseClick;
            dgFirst.ColumnHeaderMouseClick += DgFirst_ColumnHeaderMouseClick;
            dgSecond.ColumnHeaderMouseClick += DgSecond_ColumnHeaderMouseClick;
        }

        private void FormNewTrendGroup_Resize(object sender, EventArgs e)
        {
            Point dg2 = dgFirst.Location;
            Point dg3 = dgSecond.Location;

            Point bf = btnFirst.Location;
            Point bs = btnSecond.Location;

            Point newDg2 = new Point(bf.X, dg2.Y + 20);
            btnFirst.Location = newDg2;

            Point newDg3 = new Point(bs.X, dg3.Y + 20);
            btnSecond.Location = newDg3;
        }

        private void DgSecond_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_dtSecond.Rows.Count < 1) return;
            if (dg3AllSelected == false)
            {
                foreach (DataRow dr in _dtSecond.Rows)
                {
                    dr["CHK"] = true;
                }
                dg3AllSelected = true;
            }
            else
            {
                foreach (DataRow dr in _dtSecond.Rows)
                {
                    dr["CHK"] = false;
                }
                dg3AllSelected = false;
            }
        }

        private void DgFirst_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_dtFirst.Rows.Count < 1) return;
            if (dg2AllSelected == false)
            {
                foreach (DataRow dr in _dtFirst.Rows)
                {
                    dr["CHK"] = true;
                }
                dg2AllSelected = true;
            }
            else
            {
                foreach (DataRow dr in _dtFirst.Rows)
                {
                    dr["CHK"] = false;
                }
                dg2AllSelected = false;
            }
        }

        private void DgTrend_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (_dtList.Rows.Count < 1) return;
            if (dg1AllSelected == false)
            {
                foreach (DataRow dr in _dtList.Rows)
                {
                    dr["CHK"] = true;
                }
                dg1AllSelected = true;
            }
            else
            {
                foreach (DataRow dr in _dtList.Rows)
                {
                    dr["CHK"] = false;
                }
                dg1AllSelected = false;
            }
        }

        private void CmbGroup_SelectedValueChanged(object sender, EventArgs e)
        {
            _groupName = cmbGroup.Text;

            GetFirstSecond(_partName, _groupName);
            GetDPList("");
        }

        private void GetFirstSecond(string partName, string groupName)
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dtFirst.Rows.Clear();
            _dtSecond.Rows.Clear();

            string firstQuery = @"SELECT SYS_NAME1, DP_NAME1 FROM C2_TREND_GROUP WHERE PART_NAME = :1 
                                  AND GROUP_NAME = :2 AND DP_NAME1 IS NOT NULL ORDER BY DP_NAME1 ASC";

            string secondQuery = @"SELECT SYS_NAME2, DP_NAME2 FROM C2_TREND_GROUP WHERE PART_NAME = :1 
                                  AND GROUP_NAME = :2 AND DP_NAME2 IS NOT NULL ORDER BY DP_NAME2 ASC";

            OracleCommand cmd = new OracleCommand(firstQuery, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = partName;
            cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = groupName;

            OracleDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    int seq = 1;
                    while(reader.Read())
                    {
                        DataRow dr = _dtFirst.NewRow();
                        dr["CHK"] = false;
                        dr["SEQ"] = seq;
                        dr["SYSTEM"] = reader["SYS_NAME1"].ToString();
                        dr["DP_NAME"] = reader["DP_NAME1"].ToString();
                        _dtFirst.Rows.Add(dr);
                        seq++;
                    }
                }
                DecorateDataGridView(dgFirst);

                cmd.CommandText = secondQuery;
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int seq = 1;
                    while (reader.Read())
                    {
                        DataRow dr = _dtSecond.NewRow();
                        dr["CHK"] = false;
                        dr["SEQ"] = seq;
                        dr["SYSTEM"] = reader["SYS_NAME2"].ToString();
                        dr["DP_NAME"] = reader["DP_NAME2"].ToString();
                        _dtSecond.Rows.Add(dr);
                        seq++;
                    }
                }               
                DecorateDataGridView(dgSecond);

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

        private void CmbPart_SelectedValueChanged(object sender, EventArgs e)
        {
            _partName = cmbPart.Text;
            GetGroup(_partName);

            _dtFirst.Rows.Clear();
            _dtSecond.Rows.Clear();
        }

        private void GetGroup(string part)
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cmbGroup.Items.Clear();

            string query = "SELECT DISTINCT GROUP_NAME FROM C2_TREND_GROUP WHERE PART_NAME = :1";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = part;
            OracleDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
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


        private void GetPart()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            cmbPart.Items.Clear();

            string query = "SELECT DISTINCT PART_NAME FROM C2_TREND_GROUP";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            OracleDataReader reader = null;

            try
            {
                reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        cmbPart.Items.Add(reader[0].ToString());
                    }
                }
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
                    GetPart();

                    cmbPart.Text = _partName;
                    cmbGroup.Text = _groupName;

                    isNew = false;

                    MessageBox.Show("Success to Save", "Info");
                }
            }
            else if (e.Button.Properties.Caption == "New")
            {
                _dtFirst.Rows.Clear();
                _dtSecond.Rows.Clear();
                _dtList.Rows.Clear();

                txtGroup.Text = "";
                txtPart.Text = "";
                txtDpName.Enabled = true;
                btnSearch.Enabled = true;

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
                    _dtFirst.Rows.Clear();
                    _dtSecond.Rows.Clear();
                    _dtList.Rows.Clear();

                    txtGroup.Visible = false;
                    txtPart.Visible = false;
                    cmbPart.Visible = true;
                    cmbGroup.Visible = true;
                    txtDpName.Enabled = false;
                    btnSearch.Enabled = false;
                    isNew = false;
                }
            }

            else if (e.Button.Properties.Caption == "Remove")
            {
                if (isNew) return;
                if (_partName == string.Empty) return;
                if (_groupName == string.Empty) return;

                if (!Database.Open())
                {
                    MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = "DELETE C2_TREND_GROUP WHERE PART_NAME = :1 AND GROUP_NAME = :2";
                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);               
                cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = _partName;
                cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = _groupName;

                try
                {
                    cmd.ExecuteNonQuery();
                    _dtFirst.Rows.Clear();
                    _dtList.Rows.Clear();
                    _dtSecond.Rows.Clear();
                    GetPart();
                    cmbGroup.Items.Clear();
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
        }

        private bool SaveGroup()
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            int firstCount = _dtFirst.Rows.Count;
            int secondCount = _dtSecond.Rows.Count;
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


            string deleteQuery = "DELETE C2_TREND_GROUP WHERE PART_NAME = :1 AND GROUP_NAME = :2";

            OracleCommand cmd = new OracleCommand(deleteQuery, Database.OracleConn);
            
            cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = _partName;
            cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = _groupName;

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
                    cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = "ALL";
                    cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = _partName;
                    cmd.Parameters.Add(":3", OracleDbType.NVarchar2).Value = _groupName;
                    if (dgFirst.Rows.Count >= i)
                    {
                        cmd.Parameters.Add(":4", OracleDbType.NVarchar2).Value = dgFirst[3, i - 1].Value.ToString();
                        cmd.Parameters.Add(":6", OracleDbType.NVarchar2).Value = GetSystemName(dgFirst[3, i - 1].Value.ToString());
                    }
                    else
                    {
                        cmd.Parameters.Add(":4", OracleDbType.NVarchar2).Value = null;
                        cmd.Parameters.Add(":6", OracleDbType.NVarchar2).Value = null;
                    }

                    if (dgSecond.Rows.Count >= i)
                    {
                        cmd.Parameters.Add(":5", OracleDbType.NVarchar2).Value = dgSecond[3, i - 1].Value.ToString();
                        cmd.Parameters.Add(":7", OracleDbType.NVarchar2).Value = GetSystemName(dgSecond[3, i - 1].Value.ToString());
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

      

        private void BackToListFromFirst()
        {
            if (_dtFirst.Rows.Count < 1) return;

            List<DataRow> rows = new List<DataRow>();

            //이동
            foreach (DataRow dr in _dtFirst.Rows)
            {
                if (Convert.ToBoolean(dr["CHK"]) == true)
                {
                    DataRow listdr = _dtList.NewRow();
                    listdr["CHK"] = false;
                    listdr["SEQ"] = _dtFirst.Rows.Count + 1;
                    listdr["SYSTEM"] = dr["SYSTEM"].ToString();
                    listdr["DP_NAME"] = dr["DP_NAME"].ToString();

                    rows.Add(dr);
                    _dtList.Rows.Add(listdr);
                }
            }

            //이동 후 삭제
            foreach (DataRow r in rows)
            {
                _dtFirst.Rows.Remove(r);
            }

            //삭제 후 seq 다시 채번
            int seq = 1;
            foreach (DataRow r in _dtList.Rows)
            {
                r["SEQ"] = seq++;
            }

            seq = 1;
            foreach (DataRow r in _dtFirst.Rows)
            {
                r["SEQ"] = seq++;
            }

            if (dgTrend.Rows.Count > 0)
                dgTrend.FirstDisplayedScrollingRowIndex = dgTrend.Rows.Count - 1;
        }

        private void MoveToFirst()
        {
            if (_dtList.Rows.Count < 1) return;

            List<DataRow> rows = new List<DataRow>();

            //이동
            foreach (DataRow dr in _dtList.Rows)
            {
                if (Convert.ToBoolean(dr["CHK"]) == true)
                {
                    DataRow firstdr = _dtFirst.NewRow();
                    firstdr["CHK"] = false;
                    firstdr["SEQ"] = _dtFirst.Rows.Count + 1;
                    firstdr["SYSTEM"] = dr["SYSTEM"].ToString();
                    firstdr["DP_NAME"] = dr["DP_NAME"].ToString();
                    rows.Add(dr);
                    _dtFirst.Rows.Add(firstdr);
                }
            }

            DecorateDataGridView(dgFirst);

            //이동 후 삭제
            foreach (DataRow r in rows)
            {
                _dtList.Rows.Remove(r);
            }

            //삭제 후 seq 다시 채번
            int seq = 1;
            foreach (DataRow r in _dtList.Rows)
            {
                r["SEQ"] = seq++;
            }

            if (dgFirst.Rows.Count > 0)
                dgFirst.FirstDisplayedScrollingRowIndex = dgFirst.Rows.Count - 1;
        }

        private void MoveToSecond()
        {
            if (_dtList.Rows.Count < 1) return;

            List<DataRow> rows = new List<DataRow>();

            //이동
            foreach (DataRow dr in _dtList.Rows)
            {
                if (Convert.ToBoolean(dr["CHK"]) == true)
                {
                    DataRow seconddr = _dtSecond.NewRow();
                    seconddr["CHK"] = false;
                    seconddr["SEQ"] = _dtSecond.Rows.Count + 1;
                    seconddr["SYSTEM"] = dr["SYSTEM"].ToString();
                    seconddr["DP_NAME"] = dr["DP_NAME"].ToString();

                    rows.Add(dr);
                    _dtSecond.Rows.Add(seconddr);
                }
            }

            DecorateDataGridView(dgSecond);

            //이동 후 삭제
            foreach (DataRow r in rows)
            {
                _dtList.Rows.Remove(r);
            }

            //삭제 후 seq 다시 채번
            int seq = 1;
            foreach (DataRow r in _dtList.Rows)
            {
                r["SEQ"] = seq++;
            }

            if (dgSecond.Rows.Count > 0)
                dgSecond.FirstDisplayedScrollingRowIndex = dgSecond.Rows.Count - 1;
        }

        private void BackToListFromSecond()
        {
            if (_dtSecond.Rows.Count < 1) return;

            List<DataRow> rows = new List<DataRow>();

            //이동
            foreach (DataRow dr in _dtSecond.Rows)
            {
                if (Convert.ToBoolean(dr["CHK"]) == true)
                {
                    DataRow listdr = _dtList.NewRow();
                    listdr["CHK"] = false;
                    listdr["SEQ"] = _dtSecond.Rows.Count + 1;
                    listdr["SYSTEM"] = dr["SYSTEM"].ToString();
                    listdr["DP_NAME"] = dr["DP_NAME"].ToString();

                    rows.Add(dr);
                    _dtList.Rows.Add(listdr);
                }
            }

            //이동 후 삭제
            foreach (DataRow r in rows)
            {
                _dtSecond.Rows.Remove(r);
            }

            //삭제 후 seq 다시 채번
            int seq = 1;
            foreach (DataRow r in _dtList.Rows)
            {
                r["SEQ"] = seq++;
            }

            seq = 1;
            foreach (DataRow r in _dtSecond.Rows)
            {
                r["SEQ"] = seq++;
            }

            if (dgTrend.Rows.Count > 0)
                dgTrend.FirstDisplayedScrollingRowIndex = dgTrend.Rows.Count - 1;
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

        private void BtnSearch_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            _dtList.Rows.Clear();
            GetDPList(txtDpName.Text);
        }

        private void GetDPList(string dpName)
        {
            if (!Database.Open())
            {
                MessageBox.Show("DataBase connect to fail..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _dtList.Clear();

            string query = "SELECT SYSTEM, DP_NAME FROM C2_TREND_INFO WHERE DP_NAME Like :1 ORDER BY DP_NAME ASC";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dpName == "" ? "%" : "%"+dpName+"%";            

            OracleDataReader reader = null;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    int seq = 1;
                    while (reader.Read())
                    {
                        bool isFirstExists = false;
                        bool isSecondExists = false;

                        foreach (DataRow datarow in _dtFirst.Select())
                        {
                            if (datarow["DP_NAME"].ToString() == reader["DP_NAME"].ToString())
                            {
                                isFirstExists = true;
                                break;
                            }
                        }
                        foreach (DataRow datarow in _dtSecond.Select())
                        {
                            if (datarow["DP_NAME"].ToString() == reader["DP_NAME"].ToString())
                            {
                                isSecondExists = true;
                                break;
                            }
                        }


                        if (isFirstExists == false && isSecondExists == false)
                        {
                            DataRow dr = _dtList.NewRow();
                            dr["CHK"] = false;
                            dr["SEQ"] = seq;
                            dr["SYSTEM"] = reader["SYSTEM"].ToString();
                            dr["DP_NAME"] = reader["DP_NAME"].ToString();

                            _dtList.Rows.Add(dr);
                            seq++;
                        }
                    }
                }
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
                DecorateDataGridView(dgTrend);
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

            dg.Columns["SYSTEM"].Width = 70;
            dg.Columns["SYSTEM"].ReadOnly = true;
            dg.Columns["SYSTEM"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["SYSTEM"].DefaultCellStyle.ForeColor = Color.White;

            dg.Columns["DP_NAME"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dg.Columns["DP_NAME"].ReadOnly = true;
            dg.Columns["DP_NAME"].DefaultCellStyle.BackColor = Color.FromArgb(50, 50, 50);
            dg.Columns["DP_NAME"].DefaultCellStyle.ForeColor = Color.White;
   
        }

        private void dgTrend_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                bool value = Convert.ToBoolean(_dtList.Rows[e.RowIndex][e.ColumnIndex].ToString());
                _dtList.Rows[e.RowIndex][e.ColumnIndex] = !value;
            }
        }

        private void dgFirst_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                bool value = Convert.ToBoolean(_dtFirst.Rows[e.RowIndex][e.ColumnIndex].ToString());
                _dtFirst.Rows[e.RowIndex][e.ColumnIndex] = !value;
            }
        }

        private void dgSecond_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 0)
            {
                bool value = Convert.ToBoolean(_dtSecond.Rows[e.RowIndex][e.ColumnIndex].ToString());
                _dtSecond.Rows[e.RowIndex][e.ColumnIndex] = !value;
            }
        }
    }
}
