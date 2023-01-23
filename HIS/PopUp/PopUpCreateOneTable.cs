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

namespace HIS.Popup
{
    public partial class PopUpCreateOneTable : Form
    {
        public static PopUpCreateOneTable createdForm = null;

        public delegate void TrendCreateDelegate(string[] val);
        public event TrendCreateDelegate CreateTrendEvent;
        

        public PopUpCreateOneTable()
        {
            InitializeComponent();
            createdForm = this;
            this.FormClosed += PopUpCreateOneTable_FormClosed;
            this.FormClosing += PopUpCreateOneTable_FormClosing;

        }

        private void PopUpCreateOneTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            createdForm = null;
        }

        private void PopUpCreateOneTable_FormClosed1(object sender, FormClosedEventArgs e)
        {
            createdForm = null;
        }

        private void PopUpCreateOneTable_FormClosed(object sender, FormClosedEventArgs e)
        {
            createdForm = null;
        }

        private void PopUpCreateOneTable_Load(object sender, EventArgs e)
        {
            menu.ForeColor = Colors.buttonForeColor;
            rdo3Sec.Checked = true;
            rdoB.Checked = true;
            rdo3SecMulti.Checked = true;
            rdoBMulti.Checked = true;
        }

        private void menu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string menu = e.Button.Properties.Caption;
            string[] contents = new string[7];
            if(menu == "Save")
            {
                if(CheckEmptyContents() == false)
                {
                    MessageBox.Show("Fill the items..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                contents[0] = txtTableName.Text;
                contents[1] = txtDesc.Text;
                contents[2] = cmbSystem.Text;
                contents[3] = GetLoggingCycle();
                contents[4] = GetSavingPeriod();
                contents[5] = "";
                contents[6] = "";
                

                CreateTrendEvent(contents);

                txtTableName.Text = "";
                txtDesc.Text = "";
                cmbSystem.Text = "";
            }
            if(menu == "New")
            {
                txtTableName.Text = GetTableName();              
            }
        }

        private bool CheckEmptyContents()
        {
            if (txtTableName.Text == "") return false;
            if (cmbSystem.Text == "") return false;
            return true;
        }

        private bool CheckEmptyContentsMulti()
        {
            if (cmbCount.Text == "") return false;
            if (cmbSystemMulti.Text == "") return false;
            return true;
        }

        private string GetLoggingCycle()
        {
            if (rdo1Sec.Checked) return "1SEC";
            if (rdo3Sec.Checked) return "3SEC";
            if (rdo10Sec.Checked) return "10SEC";
            if (rdo1Min.Checked) return "1Min";

            return null;
        }

        private string GetSavingPeriod()
        {
            if (rdoA.Checked) return "A";
            if (rdoB.Checked) return "B";
            if (rdoC.Checked) return "C";

            return null;
        }

        private string GetLoggingCycleMulti()
        {
            if (rdo1SecMulti.Checked) return "1SEC";
            if (rdo3SecMulti.Checked) return "3SEC";
            if (rdo10SecMulti.Checked) return "10SEC";
            if (rdo1MinMulti.Checked) return "1Min";

            return null;
        }

        private string GetSavingPeriodMulti()
        {
            if (rdoAMulti.Checked) return "A";
            if (rdoBMulti.Checked) return "B";
            if (rdoCMulti.Checked) return "C";

            return null;
        }

        private string GetTableName()
        {
            if (!Database.Open()) return null;

            string query = "SELECT COUNT(TABLE_NAME) FROM C2_TREND_TABLE_INFO_MASTER";
            OracleCommand cmd = new OracleCommand(query, Database.OracleConn);
            OracleDataReader reader = cmd.ExecuteReader();
            int rowCount = 0;
            while (reader.Read())
            {
                int.TryParse(reader[0].ToString(), out rowCount);
            }

            string tableName = "C2_TREND_" + (rowCount + 1).ToString("0000") + "_" + DateTime.Now.ToString("mmss");            

            Database.Close();

            return tableName;
        }

        private void menuMulti_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;
            switch(buttonName)
            {
                case "Multi New":
                    CreateMultiTable();
                    break;
            }
        }

        private void CreateMultiTable()
        {
            if (CheckEmptyContentsMulti() == false)
            {
                MessageBox.Show("Fill the items..", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult rtn = MessageBox.Show("Do you want create tables?", "Information", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            if (rtn == DialogResult.No)
                return;
            

            string[] contents = new string[7];

            int count = int.Parse(cmbCount.Text);

            for(int i=0; i<count; i++)
            {
                contents[0] = GetTableName();
                contents[1] = txtDexcMulti.Text;
                contents[2] = cmbSystemMulti.Text;
                contents[3] = GetLoggingCycleMulti();
                contents[4] = GetSavingPeriodMulti();
                contents[5] = "";
                contents[6] = "";
                CreateTrendEvent(contents);

                System.Threading.Thread.Sleep(200);
            }
          
        }
    }
}
