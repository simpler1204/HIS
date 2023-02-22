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
using HIS.PopUp;

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
                        if (form.GetType() == typeof(FormNewTrendGroup))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    FormNewTrendGroup frm = new FormNewTrendGroup();
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

                case "HMI Trend":
                    //mainForm.sendMsgToOA("find;TrendGroup1");
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(PopUpSearchTrendGroup))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    PopUpSearchTrendGroup trend2 = new PopUpSearchTrendGroup(mainForm, "hmi");
                    //trend2.StartPosition = FormStartPosition.WindowsDefaultLocation;
                    trend2.ShowDialog();
                    break;
                case "HIS Trend":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(PopUpSearchTrendGroup))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    PopUpSearchTrendGroup frmTrend2 = new PopUpSearchTrendGroup(mainForm, "his");
                    //trend2.StartPosition = FormStartPosition.WindowsDefaultLocation;
                    frmTrend2.ShowDialog();

                    break;
                case "Trend3":
                    mainForm.sendMsgToOA("find;TrendGroup3");
                    //foreach (Form form in Application.OpenForms)
                    //{
                    //    if (form.GetType() == typeof(FormTrend3))
                    //    {
                    //        form.Activate();
                    //        form.WindowState = FormWindowState.Normal;
                    //        return;
                    //    }
                    //}
                    //FormTrend3 trend3 = new FormTrend3(mainForm);
                    //trend3.Show();
                    break;

            }
        }

      
    }
}
