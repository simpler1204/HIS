using DevExpress.XtraCharts;
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
using HIS.PopUp;
using Oracle.ManagedDataAccess.Client;
using System.Diagnostics;
using DevExpress.XtraSplashScreen;

namespace HIS.Forms
{
    abstract public partial class FormTrend : Form
    {
        public DataTable dtTrendData = new DataTable("TrendData");
        public DataTable dtRealTime = new DataTable("RealTimeValue");
        Timer realTimer = new Timer();
        MainForm mainForm;
        private int chartCount = 0;
        Timer trendAddTimer = new Timer();
        public Queue<string> trendQueue = new Queue<string>();
        SplashScreenManager splashScreenManager1;

        bool isAddChecked = false; //Trend를 추가하기위해 체크 되어 있는지
        bool isRealCheked = false; //Real time 
        bool isCrossChecked = false; // Cross Hair 사용하기 위해    

        bool isResetting = false;


        public FormTrend(MainForm mainForm)
        {
            InitializeComponent();
            Load += FormTrend_Load;
            realTimer.Interval = 1000;
            realTimer.Tick += RealTimer_Tick;
            this.mainForm = mainForm;
            mainForm.MsgFromOa += MessageFromWinccOA;

            menu1.ButtonClick += Menu1_ButtonClick;
            menu2.ButtonClick += Menu2_ButtonClick;
            menu_search.ButtonClick += Menu_search_ButtonClick;
            menu_group.ButtonClick += Menu_group_ButtonClick;

            this.splashScreenManager1 = new SplashScreenManager(this, typeof(global::HIS.Forms.WaitForm1), true, true);
            this.splashScreenManager1.ClosingDelay = 500;

            trendAddTimer.Interval = 1000;
            trendAddTimer.Tick += TrendAddTimer_Tick;
            trendAddTimer.Start();

            dgvTrendData.CellContentClick += DgvTrendData_CellContentClick;

            this.FormClosing += (sender, e) =>
            {
                realTimer.Stop();
                realTimer.Dispose();
                mainForm.MsgFromOa -= MessageFromWinccOA;
                dgvTrendData.CellContentClick -= DgvTrendData_CellContentClick;
                trendAddTimer.Stop();
                trendAddTimer.Dispose();
            };


            //Add CheckBox 이벤트
            chkAdd.CheckedChanged += (sender, e) =>
            {
                CheckBox chk = sender as CheckBox;
                if (chk.CheckState == CheckState.Checked)
                    isAddChecked = true;
                else
                    isAddChecked = false; ;
            };

            //Real CheckBox 이벤트
            chkReal.CheckedChanged += (sender, e) =>
            {
                CheckBox chk = sender as CheckBox;
                if (chk.CheckState == CheckState.Checked)
                {
                    isRealCheked = true;
                    ControlsLock(true);
                    InitControls();
                    //CreateSeriesBySearchButton();
                    realTimer.Start();
                }
                else
                {
                    isRealCheked = false;
                    ControlsLock(false);
                    realTimer.Stop();
                }
            };

            //Crosshair checkBox 이벤트
            chkCross.CheckedChanged += (sender, e) =>
            {
                CheckBox chk = sender as CheckBox;
                if (chk.CheckState == CheckState.Checked)
                {
                    isCrossChecked = true;
                    c2ChartContorl.CrosshairOptions.ShowValueLine = true;
                    c2ChartContorl.CrosshairOptions.ShowValueLabels = true;
                }
                else
                {
                    isCrossChecked = false; ;
                    c2ChartContorl.CrosshairOptions.ShowValueLine = false;
                    c2ChartContorl.CrosshairOptions.ShowValueLabels = false;
                }
            };

        }

        abstract public void MessageFromWinccOA(string val);

        private void C2ChartContorl_CustomDrawCrosshair(object sender, CustomDrawCrosshairEventArgs e)
        {
            foreach (CrosshairElementGroup g in e.CrosshairElementGroups)
            {
                int count = g.CrosshairElements.Count;
                for (int i = 0; i < count; i++)
                {
                    SeriesPoint sp = g.CrosshairElements[i].SeriesPoint;

                    if (dtTrendData.Rows.Count > 0)
                    {
                        DataRow dr = dtTrendData.Rows.Find(g.CrosshairElements[i].Series.Name);
                        dr["DATETIME"] = sp.DateTimeArgument.ToString("yyyy-MM-dd HH:mm:ss");
                        dr["CUV"] = sp.Values[0].ToString("#,##0.###");
                    }
                }
            }
        }

        private void TrendAddTimer_Tick(object sender, EventArgs e)
        {
            trendAddTimer.Stop();

            foreach (var item in trendQueue)
            {
                TrendAdd(item);
                //trendQueue.Dequeue();
            }

            trendQueue.Clear();

            trendAddTimer.Start();
        }



        private void ControlsLock(bool bVal)
        {
            menu1.Enabled = !bVal;
            menu_search.Enabled = !bVal;
            menu2.Enabled = !bVal;
            menu_group.Enabled = !bVal;
            startDate.Enabled = !bVal;
            endDate.Enabled = !bVal;
            chkAdd.Enabled = !bVal;
        }

        private void Menu_group_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            CreateSeriesByGroupButton();
        }


        public async void CreateSeriesByTrendGroup(string val)
        {

            fMin = 0f; //차트 최소값 초기화
            fMax = 100f; //차트 최대값 초기화


            splashScreenManager1.ShowWaitForm();
            TrendDatabase.Open();

            ChartTimeSpan(DateTime.Parse(startDate.Text), DateTime.Parse(endDate.Text));

            //c2SwiftPlotDiagram.AxisY.VisualRange.SetMinMaxValues(0, 100);
            //c2SwiftPlotDiagram.AxisY.WholeRange.SetMinMaxValues(0, 100);

            if (val.Length > 0)
            {
                RemoveAllSeries();

                if (!TrendDatabase.Open()) return;
                string query = "SELECT B.SYSTEM, A.GROUP_NAME, A.DP_NAME, A.DP_DESC, A.MIN, A.MAX " +
                                "FROM HMI_TREND_GROUP_DETAIL A JOIN C2_TREND_INFO B ON A.DP_NAME = B.DP_NAME  " +
                                "WHERE A.GROUP_NAME = :1";

                try
                {
                    using (OracleCommand cmd = new OracleCommand(query, TrendDatabase.OracleConn))
                    {
                        cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = val;

                        OracleDataReader reader = cmd.ExecuteReader();

                        List<Series> listSeries = new List<Series>();
                        List<Task<Series>> listMakeSeries = new List<Task<Series>>();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                c2ChartContorl.Enabled = false;
                                dgvTrendData.Enabled = false;

                                if (chartCount < 25)
                                {
                                    //차트에 이미 등록된 DP인지 확인
                                    DataRow dataRow = null;
                                    dataRow = dtTrendData.Rows.Find(reader["DP_NAME"]);

                                    if (dataRow == null) // 등록되지 않았으면..
                                    {
                                        chartCount++;

                                        DataRow dr = dtTrendData.NewRow();
                                        // dr["SEL"] = false;
                                        dr["VISIBLE"] = true;
                                        dr["DP_NAME"] = reader["DP_NAME"];
                                        dr["DP_DESC"] = reader["DP_DESC"];
                                        dr["MIN"] = reader["MIN"];
                                        dr["MAX"] = reader["MAX"];
                                        dr["SYSTEM"] = reader["SYSTEM"];
                                        dtTrendData.Rows.Add(dr);

                                        DataRow dr2 = dtRealTime.NewRow();
                                        dr2["DP_NAME"] = reader["DP_NAME"];
                                        dtRealTime.Rows.Add(dr2);

                                        SetMinMax(float.Parse(reader["MIN"].ToString()), float.Parse(reader["MAX"].ToString()));

                                        dgvTrendData.Rows[dgvTrendData.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                                        Color color = trendColor.GetTrendColor();
                                        dgvTrendData[0, dgvTrendData.Rows.Count - 1].Style.BackColor = color;
                                        dgvTrendData[1, dgvTrendData.Rows.Count - 1].Style.BackColor = color;

                                        MakeSeries makeSeries = new MakeSeries(reader["DP_NAME"].ToString(), startDate.Text, endDate.Text, trendColor, color);
                                        listMakeSeries.Add(makeSeries.MakeAsync());
                                    }
                                }
                            }

                            if (c2ChartContorl.Series.Count < 26)
                            {
                                foreach (Task<Series> item in listMakeSeries)
                                {
                                    Series series = await item;
                                    c2ChartContorl.Series.Add(series);

                                    if (series.Points.Count > 0)
                                    {
                                        double[] fVal = series.Points[series.Points.Count - 1].Values;
                                        DataRow dr = dtTrendData.Rows.Find(series.Name);
                                        dr["CURR"] = fVal[0].ToString("#,##0.###");
                                    }
                                }
                            }



                            c2ChartContorl.Enabled = true;
                            dgvTrendData.Enabled = true;
                            dgvTrendData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgvTrendData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgvTrendData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dgvTrendData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                        }
                    }
                    splashScreenManager1.CloseWaitForm();
                    lblGroupName.Text = val;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    TrendDatabase.Close();
                }

            }
        }

        public async void CreateSeriesByTrendGroup(string part, string group, string distinct)
        {

            fMin = 0f; //차트 최소값 초기화
            fMax = 100f; //차트 최대값 초기화

            splashScreenManager1.ShowWaitForm();
            chkReal.Checked = false;
            //isRealCheked = false;
            realTimer.Stop();
            InitControls();           

            TrendDatabase.Open();

            ChartTimeSpan(DateTime.Parse(startDate.Text), DateTime.Parse(endDate.Text));

            //c2SwiftPlotDiagram.AxisY.VisualRange.SetMinMaxValues(0, 100);
            //c2SwiftPlotDiagram.AxisY.WholeRange.SetMinMaxValues(0, 100);


            RemoveAllSeries();

            if (!TrendDatabase.Open()) return;

            //string query = "SELECT B.SYSTEM, A.GROUP_NAME, A.DP_NAME, A.DP_DESC, A.MIN, A.MAX " +
            //                "FROM HMI_TREND_GROUP_DETAIL A JOIN C2_TREND_INFO B ON A.DP_NAME = B.DP_NAME  " +
            //                "WHERE A.GROUP_NAME = :1";
            string query = string.Empty;
            if (distinct == "left")
            {
                query = @"SELECT B.SYSTEM, A.GROUP_NAME, A.DP_NAME1 AS DP_NAME, B.DP_DESC, B.Y_MIN AS MIN, B.Y_MAX AS MAX
                                FROM C2_TREND_GROUP A JOIN C2_TREND_INFO B ON A.DP_NAME1 = B.DP_NAME
                                WHERE A.PART_NAME = :1 AND A.GROUP_NAME = :2 AND A.DP_NAME1 IS NOT NULL
                                ORDER BY A.DP_NAME1 ASC";
            }
            else
            {
                query = @"SELECT B.SYSTEM, A.GROUP_NAME, A.DP_NAME2 AS DP_NAME, B.DP_DESC, B.Y_MIN AS MIN, B.Y_MAX AS MAX
                                FROM C2_TREND_GROUP A JOIN C2_TREND_INFO B ON A.DP_NAME1 = B.DP_NAME
                                WHERE A.PART_NAME = :1 AND A.GROUP_NAME = :2 AND A.DP_NAME2 IS NOT NULL
                                ORDER BY A.DP_NAME2 ASC";
            }

            try
            {
                using (OracleCommand cmd = new OracleCommand(query, TrendDatabase.OracleConn))
                {
                    cmd.Parameters.Add(":1", OracleDbType.NVarchar2).Value = part;
                    cmd.Parameters.Add(":2", OracleDbType.NVarchar2).Value = group;

                    OracleDataReader reader = cmd.ExecuteReader();

                    List<Series> listSeries = new List<Series>();
                    List<Task<Series>> listMakeSeries = new List<Task<Series>>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            c2ChartContorl.Enabled = false;
                            dgvTrendData.Enabled = false;
                           
                            //차트에 이미 등록된 DP인지 확인
                            DataRow dataRow = null;
                            dataRow = dtTrendData.Rows.Find(reader["DP_NAME"]);

                            if (dataRow == null) // 등록되지 않았으면..
                            {                                 

                                DataRow dr = dtTrendData.NewRow();
                                // dr["SEL"] = false;
                                dr["VISIBLE"] = true;
                                dr["DP_NAME"] = reader["DP_NAME"];
                                dr["DP_DESC"] = reader["DP_DESC"];
                                dr["MIN"] = reader["MIN"];
                                dr["MAX"] = reader["MAX"];
                                dr["SYSTEM"] = reader["SYSTEM"];
                                dtTrendData.Rows.Add(dr);

                                DataRow dr2 = dtRealTime.NewRow();
                                dr2["DP_NAME"] = reader["DP_NAME"];
                                dtRealTime.Rows.Add(dr2);

                                SetMinMax(float.Parse(reader["MIN"].ToString()), float.Parse(reader["MAX"].ToString()));

                                dgvTrendData.Rows[dgvTrendData.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                                Color color = trendColor.GetTrendColor();
                                dgvTrendData[0, dgvTrendData.Rows.Count - 1].Style.BackColor = color;
                                dgvTrendData[1, dgvTrendData.Rows.Count - 1].Style.BackColor = color;

                                MakeSeries makeSeries = new MakeSeries(reader["DP_NAME"].ToString(), startDate.Text, endDate.Text, trendColor, color);
                                listMakeSeries.Add(makeSeries.MakeAsync());
                            }
                            
                        }

                        if (c2ChartContorl.Series.Count < 26)
                        {
                            foreach (Task<Series> item in listMakeSeries)
                            {
                                Series series = await item;
                                c2ChartContorl.Series.Add(series);

                                if (series.Points.Count > 0)
                                {
                                    double[] fVal = series.Points[series.Points.Count - 1].Values;
                                    DataRow dr = dtTrendData.Rows.Find(series.Name);
                                    dr["CURR"] = fVal[0].ToString("#,##0.###");
                                }
                            }
                        }



                        c2ChartContorl.Enabled = true;
                        dgvTrendData.Enabled = true;
                        dgvTrendData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvTrendData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvTrendData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        dgvTrendData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    }
                }
                splashScreenManager1.CloseWaitForm();
                lblGroupName.Text = group;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                TrendDatabase.Close();
            }
        }


        private void CreateSeriesByGroupButton()
        {
            if (CheckStartEnd() == false) return;

            foreach (Form form in Application.OpenForms)
            {
                if (form.GetType() == typeof(PopUpTrendGroup))
                {
                    form.Activate();
                    form.TopMost = true;
                    form.WindowState = FormWindowState.Normal;
                    return;
                }
            }
            PopUpTrendGroup group = new PopUpTrendGroup();



            group.selectGroup += async (val) =>
            {
                splashScreenManager1.ShowWaitForm();
                TrendDatabase.Open();

                ChartTimeSpan(DateTime.Parse(startDate.Text), DateTime.Parse(endDate.Text));
                fMin = 0;
                fMax = 100;

                if (val.Length > 0)
                {
                    RemoveAllSeries();

                    if (!TrendDatabase.Open()) return;
                    string query = "SELECT B.SYSTEM, A.GROUP_NAME, A.DP_NAME, A.DP_DESC, A.MIN, A.MAX " +
                                    "FROM HMI_TREND_GROUP_DETAIL A JOIN C2_TREND_INFO B ON A.DP_NAME = B.DP_NAME  " +
                                    "WHERE A.GROUP_NAME = :1";

                    try
                    {
                        using (OracleCommand cmd = new OracleCommand(query, TrendDatabase.OracleConn))
                        {
                            cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = val;

                            OracleDataReader reader = cmd.ExecuteReader();

                            List<Series> listSeries = new List<Series>();
                            List<Task<Series>> listMakeSeries = new List<Task<Series>>();

                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    c2ChartContorl.Enabled = false;
                                    dgvTrendData.Enabled = false;

                                    if (chartCount < 25)
                                    {
                                        //차트에 이미 등록된 DP인지 확인
                                        DataRow dataRow = null;
                                        dataRow = dtTrendData.Rows.Find(reader["DP_NAME"]);

                                        if (dataRow == null) // 등록되지 않았으면..
                                        {
                                            chartCount++;

                                            DataRow dr = dtTrendData.NewRow();
                                            // dr["SEL"] = false;
                                            dr["VISIBLE"] = true;
                                            dr["DP_NAME"] = reader["DP_NAME"];
                                            dr["DP_DESC"] = reader["DP_DESC"];
                                            dr["MIN"] = reader["MIN"];
                                            dr["MAX"] = reader["MAX"];
                                            dr["SYSTEM"] = reader["SYSTEM"];
                                            dtTrendData.Rows.Add(dr);

                                            DataRow dr2 = dtRealTime.NewRow();
                                            dr2["DP_NAME"] = reader["DP_NAME"];
                                            dtRealTime.Rows.Add(dr2);

                                            SetMinMax(float.Parse(reader["MIN"].ToString()), float.Parse(reader["MAX"].ToString()));

                                            dgvTrendData.Rows[dgvTrendData.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                                            Color color = trendColor.GetTrendColor();
                                            dgvTrendData[0, dgvTrendData.Rows.Count - 1].Style.BackColor = color;
                                            dgvTrendData[1, dgvTrendData.Rows.Count - 1].Style.BackColor = color;

                                            MakeSeries makeSeries = new MakeSeries(reader["DP_NAME"].ToString(), startDate.Text, endDate.Text, trendColor, color);
                                            listMakeSeries.Add(makeSeries.MakeAsync());
                                        }
                                    }
                                }

                                if (c2ChartContorl.Series.Count < 26)
                                {
                                    foreach (Task<Series> item in listMakeSeries)
                                    {
                                        Series series = await item;
                                        c2ChartContorl.Series.Add(series);

                                        if (series.Points.Count > 0)
                                        {
                                            double[] fVal = series.Points[series.Points.Count - 1].Values;
                                            DataRow dr = dtTrendData.Rows.Find(series.Name);
                                            dr["CURR"] = fVal[0].ToString("#,##0.###");
                                        }
                                    }
                                }



                                c2ChartContorl.Enabled = true;
                                dgvTrendData.Enabled = true;
                                dgvTrendData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgvTrendData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgvTrendData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                dgvTrendData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                                //
                            }
                        }
                        splashScreenManager1.CloseWaitForm();
                        lblGroupName.Text = val;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    finally
                    {
                        TrendDatabase.Close();
                    }

                }
            };
            group.Show();
        }

        private async void CreateSeriesBySearchButton()
        {
            if (isResetting == true) return;
            isResetting = true;

            if (CheckStartEnd() == false) return;
            if (dtTrendData.Rows.Count < 1) return;

            if (!TrendDatabase.Open()) return;

            splashScreenManager1.ShowWaitForm();
            ChartTimeSpan(DateTime.Parse(startDate.Text), DateTime.Parse(endDate.Text));

            fMin = 0;
            fMax = 0;
            c2SwiftPlotDiagram.AxisY.VisualRange.SetMinMaxValues(0, 100);
            c2SwiftPlotDiagram.AxisY.WholeRange.SetMinMaxValues(0, 100);

            Dictionary<string, Color> dpColor = new Dictionary<string, Color>();


            foreach (DataRow item in dtTrendData.Rows)
            {
                dpColor.Add(item["DP_NAME"].ToString(), c2ChartContorl.Series[item["DP_NAME"].ToString()].View.Color);
            }

            foreach (Series item in c2ChartContorl.Series)
            {
                trendColor.RemoveTrendColor(item.View.Color);
            }

            chartCount = 0;
            c2ChartContorl.Series.Clear();

            

            try
            {
                List<Series> listSeries = new List<Series>();
                List<Task<Series>> listMakeSeries = new List<Task<Series>>();

                c2ChartContorl.Enabled = false;
                dgvTrendData.Enabled = false;


                foreach (DataRow item in dtTrendData.Rows)
                {


                    SetMinMax(float.Parse(item["MIN"].ToString()), float.Parse(item["MAX"].ToString()));

                    MakeSeries makeSeries = new MakeSeries(item["DP_NAME"].ToString(), startDate.Text, endDate.Text, trendColor, dpColor[item["DP_NAME"].ToString()]);
                    listMakeSeries.Add(makeSeries.MakeAsync());

                }

                foreach (Task<Series> item in listMakeSeries)
                {
                    Series series = await item;
                    c2ChartContorl.Series.Add(series);

                    if (series.Points.Count > 0)
                    {
                        double[] fVal = series.Points[series.Points.Count - 1].Values;
                        DataRow dr = dtTrendData.Rows.Find(series.Name);
                        dr["CURR"] = fVal[0].ToString("#,##0.###");
                    }
                }


                c2ChartContorl.Enabled = true;
                dgvTrendData.Enabled = true;


                splashScreenManager1.CloseWaitForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                TrendDatabase.Close();
                isResetting = false;

            }

        }

        private async void TrendAdd(string dpName)
        {
            if (CheckStartEnd() == false) return;

            TrendDatabase.Open();


            ChartTimeSpan(DateTime.Parse(startDate.Text), DateTime.Parse(endDate.Text));

            if (isAddChecked == false)
                RemoveAllSeries();

            if (!TrendDatabase.Open()) return;
            string query = "SELECT SYSTEM,  DP_NAME, DP_DESC, Y_MIN, Y_MAX " +
                            "FROM C2_TREND_INFO WHERE DP_NAME = :1";

            try
            {
                using (OracleCommand cmd = new OracleCommand(query, TrendDatabase.OracleConn))
                {
                    cmd.Parameters.Add(":1", OracleDbType.Varchar2).Value = dpName;

                    OracleDataReader reader = cmd.ExecuteReader();

                    List<Series> listSeries = new List<Series>();
                    List<Task<Series>> listMakeSeries = new List<Task<Series>>();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            c2ChartContorl.Enabled = false;
                            dgvTrendData.Enabled = false;

                            if (chartCount < 25)
                            {
                                //차트에 이미 등록된 DP인지 확인
                                DataRow dataRow = null;
                                dataRow = dtTrendData.Rows.Find(reader["DP_NAME"]);


                                if (dataRow == null) // 등록되지 않았으면..
                                {
                                    chartCount++;

                                    DataRow dr = dtTrendData.NewRow();
                                    //dr["SEL"] = false;
                                    dr["VISIBLE"] = true;
                                    dr["DP_NAME"] = reader["DP_NAME"];
                                    dr["DP_DESC"] = reader["DP_DESC"];
                                    dr["MIN"] = reader["Y_MIN"];
                                    dr["MAX"] = reader["Y_MAX"];
                                    dr["SYSTEM"] = reader["SYSTEM"];
                                    dtTrendData.Rows.Add(dr);

                                    DataRow dr2 = dtRealTime.NewRow();
                                    dr2["DP_NAME"] = reader["DP_NAME"];
                                    dtRealTime.Rows.Add(dr2);

                                    SetMinMax(float.Parse(reader["Y_MIN"].ToString()), float.Parse(reader["Y_MAX"].ToString()));

                                    dgvTrendData.Rows[dgvTrendData.Rows.Count - 1].DefaultCellStyle.BackColor = Colors.dgvBackColor;
                                    Color color = trendColor.GetTrendColor();
                                    dgvTrendData[0, dgvTrendData.Rows.Count - 1].Style.BackColor = color;
                                    dgvTrendData[1, dgvTrendData.Rows.Count - 1].Style.BackColor = color;

                                    MakeSeries makeSeries = new MakeSeries(reader["DP_NAME"].ToString(), startDate.Text, endDate.Text, trendColor, color);
                                    listMakeSeries.Add(makeSeries.MakeAsync());
                                }
                            }
                        }

                        if (c2ChartContorl.Series.Count < 26)
                        {
                            foreach (Task<Series> item in listMakeSeries)
                            {
                                Series series = await item;
                                c2ChartContorl.Series.Add(series);

                                if (series.Points.Count > 0)
                                {
                                    double[] fVal = series.Points[series.Points.Count - 1].Values;
                                    DataRow dr = dtTrendData.Rows.Find(series.Name);
                                    dr["CURR"] = fVal[0].ToString("#,##0.###");
                                }
                            }
                        }

                        c2ChartContorl.Enabled = true;
                        dgvTrendData.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                TrendDatabase.Close();
            }



        }



        private void Menu_search_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {

            CreateSeriesBySearchButton();

        }

        private void Menu2_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            string buttonName = e.Button.Properties.Caption;

            switch (buttonName)
            {
                case "RESET":
                    CreateSeriesBySearchButton();
                    break;
                case "CLEAR":
                    RemoveAllSeries();
                    break;
            }

        }

        private void DgvTrendData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if(e.ColumnIndex == 0)            
            //    dtTrendData.Rows[e.RowIndex]["SEL"] = !bool.Parse(dtTrendData.Rows[e.RowIndex]["SEL"].ToString());
            if (e.ColumnIndex == 1)
            {
                dtTrendData.Rows[e.RowIndex]["VISIBLE"] = !bool.Parse(dtTrendData.Rows[e.RowIndex]["VISIBLE"].ToString());

                string dpName = dtTrendData.Rows[e.RowIndex][3].ToString();
                if (bool.Parse(dtTrendData.Rows[e.RowIndex]["VISIBLE"].ToString()) == false)
                {
                    c2ChartContorl.Series[dpName].Visible = false;
                }
                else
                {
                    c2ChartContorl.Series[dpName].Visible = true;
                }
            }

        }

        private void Menu1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            if (c2ChartContorl.Series.Count == 0) return;


            string buttonName = e.Button.Properties.Caption;
            switch (buttonName)
            {
                case "7Day":
                    GetChartByPeroid("7Day");
                    break;
                case "3Day":
                    GetChartByPeroid("3Day");
                    break;
                case "1Day":
                    GetChartByPeroid("1Day");
                    break;
                case "12Hour":
                    GetChartByPeroid("12Hour");
                    break;
                case "4Hour":
                    GetChartByPeroid("4Hour");
                    break;
                case "1Hour":
                    GetChartByPeroid("1Hour");
                    break;
                case "30Min":
                    GetChartByPeroid("30Min");
                    break;
                case "10Min":
                    GetChartByPeroid("10Min");
                    break;
                case "BACK":
                    Backward();
                    break;
                case "FORW":
                    Forward();
                    break;
            }


        }

        private void Backward()
        {
            if (CheckStartEnd() == false) return;
            DateTime start = DateTime.Parse(startDate.Text);
            DateTime end = DateTime.Parse(endDate.Text);
            TimeSpan span = end - start;
            startDate.Text = start.AddSeconds(-span.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss"); ;
            endDate.Text = end.AddSeconds(-span.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss");
            c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
            c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);

            CreateSeriesBySearchButton();

        }

        private void Forward()
        {
            if (CheckStartEnd() == false) return;

            DateTime start = DateTime.Parse(startDate.Text);
            DateTime end = DateTime.Parse(endDate.Text);
            TimeSpan span = end - start;

            if (end > DateTime.Now) return;

            startDate.Text = start.AddSeconds(span.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss"); ;
            endDate.Text = end.AddSeconds(span.TotalSeconds).ToString("yyyy-MM-dd HH:mm:ss");
            c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(start, end);
            c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(start, end);

            CreateSeriesBySearchButton();
        }

        private void GetChartByPeroid(string period)
        {
            if (CheckStartEnd() == false) return;
            SetControls(period);
            CreateSeriesBySearchButton();

        }

        private void RealTimer_Tick(object sender, EventArgs e)
        {
            realTimer.Stop();

            //chart 시간 이동
            ChartTimeMove();

            //차트 series의 dp를 OA로 보내기(값 읽어오기 위해)
            try
            {
                seriesDpToOa();
                ApplyTrendValue();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                realTimer.Start();
            }


        }

        private void ApplyTrendValue()
        {
            for (int i = 0; i < dtRealTime.Rows.Count; i++)
            {
                string dpName = dtRealTime.Rows[i]["DP_NAME"].ToString();
                Series series = null;
                series = c2ChartContorl.Series[dpName];
                if (series != null)
                {
                    DateTime start = Convert.ToDateTime(startDate.Text);
                    if (series.Points.Count > 0 && Convert.ToDateTime(series.Points[0].Argument) < start)
                    {
                        series.Points.RemoveAt(0);
                    }

                    if (dtRealTime.Rows[i]["CURR"].ToString() != "")
                    {
                        series.Points.Add(new SeriesPoint(DateTime.Now, dtRealTime.Rows[i]["CURR"]));
                        TrendDataToDatagridView(dtRealTime.Rows[i]["DP_NAME"].ToString(), dtRealTime.Rows[i]["CURR"].ToString());
                    }
                }
            }

            // foreach (DataRow realTimeValue in dtRealTime.Rows)
            //{
            //foreach (Series sr in c2ChartContorl.Series)
            //{
            //    if (realTimeValue["DP_NAME"].ToString() == sr.Name)
            //    {
            //        DateTime start = Convert.ToDateTime(startDate.Text);
            //        if (sr.Points.Count > 0 && Convert.ToDateTime(sr.Points[0].Argument) < start)
            //        {
            //            sr.Points.RemoveAt(0);
            //        }

            //        if (realTimeValue["CURR"].ToString() != "")
            //        {
            //            sr.Points.Add(new SeriesPoint(DateTime.Now, realTimeValue["CURR"]));
            //            TrendDataToDatagridView(realTimeValue["DP_NAME"].ToString(), realTimeValue["CURR"].ToString());
            //        }

            //    }
            //}

            //Series series = null;
            //series = c2ChartContorl.Series[realTimeValue["DP_NAME"].ToString()];
            //if(series != null)
            //{
            //    //DateTime start = Convert.ToDateTime(startDate.Text);
            //    //if (series.Points.Count > 0 && Convert.ToDateTime(series.Points[0].Argument) < start)
            //    //{
            //    //    series.Points.RemoveAt(0);
            //    //}

            //    //if (realTimeValue["CURR"].ToString() != "")
            //    //{
            //    //    series.Points.Add(new SeriesPoint(DateTime.Now, realTimeValue["CURR"]));
            //    //    TrendDataToDatagridView(realTimeValue["DP_NAME"].ToString(), realTimeValue["CURR"].ToString());
            //    //}
            //}

            // }
        }

        private void TrendDataToDatagridView(string dpName, string realTimeValue)
        {
            var searched = dtTrendData.AsEnumerable()
                                .Where(r => r.Field<string>("DP_NAME") == dpName);
            foreach (DataRow item in searched)
            {
                float fVal;
                bool bRtn = float.TryParse(realTimeValue, out fVal);

                if (bRtn == true)
                    item["CURR"] = fVal.ToString("#,##0.###");
            }
        }

        private void seriesDpToOa()
        {
            if (dtTrendData.Rows.Count == 0) return;
            string sendMsg = $"trendValue;{this.Name};";
            foreach (DataRow item in dtTrendData.Rows)
            {
                sendMsg += item["SYSTEM"] + ":" + item["DP_NAME"] + ";";
            }
            sendMsg = sendMsg.Substring(0, sendMsg.Length - 1);

            mainForm.sendMsgToOA(sendMsg);
        }

        private void ChartTimeMove()
        {

            DateTime maxValue = DateTime.Now;
            DateTime minValue = DateTime.Now.AddMinutes(-10);
            try
            {
                c2SwiftPlotDiagram.AxisX.VisualRange.SetMinMaxValues(minValue, maxValue);
                c2SwiftPlotDiagram.AxisX.WholeRange.SetMinMaxValues(minValue, maxValue);
                startDate.Text = minValue.ToString("yyyy-MM-dd HH:mm:ss");
                endDate.Text = maxValue.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }

        }


        private void FormTrend_Load(object sender, EventArgs e)
        {
            //ChartControl 초기화
            InitChartControl();

            //다른 컨트롤 초기화
            InitControls();

            InitDatatable.Init(dtTrendData);
            dgvTrendData.DataSource = dtTrendData;

            InitDatatable.Init(dtRealTime);

            InitDataGridView.dataGridViewInit(dgvTrendData);
            InitDataGridView.AutoSettingDatagridView(dgvTrendData, new List<int>() { 3, 4 }, new List<int>());
            dgvTrendData.Columns[0].Width = 30;

            //DataBase OPEN
            TrendDatabase.CreateDatabase();
            if (!TrendDatabase.Open())
            {
                MessageBox.Show("Database connection is invalid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }

            c2ChartContorl.CustomDrawCrosshair += C2ChartContorl_CustomDrawCrosshair;

            SetDoNotSort(dgvTrendData);

        }

        private void dgvTrendData_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex == 3)
            {
                string dpName = dtTrendData.Rows[e.RowIndex][3].ToString();
                Color color = c2ChartContorl.Series[dpName].View.Color;
                c2ChartContorl.Series.Remove(c2ChartContorl.Series[dpName]);

                trendColor.RemoveTrendColor(color);
                // dgvTrendData.Rows.RemoveAt(e.RowIndex);
                dtTrendData.Rows[e.RowIndex].Delete();

                DataRow dataRow = null;
                dataRow = dtRealTime.Rows.Find(dpName);

                if (dataRow != null)
                    dataRow.Delete();
            }
        }


        private bool CheckStartEnd()
        {
            DateTime start = DateTime.Parse(startDate.Text);
            DateTime end = DateTime.Parse(endDate.Text);

            TimeSpan span = end - start;
            if (span.TotalSeconds < 0)
                return false;
            else
                return true;
        }

        private void SetDoNotSort(DataGridView dgv)
        {
            foreach (DataGridViewColumn i in dgv.Columns)
            {
                i.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

    }
}
