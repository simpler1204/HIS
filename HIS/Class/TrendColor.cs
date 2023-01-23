using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HIS.Class
{
    class TrendColor
    {
        private DataTable dtTrendColor = new DataTable("TrendColor");
        private Color trendColor;
        private int workingColorCount = 0;
        public bool isFull = false;

        public TrendColor()
        {
            InitDatatable.Init(dtTrendColor);
            InsertColorToDataTable();
        }

        private void InsertColorToDataTable()
        {
            List<Color> listColor = new List<Color> {
            Color.Red,                       Color.FromArgb(18, 132, 245),
            Color.Yellow,                    Color.Cyan,                     Color.Magenta,
            Color.FromArgb(255,110,0),       Color.FromArgb(179,255,211),
            Color.FromArgb(255,160,174),     Color.FromArgb(100,80,255),     Color.FromArgb(242,176,255),
            Color.FromArgb(168,255,106),     Color.FromArgb(132,88,17),      Color.FromArgb(153,0,204),
            Color.FromArgb(85,160,185),      Color.FromArgb(132,0,0),        Color.FromArgb(100,120,0),
            Color.FromArgb(255,204,0),       Color.FromArgb(164,130,175),    Color.FromArgb(255,250,193),
            Color.FromArgb(175,35,95),       Color.FromArgb(198,255,206),    Color.FromArgb(170,180,20),
            Color.FromArgb(153,153,153),     Color.Green,                    Color.White,
            };

            foreach (Color item in listColor)
            {
                DataRow row = dtTrendColor.NewRow();
                row["COLOR"] = item;
                row["ISWORK"] = false;
                dtTrendColor.Rows.Add(row);
            }           
        }

        public Color GetTrendColor()
        {
            foreach (DataRow item in dtTrendColor.Rows)
            {
                
                if(item["ISWORK"].ToString() == "False")
                {
                    trendColor = (Color)item["COLOR"];
                    item["ISWORK"] = "True";
                    workingColorCount++;
                    if (workingColorCount == 25)
                        isFull = true;
                    break;
                }
            }

            return trendColor;
        }

        public  void RemoveTrendColor(Color trendColor)
        {
            foreach (DataRow item in dtTrendColor.Rows)
            {
                if (bool.Parse(item["ISWORK"].ToString()) == true && (Color)item["COLOR"] == trendColor)
                {
                    item["ISWORK"] = false;
                    workingColorCount--;
                    isFull = false;
                    break;
                }
            }
        }

    }
}
