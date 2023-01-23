using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.PopUp
{
    public partial class PopUpCreateTrendGroup : Form
    {
        public delegate void createGroupEventHandler(string[] val);
        public event createGroupEventHandler eventCreate;

        public PopUpCreateTrendGroup()
        {
            InitializeComponent();
            menu.ButtonClick += Menu_ButtonClick;

            this.FormClosing += (sender, e) =>
            {
                menu.ButtonClick -= Menu_ButtonClick;
            };
        }

        private void Menu_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;
            switch(buttonName)
            {
                case "Apply":
                    ApplyGroup();
                    break;
            }
        }

        private void ApplyGroup()
        {
            string[] total = new string[3];
            total[0] = txtPart.Text.Trim();
            total[1] = txtGroup.Text.Trim();
            total[2] = txtDesc.Text.Trim();

            if(total[0] == "")
            {
                txtPart.Focus();
                return;
            }
            if (total[1] == "")
            {
                txtGroup.Focus();
                return;
            }
            if (total[2] == "")
            {
                txtDesc.Focus();
                return;
            }

            eventCreate(total);

            this.Close();

        }
    }
}
