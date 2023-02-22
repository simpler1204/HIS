namespace HIS.Forms
{
    partial class FormAlarm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions1 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlarm));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAckUser = new System.Windows.Forms.TextBox();
            this.cmbAlarmLevel = new System.Windows.Forms.ComboBox();
            this.txtDpName = new System.Windows.Forms.TextBox();
            this.txtAlarmMsg = new System.Windows.Forms.TextBox();
            this.startDt = new System.Windows.Forms.DateTimePicker();
            this.endDt = new System.Windows.Forms.DateTimePicker();
            this.chkRecent = new System.Windows.Forms.CheckBox();
            this.chkRealTime = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblUnAck = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbmAlarmLevel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.menu = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblNowCount = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.status = new System.Windows.Forms.DataGridViewImageColumn();
            this.panel1.SuspendLayout();
            this.panelProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtAckUser);
            this.panel1.Controls.Add(this.cmbAlarmLevel);
            this.panel1.Controls.Add(this.txtDpName);
            this.panel1.Controls.Add(this.txtAlarmMsg);
            this.panel1.Controls.Add(this.startDt);
            this.panel1.Controls.Add(this.endDt);
            this.panel1.Controls.Add(this.chkRecent);
            this.panel1.Controls.Add(this.chkRealTime);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.lblTotal);
            this.panel1.Controls.Add(this.lblUnAck);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cbmAlarmLevel);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.menu);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1214, 80);
            this.panel1.TabIndex = 0;
            // 
            // txtAckUser
            // 
            this.txtAckUser.BackColor = System.Drawing.SystemColors.Control;
            this.txtAckUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAckUser.ForeColor = System.Drawing.Color.Black;
            this.txtAckUser.Location = new System.Drawing.Point(389, 13);
            this.txtAckUser.Name = "txtAckUser";
            this.txtAckUser.Size = new System.Drawing.Size(125, 21);
            this.txtAckUser.TabIndex = 48;
            // 
            // cmbAlarmLevel
            // 
            this.cmbAlarmLevel.AllowDrop = true;
            this.cmbAlarmLevel.BackColor = System.Drawing.SystemColors.Control;
            this.cmbAlarmLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAlarmLevel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbAlarmLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAlarmLevel.ForeColor = System.Drawing.Color.Black;
            this.cmbAlarmLevel.FormattingEnabled = true;
            this.cmbAlarmLevel.Items.AddRange(new object[] {
            "ALL",
            "EMERGENCY",
            "ALARM",
            "WARNING",
            "CAUTION"});
            this.cmbAlarmLevel.Location = new System.Drawing.Point(389, 41);
            this.cmbAlarmLevel.Name = "cmbAlarmLevel";
            this.cmbAlarmLevel.Size = new System.Drawing.Size(125, 23);
            this.cmbAlarmLevel.TabIndex = 49;
            // 
            // txtDpName
            // 
            this.txtDpName.BackColor = System.Drawing.SystemColors.Control;
            this.txtDpName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDpName.ForeColor = System.Drawing.Color.Black;
            this.txtDpName.Location = new System.Drawing.Point(580, 13);
            this.txtDpName.Name = "txtDpName";
            this.txtDpName.Size = new System.Drawing.Size(259, 22);
            this.txtDpName.TabIndex = 50;
            // 
            // txtAlarmMsg
            // 
            this.txtAlarmMsg.BackColor = System.Drawing.SystemColors.Control;
            this.txtAlarmMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAlarmMsg.ForeColor = System.Drawing.Color.Black;
            this.txtAlarmMsg.Location = new System.Drawing.Point(580, 42);
            this.txtAlarmMsg.Name = "txtAlarmMsg";
            this.txtAlarmMsg.Size = new System.Drawing.Size(259, 22);
            this.txtAlarmMsg.TabIndex = 51;
            // 
            // startDt
            // 
            this.startDt.CalendarTitleBackColor = System.Drawing.SystemColors.Control;
            this.startDt.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.startDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDt.Location = new System.Drawing.Point(71, 14);
            this.startDt.Name = "startDt";
            this.startDt.ShowUpDown = true;
            this.startDt.Size = new System.Drawing.Size(146, 21);
            this.startDt.TabIndex = 46;
            // 
            // endDt
            // 
            this.endDt.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDt.Location = new System.Drawing.Point(71, 43);
            this.endDt.Name = "endDt";
            this.endDt.ShowUpDown = true;
            this.endDt.Size = new System.Drawing.Size(146, 21);
            this.endDt.TabIndex = 47;
            // 
            // chkRecent
            // 
            this.chkRecent.AutoSize = true;
            this.chkRecent.Checked = true;
            this.chkRecent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRecent.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRecent.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRecent.ForeColor = System.Drawing.Color.White;
            this.chkRecent.Location = new System.Drawing.Point(218, 35);
            this.chkRecent.Name = "chkRecent";
            this.chkRecent.Size = new System.Drawing.Size(78, 36);
            this.chkRecent.TabIndex = 53;
            this.chkRecent.Text = "Alaways\r\n recent";
            this.chkRecent.UseVisualStyleBackColor = true;
            this.chkRecent.CheckStateChanged += new System.EventHandler(this.chkRecent_CheckStateChanged);
            // 
            // chkRealTime
            // 
            this.chkRealTime.AutoSize = true;
            this.chkRealTime.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkRealTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRealTime.ForeColor = System.Drawing.Color.White;
            this.chkRealTime.Location = new System.Drawing.Point(872, 13);
            this.chkRealTime.Name = "chkRealTime";
            this.chkRealTime.Size = new System.Drawing.Size(90, 20);
            this.chkRealTime.TabIndex = 52;
            this.chkRealTime.Text = "Real Time";
            this.chkRealTime.UseVisualStyleBackColor = true;
            this.chkRealTime.CheckStateChanged += new System.EventHandler(this.chkRealTime_CheckStateChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(962, 47);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 41;
            this.label8.Text = "/";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.ForeColor = System.Drawing.Color.White;
            this.lblTotal.Location = new System.Drawing.Point(1032, 47);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(11, 12);
            this.lblTotal.TabIndex = 40;
            this.lblTotal.Text = "0";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnAck
            // 
            this.lblUnAck.AutoSize = true;
            this.lblUnAck.ForeColor = System.Drawing.Color.Yellow;
            this.lblUnAck.Location = new System.Drawing.Point(925, 47);
            this.lblUnAck.Name = "lblUnAck";
            this.lblUnAck.Size = new System.Drawing.Size(11, 12);
            this.lblUnAck.TabIndex = 39;
            this.lblUnAck.Text = "0";
            this.lblUnAck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(870, 47);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 12);
            this.label5.TabIndex = 38;
            this.label5.Text = "UNACK :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(979, 47);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 37;
            this.label9.Text = "TOTAL :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(23, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 40;
            this.label1.Text = "Start :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(26, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 45;
            this.label2.Text = "End :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(314, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 16);
            this.label4.TabIndex = 41;
            this.label4.Text = "Ack User :";
            // 
            // cbmAlarmLevel
            // 
            this.cbmAlarmLevel.AutoSize = true;
            this.cbmAlarmLevel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbmAlarmLevel.ForeColor = System.Drawing.Color.White;
            this.cbmAlarmLevel.Location = new System.Drawing.Point(309, 45);
            this.cbmAlarmLevel.Name = "cbmAlarmLevel";
            this.cbmAlarmLevel.Size = new System.Drawing.Size(74, 16);
            this.cbmAlarmLevel.TabIndex = 42;
            this.cbmAlarmLevel.Text = "Alarm Lev :";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(542, 16);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(33, 16);
            this.label6.TabIndex = 43;
            this.label6.Text = "DP :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(534, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.TabIndex = 44;
            this.label7.Text = "Msg :";
            // 
            // menu
            // 
            this.menu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menu.ButtonInterval = 30;
            windowsUIButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions1.SvgImage")));
            windowsUIButtonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions2.SvgImage")));
            this.menu.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Search", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Search", -1, true, null, true, false, true, null, -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Export", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Export", -1, true, null, true, false, true, null, -1, false)});
            this.menu.Location = new System.Drawing.Point(1056, 5);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(155, 72);
            this.menu.TabIndex = 0;
            this.menu.Text = "windowsUIButtonPanel1";
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.label3);
            this.panelProgress.Controls.Add(this.lblTotalCount);
            this.panelProgress.Controls.Add(this.lblNowCount);
            this.panelProgress.Controls.Add(this.progressBar1);
            this.panelProgress.Location = new System.Drawing.Point(472, 283);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(335, 72);
            this.panelProgress.TabIndex = 2;
            this.panelProgress.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(168, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "/";
            // 
            // lblTotalCount
            // 
            this.lblTotalCount.AutoSize = true;
            this.lblTotalCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalCount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTotalCount.Location = new System.Drawing.Point(186, 44);
            this.lblTotalCount.Name = "lblTotalCount";
            this.lblTotalCount.Size = new System.Drawing.Size(45, 16);
            this.lblTotalCount.TabIndex = 2;
            this.lblTotalCount.Text = "label2";
            // 
            // lblNowCount
            // 
            this.lblNowCount.AutoSize = true;
            this.lblNowCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNowCount.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblNowCount.Location = new System.Drawing.Point(116, 44);
            this.lblNowCount.Name = "lblNowCount";
            this.lblNowCount.Size = new System.Drawing.Size(45, 16);
            this.lblNowCount.TabIndex = 1;
            this.lblNowCount.Text = "label1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(4, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(328, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.status});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1214, 571);
            this.dataGridView1.TabIndex = 3;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            // 
            // status
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.NullValue = ((object)(resources.GetObject("dataGridViewCellStyle1.NullValue")));
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.status.DefaultCellStyle = dataGridViewCellStyle1;
            this.status.HeaderText = "";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.status.Width = 30;
            // 
            // FormAlarm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1214, 651);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "FormAlarm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ALARM HISTORY";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel menu;
        private System.Windows.Forms.TextBox txtAckUser;
        private System.Windows.Forms.ComboBox cmbAlarmLevel;
        private System.Windows.Forms.TextBox txtAlarmMsg;
        private System.Windows.Forms.DateTimePicker startDt;
        private System.Windows.Forms.DateTimePicker endDt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label cbmAlarmLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblNowCount;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblUnAck;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkRealTime;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkRecent;
        private System.Windows.Forms.DataGridViewImageColumn status;
        public System.Windows.Forms.TextBox txtDpName;
    }
}