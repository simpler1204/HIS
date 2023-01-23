namespace HIS.Forms
{
    partial class FormOperation
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOperation));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions6 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.panel1 = new System.Windows.Forms.Panel();
            this.startDt = new System.Windows.Forms.DateTimePicker();
            this.endDt = new System.Windows.Forms.DateTimePicker();
            this.txtEleDesc = new System.Windows.Forms.TextBox();
            this.txtEle = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtDp = new System.Windows.Forms.TextBox();
            this.menu = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblNowCount = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.startDt);
            this.panel1.Controls.Add(this.endDt);
            this.panel1.Controls.Add(this.txtEleDesc);
            this.panel1.Controls.Add(this.txtEle);
            this.panel1.Controls.Add(this.txtDesc);
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Controls.Add(this.txtDp);
            this.panel1.Controls.Add(this.menu);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1214, 80);
            this.panel1.TabIndex = 0;
            // 
            // startDt
            // 
            this.startDt.CalendarForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.startDt.CalendarMonthBackground = System.Drawing.Color.Black;
            this.startDt.CalendarTitleBackColor = System.Drawing.Color.Black;
            this.startDt.CalendarTitleForeColor = System.Drawing.Color.Black;
            this.startDt.CalendarTrailingForeColor = System.Drawing.SystemColors.ControlText;
            this.startDt.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.startDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDt.Location = new System.Drawing.Point(73, 13);
            this.startDt.Name = "startDt";
            this.startDt.ShowUpDown = true;
            this.startDt.Size = new System.Drawing.Size(136, 21);
            this.startDt.TabIndex = 68;
            // 
            // endDt
            // 
            this.endDt.CalendarMonthBackground = System.Drawing.Color.Black;
            this.endDt.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDt.Location = new System.Drawing.Point(73, 42);
            this.endDt.Name = "endDt";
            this.endDt.ShowUpDown = true;
            this.endDt.Size = new System.Drawing.Size(136, 21);
            this.endDt.TabIndex = 69;
            // 
            // txtEleDesc
            // 
            this.txtEleDesc.BackColor = System.Drawing.SystemColors.Control;
            this.txtEleDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEleDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEleDesc.ForeColor = System.Drawing.Color.Black;
            this.txtEleDesc.Location = new System.Drawing.Point(715, 41);
            this.txtEleDesc.Name = "txtEleDesc";
            this.txtEleDesc.Size = new System.Drawing.Size(147, 22);
            this.txtEleDesc.TabIndex = 81;
            // 
            // txtEle
            // 
            this.txtEle.BackColor = System.Drawing.SystemColors.Control;
            this.txtEle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEle.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEle.ForeColor = System.Drawing.Color.Black;
            this.txtEle.Location = new System.Drawing.Point(715, 12);
            this.txtEle.Name = "txtEle";
            this.txtEle.Size = new System.Drawing.Size(147, 22);
            this.txtEle.TabIndex = 80;
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.SystemColors.Control;
            this.txtDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesc.ForeColor = System.Drawing.Color.Black;
            this.txtDesc.Location = new System.Drawing.Point(276, 41);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(371, 22);
            this.txtDesc.TabIndex = 77;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.SystemColors.Control;
            this.txtUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.Color.Black;
            this.txtUser.Location = new System.Drawing.Point(931, 12);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(112, 22);
            this.txtUser.TabIndex = 73;
            // 
            // txtDp
            // 
            this.txtDp.BackColor = System.Drawing.SystemColors.Control;
            this.txtDp.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDp.ForeColor = System.Drawing.Color.Black;
            this.txtDp.Location = new System.Drawing.Point(276, 12);
            this.txtDp.Name = "txtDp";
            this.txtDp.Size = new System.Drawing.Size(371, 22);
            this.txtDp.TabIndex = 76;
            // 
            // menu
            // 
            this.menu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.menu.ButtonInterval = 30;
            windowsUIButtonImageOptions5.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions5.SvgImage")));
            windowsUIButtonImageOptions6.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions6.SvgImage")));
            this.menu.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Search", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Search", -1, true, null, true, false, true, null, -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Export", true, windowsUIButtonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Export", -1, true, null, true, false, true, null, -1, false)});
            this.menu.Location = new System.Drawing.Point(1053, 4);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(155, 72);
            this.menu.TabIndex = 1;
            this.menu.Text = "windowsUIButtonPanel1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 70;
            this.label1.Text = "Start :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label2.Location = new System.Drawing.Point(25, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 71;
            this.label2.Text = "End :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label8.Location = new System.Drawing.Point(662, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(46, 16);
            this.label8.TabIndex = 79;
            this.label8.Text = "Desc :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label7.Location = new System.Drawing.Point(880, 15);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 16);
            this.label7.TabIndex = 72;
            this.label7.Text = "User :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label9.Location = new System.Drawing.Point(674, 15);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 16);
            this.label9.TabIndex = 78;
            this.label9.Text = "Ele :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label5.Location = new System.Drawing.Point(239, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 16);
            this.label5.TabIndex = 74;
            this.label5.Text = "DP:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label3.Location = new System.Drawing.Point(226, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 16);
            this.label3.TabIndex = 75;
            this.label3.Text = "Desc:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 80);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(1214, 571);
            this.dataGridView1.TabIndex = 1;
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.label4);
            this.panelProgress.Controls.Add(this.lblTotalCount);
            this.panelProgress.Controls.Add(this.lblNowCount);
            this.panelProgress.Controls.Add(this.progressBar1);
            this.panelProgress.Location = new System.Drawing.Point(502, 258);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(335, 72);
            this.panelProgress.TabIndex = 3;
            this.panelProgress.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(168, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "/";
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
            // FormOperation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1214, 651);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.panel1);
            this.Name = "FormOperation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OPERATION HISTORY";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtEleDesc;
        private System.Windows.Forms.TextBox txtEle;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel menu;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblNowCount;
        private System.Windows.Forms.ProgressBar progressBar1;
        public System.Windows.Forms.DateTimePicker startDt;
        public System.Windows.Forms.DateTimePicker endDt;
        public System.Windows.Forms.TextBox txtDp;
    }
}