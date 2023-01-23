using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace HIS.Class
{
    class SockClient
    {

        //Util util = new Util(@"C:\system_db\config\info.ini");
        //string sHost = util.GetIni("ORACLE", "host");
        //string sUser = util.GetIni("ORACLE", "user");
        //string sPwd = util.GetIni("ORACLE", "pwd");
        //string sService = util.GetIni("ORACLE", "service");
        //oradb = $"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={sHost})(PORT=1521)))" +
        //        $"(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={sService})));User ID={sUser};Password={sPwd};Connection Timeout=30;";


        public static bool sendMsg(string system, string msg)
        {
            try
            {
                Util util = new Util(@"C:\system_db\config\info.ini");
                string[] ServerIP = new string[2];
                if(system == "C2_HVAC_S1")
                {
                    ServerIP[0] = util.GetIni("SOCKET", "SVR-C2-HVAC-P1");
                    ServerIP[1] = util.GetIni("SOCKET", "SVR-C2-HVAC-S1");
                }
                else if (system == "C2_HVAC_S2")
                {
                    ServerIP[0] = util.GetIni("SOCKET", "SVR-C2-HVAC-P2");
                    ServerIP[1] = util.GetIni("SOCKET", "SVR-C2-HVAC-S2");
                }
                else if (system == "C2_HVAC_S3")
                {
                    ServerIP[0] = util.GetIni("SOCKET", "SVR-C2-HVAC-P3");
                    ServerIP[1] = util.GetIni("SOCKET", "SVR-C2-HVAC-S3");
                }
                else if (system == "C2_HVAC_S4")
                {
                    ServerIP[0] = util.GetIni("SOCKET", "SVR-C2-HVAC-P4");
                    ServerIP[1] = util.GetIni("SOCKET", "SVR-C2-HVAC-S4");
                }

                for(int i=0;i<ServerIP.Length;i++)
                {
                    Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    var ep = new IPEndPoint(IPAddress.Parse(ServerIP[i]), 31313);

                    try
                    {
                        // (2) 서버에 연결
                       
                        sock.Connect(ep);
                        byte[] receiverBuff = new byte[8192];
                        byte[] buff = Encoding.UTF8.GetBytes(msg);


                        // (3) 서버에 데이타 전송
                        sock.Send(buff, SocketFlags.None);

                        // (4) 서버에서 데이타 수신
                        int n = sock.Receive(receiverBuff);
                        string data = Encoding.UTF8.GetString(receiverBuff, 0, n);
                      
                    }
                    catch
                    {                       
                        return false;
                    }
                    finally
                    {
                        sock.Close();
                        System.Threading.Thread.Sleep(100);
                    }                    
                   
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return true;
        }
    }
}
