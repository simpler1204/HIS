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
    public partial class FormSmsMain : Form
    {
        MainForm mainForm;
        public FormSmsMain(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
           // if (MainForm.isOAConnected == false)
           if(mainForm.userGrade < 3)
            {
                menuPanel.Buttons["Setting"].Properties.Visible = false;
            }

            mainForm.MsgFromOa += MainForm_MsgFromOa;


            this.FormClosing += (sender, e) =>
            {
                mainForm.MsgFromOa -= MainForm_MsgFromOa;
            };
        }

        private void MainForm_MsgFromOa(string val)
        {
            string[] receiveData = val.Split(';');

            if (receiveData[0] == "disconnected")
            {
                if (this.InvokeRequired)
                {
                    try
                    {
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            menuPanel.Buttons["Setting"].Properties.Visible = false;
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
                                menuPanel.Buttons["Setting"].Properties.Visible = false;
                            }
                            else if (userGrade == 1)
                            {
                                // "USER";
                                menuPanel.Buttons["Setting"].Properties.Visible = false;
                            }
                            else if (userGrade == 2)
                            {
                                //"ADMIN";
                                menuPanel.Buttons["Setting"].Properties.Visible = false;
                            }
                            else if (userGrade == 3)
                            {
                                // "SYSTEM";
                                menuPanel.Buttons["Setting"].Properties.Visible = true;
                            }
                            else
                            {
                                // "GUEST";
                                menuPanel.Buttons["Setting"].Properties.Visible = false;
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
    }
}
