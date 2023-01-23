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
    public partial class PopUpInsertDp : Form
    {
        public event EventHandler<List<string>> InsertEvent;
        public PopUpInsertDp()
        {
            InitializeComponent();
            this.Load += PopUpInsertDp_Load;
            menu.ButtonClick += Menu_ButtonClick;

            this.FormClosing += (sender, e) =>
            {
                this.Load -= PopUpInsertDp_Load;
                menu.ButtonClick -= Menu_ButtonClick;
            };
        }

        private void Menu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string name = e.Button.Properties.Caption;
            switch(name)
            {
                case "New":
                    NewDp();
                    break;
                case "Cancel":
                    CancelDp();
                    break;
                case "Save":
                    DialogResult rtn = MessageBox.Show("Do you want insert new dp ?", "Warning", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                    if (rtn == DialogResult.Yes)
                    {
                        SaveDp();
                    }
                    break;
            }
        }

        private void SaveDp()
        {
            string system = cmbSystem.Text;
            string dpName = txtDpName.Text.Trim();
            string dpDesc = txtDpDesc.Text;
            string yMin = txtYMin.Text;
            string yMax = txtYMax.Text;
                       
            bool rtnMin = float.TryParse(yMin, out float fMin);
            bool rtnMax = float.TryParse(yMax, out float fMax);

            if(!rtnMin || !rtnMax)
            {
                MessageBox.Show("Check Min, Max value", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if(dpName == "")
            {
                MessageBox.Show("Check DP name..", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!Database.Open()) return;
            string query = @"INSERT INTO C2_TREND_INFO(SYSTEM, DP_NAME, DP_DESC, Y_MIN, Y_MAX) 
                            VALUES(:1, :2, :3, :4, :5)";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = system;
            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = dpName;
            cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = dpDesc;
            cmd.Parameters.Add(":4", OracleDbType.Varchar2).Value = yMin;
            cmd.Parameters.Add(":5", OracleDbType.Varchar2).Value = yMax;

            int rtn = 0;
            try
            {
                rtn = cmd.ExecuteNonQuery();
                CancelDp();
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
            
            if(rtn > 0)
            {
                List<string> para = new List<string>();
                para.Add(system);
                para.Add(dpName);
                para.Add(dpDesc);
                para.Add(yMin);
                para.Add(yMax);

                InsertEvent(this, para);
            }
        }

        private void CancelDp()
        {
            cmbSystem.Text = "C2_HVAC_S1";
            txtDpName.Text = "";
            txtDpDesc.Text = "";
            txtYMin.Text = "";
            txtYMax.Text = "";

            cmbSystem.Enabled = false;
            txtDpName.Enabled = false;
            txtDpDesc.Enabled = false;
            txtYMin.Enabled = false;
            txtYMax.Enabled = false;
        }

        private void NewDp()
        {
            CancelDp();
            cmbSystem.Enabled = true;
            cmbSystem.Text = "C2_HVAC_S1";
            txtDpName.Enabled = true;
            txtDpDesc.Enabled = true;
            txtYMin.Enabled = true;
            txtYMax.Enabled = true;

        }

        private void PopUpInsertDp_Load(object sender, EventArgs e)
        {
            menu.ForeColor = Colors.buttonForeColor;
        }
    }
}
