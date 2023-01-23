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
    public partial class FormMenuMain : Form
    {
        MainForm mainForm;
        public FormMenuMain(MainForm mainForm)
        {
            InitializeComponent();
            menuPanel.ButtonClick += MenuPanel_ButtonClick;
            this.mainForm = mainForm;
            //if(MainForm.isOAConnected == false)
            if(mainForm.userGrade < 3)
            {
                menuPanel.Buttons["Edit"].Properties.Visible = false;
            }
            mainForm.MsgFromOa += MainForm_MsgFromOa;

            this.FormClosing += (sender, e) =>
            {
                menuPanel.ButtonClick -= MenuPanel_ButtonClick;
                mainForm.MsgFromOa -= MainForm_MsgFromOa;
            };
            
        }

        private void MainForm_MsgFromOa(string val)
        {
            string[] receiveData = val.Split(';');

            if(receiveData[0] == "disconnected")
            {
                if (this.InvokeRequired)
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {                            
                                menuPanel.Buttons["Edit"].Properties.Visible = false;                           
                        }));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }

            if (receiveData[0] == "User")
            {
                if (receiveData.Length < 4)
                {
                    Console.WriteLine("Check user id, name, grade..");
                    return;
                }

                string userID = receiveData[1];
                string userName = receiveData[2];
                int userGrade = Convert.ToInt32(receiveData[3]);

                if (this.InvokeRequired)
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            if (userGrade == 0)
                            {
                                //"GUEST";
                                menuPanel.Buttons["Edit"].Properties.Visible = false;
                            }
                            else if (userGrade == 1)
                            {
                                // "USER";
                                menuPanel.Buttons["Edit"].Properties.Visible = false;
                            }
                            else if (userGrade == 2)
                            {
                                //"ADMIN";
                                menuPanel.Buttons["Edit"].Properties.Visible = false;
                            }
                            else if (userGrade == 3)
                            {
                                // "SYSTEM";
                                menuPanel.Buttons["Edit"].Properties.Visible = true;
                            }
                            else
                            {
                                // "GUEST";
                                menuPanel.Buttons["Edit"].Properties.Visible = false;
                            }
                        }));


                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

      

        private void MenuPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;

            switch(buttonName)
            {
                case "Edit":
                  foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormMenu))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    FormMenu menu = new FormMenu();
                    menu.Show();
                    break;
            }
        }
    }
}
