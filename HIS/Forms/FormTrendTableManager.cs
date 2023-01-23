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
using HIS.Forms;
using Oracle.ManagedDataAccess.Client;

namespace HIS.Forms
{
    public partial class FormTrendTableManager : Form
    {
        MainForm mainForm;
        public FormTrendTableManager(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            //if (MainForm.isOAConnected == false)
            if(mainForm.userGrade < 3)
            {                
                menuPanel.Buttons["TrendInfo"].Properties.Visible = false;
                menuPanel.Buttons["Manager"].Properties.Visible = false;
                menuPanel.Buttons["Mapping"].Properties.Visible = false;
                menuPanel.Buttons["Initialize"].Properties.Visible = false;
            }

            this.mainForm.MsgFromOa += MainForm_MsgFromOa;


            this.FormClosing += (sender, e) =>
            {
                this.mainForm.MsgFromOa -= MainForm_MsgFromOa;
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
                            menuPanel.Buttons["TrendInfo"].Properties.Visible = false;
                            menuPanel.Buttons["Manager"].Properties.Visible = false;
                            menuPanel.Buttons["Mapping"].Properties.Visible = false;
                            menuPanel.Buttons["Initialize"].Properties.Visible = false;
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
                                menuPanel.Buttons["TrendInfo"].Properties.Visible = false;
                                menuPanel.Buttons["Manager"].Properties.Visible = false;
                                menuPanel.Buttons["Mapping"].Properties.Visible = false;
                                menuPanel.Buttons["Initialize"].Properties.Visible = false;
                            }
                            else if (userGrade == 1)
                            {
                                // "USER";
                                menuPanel.Buttons["TrendInfo"].Properties.Visible = false;
                                menuPanel.Buttons["Manager"].Properties.Visible = false;
                                menuPanel.Buttons["Mapping"].Properties.Visible = false;
                                menuPanel.Buttons["Initialize"].Properties.Visible = false;
                            }
                            else if (userGrade == 2)
                            {
                                //"ADMIN";
                                menuPanel.Buttons["TrendInfo"].Properties.Visible = false;
                                menuPanel.Buttons["Manager"].Properties.Visible = false;
                                menuPanel.Buttons["Mapping"].Properties.Visible = false;
                                menuPanel.Buttons["Initialize"].Properties.Visible = false;
                            }
                            else if (userGrade == 3)
                            {
                                // "SYSTEM";
                                menuPanel.Buttons["TrendInfo"].Properties.Visible = true;
                                menuPanel.Buttons["Manager"].Properties.Visible = true;
                                menuPanel.Buttons["Mapping"].Properties.Visible = true;
                                menuPanel.Buttons["Initialize"].Properties.Visible = true;
                            }
                            else
                            {
                                // "GUEST";
                                menuPanel.Buttons["TrendInfo"].Properties.Visible = false;
                                menuPanel.Buttons["Manager"].Properties.Visible = false;
                                menuPanel.Buttons["Mapping"].Properties.Visible = false;
                                menuPanel.Buttons["Initialize"].Properties.Visible = false;
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

        private void FormTrendTableManager_Load(object sender, EventArgs e)
        {
            panel2.Focus();

            //버튼 컬러
            menuPanel.ForeColor = Colors.buttonForeColor;

        }

        private void menuPanel_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string menuName = e.Button.Properties.Caption;
            TrendManagerFormOpen(menuName);
        }

        
        private void TrendManagerFormOpen(string menuName)
        {
            Form createForm = null;

            switch(menuName)
            {
                case "Manager":
                    // popup form single tone  적용됐음
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormPopupCreateTable))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    createForm = FormPopupCreateTable.createdForm == null ? new FormPopupCreateTable() : FormPopupCreateTable.createdForm;
                    break;
                case "TrendInfo":
                    // popup form single tone  적용됐음
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormPopupTrendInfo))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    createForm = FormPopupTrendInfo.createdForm == null ? new FormPopupTrendInfo() : FormPopupTrendInfo.createdForm;
                    break;

                case "Mapping":
                    foreach (Form form in Application.OpenForms)
                    {
                        if (form.GetType() == typeof(FormSettingDpToTable))
                        {
                            form.Activate();
                            form.WindowState = FormWindowState.Normal;
                            return;
                        }
                    }
                    createForm = FormSettingDpToTable.createdForm == null ? new FormSettingDpToTable() : FormSettingDpToTable.createdForm;
                    break;

                case "Initialize":
                    InitializeTrend();
                    break;
            }

            if (createForm != null)
            {
                createForm.StartPosition = FormStartPosition.CenterScreen;
                
                //createForm.TopLevel = false;
                //formPanel.Controls.Add(createForm);
                //createForm.FormBorderStyle = FormBorderStyle.None;
                //createForm.WindowState = FormWindowState.Maximized;
                createForm.Show();
                createForm.BringToFront();
            }
            
        }

        private void InitializeTrend()
        {

            DialogResult result = MessageBox.Show("You are going to lose all trend data, Do you really want to initialize?", "Warning",
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

            if (result == DialogResult.No) return;

            DialogResult rtn = MessageBox.Show("Do you really want to Initialize Trend data?", "Warning", 
                                                MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button2);

            if(rtn == DialogResult.Yes)
            {
                if (!Database.Open()) return;

                string[] query = new string[4];
                query[0] = "UPDATE C2_TREND_INFO SET TB_NAME = NULL, COL_NAME = NULL, UPDATED_AT = NULL";
                query[1] = "UPDATE C2_TREND_TABLE_INFO_MASTER SET EMPTY = 100, CREATED_AT = NULL, UPDATED_AT = NULL";
                query[2] = "DELETE C2_DP_TREND_MOVE_HISTORY";
                query[3] = "UPDATE C2_TREND_TABLE_INFO_DETAIL SET ";
                for(int i=1;i<=99;i++)
                {
                    query[3] += "COL_" + i.ToString("0000") + " = NULL, ";
                }
                query[3] += "COL_0100 = NULL";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = Database.OracleConn;

                try
                {
                    for (int i = 0; i < query.Length; i++)
                    {
                        cmd.CommandText = query[i];
                        cmd.ExecuteNonQuery();
                    }
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

            }
        }

    }
}

 


