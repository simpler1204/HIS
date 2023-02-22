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
            Color.Red,                       Color.FromArgb(18, 132, 245),  Color.SkyBlue,                
            Color.Yellow,                    Color.Cyan,                    Color.Magenta,                  
            Color.FromArgb(153,153,153),     Color.Green,                                    
            Color.FromArgb(255,255,255),    Color.FromArgb(255,110,0),      Color.FromArgb(179,255,211),    Color.FromArgb(255,160,174),
            Color.FromArgb(25,25,255),      Color.FromArgb(242,176,255),    Color.FromArgb(168,255,106),    Color.FromArgb(132,88,17),
            Color.FromArgb(153,0,204),      Color.FromArgb(85,160,185),     Color.FromArgb(132,0,90),       Color.FromArgb(10,100,100),
            Color.FromArgb(255,204,0),      Color.FromArgb(164,130,175),    Color.FromArgb(255,250,193),    Color.FromArgb(175,35,95),
            Color.FromArgb(200,255,200),    Color.FromArgb(200,140,30),     Color.FromArgb(150,255,220),    Color.FromArgb(200,160,194),
            Color.FromArgb(55,55,220),      Color.FromArgb(200,155,200),    Color.FromArgb(168,200,106),    Color.FromArgb(100,188,17),
            Color.FromArgb(153,50,204),     Color.FromArgb(185,160,185),    Color.FromArgb(132,60,200),     Color.FromArgb(100,100,100),
            Color.FromArgb(255,204,60),     Color.FromArgb(200,65,175),     Color.FromArgb(200,220,193),    Color.FromArgb(175,175,175),
            Color.FromArgb(255,255,255),    Color.FromArgb(255,110,50),     Color.FromArgb(150,220,211),    Color.FromArgb(174,160,174),
            Color.FromArgb(251,65,255),     Color.FromArgb(150,176,200),    Color.FromArgb(168,255,206),    Color.FromArgb(132,88,217),
            Color.FromArgb(173,30,204),     Color.FromArgb(185,160,185),    Color.FromArgb(132,100,200),    Color.FromArgb(200,160,100),
            Color.FromArgb(255,204,30),     Color.FromArgb(184,190,175),    Color.FromArgb(175,250,193),    Color.FromArgb(190,135,95),
            Color.FromArgb(55,200,200),     Color.FromArgb(200,190,80),     Color.FromArgb(200,80,211),     Color.FromArgb(200,140,154),
            Color.FromArgb(25,25,255),      Color.FromArgb(142,176,255),    Color.FromArgb(168,150,106),    Color.FromArgb(100,188,107),
            Color.FromArgb(100,100,204),    Color.FromArgb(185,168,195),    Color.FromArgb(200,0,200),      Color.FromArgb(190,50,200),
            Color.FromArgb(200,200,30),     Color.FromArgb(184,150,195),    Color.FromArgb(210,210,193),    Color.FromArgb(155,135,195),
            Color.FromArgb(210,155,35),     Color.FromArgb(255,110,255),    Color.FromArgb(130,255,225),    Color.FromArgb(255,160,255),
            Color.FromArgb(205,250,255),    Color.FromArgb(220,196,255),    Color.FromArgb(198,225,206),    Color.FromArgb(132,88,123),
            Color.FromArgb(150,60,164),     Color.FromArgb(200,160,185),    Color.FromArgb(232,100,100),    Color.FromArgb(80,100,150),
            Color.FromArgb(200,221,100),    Color.FromArgb(200,230,175),    Color.FromArgb(198,198,198),    Color.FromArgb(195,135,195),
            Color.FromArgb(190,180,130),    Color.FromArgb(215,160,115),    Color.FromArgb(155,250,163),    Color.FromArgb(150,195,195),
            Color.FromArgb(198,255,206),    Color.FromArgb(150,200,80),     Color.FromArgb(153,153,153),    Color.FromArgb(200,100,190),
            Color.Silver,                   Color.YellowGreen,              Color.FloralWhite,               Color.White
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
                    if (workingColorCount == 100)
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
