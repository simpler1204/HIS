using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using HIS.Class;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace HIS.Forms
{
    abstract public partial class FormTrend : Form
    {
        public SwiftPlotDiagram c2SwiftPlotDiagram;
        public ChartControl c2ChartContorl;
        private TrendColor trendColor = new TrendColor();

        float fMin = 0f; //차트 최소값
        float fMax = 100f; //차트 최대값


        public void InitChartControl()
        {   
            Color backColor = Color.FromArgb(255, 30, 30, 30);
            Color foreColor = Color.FromArgb(255,200, 200, 200);

           

            //swift 그래프를 사용하기 위해서 생성
            c2SwiftPlotDiagram = new SwiftPlotDiagram();
            ((ISupportInitialize)(c2SwiftPlotDiagram)).BeginInit(); //초기화가 시작 되었음을 알림

            c2SwiftPlotDiagram.DefaultPane.BackColor = backColor;
            c2SwiftPlotDiagram.AxisY.Label.TextColor = foreColor;
            c2SwiftPlotDiagram.AxisX.Label.TextColor = foreColor;
            c2SwiftPlotDiagram.AxisX.Label.TextPattern = "{A:MM-dd\nHH:mm:ss}";

            //가로줄 정의
            c2SwiftPlotDiagram.AxisY.GridLines.LineStyle.Thickness = 1;
            c2SwiftPlotDiagram.AxisY.GridLines.LineStyle.DashStyle = DashStyle.Dot;
            c2SwiftPlotDiagram.AxisY.GridLines.Color = Color.FromArgb(70, 70, 70);
            c2SwiftPlotDiagram.AxisY.GridLines.Visible = true;

            //Y축 정의         
            c2SwiftPlotDiagram.AxisY.MinorCount = 1;
            c2SwiftPlotDiagram.AxisY.VisibleInPanesSerializable = "-1";
            c2SwiftPlotDiagram.AxisY.VisualRange.Auto = false;//
            c2SwiftPlotDiagram.AxisY.VisualRange.AutoSideMargins = false;
            //c2SwiftPlotDiagram.AxisY.VisualRange.EndSideMargin = 0D;
            //c2SwiftPlotDiagram.AxisY.VisualRange.StartSideMargin = 0D;
            c2SwiftPlotDiagram.AxisY.WholeRange.Auto = false; //
            c2SwiftPlotDiagram.AxisY.WholeRange.AutoSideMargins = false; //
            c2SwiftPlotDiagram.AxisY.WholeRange.EndSideMargin = 0D;
            c2SwiftPlotDiagram.AxisY.WholeRange.StartSideMargin = 0D;
            c2SwiftPlotDiagram.AxisY.Visibility = DevExpress.Utils.DefaultBoolean.True;
            c2SwiftPlotDiagram.AxisY.GridLines.Visible = false;//가로줄 없애기

            c2SwiftPlotDiagram.EnableAxisXScrolling = true;
            c2SwiftPlotDiagram.EnableAxisXZooming = true;
            c2SwiftPlotDiagram.EnableAxisYScrolling = true;
            c2SwiftPlotDiagram.EnableAxisYZooming = true;
            c2SwiftPlotDiagram.PaneLayout.Direction = PaneLayoutDirection.Horizontal;

            c2SwiftPlotDiagram.AxisX.MinorCount = 1;
            c2SwiftPlotDiagram.AxisX.VisibleInPanesSerializable = "-1";
            c2SwiftPlotDiagram.AxisX.VisualRange.Auto = true;
            c2SwiftPlotDiagram.AxisX.VisualRange.AutoSideMargins = true;
            c2SwiftPlotDiagram.AxisX.VisualRange.StartSideMargin = 0D;
            c2SwiftPlotDiagram.AxisX.VisualRange.EndSideMargin = 0D;
            c2SwiftPlotDiagram.AxisX.WholeRange.Auto = true;
            c2SwiftPlotDiagram.AxisX.WholeRange.AutoSideMargins = true;
            c2SwiftPlotDiagram.AxisX.WholeRange.EndSideMargin = 0D;
            c2SwiftPlotDiagram.AxisX.WholeRange.StartSideMargin = 0D;
            c2SwiftPlotDiagram.AxisX.Label.TextColor = System.Drawing.Color.White;
            c2SwiftPlotDiagram.AxisX.GridLines.MinorLineStyle.DashStyle = DashStyle.Dot;
            c2SwiftPlotDiagram.AxisX.GridLines.MinorVisible = true;
            c2SwiftPlotDiagram.AxisX.GridLines.MinorColor = Color.FromArgb(70, 70, 70);
            c2SwiftPlotDiagram.AxisY.Label.TextPattern = "{V:F2}";


            c2SwiftPlotDiagram.DefaultPane.ScrollBarOptions.XAxisAnnotationOptions.ShowConstantLines = false;
            c2SwiftPlotDiagram.DefaultPane.ScrollBarOptions.YAxisAnnotationOptions.ShowConstantLines = false;
            
            
            //Control버튼 누르고 줌 또는 축소
            c2SwiftPlotDiagram.ZoomingOptions.ZoomInMouseAction.ModifierKeys = ChartModifierKeys.Control;
            c2SwiftPlotDiagram.ZoomingOptions.ZoomToRectangleMouseAction.ModifierKeys = ChartModifierKeys.Control;

            //줌 배 수(1000=> 10배)
            //c2SwiftPlotDiagram.ZoomingOptions.AxisXMaxZoomPercent = 1000D;
            //c2SwiftPlotDiagram.ZoomingOptions.AxisYMaxZoomPercent = 1000D;

            //X축 기간 및 간격 설정
            //c2SwiftPlotDiagram.AxisX.DateTimeScaleOptions.AutoGrid = true;
            //c2SwiftPlotDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute;
            //c2SwiftPlotDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 2D;


            c2SwiftPlotDiagram.DefaultPane.BorderVisible = false;

            //ChartControl 생성
            c2ChartContorl = new ChartControl();
            ((ISupportInitialize)(c2ChartContorl)).BeginInit();
            c2ChartContorl.Location = new System.Drawing.Point(3, 3);
            c2ChartContorl.Dock = DockStyle.Fill; //화면 꽉 차게
            c2ChartContorl.BackColor = backColor;
            c2ChartContorl.Legend.Visibility = DevExpress.Utils.DefaultBoolean.False; //Legend 안보이게
            c2ChartContorl.Diagram = c2SwiftPlotDiagram;  //차트의 다이어 그램은 swiftPlotDiagram1


            //CrossHair
            c2ChartContorl.CrosshairOptions.ShowArgumentLine = true;
            c2ChartContorl.CrosshairOptions.ShowArgumentLabels = true;
            c2ChartContorl.CrosshairOptions.ShowValueLine = false;
            c2ChartContorl.CrosshairOptions.ShowValueLabels = false;
            c2ChartContorl.CrosshairOptions.ShowCrosshairLabels = true;
            c2ChartContorl.CrosshairOptions.CrosshairLabelBackColor = Color.Black;
            c2ChartContorl.CrosshairOptions.CrosshairLabelTextOptions.TextColor = Colors.buttonForeColor;          
            //c2ChartContorl.CrosshairOptions.GroupHeaderTextOptions.TextColor = Color.White;
            //c2ChartContorl.CrosshairOptions.GroupHeaderPattern = "{A:yyyy-MM-dd HH:mm:ss}";
            c2ChartContorl.CrosshairOptions.ShowGroupHeaders = false;


            ((ISupportInitialize)(c2SwiftPlotDiagram)).EndInit();
            ((ISupportInitialize)(c2ChartContorl)).EndInit();

            c2ChartContorl.Zoom += C2ChartContorl_Zoom;
            c2ChartContorl.MouseWheel += C2ChartContorl_MouseWheel;

            tableLayoutPanel4.Controls.Add(c2ChartContorl, 0, 1);
        }

        

        private void C2ChartContorl_MouseWheel(object sender, MouseEventArgs e)
        {
            VisualRange range = c2SwiftPlotDiagram.AxisY.VisualRange;

            float fTempMin;
            bool rtn1 = float.TryParse(range.MinValue.ToString(), out fTempMin);

            if ((fMax - fMin) < 60) return;


            if (e.X > 0 && e.X < 50)
            {
                if (rtn1)
                {
                    if (e.Delta < 0)
                    {
                        fMin -= 2;
                        fMax += 2;
                        c2SwiftPlotDiagram.AxisY.VisualRange.SetMinMaxValues(fMin, fMax);
                        c2SwiftPlotDiagram.AxisY.WholeRange.SetMinMaxValues(fMin, fMax);
                    }
                    else if (e.Delta > 0)
                    {
                        fMin += 2;
                        fMax -= 2;
                        c2SwiftPlotDiagram.AxisY.VisualRange.SetMinMaxValues(fMin, fMax);
                        //c2SwiftPlotDiagram.AxisY.WholeRange.SetMinMaxValues(fMin, fMax);
                    }
                }
            }
           
        }

        private void C2ChartContorl_Zoom(object sender, ChartZoomEventArgs e)
        {
            chkReal.Checked = false;
            isRealCheked = false;

            
        }

        //private void AddSeries(string dpName, string startDt, string endDt)
        //{
        //    if (TrendDatabase.OracleConn.State == System.Data.ConnectionState.Closed) return;


        //    Color color;
        //    if (trendColor.isFull == false)
        //        color = trendColor.GetTrendColor();
        //    else
        //    {
        //        MessageBox.Show("You can no longer add trend. Up to 25.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        //        return;
        //    }

        //    Series series = new Series(dpName, ViewType.SwiftPlot);
        //    series.ArgumentScaleType = ScaleType.DateTime;
        //    series.ArgumentDataMember = "insert_date";
        //    series.ValueScaleType = ScaleType.Numerical;
        //    series.ValueDataMembers.AddRange(new string[] { "Value" });
        //    series.CrosshairLabelPattern = "{S} : {V:F3}";
        //    series.CrosshairTextOptions.TextColor = color;

        //    string query = string.Empty;
        //    query += "SELECT B.TB_NAME, B.COL_NAME, A.DP_DESC, A.Y_MIN, A.Y_MAX, SYSTEM, B.START_AT, NVL(B.END_AT,SYSDATE) ";
        //    query += "FROM C2_TREND_INFO A JOIN  C2_DP_TREND_MOVE_HISTORY B  ON A.DP_NAME = B.DP_NAME ";
        //    query += "WHERE A.DP_NAME = :1 ";
        //    query += "AND NVL(B.END_AT, SYSDATE) > TO_DATE(:2, 'YYYY-MM-DD HH24:mi:ss') ";
        //    query += "AND B.START_AT < TO_DATE(:3, 'YYYY-MM-DD HH24:mi:ss') ";
        //    query += "AND B.TB_NAME IS NOT NULL ";
        //    query += "ORDER BY  B.START_AT DESC ";

        //    OracleCommand cmd = new OracleCommand(query, TrendDatabase.OracleConn);
        //    cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dpName;
        //    cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = startDt;
        //    cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = endDt;

        //    try
        //    {
        //        OracleDataReader reader = cmd.ExecuteReader();
        //        if (reader.HasRows)
        //        {
        //            string tableName = string.Empty;
        //            string colName = string.Empty;
        //            string dpDesc = string.Empty;
        //            string yMin = string.Empty;
        //            string yMax = string.Empty;
        //            string system = string.Empty;
        //            DateTime start;
        //            DateTime end;
        //            DateTime finalStart, finalEnd;

        //            while (reader.Read())
        //            {
        //                tableName = reader[0].ToString();
        //                colName = reader[1].ToString().Replace("COL_", "VAL_");
        //                dpDesc = reader[2].ToString();
        //                yMin = reader[3].ToString();
        //                yMax = reader[4].ToString();
        //                system = reader[5].ToString();
        //                start = DateTime.Parse(reader[6].ToString());
        //                end = DateTime.Parse(reader[7].ToString());

        //                if (start >= DateTime.Parse(startDt))
        //                {
        //                    finalStart = start;
        //                }
        //                else
        //                {
        //                    finalStart = DateTime.Parse(startDt);
        //                }

        //                if (end >= DateTime.Parse(endDt))
        //                {
        //                    finalEnd = DateTime.Parse(endDt);
        //                }
        //                else
        //                {
        //                    finalEnd = end;
        //                }


        //                GetSeriesValues(ref series, tableName, colName, finalStart.ToString("yyyy-MM-dd HH:mm:ss"), finalEnd.ToString("yyyy-MM-dd HH:mm:ss"));


        //            }

        //            DataRow dr = dtTrendData.NewRow();
        //            dr["SEL"] = false;
        //            dr["VISIBLE"] = false;
        //            dr["DP_NAME"] = dpName;
        //            dr["DP_DESC"] = dpDesc;
        //            dr["MIN"] = yMin;
        //            dr["MAX"] = yMax;
        //            dr["SYSTEM"] = system;
        //            dtTrendData.Rows.Add(dr);
        //            dgvTrendData.Rows[dgvTrendData.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;

        //            dgvTrendData[0, dgvTrendData.Rows.Count - 1].Style.BackColor = color;
        //            dgvTrendData[1, dgvTrendData.Rows.Count - 1].Style.BackColor = color;

        //            SetMinMax(float.Parse(yMin), float.Parse(yMax));


        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //    }
        //    finally
        //    {
        //        cmd.Dispose();
        //    }

        //    SwiftPlotSeriesView view = new SwiftPlotSeriesView();
        //    series.View = view;
        //    series.View.Color = color;

        //    c2ChartContorl.Series.Add(series);
        //}

        //private void GetSeriesValues(ref Series series, string tableName, string columnName, string startDt, string endDt)
        //{
        //    OracleConnection conn = new OracleConnection(Database.oradb);
        //    conn.Open();
        //    string query = $" SELECT INSERT_TIME, {columnName} FROM {tableName} " +
        //        $"  WHERE INSERT_TIME BETWEEN TO_DATE(:1, 'YYYY-MM-DD HH24:mi:ss') AND TO_DATE(:2, 'YYYY-MM-DD HH24:mi:ss') " +
        //        $"  AND {columnName} IS NOT NULL ";

        //    OracleCommand cmd = new OracleCommand(query, conn);
        //    cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = startDt;
        //    cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = endDt;

        //    //MessageBox.Show($"{columnName}, {tableName}, {startDt}, {endDt}");


        //    OracleDataReader reader = cmd.ExecuteReader();

        //    if (reader.HasRows)
        //    {
        //        while (reader.Read())
        //        {
        //            float fVal;
        //            DateTime insert_time;
        //            string dpName = series.Name;
        //            if (float.TryParse(reader[1].ToString(), out fVal))
        //            {
        //                insert_time = DateTime.Parse(reader[0].ToString());
        //                series.Points.Add(new SeriesPoint(insert_time, fVal));
        //                SeriesPoint point = new SeriesPoint(insert_time, fVal);


        //            }
        //        }
        //    }

        //    cmd.Dispose();
        //    conn.Close();

        //}

        private void SetMinMax(float min, float max)
        {
            float tempMin = 0f, tempMax = 0f;
            bool rtn = false;

            foreach (DataRow item in dtTrendData.Rows)
            {
                rtn = float.TryParse(item["MIN"].ToString(), out tempMin);
                if(rtn == true)
                {
                    if (tempMin < fMin) fMin = tempMin;
                }

                rtn = float.TryParse(item["MAX"].ToString(), out tempMax);
                if (rtn == true)
                {
                    if (tempMax > fMax) fMax = tempMax;
                }

                c2SwiftPlotDiagram.AxisY.VisualRange.SetMinMaxValues(fMin, fMax + 50);
                c2SwiftPlotDiagram.AxisY.WholeRange.SetMinMaxValues(fMin, fMax + 50);
                
                //c2SwiftPlotDiagram.EnableAxisXScrolling = false;
                //c2SwiftPlotDiagram.EnableAxisYScrolling = false;


            }
        }

        private void InitControls()
        {
            //Time Picker 설정
           // startDate.Properties.MaskSettings.MaskExpression = "yyyy-MM-dd HH:mm:ss";
            startDate.Text = DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss");
           
           // endDate.Properties.MaskSettings.MaskExpression = "yyyy-MM-dd HH:mm:ss";
            endDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            DateTime start = DateTime.Now.AddMinutes(-10);
            DateTime end = DateTime.Now;
            c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
            c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);

        }
        
        private void SetControls(string period)
        {
            // startDate.Properties.MaskSettings.MaskExpression = "yyyy-MM-dd HH:mm:ss";
            // endDate.Properties.MaskSettings.MaskExpression = "yyyy-MM-dd HH:mm:ss";
            if (period == "7Day")
            {
                //Time Picker 설정               
                startDate.Text = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd HH:mm:ss");
                endDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DateTime start = DateTime.Now.AddDays(-7);
                DateTime end = DateTime.Now;
                c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
                c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);
            }
            if (period == "3Day")
            {
                //Time Picker 설정               
                startDate.Text = DateTime.Now.AddDays(-3).ToString("yyyy-MM-dd HH:mm:ss");
                endDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DateTime start = DateTime.Now.AddDays(-3);
                DateTime end = DateTime.Now;
                c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
                c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);
            }            
            if (period == "1Day")
            {
                //Time Picker 설정               
                startDate.Text = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd HH:mm:ss");               
                endDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DateTime start = DateTime.Now.AddDays(-1);
                DateTime end = DateTime.Now;
                c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
                c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);
            }
            if (period == "12Hour")
            {
                //Time Picker 설정               
                startDate.Text = DateTime.Now.AddHours(-12).ToString("yyyy-MM-dd HH:mm:ss");
                endDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DateTime start = DateTime.Now.AddHours(-12);
                DateTime end = DateTime.Now;
                c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
                c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);
            }
            if (period == "4Hour")
            {
                //Time Picker 설정               
                startDate.Text = DateTime.Now.AddHours(-4).ToString("yyyy-MM-dd HH:mm:ss");
                endDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DateTime start = DateTime.Now.AddHours(-4);
                DateTime end = DateTime.Now;
                c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
                c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);
            }
            if (period == "1Hour")
            {
                //Time Picker 설정               
                startDate.Text = DateTime.Now.AddHours(-1).ToString("yyyy-MM-dd HH:mm:ss");
                endDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DateTime start = DateTime.Now.AddHours(-1);
                DateTime end = DateTime.Now;
                c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
                c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);
            }
            if (period == "30Min")
            {
                //Time Picker 설정               
                startDate.Text = DateTime.Now.AddMinutes(-30).ToString("yyyy-MM-dd HH:mm:ss");
                endDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DateTime start = DateTime.Now.AddMinutes(-30);
                DateTime end = DateTime.Now;
                c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
                c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);
            }
            if (period == "10Min")
            {
                //Time Picker 설정               
                startDate.Text = DateTime.Now.AddMinutes(-10).ToString("yyyy-MM-dd HH:mm:ss");
                endDate.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                DateTime start = DateTime.Now.AddMinutes(-10);
                DateTime end = DateTime.Now;
                c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
                c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);
            }
        }

        public void RemoveAllSeries()
        {
            foreach (Series item in c2ChartContorl.Series)
            {
                trendColor.RemoveTrendColor(item.View.Color);
            }

            chartCount = 0;
            dtTrendData.Rows.Clear();
            dtRealTime.Rows.Clear();
            c2ChartContorl.Series.Clear();

        }

        private void ChartTimeSpan(DateTime start, DateTime end)
        {
            TimeSpan timeDiff = end - start;

            double diffDays = timeDiff.TotalDays;
            double diffHour = timeDiff.TotalHours;
            double diffMiniute = timeDiff.TotalMinutes;
            double diffSecond = timeDiff.TotalSeconds;

            //DateTime s = DateTime.Parse(startDate.Text);
            //DateTime e = DateTime.Parse(endDate.Text);
            c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
            c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);

            try
            {
                if (diffMiniute > 0 && diffMiniute < 11)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 1;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute;
                }
                else if (diffMiniute >= 11 && diffMiniute < 21)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 2;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute;
                }
                else if (diffMiniute >= 21 && diffMiniute < 31)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 3;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute;
                }
                else if (diffMiniute >= 31 && diffMiniute < 61)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 5;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute;
                }
                else if (diffHour >= 1 && diffHour < 2)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 10;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute;
                }
                else if (diffHour >= 2 && diffHour < 5)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 30;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Minute;
                }
                else if (diffHour >= 5 && diffHour < 10)
                {
                    

                    c2SwiftPlotDiagram.AxisX.DateTimeScaleOptions.AutoGrid = true;
                    c2SwiftPlotDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour;
                    // SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    c2SwiftPlotDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 2D;
                    //c2SwiftPlotDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    
                }
                else if (diffHour >= 10 && diffHour < 21)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 2;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour;
                }
                else if (diffHour >= 21 && diffHour < 31)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 3;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour;
                }
                else if (diffHour >= 31 && diffHour < 51)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 5;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour;
                }
                else if (diffHour >= 51 && diffHour < 101)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 10;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour;
                }
                else if (diffHour >= 101 && diffHour < 121)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 12;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour;
                }
                else if (diffHour >= 121 && diffHour < 241)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 24;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Hour;
                }
                else if (diffDays >= 10 && diffDays < 21)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 2;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
                }
                else if (diffDays >= 21 && diffDays < 31)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 3;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
                }
                else if (diffDays >= 31 && diffDays < 51)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 5;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
                }
                else if (diffDays >= 51 && diffDays < 101)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 10;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
                }
                else if (diffDays >= 101 && diffDays < 201)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 20;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Day;
                }
                else if (diffDays >= 201 && diffDays < 301)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 1;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month;
                }
                else if (diffDays >= 301 && diffDays < 501)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 2;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month;
                }
                else if (diffDays >= 501 && diffDays < 1001)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 3;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month;
                }
                else if (diffDays >= 1001 && diffDays < 2001)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 7;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month;
                }
                else if (diffDays >= 2001 && diffDays < 3001)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 12;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Month;
                }
                else if (diffDays >= 3001 && diffDays < 5001)
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridSpacing = 2;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridOffset = 0;
                    eventDiagram.AxisX.DateTimeScaleOptions.GridAlignment = DateTimeGridAlignment.Year;
                }
                else
                {
                    SwiftPlotDiagram eventDiagram = (SwiftPlotDiagram)c2ChartContorl.Diagram;
                    eventDiagram.AxisX.WholeRange.Auto = true;
                    eventDiagram.AxisX.VisualRange.Auto = true;
                }


            }
            catch
            {
                ///MessageBox.Show(ex.ToString(), "TimeSpan function");
            }


        }
    }

    class MakeSeries
    {
        public Series series;
        private string dpName, startDt, endDt;
       

        public MakeSeries(string dpName, string startDt, string endDt, TrendColor trendColor, Color color)
        {
            series = new Series(dpName, ViewType.SwiftPlot);
            series.ArgumentScaleType = ScaleType.DateTime;
            series.ArgumentDataMember = "insert_date";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });
            series.CrosshairLabelPattern = "{S} : {V:F3}";
            series.View.Color = color;            
            
            this.dpName = dpName;
            this.startDt = startDt;
            this.endDt = endDt;           
        }

        public async Task<Series> MakeAsync()
        {
            
            series.ArgumentScaleType = ScaleType.DateTime;
            series.ArgumentDataMember = "insert_date";
            series.ValueScaleType = ScaleType.Numerical;
            series.ValueDataMembers.AddRange(new string[] { "Value" });
            series.CrosshairLabelPattern = "{S} : {V:F3}";
           


            string query = string.Empty;
            query += "SELECT B.TB_NAME, B.COL_NAME, A.DP_DESC, A.Y_MIN, A.Y_MAX, SYSTEM, B.START_AT, NVL(B.END_AT,SYSDATE) ";
            query += "FROM C2_TREND_INFO A JOIN  C2_DP_TREND_MOVE_HISTORY B  ON A.DP_NAME = B.DP_NAME ";
            query += "WHERE A.DP_NAME = :1 ";
            query += "AND NVL(B.END_AT, SYSDATE) > TO_DATE(:2, 'YYYY-MM-DD HH24:mi:ss') ";
            query += "AND B.START_AT < TO_DATE(:3, 'YYYY-MM-DD HH24:mi:ss') ";
            query += "AND B.TB_NAME IS NOT NULL ";
            query += "ORDER BY  B.START_AT DESC ";

            OracleCommand cmd = new OracleCommand(query, TrendDatabase.OracleConn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dpName;
            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = startDt;
            cmd.Parameters.Add(":3", OracleDbType.Varchar2).Value = endDt;
            
            OracleDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                string tableName = string.Empty;
                string colName = string.Empty;
                string dpDesc = string.Empty;
                string yMin = string.Empty;
                string yMax = string.Empty;
                string system = string.Empty;
                DateTime start;
                DateTime end;
                DateTime finalStart, finalEnd;
                
              
                await Task.Run(() =>
                {
                    while (reader.Read())
                    {
                        tableName = reader[0].ToString();
                        colName = reader[1].ToString().Replace("COL_", "VAL_");
                        dpDesc = reader[2].ToString();
                        yMin = reader[3].ToString();
                        yMax = reader[4].ToString();
                        system = reader[5].ToString();
                        start = DateTime.Parse(reader[6].ToString());
                        end = DateTime.Parse(reader[7].ToString());
                                               
                        if (start >= DateTime.Parse(startDt))
                        {
                            finalStart = start;
                        }
                        else
                        {
                            finalStart = DateTime.Parse(startDt);
                        }

                        if (end >= DateTime.Parse(endDt))
                        {
                            finalEnd = DateTime.Parse(endDt);
                        }
                        else
                        {
                            finalEnd = end;
                        }

                        GetSeriesValues(tableName, colName, finalStart.ToString("yyyy-MM-dd HH:mm:ss"), finalEnd.ToString("yyyy-MM-dd HH:mm:ss"));

                    }
                });               
            }

            return series;
            
        }

        private void GetSeriesValues(string tableName, string columnName, string startDt, string endDt)
        {
            OracleConnection conn = new OracleConnection(Database.oradb);
            conn.Open();
            string query = $" SELECT INSERT_TIME, {columnName} FROM {tableName} " +
                $"  WHERE INSERT_TIME BETWEEN TO_DATE(:1, 'YYYY-MM-DD HH24:mi:ss') AND TO_DATE(:2, 'YYYY-MM-DD HH24:mi:ss') " +
                $"  AND {columnName} IS NOT NULL ";

            OracleCommand cmd = new OracleCommand(query, conn);
            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = startDt;
            cmd.Parameters.Add(":2", OracleDbType.Varchar2).Value = endDt;

            OracleDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    float fVal;
                    DateTime insert_time;
                    string dpName = series.Name;
                    if (float.TryParse(reader[1].ToString(), out fVal))
                    {
                        insert_time = DateTime.Parse(reader[0].ToString());
                        series.Points.Add(new SeriesPoint(insert_time, fVal));
                       // SeriesPoint point = new SeriesPoint(insert_time, fVal);                       
                    }
                }
            }

            cmd.Dispose();
            conn.Close();
        }

             
    }
}
