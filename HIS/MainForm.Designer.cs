namespace HIS
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btn_logging = new FontAwesome.Sharp.IconButton();
            this.btnMenu = new FontAwesome.Sharp.IconButton();
            this.btnSms = new FontAwesome.Sharp.IconButton();
            this.btnMultiTrend = new FontAwesome.Sharp.IconButton();
            this.btnOperation = new FontAwesome.Sharp.IconButton();
            this.btnAlarm = new FontAwesome.Sharp.IconButton();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.btnHome = new System.Windows.Forms.PictureBox();
            this.panelTitleBar = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGrade = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.btn_minimize = new FontAwesome.Sharp.IconPictureBox();
            this.btn_close = new FontAwesome.Sharp.IconPictureBox();
            this.btn_restore = new FontAwesome.Sharp.IconPictureBox();
            this.lblTitleChildForm = new System.Windows.Forms.Label();
            this.iconCurrentChildFrom = new FontAwesome.Sharp.IconPictureBox();
            this.panelShow = new System.Windows.Forms.Panel();
            this.panelDesktop = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelMenu.SuspendLayout();
            this.panelLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).BeginInit();
            this.panelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_minimize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_restore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildFrom)).BeginInit();
            this.panelDesktop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.panelMenu.Controls.Add(this.btn_logging);
            this.panelMenu.Controls.Add(this.btnMenu);
            this.panelMenu.Controls.Add(this.btnSms);
            this.panelMenu.Controls.Add(this.btnMultiTrend);
            this.panelMenu.Controls.Add(this.btnOperation);
            this.panelMenu.Controls.Add(this.btnAlarm);
            this.panelMenu.Controls.Add(this.panelLogo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(220, 1061);
            this.panelMenu.TabIndex = 1;
            // 
            // btn_logging
            // 
            this.btn_logging.Dock = System.Windows.Forms.DockStyle.Top;
            this.btn_logging.FlatAppearance.BorderSize = 0;
            this.btn_logging.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_logging.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btn_logging.ForeColor = System.Drawing.Color.Gainsboro;
            this.btn_logging.IconChar = FontAwesome.Sharp.IconChar.Pen;
            this.btn_logging.IconColor = System.Drawing.Color.Gainsboro;
            this.btn_logging.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_logging.IconSize = 30;
            this.btn_logging.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_logging.Location = new System.Drawing.Point(0, 440);
            this.btn_logging.Name = "btn_logging";
            this.btn_logging.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btn_logging.Size = new System.Drawing.Size(220, 60);
            this.btn_logging.TabIndex = 6;
            this.btn_logging.Text = "LOGGING";
            this.btn_logging.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_logging.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btn_logging.UseVisualStyleBackColor = true;
            this.btn_logging.Click += new System.EventHandler(this.btn_logging_Click);
            // 
            // btnMenu
            // 
            this.btnMenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMenu.FlatAppearance.BorderSize = 0;
            this.btnMenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMenu.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMenu.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMenu.IconChar = FontAwesome.Sharp.IconChar.ListAlt;
            this.btnMenu.IconColor = System.Drawing.Color.Gainsboro;
            this.btnMenu.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMenu.IconSize = 30;
            this.btnMenu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenu.Location = new System.Drawing.Point(0, 380);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnMenu.Size = new System.Drawing.Size(220, 60);
            this.btnMenu.TabIndex = 5;
            this.btnMenu.Text = "MENU";
            this.btnMenu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMenu.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMenu.UseVisualStyleBackColor = true;
            this.btnMenu.Click += new System.EventHandler(this.btnMenu_Click);
            // 
            // btnSms
            // 
            this.btnSms.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSms.FlatAppearance.BorderSize = 0;
            this.btnSms.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSms.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnSms.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnSms.IconChar = FontAwesome.Sharp.IconChar.Sms;
            this.btnSms.IconColor = System.Drawing.Color.Gainsboro;
            this.btnSms.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnSms.IconSize = 30;
            this.btnSms.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSms.Location = new System.Drawing.Point(0, 320);
            this.btnSms.Name = "btnSms";
            this.btnSms.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnSms.Size = new System.Drawing.Size(220, 60);
            this.btnSms.TabIndex = 4;
            this.btnSms.Text = "SMS 威信";
            this.btnSms.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSms.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSms.UseVisualStyleBackColor = true;
            this.btnSms.Click += new System.EventHandler(this.btnSms_Click);
            // 
            // btnMultiTrend
            // 
            this.btnMultiTrend.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnMultiTrend.FlatAppearance.BorderSize = 0;
            this.btnMultiTrend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMultiTrend.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnMultiTrend.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnMultiTrend.IconChar = FontAwesome.Sharp.IconChar.ChartGantt;
            this.btnMultiTrend.IconColor = System.Drawing.Color.Gainsboro;
            this.btnMultiTrend.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnMultiTrend.IconSize = 30;
            this.btnMultiTrend.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMultiTrend.Location = new System.Drawing.Point(0, 260);
            this.btnMultiTrend.Name = "btnMultiTrend";
            this.btnMultiTrend.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnMultiTrend.Size = new System.Drawing.Size(220, 60);
            this.btnMultiTrend.TabIndex = 3;
            this.btnMultiTrend.Text = "TREND";
            this.btnMultiTrend.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnMultiTrend.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnMultiTrend.UseVisualStyleBackColor = true;
            this.btnMultiTrend.Click += new System.EventHandler(this.btnMultiTrend_Click);
            // 
            // btnOperation
            // 
            this.btnOperation.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOperation.FlatAppearance.BorderSize = 0;
            this.btnOperation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOperation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOperation.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnOperation.IconChar = FontAwesome.Sharp.IconChar.Toolbox;
            this.btnOperation.IconColor = System.Drawing.Color.Gainsboro;
            this.btnOperation.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnOperation.IconSize = 30;
            this.btnOperation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOperation.Location = new System.Drawing.Point(0, 200);
            this.btnOperation.Name = "btnOperation";
            this.btnOperation.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnOperation.Size = new System.Drawing.Size(220, 60);
            this.btnOperation.TabIndex = 2;
            this.btnOperation.Text = "OPERATION";
            this.btnOperation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnOperation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOperation.UseVisualStyleBackColor = true;
            this.btnOperation.Click += new System.EventHandler(this.btnOperation_Click);
            // 
            // btnAlarm
            // 
            this.btnAlarm.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnAlarm.FlatAppearance.BorderSize = 0;
            this.btnAlarm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAlarm.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnAlarm.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnAlarm.IconChar = FontAwesome.Sharp.IconChar.Bell;
            this.btnAlarm.IconColor = System.Drawing.Color.Gainsboro;
            this.btnAlarm.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btnAlarm.IconSize = 30;
            this.btnAlarm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlarm.Location = new System.Drawing.Point(0, 140);
            this.btnAlarm.Name = "btnAlarm";
            this.btnAlarm.Padding = new System.Windows.Forms.Padding(10, 0, 20, 0);
            this.btnAlarm.Size = new System.Drawing.Size(220, 60);
            this.btnAlarm.TabIndex = 1;
            this.btnAlarm.Text = "ALARM";
            this.btnAlarm.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAlarm.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAlarm.UseVisualStyleBackColor = true;
            this.btnAlarm.Click += new System.EventHandler(this.btnAlarm_Click);
            // 
            // panelLogo
            // 
            this.panelLogo.Controls.Add(this.btnHome);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(220, 140);
            this.panelLogo.TabIndex = 0;
            // 
            // btnHome
            // 
            this.btnHome.Image = global::HIS.Properties.Resources.hynix;
            this.btnHome.Location = new System.Drawing.Point(8, 24);
            this.btnHome.Name = "btnHome";
            this.btnHome.Size = new System.Drawing.Size(180, 70);
            this.btnHome.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnHome.TabIndex = 0;
            this.btnHome.TabStop = false;
            this.btnHome.Click += new System.EventHandler(this.btnHome_Click);
            // 
            // panelTitleBar
            // 
            this.panelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.panelTitleBar.Controls.Add(this.label2);
            this.panelTitleBar.Controls.Add(this.lblGrade);
            this.panelTitleBar.Controls.Add(this.label4);
            this.panelTitleBar.Controls.Add(this.lblID);
            this.panelTitleBar.Controls.Add(this.btn_minimize);
            this.panelTitleBar.Controls.Add(this.btn_close);
            this.panelTitleBar.Controls.Add(this.btn_restore);
            this.panelTitleBar.Controls.Add(this.lblTitleChildForm);
            this.panelTitleBar.Controls.Add(this.iconCurrentChildFrom);
            this.panelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitleBar.Font = new System.Drawing.Font("굴림", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panelTitleBar.Location = new System.Drawing.Point(220, 0);
            this.panelTitleBar.Name = "panelTitleBar";
            this.panelTitleBar.Size = new System.Drawing.Size(1700, 75);
            this.panelTitleBar.TabIndex = 2;
            this.panelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelTitleBar_MouseDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(1471, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 16);
            this.label2.TabIndex = 10;
            this.label2.Text = "ID :";
            this.label2.Visible = false;
            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGrade.ForeColor = System.Drawing.Color.Lime;
            this.lblGrade.Location = new System.Drawing.Point(1499, 45);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(48, 16);
            this.lblGrade.TabIndex = 15;
            this.lblGrade.Text = "Guest";
            this.lblGrade.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label4.Location = new System.Drawing.Point(1446, 45);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 12;
            this.label4.Text = "AUTH :";
            this.label4.Visible = false;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblID.ForeColor = System.Drawing.Color.Lime;
            this.lblID.Location = new System.Drawing.Point(1499, 18);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(48, 16);
            this.lblID.TabIndex = 13;
            this.lblID.Text = "Guest";
            this.lblID.Visible = false;
            // 
            // btn_minimize
            // 
            this.btn_minimize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_minimize.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.btn_minimize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_minimize.ForeColor = System.Drawing.Color.DimGray;
            this.btn_minimize.IconChar = FontAwesome.Sharp.IconChar.WindowMinimize;
            this.btn_minimize.IconColor = System.Drawing.Color.DimGray;
            this.btn_minimize.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_minimize.IconSize = 20;
            this.btn_minimize.Location = new System.Drawing.Point(1603, 12);
            this.btn_minimize.Name = "btn_minimize";
            this.btn_minimize.Size = new System.Drawing.Size(20, 20);
            this.btn_minimize.TabIndex = 7;
            this.btn_minimize.TabStop = false;
            this.btn_minimize.Click += new System.EventHandler(this.btn_minimize_Click);
            // 
            // btn_close
            // 
            this.btn_close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.btn_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_close.ForeColor = System.Drawing.Color.DimGray;
            this.btn_close.IconChar = FontAwesome.Sharp.IconChar.X;
            this.btn_close.IconColor = System.Drawing.Color.DimGray;
            this.btn_close.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_close.IconSize = 20;
            this.btn_close.Location = new System.Drawing.Point(1676, 12);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(20, 20);
            this.btn_close.TabIndex = 6;
            this.btn_close.TabStop = false;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_restore
            // 
            this.btn_restore.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_restore.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.btn_restore.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_restore.ForeColor = System.Drawing.Color.DimGray;
            this.btn_restore.IconChar = FontAwesome.Sharp.IconChar.WindowRestore;
            this.btn_restore.IconColor = System.Drawing.Color.DimGray;
            this.btn_restore.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.btn_restore.IconSize = 20;
            this.btn_restore.Location = new System.Drawing.Point(1639, 12);
            this.btn_restore.Name = "btn_restore";
            this.btn_restore.Size = new System.Drawing.Size(20, 20);
            this.btn_restore.TabIndex = 5;
            this.btn_restore.TabStop = false;
            this.btn_restore.Click += new System.EventHandler(this.btn_restore_Click);
            // 
            // lblTitleChildForm
            // 
            this.lblTitleChildForm.AutoSize = true;
            this.lblTitleChildForm.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblTitleChildForm.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblTitleChildForm.Location = new System.Drawing.Point(73, 36);
            this.lblTitleChildForm.Name = "lblTitleChildForm";
            this.lblTitleChildForm.Size = new System.Drawing.Size(56, 16);
            this.lblTitleChildForm.TabIndex = 1;
            this.lblTitleChildForm.Text = " HOME";
            // 
            // iconCurrentChildFrom
            // 
            this.iconCurrentChildFrom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(25)))), ((int)(((byte)(62)))));
            this.iconCurrentChildFrom.ForeColor = System.Drawing.Color.MediumPurple;
            this.iconCurrentChildFrom.IconChar = FontAwesome.Sharp.IconChar.Home;
            this.iconCurrentChildFrom.IconColor = System.Drawing.Color.MediumPurple;
            this.iconCurrentChildFrom.IconFont = FontAwesome.Sharp.IconFont.Auto;
            this.iconCurrentChildFrom.IconSize = 35;
            this.iconCurrentChildFrom.Location = new System.Drawing.Point(31, 24);
            this.iconCurrentChildFrom.Name = "iconCurrentChildFrom";
            this.iconCurrentChildFrom.Size = new System.Drawing.Size(35, 35);
            this.iconCurrentChildFrom.TabIndex = 0;
            this.iconCurrentChildFrom.TabStop = false;
            // 
            // panelShow
            // 
            this.panelShow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(24)))), ((int)(((byte)(58)))));
            this.panelShow.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelShow.Location = new System.Drawing.Point(220, 75);
            this.panelShow.Name = "panelShow";
            this.panelShow.Size = new System.Drawing.Size(1700, 9);
            this.panelShow.TabIndex = 3;
            // 
            // panelDesktop
            // 
            this.panelDesktop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.panelDesktop.Controls.Add(this.pictureBox1);
            this.panelDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelDesktop.Location = new System.Drawing.Point(220, 84);
            this.panelDesktop.Name = "panelDesktop";
            this.panelDesktop.Size = new System.Drawing.Size(1700, 977);
            this.panelDesktop.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.Image = global::HIS.Properties.Resources.hynix;
            this.pictureBox1.Location = new System.Drawing.Point(759, 443);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 80);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(1920, 1061);
            this.Controls.Add(this.panelDesktop);
            this.Controls.Add(this.panelShow);
            this.Controls.Add(this.panelTitleBar);
            this.Controls.Add(this.panelMenu);
            this.Name = "MainForm";
            this.Text = "HIS - HMI Integrated System";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panelMenu.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelLogo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnHome)).EndInit();
            this.panelTitleBar.ResumeLayout(false);
            this.panelTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btn_minimize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_close)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btn_restore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconCurrentChildFrom)).EndInit();
            this.panelDesktop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private FontAwesome.Sharp.IconButton btnAlarm;
        private System.Windows.Forms.Panel panelLogo;
        private FontAwesome.Sharp.IconButton btnOperation;
        private System.Windows.Forms.PictureBox btnHome;
        private System.Windows.Forms.Panel panelTitleBar;
        private System.Windows.Forms.Label lblTitleChildForm;
        private FontAwesome.Sharp.IconPictureBox iconCurrentChildFrom;
        private System.Windows.Forms.Panel panelShow;
        private System.Windows.Forms.Panel panelDesktop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private FontAwesome.Sharp.IconPictureBox btn_minimize;
        private FontAwesome.Sharp.IconPictureBox btn_close;
        private FontAwesome.Sharp.IconPictureBox btn_restore;
        private FontAwesome.Sharp.IconButton btnMenu;
        private FontAwesome.Sharp.IconButton btnSms;
        private FontAwesome.Sharp.IconButton btnMultiTrend;
        private FontAwesome.Sharp.IconButton btn_logging;
        private System.Windows.Forms.Label lblGrade;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}

