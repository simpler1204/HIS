using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HIS.Class;

namespace HIS.Forms
{
    public class FormTrend3 : FormTrend, IReceiveMessage
    {
        public FormTrend3(MainForm mainForm) : base(mainForm)
        {
            this.Name = "FormTrend3";
            this.Text = "MULTI TREND 3";
        }


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
            //if (msg[0] == "trendAdd")
            //{
            //    trendQueue.Enqueue(msg[1]);
            //}
        }
    }
}
