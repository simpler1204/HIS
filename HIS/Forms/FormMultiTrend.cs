using HIS.Class;
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
    public partial class FormMultiTrend : Form
    {
        MainForm mainForm;
        public FormMultiTrend(MainForm mainForm)
        {
            InitializeComponent();
            Load += FormMultiTrend_Load;
            menuPanel.ButtonClick += MenuPanel_ButtonClick;
            this.mainForm = mainForm;

            this.FormClosing += (sender, e) =>
            {
                Load -= FormMultiTrend_Load;
                menuPanel.ButtonClick -= MenuPanel_ButtonClick;
            };
        }

        private void FormMultiTrend_Load(object sender, EventArgs e)
        {
            
        }

        private void MenuPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;
            switch(buttonName)
            {
                case "Group":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormCreateGroup))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    FormCreateGroup frm = new FormCreateGroup();
                    frm.Show();
                    break;

                case "Mapping":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormTrendGroupSetting))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    FormTrendGroupSetting group = new FormTrendGroupSetting();
                    group.Show();
                    break;

                case "Trend1":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormTrend1))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    FormTrend1 trend1 = new FormTrend1(mainForm);
                    trend1.Show();
                    break;
                case "Trend2":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormTrend2))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    FormTrend2 trend2 = new FormTrend2(mainForm);
                    trend2.Show();
                    break;
                case "Trend3":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormTrend3))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    FormTrend3 trend3 = new FormTrend3(mainForm);
                    trend3.Show();
                    break;

            }
        }

      
    }
}
