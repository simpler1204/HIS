using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Forms
{
    public partial class FormAlarmMain : Form
    {
        MainForm mainForm;
        public FormAlarmMain(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            menuPanel.ButtonClick += MenuPanel_ButtonClick;
        }

        private void MenuPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;
            switch(buttonName)
            {
                case "History":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormAlarm))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    FormAlarm alarm = new FormAlarm(mainForm);
                    alarm.Show();
                    break;
            }
        }
    }
}
