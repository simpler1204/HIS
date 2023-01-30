using HIS.Class;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Windows.Forms;

namespace HIS.Forms
{
    public partial class FormAddDp : Form
    {      
        private readonly OracleConnection conn;
        private string _system = string.Empty;
        private string _dp = string.Empty;
        private string _desc = string.Empty;
        private string _tableName = null;
        private string _columnName = null;
        private string _min = null;
        private string _max = null;


        public FormAddDp(string msg)
        {
            InitializeComponent();
            conn = Database.OracleConn;
            menu.ButtonClick += Menu_ButtonClick;          
            GetTables(msg);
            this.Show();
            this.TopMost = true;
            this.Activate();

           
           
        }

        private void Menu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (e.Button.Properties.Caption == "Save")
            {
                _columnName = GetColumnName();
                if(CheckMinMax() == false) return;
                if(InsertTrendInfo() == false) return;
                if(UpdateTrendInfoDetail() == false) return;
                if(InsertTrendMoveHistory() == false) return;

                if (SockClient.sendMsg(_system, $"restart;{_tableName}") == false)
                {
                    MessageBox.Show("Socket Error");
                    return;
                }

                MessageBox.Show("succeed to Add Trend Data Point", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }

        private bool InsertTrendMoveHistory()
        {
            if(Database.Open())
            {
                string query = @"INSERT INTO C2_DP_TREND_MOVE_HISTORY(SEQ, DP_NAME, TB_NAME, COL_NAME, START_AT)
                                 VALUES(DP_MOVE.NEXTVAL, :1, :2, :3, SYSDATE)";
                try
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = _dp;
                        cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = _tableName;
                        cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = _columnName;

                        cmd.ExecuteNonQuery();
                    }

                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    Database.Close();
                }
            }

            return true;
        }

        private bool UpdateTrendInfoDetail()
        {
           if(Database.Open())
            {
                string query = $"UPDATE C2_TREND_TABLE_INFO_DETAIL SET {_columnName} = '{_dp}' WHERE TABLE_NAME = '{_tableName}'";

                try
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
                finally
                {
                    Database.Close();
                }
            }

            return true;
        }

        private bool InsertTrendInfo()
        {
            if(Database.Open())
            {
                string query = @"INSERT INTO C2_TREND_INFO(SYSTEM, DP_NAME, DP_DESC, Y_MIN, Y_MAX, TB_NAME, COL_NAME) 
                                VALUES(:1, :2, :3, :4, :5, :6, :7)";

                try
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = _system;
                        cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = _dp;
                        cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = _desc;
                        cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = _min;
                        cmd.Parameters.Add(":5", OracleDbType.Varchar2).Value = _max;
                        cmd.Parameters.Add(":6", OracleDbType.Varchar2).Value = _tableName;
                        cmd.Parameters.Add(":7", OracleDbType.Varchar2).Value = _columnName;

                        cmd.ExecuteNonQuery();
                    }
                    return true;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;

                }
                finally
                {
                    Database.Close();
                }           
            }

            return true;
        }

       

        private string GetColumnName()
        {
            if (dataGridView1.Rows.Count < 1) return null;

            int col = 0;

            _tableName = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();

            string query = "SELECT ";
            for(int i=1; i<100; i++)
            {
                query += "COL_" + i.ToString("0000") + ",";
            }
            query += "COL_0100 FROM C2_TREND_TABLE_INFO_DETAIL WHERE TABLE_NAME = :1";

            if (Database.Open())
            {
                try
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = _tableName;
                        OracleDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                for (int i = 1; i <= 100; i++)
                                {
                                    if (reader[i].ToString() == "")
                                    {
                                        col = i;
                                        break;
                                    }
                                }
                            }
                        }

                        reader.Close();
                        reader.Dispose();
                        cmd.Dispose();
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    Database.Close();
                }
                
            }

                return "COL_" + (col+1).ToString("0000");
        }

        private bool CheckAlreadyRegistered(string dp)
        {
            string query = "SELECT COUNT(DP_NAME) FROM C2_TREND_INFO WHERE DP_NAME = :1";
            int count = 0;

            try
            {
                if (Database.Open())
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dp;
                        OracleDataReader reader = cmd.ExecuteReader();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int.TryParse(reader[0].ToString(), out count);
                            }
                        }

                        reader.Close();
                        reader.Dispose();
                        cmd.Dispose();
                    }                   
                }

                if (count > 0)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                Database.Close();
            }
        }

        private bool CheckMinMax()
        {
            float fMin = 0f;
            float fMax = 0f;

            bool bMin = float.TryParse(txtMin.Text, out fMin);
            if(bMin == false)
            {
                MessageBox.Show("Check Y-MIN value");
                txtMin.Focus();
                return false;
            }

            bool bMax = float.TryParse(txtMax.Text, out fMax);
            if (bMax == false)
            {
                MessageBox.Show("Check Y-MAX value");
                txtMax.Focus();
                return false;
            }

            _min = txtMin.Text;
            _max = txtMax.Text;

            return true;
        }

        private void GetTables(string msg)
        {
            string[] content = msg.Split(';');
            if (content.Length != 6) return;
            _system = content[1];
            _dp = content[2];
            _desc = content[3];
            _min = content[4];
            _max = content[5];


            bool rtn = CheckAlreadyRegistered(_dp);
            if (rtn == true)
            {
                lblRegistered.Text = $"{_dp}\r\n\r\nalready registered.";
                lblRegistered.Visible = true;
                // MessageBox.Show($"{_dp} is already registered.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblDp.Visible = false;
                lblDesc.Visible = false;
                lblSystem.Visible = false;
                label1.Visible = false;
                label2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                label7.Visible = false;
                dataGridView1.Visible = false;
                txtMax.Visible = false;
                txtMin.Visible = false;
                menu.Visible = false;
                return;
            }

           


            int totalCount = 0;


            lblDp.Text = _dp;
            lblSystem.Text = _system;
            lblDesc.Text = _desc;
            txtMin.Text = _min;
            txtMax.Text = _max;

            string query = @"SELECT TABLE_NAME, TABLE_DESC, LOGGING_CYCLE, SAVING_GRADE, EMPTY FROM C2_TREND_TABLE_INFO_MASTER
                                WHERE SYSTEM = :1 AND EMPTY > 0  ORDER BY TABLE_NAME ASC";

            dataGridView1.Rows.Clear();
            try
            {
                if (Database.Open())
                {
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = _system;
                        OracleDataReader reader = cmd.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                totalCount++;
                                int row = dataGridView1.Rows.Add();
                                dataGridView1[0, row].Value = reader["TABLE_NAME"].ToString();
                                dataGridView1[1, row].Value = reader["TABLE_DESC"].ToString();
                                dataGridView1[2, row].Value = reader["LOGGING_CYCLE"].ToString();
                                if (reader["SAVING_GRADE"].ToString() == "A")
                                {
                                    dataGridView1[3, row].Value = "18 months";
                                }
                                if (reader["SAVING_GRADE"].ToString() == "B")
                                {
                                    dataGridView1[3, row].Value = "12 months";
                                }
                                if (reader["SAVING_GRADE"].ToString() == "C")
                                {
                                    dataGridView1[3, row].Value = "9 months";
                                }

                                dataGridView1[4, row].Value = reader["EMPTY"].ToString();
                                dataGridView1.Rows[totalCount - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                            }
                        }

                        reader.Close();
                        reader.Dispose();
                        cmd.Dispose();
                    }
                }
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {
                Database.Close();
            }

            
        }

    }
}
