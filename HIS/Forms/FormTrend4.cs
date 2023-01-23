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
    public partial class FormTrend4 : FormTrend, IReceiveMessage
    {
        public FormTrend4(MainForm mainForm) : base(mainForm)
        {
            this.Name = "FormTrend4";
            this.Text = "MULTI TREND";           
        }

        //private void FormTrend4_FormClosing(object sender, FormClosingEventArgs e)
        //{           
        //    this.Visible = false;
        //    base.RemoveAllSeries();
        //    e.Cancel = true;
        //}

        override public void MessageFromWinccOA(string val)
        {
            string[] msg = val.Split(';');
            if (msg[0] == "trendValue")
            {
                if (c2ChartContorl.Series.Count == 0) return;
                if (msg[1] == this.Name)
                {
                    for (int i = 2; i < msg.Length; i++)
                    {
                        string[] temp1 = msg[i].Split(':');
                        string[] temp2 = temp1[1].Split(',');
                        string dpName = temp2[0];
                        string value = temp2[1];

                        DataRow dr = dtRealTime.Rows.Find(dpName);
                        dr["DATETIME"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        dr["CURR"] = float.Parse(value);
                    }
                }
            }
            if (msg[0] == "trendAdd")
            {
                if (this.Visible == true)
                {
                    trendQueue.Enqueue(msg[1]);
                }

                try
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        if (this.Visible == false)
                        {
                                // MessageBox.Show("aa");
                                this.Visible = true;

                            this.Show();
                                //this.Show();
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
