using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontAwesome.Sharp;
using System.Threading;
using Microsoft.Win32;
using HIS.Class;
using Oracle.ManagedDataAccess.Client;
using System.IO.Pipes;
using DevExpress.XtraSplashScreen;
using System.IO;
using HIS.Forms;

namespace HIS
{
    public partial class MainForm : Form
    {
        //Fields
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;

        //IPC
    
        static public bool isOAConnected = false;
        public delegate void ipcMsgEventHandler(string val);
        public  event  ipcMsgEventHandler MsgFromOa;


        private static string ServerString = "ClientOnOA";  //WINCCOA와 서로 반대
        private static string ClientString = "ServerOnOA";

        private  NamedPipeServerStream Server;
        private  NamedPipeClientStream Client;
        private static Thread ServerThread;
        

        //user정보
        string userID = string.Empty;
        string userName = string.Empty;
        public int userGrade = 0;

        //
        FormTrend4 trend4;

        public static SplashScreenManager splashScreenManager1;


        //Constructor
        public MainForm()
        {
            /**************** checking pc permission ********************/
            RegistryKey rk = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Simens\Qt\register");
            object rtn;

            if (rk == null)
            {
                //Console.WriteLine("not allowed this pc.");
                return;
            }
            else
            {
                rtn = rk.GetValue("Enable");
            }

            if (rtn.ToString() != "true")
            {
                // Console.WriteLine("not allowed this pc");
                return;
            }
            /************************************/


            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 60);
            panelMenu.Controls.Add(leftBorderBtn);

            //Form
            this.Text = string.Empty;
            this.ControlBox = false;
            this.DoubleBuffered = true;

        }
     

        //Methods
        private void ActivateButton(object senderBtn, Color color)
        {
            if (senderBtn != null)
            {
                DisableButton();

                //Button
                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(37, 36, 81);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                //Left border button
                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                //Icon Current Child Form
                iconCurrentChildFrom.IconChar = currentBtn.IconChar;
                iconCurrentChildFrom.IconColor = color;
            }
        }

        private void DisableButton()
        {
            if(currentBtn != null)
            {             
                currentBtn.BackColor = Color.FromArgb(31, 30, 68);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if(currentChildForm != null)
            {
                //open only form
                currentChildForm.Close();
            }

            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitleChildForm.Text = childForm.Text;

        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.color1);
            OpenChildForm(new Forms.FormAlarmMain(this));
        }

        private void btnOperation_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.color2);
            OpenChildForm(new Forms.FormOperationMain());
        }

        private void btnMultiTrend_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.color3);
            OpenChildForm(new Forms.FormMultiTrend(this));
        }

        private void btnSms_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.color4);
            OpenChildForm(new Forms.FormSmsMain(this));
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.color5);
            OpenChildForm(new Forms.FormMenuMain(this));
        }        

        private void btn_logging_Click(object sender, EventArgs e)
        {
            ActivateButton(sender, Colors.color6);
            OpenChildForm(new Forms.FormTrendTableManager(this));
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            sendMsgToOA("User");
            if (currentChildForm != null)
                currentChildForm.Close();

            Reset();
        }

        

        private void Reset()
        {
            DisableButton();
            leftBorderBtn.Visible = false;
            iconCurrentChildFrom.IconChar = IconChar.Home;
            iconCurrentChildFrom.IconColor = Color.MediumPurple;
            lblTitleChildForm.Text = "HOME";
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_restore_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                WindowState = FormWindowState.Maximized;
            else
                WindowState = FormWindowState.Normal;
        }

        private void btn_minimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

       
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            // OA로 부터 오는 MSG 수신 이벤트
            //MsgFromOa += getIpcMessage;
            //ipcServer = new Thread(new ThreadStart(openServer));
            //ipcServer.Start();  

            MsgFromOa += getIpcMessage;         

            ServerThread = new Thread(new ThreadStart(StartServer));
            ServerThread.Start();
                      


            // 두번째 모니터에 화면 Max size
            ScreenSetting(this);

           // Oracle 연결, 연결 안되면 프로그램 종료
            OracleConnection result = Database.CreateDatabase();
            if (result == null)
            {
                MessageBox.Show("Oracle connecting was failed.\nHIS will be terminated.",
                    "Database Connection", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            splashScreenManager1 = new SplashScreenManager(this, typeof(global::HIS.Forms.WaitForm1), true, true);
            splashScreenManager1.ClosingDelay = 500;           

        }


        private async void StartServer()
        {
            while (true)
            {
                Server = new NamedPipeServerStream(ServerString);
                try
                {
                    Server.WaitForConnection();

                    try
                    {
                        ClientOpen();
                    }
                    catch
                    {
                        //MessageBox.Show(ex.Message);
                    }

                    if (Client.IsConnected == true)
                    {
                        isOAConnected = true;
                        MsgFromOa("connected");
                    }


                    try
                    {
                        if (Client.IsConnected == true)
                        {
                            sendMsgToOA("User;");
                        }
                    }
                    catch//(Exception ex)
                    {
                    }

                    while (Server.IsConnected)
                    {
                        var read = new byte[4096];
                        await Server.ReadAsync(read, 0, read.Length);
                        var msg = Encoding.UTF8.GetString(read).TrimEnd('\0');
                        MsgFromOa(msg);
                    }
                }
                catch
                {
                    MsgFromOa("disconnected");
                     // MessageBox.Show(ex.ToString());
                }
                finally
                {
                    Server.Disconnect();
                    Server.Dispose();
                    isOAConnected = false;
                    Thread.Sleep(1000);
                    MsgFromOa("disconnected");
                }
            }
        }
               

        public void ClientOpen()
        {
            Client = new NamedPipeClientStream(ClientString);
            Client.Connect();

        }

        public  async void Write(string msg)
        {
            try
            {
                var write = Encoding.UTF8.GetBytes(msg);

                await Client.WriteAsync(write, 0, write.Length);
                Client.Flush();
            }
            catch
            { }
        }



        private void ScreenSetting(Form frm)
        {
            Screen[] sc = Screen.AllScreens;
            if (sc.Length > 1)
            {
                Screen screen = (sc[0].WorkingArea.Contains(this.Location)) ? sc[1] : sc[0];
               
                frm.Location = new Point(2000, 10);//screen.Bounds.Location;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }
        }

        public void getIpcMessage(string val)
        {
            
           
            if (val == "Restart")
            {               
                isOAConnected = false;
                //OaDisconnected();
            }

            string[] receiveData = val.Split(';');              

          

            if (receiveData[0] == "User") //user login
            {
                
                UserInfo(receiveData);
            }

            if (receiveData[0] == "disconnected") //OA status
            {
               // OaDisconnected();
            }

            if (receiveData[0] == "connected") //OA status
            {
                //OaConnected();
            }

            if (receiveData[0] == "trendAdd" || receiveData[0] == "trendGroup")
            {
                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {                      
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form.GetType() == typeof(FormTrend1))
                            {                               
                                return;
                            }

                            if (form.GetType() == typeof(FormTrend2))
                            {                               
                                return;
                            }

                            if (form.GetType() == typeof(FormTrend3))
                            {                              
                                return;
                            }

                            if (form.GetType() == typeof(FormTrend4))
                            {                                
                                form.Activate();
                                form.WindowState = FormWindowState.Normal;
                                return;
                            }
                        }
                        trend4 = new FormTrend4(this);
                        trend4.StartPosition = FormStartPosition.CenterScreen;
                        trend4.TopMost = true;
                        trend4.Show();
                        trend4.Activate();

                        if (receiveData[0] == "trendAdd")
                            trend4.trendQueue.Enqueue(receiveData[1]);
                        else if (receiveData[0] == "trendGroup")
                            trend4.CreateSeriesByTrendGroup(receiveData[1]);

                    }));
                }
                catch
                {
                    // MessageBox.Show(ex.Message);
                }
            }

            if (receiveData[0] == "alarm" && receiveData[1] == "select") //알람조회 창을 켜면서 실행
            {
                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form.GetType() == typeof(FormAlarm))
                            {
                                // form.Close();
                                form.Activate();
                                //form.WindowState = FormWindowState.Normal;
                                FormAlarm alarm1 = form as FormAlarm;
                                alarm1.txtDpName.Text = receiveData[2];
                                alarm1.SearhAlarmHistory();
                                return;
                            }
                        }
                        FormAlarm alarm2 = new FormAlarm(this);
                        alarm2.Show();
                        alarm2.Activate();
                        alarm2.txtDpName.Text = receiveData[2];
                        alarm2.SearhAlarmHistory();

                    }));
                }
                catch
                {
                    // MessageBox.Show(ex.Message);
                }
               
            }

            if (receiveData[0] == "operation" && receiveData[1] == "select") //조작이력
            {

                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form.GetType() == typeof(FormOperation))
                            {
                                // form.Close();
                                form.Activate();
                                //form.WindowState = FormWindowState.Normal;
                                FormOperation operation1 = form as FormOperation;
                                operation1.txtDp.Text = receiveData[2];
                                operation1.startDt.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
                                operation1.endDt.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                                operation1.SearchOperationHistory();
                                return;
                            }
                        }
                        FormOperation operation2 = new FormOperation();
                        operation2.Show();
                        operation2.Activate();
                        operation2.txtDp.Text = receiveData[2];
                        operation2.startDt.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
                        operation2.endDt.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        operation2.SearchOperationHistory();

                    }));
                }
                catch 
                {
                    // MessageBox.Show(ex.Message);
                }

            }

            if (receiveData[0] == "addDp") //trend dp 추가
            {
                string system = receiveData[1];
                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form.GetType() == typeof(FormAddDp))
                            {
                                form.Close();
                                break;
                                //form.Activate();
                                //FormAddDp formAddDp = form as FormAddDp;
                                //formAddDp.GetTables(val);
                                //return;
                            }
                          
                        }
                        FormAddDp addDp = new FormAddDp(val);                        
                        //addDp.Show();
                        //addDp.Activate();
                        //addDp.GetTables(val);

                    }));
                }
                catch
                {
                    // MessageBox.Show(ex.Message);
                }

            }

            if (receiveData[0] == "trendGroupSetting") //trend group setting
            {
                string system = receiveData[1];
                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        foreach (Form form in Application.OpenForms)
                        {
                            if (form.GetType() == typeof(FormsTrendGroupSetting))
                            {
                                form.Close();
                                break;
                            }
                        }
                        FormsTrendGroupSetting trendGrouSetting = new FormsTrendGroupSetting(receiveData[1]);
                        trendGrouSetting.Show();
                        trendGrouSetting.Activate();                       

                    }));
                }
                catch(Exception ex)
                {
                     MessageBox.Show(ex.ToString());
                }

            }
        }


        private void UserInfo(string[] receiveData)
        {
            if (receiveData.Length < 4)
            {
                Console.WriteLine("Check user id, name, grade..");
                return;
            }

            userID = receiveData[1];
            userName = receiveData[2];
            userGrade = Convert.ToInt32(receiveData[3]);

            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        lblID.Text = userID.ToUpper();
                        //lblName.Text = userName;
                        if (userGrade == 0)
                        {
                            lblGrade.Text = "GUEST";
                            label2.Visible = false;
                            label4.Visible = false;
                            lblID.Visible = false;
                            lblGrade.Visible = false;
                            // btnMenu.Visible = false;
                            // btnSMS.Visible = false;
                        }
                        else if (userGrade == 1)
                        {
                            lblGrade.Text = "USER";
                            label2.Visible = false;
                            label4.Visible = false;
                            lblID.Visible = false;
                            lblGrade.Visible = false;
                            // btnMenu.Visible = false;
                            // btnSMS.Visible = false;
                        }
                        else if (userGrade == 2)
                        {
                            lblGrade.Text = "ADMIN";
                            label2.Visible = false;
                            label4.Visible = false;
                            lblID.Visible = false;
                            lblGrade.Visible = false;
                            // btnMenu.Visible = false;
                            // btnSMS.Visible = false;
                        }
                        else if (userGrade == 3)
                        {
                            lblGrade.Text = "SYSTEM";
                            label2.Visible = false;
                            label4.Visible = false;
                            lblID.Visible = false;
                            lblGrade.Visible = false;
                            // btnMenu.Visible = true;
                            // btnSMS.Visible = true;
                        }
                        else
                        {
                            lblGrade.Text = "GUEST";
                            label2.Visible = false;
                            label4.Visible = false;
                            lblID.Visible = false;
                            lblGrade.Visible = false;
                            // btnMenu.Visible = false;
                            // btnSMS.Visible = false;
                        }

                    }));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void OaDisconnected()
        {
            
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {     
                        lblGrade.Text = "GUEST";
                        label2.Visible = false;
                        label4.Visible = false;
                        lblID.Visible = false;
                        lblGrade.Visible = false;
                        //btnMenu.Visible = false;
                        //btnSMS.Visible = false;

                    }));
                }
                catch
                {
                }
            }
        }

        private void OaConnected()
        {
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        //lblConnect.Text = "Connected";

                    }));
                }
                catch
                {                    
                }
            }
        }


        public void sendMsgToOA(string val)
        {
            if (isOAConnected == true)
            {
                Write(val);
            }
        }      


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            isOAConnected = false;
            sendMsgToOA("shutdown");

            Thread.Sleep(100);
            //Client.Close();

            //foreach (Forms.FormMultiTrend frm in multiTrends)
            //{
            //    frm.ClearChart();
            //    Thread.Sleep(200);
            //}

            //Application.Exit();
            try
            {
               // ipcServer.Abort();
                Application.ExitThread();
                Environment.Exit(0);
            }
            catch
            { }
        }
    }
}
