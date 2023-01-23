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
    public partial class FormMenu : Form
    {
        string[] selectQueryString = new string[4];
        string[] insertQueryString = new string[4];
        string[] updateQueryString = new string[4];

        public FormMenu()
        {
            InitializeComponent();
            Init();
            GetStep1Data();
        }

        private void Init()
        {
            this.Refresh();      

            selectQueryString[0] = "SELECT MENU_NAME, PANEL, SEQ, MENU_SEQ FROM HMI_MENU_ONE ORDER BY SEQ ASC";
            selectQueryString[1] = "SELECT MENU_ONE_NAME, MENU_NAME, PANEL, SEQ, MENU_SEQ FROM HMI_MENU_TWO WHERE MENU_ONE_NAME = :1 ORDER BY SEQ ASC";
            selectQueryString[2] = "SELECT MENU_ONE_NAME, MENU_TWO_NAME, MENU_NAME, PANEL, SEQ, MENU_SEQ FROM HMI_MENU_THREE WHERE MENU_ONE_NAME = :1  AND MENU_TWO_NAME = :2 ORDER BY SEQ ASC";
            selectQueryString[3] = "SELECT MENU_ONE_NAME, MENU_TWO_NAME, MENU_THREE_NAME, MENU_NAME, PANEL, SEQ, MENU_SEQ FROM HMI_MENU_FOUR WHERE MENU_ONE_NAME = :1  AND MENU_TWO_NAME = :2 AND MENU_THREE_NAME = :3 ORDER BY SEQ ASC";
            insertQueryString[0] = "INSERT INTO hmi_menu_one( menu_name, panel, seq, menu_seq) VALUES(:1,:2,:3,:4)";
            insertQueryString[1] = "INSERT INTO hmi_menu_two(menu_one_name, menu_name, panel, seq, menu_seq) VALUES(:1,:2,:3,:4,:5)";
            insertQueryString[2] = "INSERT INTO hmi_menu_three(menu_one_name, menu_two_name, menu_name, panel, seq, menu_seq) VALUES(:1,:2,:3,:4, :5,:6)";
            insertQueryString[3] = "INSERT INTO hmi_menu_four(menu_one_name, menu_two_name, menu_three_name, menu_name, panel, seq, menu_seq) VALUES(:1,:2,:3,:4, :5, :6,:7)";
            updateQueryString[0] = "UPDATE HMI_MENU_ONE SET MENU_NAME = :1, PANEL = :2, SEQ = :3 WHERE MENU_SEQ = :4";
            updateQueryString[1] = "UPDATE HMI_MENU_TWO SET MENU_NAME = :1, PANEL = :2, SEQ = :3 WHERE MENU_SEQ = :4";
            updateQueryString[2] = "UPDATE HMI_MENU_THREE SET MENU_NAME = :1, PANEL = :2, SEQ = :3 WHERE MENU_SEQ = :4";
            updateQueryString[3] = "UPDATE HMI_MENU_FOUR SET MENU_NAME = :1, PANEL = :2, SEQ = :3 WHERE MENU_SEQ = :4";

            btnStep1Save.Click += BtnSave_Click;
            btnStep2Save.Click += BtnSave_Click;
            btnStep3Save.Click += BtnSave_Click;
            btnStep4Save.Click += BtnSave_Click;

            btnStep1Insert.Click += btnInsert_Click;
            btnStep2Insert.Click += btnInsert_Click;
            btnStep3Insert.Click += btnInsert_Click;
            btnStep4Insert.Click += btnInsert_Click;

            dataGridView1.CurrentCellChanged += DataGridView_CurrentCellChanged;
            dataGridView2.CurrentCellChanged += DataGridView_CurrentCellChanged;
            dataGridView3.CurrentCellChanged += DataGridView_CurrentCellChanged;

            InitDataGridView.dataGridViewInit(dataGridView1);
            InitDataGridView.dataGridViewInit(dataGridView2);
            InitDataGridView.dataGridViewInit(dataGridView3);
            InitDataGridView.dataGridViewInit(dataGridView4);

            


            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; //row 높이 고정
            //dataGridView1.AllowUserToResizeRows = false;//row 높이 고정       
            //dataGridView1.EnableHeadersVisualStyles = false;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //dataGridView2.EnableHeadersVisualStyles = false;
            //dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            //dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //dataGridView2.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; //row 높이 고정
            //dataGridView2.AllowUserToResizeRows = false;//row 높이 고정           
            //dataGridView2.EnableHeadersVisualStyles = false;
            //dataGridView2.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            //dataGridView2.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //dataGridView3.EnableHeadersVisualStyles = false;
            //dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            //dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; //row 높이 고정
            //dataGridView3.AllowUserToResizeRows = false;//row 높이 고정         
            //dataGridView3.EnableHeadersVisualStyles = false;
            //dataGridView3.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            //dataGridView3.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            //dataGridView4.EnableHeadersVisualStyles = false;
            //dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);
            //dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            //dataGridView4.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None; //row 높이 고정
            //dataGridView4.AllowUserToResizeRows = false;//row 높이 고정         
            //dataGridView4.EnableHeadersVisualStyles = false;
            //dataGridView4.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            //dataGridView4.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);

            dataGridView1.CellValueChanged -= DataGridView_CellValueChanged;
            dataGridView2.CellValueChanged -= DataGridView_CellValueChanged;
            dataGridView3.CellValueChanged -= DataGridView_CellValueChanged;
            dataGridView4.CellValueChanged -= DataGridView_CellValueChanged;

            string query = string.Empty;
            string uQuery = string.Empty;
            int rowCount = 0;
            if (btn.Name == "btnStep1Save")
            {
                query = insertQueryString[0];
                uQuery = updateQueryString[0];
                rowCount = dataGridView1.Rows.Count;
                if (rowCount > 10)
                {
                    MessageBox.Show("Step1, You can insert up to 10");
                    return;
                }
            }
            else if (btn.Name == "btnStep2Save")
            {
                query = insertQueryString[1];
                uQuery = updateQueryString[1];
                rowCount = dataGridView2.Rows.Count;
                if (rowCount > 20)
                {
                    MessageBox.Show("Step2, You can insert up to 20");
                    return;
                }
            }
            else if (btn.Name == "btnStep3Save")
            {
                query = insertQueryString[2];
                uQuery = updateQueryString[2];
                rowCount = dataGridView3.Rows.Count;
                if (rowCount > 20)
                {
                    MessageBox.Show("Step3, You can insert up to 20");
                    return;
                }

            }
            else if (btn.Name == "btnStep4Save")
            {
                query = insertQueryString[3];
                uQuery = updateQueryString[3];
                rowCount = dataGridView4.Rows.Count;
                if (rowCount > 25)
                {
                    MessageBox.Show("Step4, You can insert up to 25");
                    return;
                }
            }

            try
            {
                if (!Database.Open()) return;

                for (int i = 0; i < rowCount; i++)
                {
                    if (btn.Name == "btnStep1Save")
                    {
                        int menu_seq_number = GetMenuSeqNumber("btnStep1Save");
                        if (dataGridView1[3, i].Value.ToString() == "I")
                        {
                            if (dataGridView1[0, i].Value == null || dataGridView1[2, i].Value == null)
                            {
                                MessageBox.Show("You should fill NAME, PANEL columns");
                                return;
                            }
                            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dataGridView1[0, i].Value.ToString();
                            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dataGridView1[1, i].Value.ToString();
                            cmd.Parameters.Add(":3", OracleDbType.Int32).Value = dataGridView1[2, i].Value;
                            cmd.Parameters.Add(":4", OracleDbType.Int32).Value = menu_seq_number;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            dataGridView1[3, i].Value = "N";
                            dataGridView1[4, i].Value = menu_seq_number.ToString();                            

                        }
                        else if (dataGridView1[3, i].Value.ToString() == "U")
                        {
                            if (dataGridView1[0, i].Value == null || dataGridView1[2, i].Value == null)
                            {
                                MessageBox.Show("You should fill NAME, PANEL columns");
                                return;
                            }
                            OracleCommand cmd = new OracleCommand(uQuery, Database.OracleConn);
                            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dataGridView1[0, i].Value.ToString();

                            if (dataGridView1[1, i].Value == null)
                                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = "";
                            else
                                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dataGridView1[1, i].Value.ToString();

                            cmd.Parameters.Add(":3", OracleDbType.Int32).Value = dataGridView1[2, i].Value;
                            cmd.Parameters.Add(":4", OracleDbType.Int32).Value = dataGridView1[4, i].Value;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            dataGridView1[3, i].Value = "N";

                        }

                    }
                    if (btn.Name == "btnStep2Save")
                    {
                        int menu_seq_number = GetMenuSeqNumber("btnStep2Save");
                        if (dataGridView2[4, i].Value.ToString() == "I")
                        {
                            if (dataGridView2[1, i].Value == null || dataGridView2[3, i].Value == null)
                            {
                                MessageBox.Show("You should fill NAME, PANEL columns");
                                return;
                            }

                            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dataGridView2[0, i].Value.ToString();
                            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dataGridView2[1, i].Value.ToString();

                            if (dataGridView2[2, i].Value == null)
                                cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = "";
                            else
                                cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = dataGridView2[2, i].Value.ToString();

                            cmd.Parameters.Add(":4", OracleDbType.Int32).Value = dataGridView2[3, i].Value;
                            cmd.Parameters.Add(":5", OracleDbType.Int32).Value = menu_seq_number;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            dataGridView2[4, i].Value = "N";
                            dataGridView2[5, i].Value = menu_seq_number.ToString();
                            //dataGridView2.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                            //dataGridView2.Rows[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                        else if (dataGridView2[4, i].Value.ToString() == "U")
                        {
                            if (dataGridView2[1, i].Value == null || dataGridView2[3, i].Value == null)
                            {
                                MessageBox.Show("You should fill NAME, PANEL columns");
                                return;
                            }

                            OracleCommand cmd = new OracleCommand(uQuery, Database.OracleConn);
                            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dataGridView2[1, i].Value.ToString();

                            if (dataGridView2[2, i].Value == null)
                                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = "";
                            else
                                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dataGridView2[2, i].Value.ToString();

                            cmd.Parameters.Add(":3", OracleDbType.Int32).Value = dataGridView2[3, i].Value;
                            cmd.Parameters.Add(":4", OracleDbType.Int32).Value = dataGridView2[5, i].Value;

                            cmd.ExecuteNonQuery();
                            cmd.Dispose();

                            dataGridView2[4, i].Value = "N";
                        }

                    }
                    if (btn.Name == "btnStep3Save")
                    {
                        int menu_seq_number = GetMenuSeqNumber("btnStep3Save");

                        if (dataGridView3[5, i].Value.ToString() == "I")
                        {
                            if (dataGridView3[2, i].Value == null || dataGridView3[4, i].Value == null)
                            {
                                MessageBox.Show("You should fill NAME, PANEL columns");
                                return;
                            }

                            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dataGridView3[0, i].Value.ToString();
                            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dataGridView3[1, i].Value.ToString();
                            cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = dataGridView3[2, i].Value.ToString();


                            if (dataGridView3[3, i].Value == null)
                                cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = "";
                            else
                                cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = dataGridView3[3, i].Value.ToString();

                            cmd.Parameters.Add(":5", OracleDbType.Int32).Value = dataGridView3[4, i].Value;
                            cmd.Parameters.Add(":6", OracleDbType.Int32).Value = menu_seq_number;
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            dataGridView3[5, i].Value = "N";
                            dataGridView3[6, i].Value = menu_seq_number.ToString();
                            //dataGridView3.Rows[i].DefaultCellStyle.BackColor = Color.Black;
                            //dataGridView3.Rows[i].DefaultCellStyle.ForeColor = Color.White;

                        }
                        else if (dataGridView3[5, i].Value.ToString() == "U")
                        {
                            if (dataGridView3[2, i].Value == null || dataGridView3[4, i].Value == null)
                            {
                                MessageBox.Show("You should fill NAME, PANEL columns");
                                return;
                            }

                            OracleCommand cmd = new OracleCommand(uQuery, Database.OracleConn);
                            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dataGridView3[2, i].Value.ToString();

                            if (dataGridView3[3, i].Value == null)
                                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = "";
                            else
                                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dataGridView3[3, i].Value.ToString();

                            cmd.Parameters.Add(":3", OracleDbType.Int32).Value = dataGridView3[4, i].Value;
                            cmd.Parameters.Add(":4", OracleDbType.Int32).Value = dataGridView3[6, i].Value;

                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            dataGridView3[5, i].Value = "N";

                        }


                    }
                    if (btn.Name == "btnStep4Save")
                    {
                        if (dataGridView4[6, i].Value.ToString() == "I")
                        {
                            int menu_seq_number = GetMenuSeqNumber("btnStep4Save");

                            if (dataGridView4[3, i].Value == null || dataGridView4[5, i].Value == null)
                            {
                                MessageBox.Show("You should fill NAME, PANEL columns");
                                return;
                            }

                            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dataGridView4[0, i].Value.ToString();
                            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dataGridView4[1, i].Value.ToString();
                            cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = dataGridView4[2, i].Value.ToString();
                            cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = dataGridView4[3, i].Value.ToString();

                            if (dataGridView4[4, i].Value == null)
                                cmd.Parameters.Add(":5", OracleDbType.Varchar2).Value = "";
                            else
                                cmd.Parameters.Add(":5", OracleDbType.Varchar2).Value = dataGridView4[4, i].Value.ToString();

                            cmd.Parameters.Add(":6", OracleDbType.Int32).Value = dataGridView4[5, i].Value;
                            cmd.Parameters.Add(":7", OracleDbType.Int32).Value = menu_seq_number;

                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            dataGridView4[6, i].Value = "N";
                            dataGridView4[7, i].Value = menu_seq_number.ToString();                           

                        }
                        else if (dataGridView4[6, i].Value.ToString() == "U")
                        {
                            if (dataGridView4[3, i].Value == null || dataGridView4[5, i].Value == null)
                            {
                                MessageBox.Show("You should fill NAME, PANEL columns");
                                return;
                            }

                            OracleCommand cmd = new OracleCommand(uQuery, Database.OracleConn);
                            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dataGridView4[3, i].Value.ToString();

                            if (dataGridView4[4, i].Value == null)
                                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = "";
                            else
                                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dataGridView4[4, i].Value.ToString();

                            cmd.Parameters.Add(":3", OracleDbType.Int32).Value = int.Parse(dataGridView4[5, i].Value.ToString());
                            cmd.Parameters.Add(":4", OracleDbType.Int32).Value = int.Parse(dataGridView4[7, i].Value.ToString());

                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            dataGridView4[6, i].Value = "N";
                            //dataGridView4[7, i].Value = dataGridView4[5, i].Value;
                        }
                    }

                }

                dataGridView1.CellValueChanged += DataGridView_CellValueChanged;
                dataGridView2.CellValueChanged += DataGridView_CellValueChanged;
                dataGridView3.CellValueChanged += DataGridView_CellValueChanged;
                dataGridView4.CellValueChanged += DataGridView_CellValueChanged;

                ButtonSaveClicked();

               // MessageBox.Show("Success to save");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            finally
            {
                Database.Close();
            }
            return;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            Button btn = (Button)(sender);
            try
            {
                if (btn.Name == "btnStep1Insert")
                {
                    
                    dataGridView1.CellValueChanged -= DataGridView_CellValueChanged;
                    int row = dataGridView1.Rows.Add();
                    dataGridView1[1, row].Value = "";
                    dataGridView1[3, row].Value = "I";
                    dataGridView1.CellValueChanged -= DataGridView_CellValueChanged;
                    dataGridView1.Rows[dataGridView1.Rows.Count-1].DefaultCellStyle.BackColor = Colors.dgvBackColor;

                }
                else if (btn.Name == "btnStep2Insert")
                {
                    if (dataGridView1.Rows.Count < 1) return;
                    dataGridView2.CellValueChanged -= DataGridView_CellValueChanged;
                    int row1 = dataGridView1.CurrentCell.RowIndex;
                    if (dataGridView1[0, row1].Value == null) return;
                    string step1 = dataGridView1[0, row1].Value.ToString();

                    int row2 = dataGridView2.Rows.Add();
                    dataGridView2[0, row2].Value = step1;
                    dataGridView2[2, row2].Value = "";
                    dataGridView2[4, row2].Value = "I";
                    dataGridView2.CellValueChanged += DataGridView_CellValueChanged;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                }
                else if (btn.Name == "btnStep3Insert")
                {
                    if (dataGridView2.Rows.Count < 1) return;
                    dataGridView3.CellValueChanged -= DataGridView_CellValueChanged;
                    int row2 = dataGridView2.CurrentCell.RowIndex;
                    if (dataGridView2[1, row2].Value == null) return;
                    string step1 = dataGridView2[0, row2].Value.ToString();
                    string step2 = dataGridView2[1, row2].Value.ToString();

                    int row3 = dataGridView3.Rows.Add();
                    dataGridView3[0, row3].Value = step1;
                    dataGridView3[1, row3].Value = step2;
                    dataGridView3[3, row3].Value = "";
                    dataGridView3[5, row3].Value = "I";
                    dataGridView3.CellValueChanged += DataGridView_CellValueChanged;
                    dataGridView3.Rows[dataGridView3.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                }
                else if (btn.Name == "btnStep4Insert")
                {
                    if (dataGridView3.Rows.Count < 1) return;
                    dataGridView4.CellValueChanged -= DataGridView_CellValueChanged;
                    int row3 = dataGridView3.CurrentCell.RowIndex;
                    if (dataGridView3[1, row3].Value == null) return;
                    string step1 = dataGridView3[0, row3].Value.ToString();
                    string step2 = dataGridView3[1, row3].Value.ToString();
                    string step3 = dataGridView3[2, row3].Value.ToString();

                    int row4 = dataGridView4.Rows.Add();
                    dataGridView4[0, row4].Value = step1;
                    dataGridView4[1, row4].Value = step2;
                    dataGridView4[2, row4].Value = step3;
                    dataGridView4[4, row4].Value = "";
                    dataGridView4[6, row4].Value = "I";
                    dataGridView4.CellValueChanged += DataGridView_CellValueChanged;
                    dataGridView4.Rows[dataGridView4.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                }

                ButtonInsertClicked(btn.Name);
            }
            catch
            {

            }
        }

        private void DataGridView_CurrentCellChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)(sender);
            try
            {
                if (dgv.Name == "dataGridView1")
                {
                    int row = dgv.CurrentCell.RowIndex;
                    if (dgv[0, row].Value == null) return;
                    string menuName = dgv[0, row].Value.ToString();
                    GetStep2Data(menuName);

                }
                else if (dgv.Name == "dataGridView2")
                {
                    int row = dgv.CurrentCell.RowIndex;
                    if (dgv[1, row].Value == null) return;

                    string step1 = dgv[0, row].Value.ToString();
                    string step2 = dgv[1, row].Value.ToString();
                    GetStep3Data(step1, step2);

                }
                else if (dgv.Name == "dataGridView3")
                {
                    int row = dgv.CurrentCell.RowIndex;
                    if (dgv[1, row].Value == null) return;

                    string step1 = dgv[0, row].Value.ToString();
                    string step2 = dgv[1, row].Value.ToString();
                    string step3 = dgv[2, row].Value.ToString();
                    GetStep4Data(step1, step2, step3);

                }
                else if (dgv.Name == "dataGridView4")
                {
                    int row = dgv.CurrentCell.RowIndex;
                    if (dgv[1, row].Value == null) return;
                }
            }
            catch
            {

            }
        }

        private void DataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)(sender);

            if (dgv.Name == "dataGridView1")
            {
                if (dgv[3, e.RowIndex].Value.ToString() != "I")
                    dgv[3, e.RowIndex].Value = "U";
            }
            else if (dgv.Name == "dataGridView2")
            {
                if (dgv[4, e.RowIndex].Value.ToString() != "I")
                    dgv[4, e.RowIndex].Value = "U";
            }
            else if (dgv.Name == "dataGridView3")
            {
                if (dgv[5, e.RowIndex].Value.ToString() != "I")
                    dgv[5, e.RowIndex].Value = "U";
            }
            else if (dgv.Name == "dataGridView4")
            {
                if (dgv[6, e.RowIndex].Value.ToString() != "I")
                    dgv[6, e.RowIndex].Value = "U";
            }
        }

        private int GetMenuSeqNumber(string btnName)
        {
            int count = 0;
            string query = string.Empty;
            if (btnName == "btnStep1Save")
                query = "SELECT MAX(MENU_SEQ) FROM HMI_MENU_ONE";
            else if (btnName == "btnStep2Save")
                query = "SELECT MAX(MENU_SEQ) FROM HMI_MENU_TWO";
            else if (btnName == "btnStep3Save")
                query = "SELECT MAX(MENU_SEQ) FROM HMI_MENU_THREE";
            else if (btnName == "btnStep4Save")
                query = "SELECT MAX(MENU_SEQ) FROM HMI_MENU_FOUR";

            try
            {

                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                OracleDataReader reader = cmd.ExecuteReader();
                cmd.Dispose();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader[0] == null)
                        {
                            count = 1;
                        }
                        else
                        {
                            count = Convert.ToInt32(reader[0].ToString()) + 1;
                        }
                    }

                    return count;
                }

                return -1;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }


            return 0;
        }

        private void ButtonSaveClicked()
        {
            btnStep1Insert.Enabled = true;
            btnStep1Save.Enabled = true;
            btnStep1Delete.Enabled = true;

            btnStep2Insert.Enabled = true;
            btnStep2Save.Enabled = true;
            btnStep2Delete.Enabled = true;

            btnStep3Insert.Enabled = true;
            btnStep3Save.Enabled = true;
            btnStep3Delete.Enabled = true;

            btnStep4Insert.Enabled = true;
            btnStep4Save.Enabled = true;
            btnStep4Delete.Enabled = true;
        }

        private void ButtonInsertClicked(string buttonName)
        {
            if (buttonName == "btnStep1Insert")
            {
                btnStep1Insert.Enabled = true;
                btnStep1Save.Enabled = true;
                btnStep1Delete.Enabled = true;

                btnStep2Insert.Enabled = false;
                btnStep2Save.Enabled = false;
                btnStep2Delete.Enabled = false;

                btnStep3Insert.Enabled = false;
                btnStep3Save.Enabled = false;
                btnStep3Delete.Enabled = false;

                btnStep4Insert.Enabled = false;
                btnStep4Save.Enabled = false;
                btnStep4Delete.Enabled = false;

            }
            else if (buttonName == "btnStep2Insert")
            {
                btnStep1Insert.Enabled = false;
                btnStep1Save.Enabled = false;
                btnStep1Delete.Enabled = false;

                btnStep2Insert.Enabled = true;
                btnStep2Save.Enabled = true;
                btnStep2Delete.Enabled = true;

                btnStep3Insert.Enabled = false;
                btnStep3Save.Enabled = false;
                btnStep3Delete.Enabled = false;

                btnStep4Insert.Enabled = false;
                btnStep4Save.Enabled = false;
                btnStep4Delete.Enabled = false;
            }
            else if (buttonName == "btnStep3Insert")
            {
                btnStep1Insert.Enabled = false;
                btnStep1Save.Enabled = false;
                btnStep1Delete.Enabled = false;

                btnStep2Insert.Enabled = false;
                btnStep2Save.Enabled = false;
                btnStep2Delete.Enabled = false;

                btnStep3Insert.Enabled = true;
                btnStep3Save.Enabled = true;
                btnStep3Delete.Enabled = true;

                btnStep4Insert.Enabled = false;
                btnStep4Save.Enabled = false;
                btnStep4Delete.Enabled = false;
            }
            else if (buttonName == "btnStep4Insert")
            {
                btnStep1Insert.Enabled = false;
                btnStep1Save.Enabled = false;
                btnStep1Delete.Enabled = false;

                btnStep2Insert.Enabled = false;
                btnStep2Save.Enabled = false;
                btnStep2Delete.Enabled = false;

                btnStep3Insert.Enabled = false;
                btnStep3Save.Enabled = false;
                btnStep3Delete.Enabled = false;

                btnStep4Insert.Enabled = true;
                btnStep4Save.Enabled = true;
                btnStep4Delete.Enabled = true;
            }
        }

        private void GetStep2Data(string firstMenu)
        {
            try
            {
                dataGridView2.CellValueChanged -= DataGridView_CellValueChanged;
                dataGridView2.CurrentCellChanged -= DataGridView_CurrentCellChanged;
                dataGridView3.CurrentCellChanged -= DataGridView_CurrentCellChanged;

                if (!Database.Open()) return;
                OracleCommand cmd = new OracleCommand(selectQueryString[1], Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = firstMenu;
                OracleDataReader reader = cmd.ExecuteReader();

                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                dataGridView4.Rows.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int row = dataGridView2.Rows.Add();
                        dataGridView2[0, row].Value = reader[0];
                        dataGridView2[1, row].Value = reader[1];
                        dataGridView2[2, row].Value = reader[2];
                        dataGridView2[3, row].Value = reader[3];
                        dataGridView2[4, row].Value = "N";
                        dataGridView2[5, row].Value = reader[4];

                        dataGridView2.Rows[row].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                    }
                }

                InitDataGridView.AutoSettingDatagridView(dataGridView2, new List<int>() { 1, 2 }, new List<int>());
                dataGridView2.CurrentCellChanged += DataGridView_CurrentCellChanged;
                dataGridView3.CurrentCellChanged += DataGridView_CurrentCellChanged;
                dataGridView2.CellValueChanged += DataGridView_CellValueChanged;
                dataGridView2.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Database.Close();
                if (dataGridView2.Rows.Count > 0)
                    GetStep3Data(dataGridView2[0, 0].Value.ToString(), dataGridView2[1, 0].Value.ToString());
            }
        }

        private void GetStep3Data(string step1, string step2)
        {
            try
            {
                dataGridView3.CellValueChanged -= DataGridView_CellValueChanged;
                dataGridView3.CurrentCellChanged -= DataGridView_CurrentCellChanged;

                dataGridView3.Rows.Clear();
                dataGridView4.Rows.Clear();

                if (!Database.Open()) return;
                OracleCommand cmd = new OracleCommand(selectQueryString[2], Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = step1;
                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = step2;
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int row = dataGridView3.Rows.Add();
                        dataGridView3[0, row].Value = reader[0];
                        dataGridView3[1, row].Value = reader[1];
                        dataGridView3[2, row].Value = reader[2];
                        dataGridView3[3, row].Value = reader[3];
                        dataGridView3[4, row].Value = reader[4];
                        dataGridView3[5, row].Value = "N";
                        dataGridView3[6, row].Value = reader[5]; ;
                        dataGridView3.Rows[row].DefaultCellStyle.BackColor = Colors.dgvBackColor;                       
                    }
                }

                InitDataGridView.AutoSettingDatagridView(dataGridView3, new List<int>() { 1, 2 }, new List<int>());
                dataGridView3.CellValueChanged += DataGridView_CellValueChanged;
                dataGridView3.CurrentCellChanged += DataGridView_CurrentCellChanged;
                dataGridView3.ReadOnly = false;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Database.Close();
                if (dataGridView3.Rows.Count > 0)
                    GetStep4Data(dataGridView3[0, 0].Value.ToString(), dataGridView3[1, 0].Value.ToString(), dataGridView3[2, 0].Value.ToString());

            }
        }

        private void GetStep4Data(string step1, string step2, string step3)
        {
            try
            {

                dataGridView4.CellValueChanged -= DataGridView_CellValueChanged;

                if (!Database.Open()) return;
                OracleCommand cmd = new OracleCommand(selectQueryString[3], Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = step1;
                cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = step2;
                cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = step3;

                OracleDataReader reader = cmd.ExecuteReader();

                dataGridView4.Rows.Clear();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int row = dataGridView4.Rows.Add();
                        dataGridView4[0, row].Value = reader[0];
                        dataGridView4[1, row].Value = reader[1];
                        dataGridView4[2, row].Value = reader[2];
                        dataGridView4[3, row].Value = reader[3];
                        dataGridView4[4, row].Value = reader[4];
                        dataGridView4[5, row].Value = reader[5];
                        dataGridView4[6, row].Value = "N";
                        dataGridView4[7, row].Value = reader[6];

                        dataGridView4.Rows[row].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                    }
                }

                InitDataGridView.AutoSettingDatagridView(dataGridView4, new List<int>() { 1,2 }, new List<int>());
                dataGridView4.CellValueChanged += DataGridView_CellValueChanged;
                dataGridView4.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Database.Close();
            }
        }

        private void GetStep1Data()
        {
            try
            {
                dataGridView1.CellValueChanged -= DataGridView_CellValueChanged;

                if (!Database.Open()) return;
                OracleCommand cmd = new OracleCommand(selectQueryString[0], Database.OracleConn);
                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        int row = dataGridView1.Rows.Add();
                        dataGridView1[0, row].Value = reader[0];
                        dataGridView1[1, row].Value = reader[1];
                        dataGridView1[2, row].Value = reader[2];
                        dataGridView1[3, row].Value = "N";
                        dataGridView1[4, row].Value = reader[3];

                        dataGridView1.Rows[row].DefaultCellStyle.BackColor = Colors.dgvBackColor;                       
                    }
                }

                InitDataGridView.AutoSettingDatagridView(dataGridView1, new List<int>() { 1 }, new List<int>());
                dataGridView1.CellValueChanged += DataGridView_CellValueChanged;
                dataGridView1.ReadOnly = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                Database.Close();
            }
        }
                

        private bool DeleteMenu1(int seq)
        {
            string query = string.Empty;
            query = "DELETE HMI_MENU_ONE WHERE SEQ = :1";

            try
            {
                if (!Database.Open()) return false;
                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Int32).Value = seq;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                Database.Close();
            }

            return true;
        }

        private bool DeleteMenu2(int seq)
        {
            string query = string.Empty;
            query = "DELETE HMI_MENU_TWO WHERE MENU_SEQ = :1";

            try
            {
                if (!Database.Open()) return false;
                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Int32).Value = seq;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                Database.Close();
            }

            return true;
        }

        private bool DeleteMenu3(int seq)
        {

            string query = string.Empty;
            query = "DELETE HMI_MENU_THREE WHERE MENU_SEQ = :1";

            try
            {
                if (!Database.Open()) return false;
                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Int32).Value = seq;

                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                Database.Close();
            }

            return true;
        }

        private bool DeleteMenu4(int seq)
        {
            string query = string.Empty;
            query = "DELETE HMI_MENU_FOUR WHERE MENU_SEQ = :1";

            try
            {
                if (!Database.Open()) return false;
                OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
                cmd.Parameters.Add(":1", OracleDbType.Int32).Value = seq;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            finally
            {
                Database.Close();
            }

            return true;
        }

        private void btnStep4Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView4.Rows.Count < 1) return;
                int row = dataGridView4.CurrentCell.RowIndex;
                string step4 = dataGridView4[3, row].Value.ToString();
                int seq = Convert.ToInt32(dataGridView4[7, row].Value.ToString());

                DialogResult rtn = MessageBox.Show("Do you want delete " + step4 + "?", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rtn == DialogResult.Yes)
                {
                    if (DeleteMenu4(seq) == true)
                    {
                        dataGridView4.Rows.RemoveAt(row);                       
                    }
                }
                else
                {
                    //MessageBox.Show("Canceled");
                }
            }
            catch
            {

            }
        }

        private void btnStep3Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView3.Rows.Count < 1) return;
                int row = dataGridView3.CurrentCell.RowIndex;
                string step3 = dataGridView3[2, row].Value.ToString();
                int seq = Convert.ToInt32(dataGridView3[6, row].Value.ToString());

                DialogResult rtn = MessageBox.Show("Do you want delete " + step3 + "?", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rtn == DialogResult.Yes)
                {
                    if (DeleteMenu3(seq) == true)
                    {
                        dataGridView3.Rows.RemoveAt(row);
                        //MessageBox.Show("Deleted");
                    }
                }
                else
                {
                   // MessageBox.Show("Canceled");
                }
            }
            catch
            {

            }
        }

        private void btnStep2Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count < 1) return;

                int row = dataGridView2.CurrentCell.RowIndex;
                string step2 = dataGridView2[1, row].Value.ToString();
                int seq = Convert.ToInt32(dataGridView2[5, row].Value.ToString());

                DialogResult rtn = MessageBox.Show("Do you want delete " + step2 + "?", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rtn == DialogResult.Yes)
                {
                    if (DeleteMenu2(seq) == true)
                    {
                        dataGridView2.Rows.RemoveAt(row);
                       // MessageBox.Show("Deleted");
                    }
                }
                else
                {
                    //MessageBox.Show("Canceled");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void btnStep1Delete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count < 1) return;

                int row = dataGridView1.CurrentCell.RowIndex;
                int seq = Convert.ToInt32(dataGridView1[2, row].Value.ToString());
                string menuName = dataGridView1[0, row].Value.ToString();

                DialogResult rtn = MessageBox.Show("Do you want to delete " + menuName + "?", "delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rtn == DialogResult.Yes)
                {
                    if (DeleteMenu1(seq) == true)
                    {
                        dataGridView1.Rows.RemoveAt(row);
                       // MessageBox.Show("Deleted");
                    }
                }
                else
                {
                   // MessageBox.Show("Canceled");
                }
            }
            catch
            {

            }
        }
    }
}
