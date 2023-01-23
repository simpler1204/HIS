namespace HIS.Popup
{
    partial class PopUpCreateOneTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUpCreateOneTable));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.menu = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdo1Min = new System.Windows.Forms.RadioButton();
            this.rdo10Sec = new System.Windows.Forms.RadioButton();
            this.rdo3Sec = new System.Windows.Forms.RadioButton();
            this.rdo1Sec = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rdoC = new System.Windows.Forms.RadioButton();
            this.rdoB = new System.Windows.Forms.RadioButton();
            this.rdoA = new System.Windows.Forms.RadioButton();
            this.cmbSystem = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmbCount = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rdoCMulti = new System.Windows.Forms.RadioButton();
            this.rdoBMulti = new System.Windows.Forms.RadioButton();
            this.rdoAMulti = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rdo1MinMulti = new System.Windows.Forms.RadioButton();
            this.rdo10SecMulti = new System.Windows.Forms.RadioButton();
            this.rdo3SecMulti = new System.Windows.Forms.RadioButton();
            this.rdo1SecMulti = new System.Windows.Forms.RadioButton();
            this.menuMulti = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.cmbSystemMulti = new System.Windows.Forms.ComboBox();
            this.txtDexcMulti = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.ButtonInterval = 30;
            windowsUIButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions1.SvgImage")));
            windowsUIButtonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions2.SvgImage")));
            this.menu.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("New", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "New Table", -1, true, null, true, false, true, null, -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Save", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Save", -1, true, null, true, false, true, null, -1, false)});
            this.menu.Location = new System.Drawing.Point(119, 250);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(142, 84);
            this.menu.TabIndex = 4;
            this.menu.Text = "windowsUIButtonPanel1";
            this.menu.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.menu_ButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Location = new System.Drawing.Point(30, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Table name :";
            // 
            // txtTableName
            // 
            this.txtTableName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTableName.Enabled = false;
            this.txtTableName.Location = new System.Drawing.Point(121, 42);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(143, 21);
            this.txtTableName.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label2.Location = new System.Drawing.Point(34, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "Table desc :";
            // 
            // txtDesc
            // 
            this.txtDesc.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtDesc.Location = new System.Drawing.Point(121, 67);
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(143, 21);
            this.txtDesc.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label8.Location = new System.Drawing.Point(21, 44);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 12;
            this.label8.Text = "*";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdo1Min);
            this.groupBox1.Controls.Add(this.rdo10Sec);
            this.groupBox1.Controls.Add(this.rdo3Sec);
            this.groupBox1.Controls.Add(this.rdo1Sec);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(36, 124);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(99, 120);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Logging Cycle";
            // 
            // rdo1Min
            // 
            this.rdo1Min.AutoSize = true;
            this.rdo1Min.Location = new System.Drawing.Point(16, 93);
            this.rdo1Min.Name = "rdo1Min";
            this.rdo1Min.Size = new System.Drawing.Size(46, 19);
            this.rdo1Min.TabIndex = 3;
            this.rdo1Min.TabStop = true;
            this.rdo1Min.Text = "1 M";
            this.rdo1Min.UseVisualStyleBackColor = true;
            // 
            // rdo10Sec
            // 
            this.rdo10Sec.AutoSize = true;
            this.rdo10Sec.Location = new System.Drawing.Point(16, 71);
            this.rdo10Sec.Name = "rdo10Sec";
            this.rdo10Sec.Size = new System.Drawing.Size(66, 19);
            this.rdo10Sec.TabIndex = 2;
            this.rdo10Sec.TabStop = true;
            this.rdo10Sec.Text = "10 SEC";
            this.rdo10Sec.UseVisualStyleBackColor = true;
            // 
            // rdo3Sec
            // 
            this.rdo3Sec.AutoSize = true;
            this.rdo3Sec.Location = new System.Drawing.Point(16, 49);
            this.rdo3Sec.Name = "rdo3Sec";
            this.rdo3Sec.Size = new System.Drawing.Size(59, 19);
            this.rdo3Sec.TabIndex = 1;
            this.rdo3Sec.TabStop = true;
            this.rdo3Sec.Text = "3 SEC";
            this.rdo3Sec.UseVisualStyleBackColor = true;
            // 
            // rdo1Sec
            // 
            this.rdo1Sec.AutoSize = true;
            this.rdo1Sec.Location = new System.Drawing.Point(16, 27);
            this.rdo1Sec.Name = "rdo1Sec";
            this.rdo1Sec.Size = new System.Drawing.Size(59, 19);
            this.rdo1Sec.TabIndex = 0;
            this.rdo1Sec.TabStop = true;
            this.rdo1Sec.Text = "1 SEC";
            this.rdo1Sec.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rdoC);
            this.groupBox2.Controls.Add(this.rdoB);
            this.groupBox2.Controls.Add(this.rdoA);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(141, 124);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(120, 120);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Saving Period";
            // 
            // rdoC
            // 
            this.rdoC.AutoSize = true;
            this.rdoC.Location = new System.Drawing.Point(21, 71);
            this.rdoC.Name = "rdoC";
            this.rdoC.Size = new System.Drawing.Size(95, 19);
            this.rdoC.TabIndex = 2;
            this.rdoC.TabStop = true;
            this.rdoC.Text = "C (9 Months)";
            this.rdoC.UseVisualStyleBackColor = true;
            // 
            // rdoB
            // 
            this.rdoB.AutoSize = true;
            this.rdoB.Location = new System.Drawing.Point(21, 49);
            this.rdoB.Name = "rdoB";
            this.rdoB.Size = new System.Drawing.Size(79, 19);
            this.rdoB.TabIndex = 1;
            this.rdoB.TabStop = true;
            this.rdoB.Text = "B (1 Year)";
            this.rdoB.UseVisualStyleBackColor = true;
            // 
            // rdoA
            // 
            this.rdoA.AutoSize = true;
            this.rdoA.Location = new System.Drawing.Point(21, 27);
            this.rdoA.Name = "rdoA";
            this.rdoA.Size = new System.Drawing.Size(88, 19);
            this.rdoA.TabIndex = 0;
            this.rdoA.TabStop = true;
            this.rdoA.Text = "A (1.5 Year)";
            this.rdoA.UseVisualStyleBackColor = true;
            // 
            // cmbSystem
            // 
            this.cmbSystem.FormattingEnabled = true;
            this.cmbSystem.Items.AddRange(new object[] {
            "C2_HVAC_S1",
            "C2_HVAC_S2",
            "C2_HVAC_S3",
            "C2_HVAC_S4"});
            this.cmbSystem.Location = new System.Drawing.Point(121, 91);
            this.cmbSystem.Name = "cmbSystem";
            this.cmbSystem.Size = new System.Drawing.Size(143, 20);
            this.cmbSystem.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.Location = new System.Drawing.Point(55, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "System :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(46, 93);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(11, 12);
            this.label4.TabIndex = 17;
            this.label4.Text = "*";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtTableName);
            this.groupBox3.Controls.Add(this.cmbSystem);
            this.groupBox3.Controls.Add(this.txtDesc);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.menu);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.groupBox2);
            this.groupBox3.Controls.Add(this.groupBox1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox3.Location = new System.Drawing.Point(12, 23);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(298, 368);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "One Create";
            // 
            // cmbCount
            // 
            this.cmbCount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCount.FormattingEnabled = true;
            this.cmbCount.Items.AddRange(new object[] {
            "3",
            "5",
            "10",
            "15",
            "20"});
            this.cmbCount.Location = new System.Drawing.Point(116, 42);
            this.cmbCount.Name = "cmbCount";
            this.cmbCount.Size = new System.Drawing.Size(50, 20);
            this.cmbCount.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label5.Location = new System.Drawing.Point(22, 46);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(85, 12);
            this.label5.TabIndex = 20;
            this.label5.Text = "Create count :";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rdoCMulti);
            this.groupBox4.Controls.Add(this.rdoBMulti);
            this.groupBox4.Controls.Add(this.rdoAMulti);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(134, 124);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(120, 120);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Saving Period";
            // 
            // rdoCMulti
            // 
            this.rdoCMulti.AutoSize = true;
            this.rdoCMulti.Location = new System.Drawing.Point(21, 71);
            this.rdoCMulti.Name = "rdoCMulti";
            this.rdoCMulti.Size = new System.Drawing.Size(95, 19);
            this.rdoCMulti.TabIndex = 2;
            this.rdoCMulti.TabStop = true;
            this.rdoCMulti.Text = "C (9 Months)";
            this.rdoCMulti.UseVisualStyleBackColor = true;
            // 
            // rdoBMulti
            // 
            this.rdoBMulti.AutoSize = true;
            this.rdoBMulti.Location = new System.Drawing.Point(21, 49);
            this.rdoBMulti.Name = "rdoBMulti";
            this.rdoBMulti.Size = new System.Drawing.Size(79, 19);
            this.rdoBMulti.TabIndex = 1;
            this.rdoBMulti.TabStop = true;
            this.rdoBMulti.Text = "B (1 Year)";
            this.rdoBMulti.UseVisualStyleBackColor = true;
            // 
            // rdoAMulti
            // 
            this.rdoAMulti.AutoSize = true;
            this.rdoAMulti.Location = new System.Drawing.Point(21, 27);
            this.rdoAMulti.Name = "rdoAMulti";
            this.rdoAMulti.Size = new System.Drawing.Size(88, 19);
            this.rdoAMulti.TabIndex = 0;
            this.rdoAMulti.TabStop = true;
            this.rdoAMulti.Text = "A (1.5 Year)";
            this.rdoAMulti.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rdo1MinMulti);
            this.groupBox5.Controls.Add(this.rdo10SecMulti);
            this.groupBox5.Controls.Add(this.rdo3SecMulti);
            this.groupBox5.Controls.Add(this.rdo1SecMulti);
            this.groupBox5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox5.ForeColor = System.Drawing.Color.White;
            this.groupBox5.Location = new System.Drawing.Point(29, 124);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(99, 120);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Logging Cycle";
            // 
            // rdo1MinMulti
            // 
            this.rdo1MinMulti.AutoSize = true;
            this.rdo1MinMulti.Location = new System.Drawing.Point(16, 93);
            this.rdo1MinMulti.Name = "rdo1MinMulti";
            this.rdo1MinMulti.Size = new System.Drawing.Size(46, 19);
            this.rdo1MinMulti.TabIndex = 3;
            this.rdo1MinMulti.TabStop = true;
            this.rdo1MinMulti.Text = "1 M";
            this.rdo1MinMulti.UseVisualStyleBackColor = true;
            // 
            // rdo10SecMulti
            // 
            this.rdo10SecMulti.AutoSize = true;
            this.rdo10SecMulti.Location = new System.Drawing.Point(16, 71);
            this.rdo10SecMulti.Name = "rdo10SecMulti";
            this.rdo10SecMulti.Size = new System.Drawing.Size(66, 19);
            this.rdo10SecMulti.TabIndex = 2;
            this.rdo10SecMulti.TabStop = true;
            this.rdo10SecMulti.Text = "10 SEC";
            this.rdo10SecMulti.UseVisualStyleBackColor = true;
            // 
            // rdo3SecMulti
            // 
            this.rdo3SecMulti.AutoSize = true;
            this.rdo3SecMulti.Location = new System.Drawing.Point(16, 49);
            this.rdo3SecMulti.Name = "rdo3SecMulti";
            this.rdo3SecMulti.Size = new System.Drawing.Size(59, 19);
            this.rdo3SecMulti.TabIndex = 1;
            this.rdo3SecMulti.TabStop = true;
            this.rdo3SecMulti.Text = "3 SEC";
            this.rdo3SecMulti.UseVisualStyleBackColor = true;
            // 
            // rdo1SecMulti
            // 
            this.rdo1SecMulti.AutoSize = true;
            this.rdo1SecMulti.Location = new System.Drawing.Point(16, 27);
            this.rdo1SecMulti.Name = "rdo1SecMulti";
            this.rdo1SecMulti.Size = new System.Drawing.Size(59, 19);
            this.rdo1SecMulti.TabIndex = 0;
            this.rdo1SecMulti.TabStop = true;
            this.rdo1SecMulti.Text = "1 SEC";
            this.rdo1SecMulti.UseVisualStyleBackColor = true;
            // 
            // menuMulti
            // 
            windowsUIButtonImageOptions3.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions3.SvgImage")));
            this.menuMulti.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Multi New", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Multi New", -1, true, null, true, false, true, null, -1, false)});
            this.menuMulti.Location = new System.Drawing.Point(160, 250);
            this.menuMulti.Name = "menuMulti";
            this.menuMulti.Size = new System.Drawing.Size(83, 84);
            this.menuMulti.TabIndex = 23;
            this.menuMulti.Text = "windowsUIButtonPanel1";
            this.menuMulti.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.menuMulti_ButtonClick);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.cmbSystemMulti);
            this.groupBox6.Controls.Add(this.txtDexcMulti);
            this.groupBox6.Controls.Add(this.cmbCount);
            this.groupBox6.Controls.Add(this.label10);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.label6);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.label5);
            this.groupBox6.Controls.Add(this.menuMulti);
            this.groupBox6.Controls.Add(this.groupBox4);
            this.groupBox6.Controls.Add(this.groupBox5);
            this.groupBox6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.groupBox6.Location = new System.Drawing.Point(332, 23);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(298, 368);
            this.groupBox6.TabIndex = 24;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Multi Create";
            // 
            // cmbSystemMulti
            // 
            this.cmbSystemMulti.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSystemMulti.FormattingEnabled = true;
            this.cmbSystemMulti.Items.AddRange(new object[] {
            "C2_HVAC_S1",
            "C2_HVAC_S2",
            "C2_HVAC_S3",
            "C2_HVAC_S4"});
            this.cmbSystemMulti.Location = new System.Drawing.Point(115, 94);
            this.cmbSystemMulti.Name = "cmbSystemMulti";
            this.cmbSystemMulti.Size = new System.Drawing.Size(143, 20);
            this.cmbSystemMulti.TabIndex = 18;
            // 
            // txtDexcMulti
            // 
            this.txtDexcMulti.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.txtDexcMulti.Location = new System.Drawing.Point(115, 67);
            this.txtDexcMulti.Name = "txtDexcMulti";
            this.txtDexcMulti.Size = new System.Drawing.Size(143, 21);
            this.txtDexcMulti.TabIndex = 18;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label10.Location = new System.Drawing.Point(13, 45);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(11, 12);
            this.label10.TabIndex = 18;
            this.label10.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(40, 96);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 20;
            this.label7.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label6.Location = new System.Drawing.Point(28, 71);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "Table desc :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.label9.Location = new System.Drawing.Point(49, 97);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 12);
            this.label9.TabIndex = 19;
            this.label9.Text = "System :";
            // 
            // PopUpCreateOneTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(654, 442);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "PopUpCreateOneTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = " CREATE TREND TABLE";
            this.Load += new System.EventHandler(this.PopUpCreateOneTable_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel menu;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdo3Sec;
        private System.Windows.Forms.RadioButton rdo1Sec;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton rdoB;
        private System.Windows.Forms.RadioButton rdoA;
        private System.Windows.Forms.RadioButton rdo1Min;
        private System.Windows.Forms.RadioButton rdo10Sec;
        private System.Windows.Forms.RadioButton rdoC;
        private System.Windows.Forms.ComboBox cmbSystem;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cmbCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton rdoCMulti;
        private System.Windows.Forms.RadioButton rdoBMulti;
        private System.Windows.Forms.RadioButton rdoAMulti;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton rdo1MinMulti;
        private System.Windows.Forms.RadioButton rdo10SecMulti;
        private System.Windows.Forms.RadioButton rdo3SecMulti;
        private System.Windows.Forms.RadioButton rdo1SecMulti;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel menuMulti;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDexcMulti;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbSystemMulti;
    }
}