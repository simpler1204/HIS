namespace HIS.Forms
{
    partial class FormSmsHIST
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSmsHIST));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dgHist = new System.Windows.Forms.DataGridView();
            this.btnMenu = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.endDt = new System.Windows.Forms.DateTimePicker();
            this.startDt = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbSuccess = new System.Windows.Forms.ComboBox();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtMsg = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbDsnt = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panelProgress = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTotalCount = new System.Windows.Forms.Label();
            this.lblNowCount = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dgHist)).BeginInit();
            this.panel1.SuspendLayout();
            this.panelProgress.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgHist
            // 
            this.dgHist.AllowUserToAddRows = false;
            this.dgHist.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dgHist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgHist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgHist.Location = new System.Drawing.Point(0, 80);
            this.dgHist.Name = "dgHist";
            this.dgHist.RowHeadersVisible = false;
            this.dgHist.RowTemplate.Height = 23;
            this.dgHist.Size = new System.Drawing.Size(1214, 571);
            this.dgHist.TabIndex = 0;
            // 
            // btnMenu
            // 
            this.btnMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMenu.ButtonInterval = 25;
            windowsUIButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions1.SvgImage")));
            windowsUIButtonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions2.SvgImage")));
            this.btnMenu.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Search", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Export", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1, false)});
            this.btnMenu.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenu.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnMenu.Location = new System.Drawing.Point(1062, 3);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(143, 75);
            this.btnMenu.TabIndex = 2;
            this.btnMenu.Text = "windowsUIButtonPanel1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label2.Location = new System.Drawing.Point(17, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 16);
            this.label2.TabIndex = 59;
            this.label2.Text = "End :";
            // 
            // endDt
            // 
            this.endDt.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.endDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endDt.Location = new System.Drawing.Point(64, 42);
            this.endDt.Name = "endDt";
            this.endDt.ShowUpDown = true;
            this.endDt.Size = new System.Drawing.Size(146, 21);
            this.endDt.TabIndex = 57;
            // 
            // startDt
            // 
            this.startDt.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.startDt.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.startDt.Location = new System.Drawing.Point(64, 13);
            this.startDt.Name = "startDt";
            this.startDt.ShowUpDown = true;
            this.startDt.Size = new System.Drawing.Size(146, 21);
            this.startDt.TabIndex = 56;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 16);
            this.label1.TabIndex = 58;
            this.label1.Text = "Start :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label4.Location = new System.Drawing.Point(261, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 16);
            this.label4.TabIndex = 60;
            this.label4.Text = "User :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label3.Location = new System.Drawing.Point(238, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 67;
            this.label3.Text = "Success :";
            // 
            // cmbSuccess
            // 
            this.cmbSuccess.AllowDrop = true;
            this.cmbSuccess.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.cmbSuccess.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSuccess.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbSuccess.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSuccess.ForeColor = System.Drawing.Color.Black;
            this.cmbSuccess.FormattingEnabled = true;
            this.cmbSuccess.Items.AddRange(new object[] {
            "ALL",
            "Y",
            "N"});
            this.cmbSuccess.Location = new System.Drawing.Point(310, 42);
            this.cmbSuccess.Name = "cmbSuccess";
            this.cmbSuccess.Size = new System.Drawing.Size(101, 24);
            this.cmbSuccess.TabIndex = 66;
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUser.ForeColor = System.Drawing.Color.Black;
            this.txtUser.Location = new System.Drawing.Point(310, 12);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(101, 22);
            this.txtUser.TabIndex = 68;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbDsnt);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btnMenu);
            this.panel1.Controls.Add(this.txtUser);
            this.panel1.Controls.Add(this.cmbSuccess);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtMsg);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.startDt);
            this.panel1.Controls.Add(this.endDt);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1214, 80);
            this.panel1.TabIndex = 1;
            // 
            // txtMsg
            // 
            this.txtMsg.BackColor = System.Drawing.Color.White;
            this.txtMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMsg.ForeColor = System.Drawing.Color.Black;
            this.txtMsg.Location = new System.Drawing.Point(521, 41);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(201, 22);
            this.txtMsg.TabIndex = 65;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label7.Location = new System.Drawing.Point(474, 44);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 16);
            this.label7.TabIndex = 63;
            this.label7.Text = "Msg :";
            // 
            // cmbDsnt
            // 
            this.cmbDsnt.AllowDrop = true;
            this.cmbDsnt.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.cmbDsnt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDsnt.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cmbDsnt.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbDsnt.ForeColor = System.Drawing.Color.Black;
            this.cmbDsnt.FormattingEnabled = true;
            this.cmbDsnt.Items.AddRange(new object[] {
            "ALL",
            "SMS",
            "WECHAT"});
            this.cmbDsnt.Location = new System.Drawing.Point(521, 11);
            this.cmbDsnt.Name = "cmbDsnt";
            this.cmbDsnt.Size = new System.Drawing.Size(120, 24);
            this.cmbDsnt.TabIndex = 69;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.label5.Location = new System.Drawing.Point(464, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 16);
            this.label5.TabIndex = 70;
            this.label5.Text = "Distict :";
            // 
            // panelProgress
            // 
            this.panelProgress.Controls.Add(this.label6);
            this.panelProgress.Controls.Add(this.lblTotalCount);
            this.panelProgress.Controls.Add(this.lblNowCount);
            this.panelProgress.Controls.Add(this.progressBar1);
            this.panelProgress.Location = new System.Drawing.Point(449, 294);
            this.panelProgress.Name = "panelProgress";
            this.panelProgress.Size = new System.Drawing.Size(335, 72);
            this.panelProgress.TabIndex = 71;
            this.panelProgress.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Location = new System.Drawing.Point(168, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 16);
            this.label6.TabIndex = 3;
            this.label6.Text = "/";
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
            // FormSmsHIST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1214, 651);
            this.Controls.Add(this.panelProgress);
            this.Controls.Add(this.dgHist);
            this.Controls.Add(this.panel1);
            this.Name = "FormSmsHIST";
            this.Text = "SMS";
            ((System.ComponentModel.ISupportInitialize)(this.dgHist)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panelProgress.ResumeLayout(false);
            this.panelProgress.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgHist;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnMenu;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker endDt;
        private System.Windows.Forms.DateTimePicker startDt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbSuccess;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtMsg;
        private System.Windows.Forms.ComboBox cmbDsnt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panelProgress;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblTotalCount;
        private System.Windows.Forms.Label lblNowCount;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}