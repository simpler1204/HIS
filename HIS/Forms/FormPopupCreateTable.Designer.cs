namespace HIS.Forms
{
    partial class FormPopupCreateTable
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPopupCreateTable));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions3 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions4 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions5 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.tableMenu = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.seq = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.table_desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SYSTEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LOGGING = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SAVING = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtUpdatedAt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmbSystem = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.grb_saving = new System.Windows.Forms.GroupBox();
            this.rdoC = new System.Windows.Forms.RadioButton();
            this.rdoB = new System.Windows.Forms.RadioButton();
            this.rdoA = new System.Windows.Forms.RadioButton();
            this.txtTableDesc = new System.Windows.Forms.TextBox();
            this.grb_logging = new System.Windows.Forms.GroupBox();
            this.rdo1Min = new System.Windows.Forms.RadioButton();
            this.rdo10Sec = new System.Windows.Forms.RadioButton();
            this.rdo3Sec = new System.Windows.Forms.RadioButton();
            this.rdo1Sec = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTableName = new System.Windows.Forms.TextBox();
            this.txtCreatedAt = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbSearchSystem = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbSavingPeriod = new System.Windows.Forms.ComboBox();
            this.cmbSearchLogging = new System.Windows.Forms.ComboBox();
            this.searchMenu = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtSearchDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.detailMenu = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.grb_saving.SuspendLayout();
            this.grb_logging.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableMenu
            // 
            this.tableMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tableMenu.ButtonInterval = 30;
            windowsUIButtonImageOptions1.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions1.SvgImage")));
            windowsUIButtonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions2.SvgImage")));
            this.tableMenu.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Create", true, windowsUIButtonImageOptions1, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Create Table", -1, true, null, true, false, true, null, -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Remove", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Remove Table", -1, true, null, true, false, true, null, -1, false)});
            this.tableMenu.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.tableMenu.Location = new System.Drawing.Point(3, 3);
            this.tableMenu.Name = "tableMenu";
            this.tableMenu.Size = new System.Drawing.Size(158, 68);
            this.tableMenu.TabIndex = 0;
            this.tableMenu.Text = "tableMenu";
            this.tableMenu.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.tableMenu_ButtonClick);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeight = 25;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.seq,
            this.table_name,
            this.table_desc,
            this.SYSTEM,
            this.LOGGING,
            this.SAVING});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 88);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(832, 464);
            this.dataGridView1.TabIndex = 1;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // seq
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.seq.DefaultCellStyle = dataGridViewCellStyle2;
            this.seq.HeaderText = "SEQ";
            this.seq.Name = "seq";
            this.seq.ReadOnly = true;
            this.seq.Width = 40;
            // 
            // table_name
            // 
            this.table_name.HeaderText = "TABLE NAME";
            this.table_name.Name = "table_name";
            this.table_name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.table_name.Width = 217;
            // 
            // table_desc
            // 
            this.table_desc.HeaderText = "TABLE DESC";
            this.table_desc.Name = "table_desc";
            this.table_desc.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.table_desc.Width = 220;
            // 
            // SYSTEM
            // 
            this.SYSTEM.HeaderText = "SYSTEM";
            this.SYSTEM.Name = "SYSTEM";
            // 
            // LOGGING
            // 
            this.LOGGING.HeaderText = "LOGGING";
            this.LOGGING.Name = "LOGGING";
            this.LOGGING.Width = 70;
            // 
            // SAVING
            // 
            this.SAVING.HeaderText = "SAVING";
            this.SAVING.Name = "SAVING";
            this.SAVING.Width = 50;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1210, 647);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1204, 561);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(857, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(344, 555);
            this.panel1.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtUpdatedAt);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.cmbSystem);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.grb_saving);
            this.groupBox4.Controls.Add(this.txtTableDesc);
            this.groupBox4.Controls.Add(this.grb_logging);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Controls.Add(this.txtTableName);
            this.groupBox4.Controls.Add(this.txtCreatedAt);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.ForeColor = System.Drawing.Color.Silver;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(344, 555);
            this.groupBox4.TabIndex = 22;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Detail";
            // 
            // txtUpdatedAt
            // 
            this.txtUpdatedAt.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtUpdatedAt.Enabled = false;
            this.txtUpdatedAt.Location = new System.Drawing.Point(124, 286);
            this.txtUpdatedAt.Name = "txtUpdatedAt";
            this.txtUpdatedAt.Size = new System.Drawing.Size(149, 21);
            this.txtUpdatedAt.TabIndex = 22;
            this.txtUpdatedAt.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Silver;
            this.label9.Location = new System.Drawing.Point(41, 288);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 21;
            this.label9.Text = "Updated At :  ";
            this.label9.Visible = false;
            // 
            // cmbSystem
            // 
            this.cmbSystem.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.cmbSystem.Enabled = false;
            this.cmbSystem.FormattingEnabled = true;
            this.cmbSystem.Location = new System.Drawing.Point(124, 89);
            this.cmbSystem.Name = "cmbSystem";
            this.cmbSystem.Size = new System.Drawing.Size(150, 20);
            this.cmbSystem.TabIndex = 20;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Silver;
            this.label8.Location = new System.Drawing.Point(59, 88);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(62, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "System : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(46, 263);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Created At :";
            this.label3.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(33, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Table Desc : ";
            // 
            // grb_saving
            // 
            this.grb_saving.Controls.Add(this.rdoC);
            this.grb_saving.Controls.Add(this.rdoB);
            this.grb_saving.Controls.Add(this.rdoA);
            this.grb_saving.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_saving.ForeColor = System.Drawing.Color.Silver;
            this.grb_saving.Location = new System.Drawing.Point(153, 120);
            this.grb_saving.Name = "grb_saving";
            this.grb_saving.Size = new System.Drawing.Size(121, 120);
            this.grb_saving.TabIndex = 16;
            this.grb_saving.TabStop = false;
            this.grb_saving.Text = "Saving Period";
            // 
            // rdoC
            // 
            this.rdoC.AutoSize = true;
            this.rdoC.Location = new System.Drawing.Point(21, 71);
            this.rdoC.Name = "rdoC";
            this.rdoC.Size = new System.Drawing.Size(99, 20);
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
            this.rdoB.Size = new System.Drawing.Size(85, 20);
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
            this.rdoA.Size = new System.Drawing.Size(95, 20);
            this.rdoA.TabIndex = 0;
            this.rdoA.TabStop = true;
            this.rdoA.Text = "A (1.5 Year)";
            this.rdoA.UseVisualStyleBackColor = true;
            // 
            // txtTableDesc
            // 
            this.txtTableDesc.Enabled = false;
            this.txtTableDesc.Location = new System.Drawing.Point(124, 61);
            this.txtTableDesc.Name = "txtTableDesc";
            this.txtTableDesc.Size = new System.Drawing.Size(150, 21);
            this.txtTableDesc.TabIndex = 2;
            // 
            // grb_logging
            // 
            this.grb_logging.Controls.Add(this.rdo1Min);
            this.grb_logging.Controls.Add(this.rdo10Sec);
            this.grb_logging.Controls.Add(this.rdo3Sec);
            this.grb_logging.Controls.Add(this.rdo1Sec);
            this.grb_logging.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grb_logging.ForeColor = System.Drawing.Color.Silver;
            this.grb_logging.Location = new System.Drawing.Point(36, 120);
            this.grb_logging.Name = "grb_logging";
            this.grb_logging.Size = new System.Drawing.Size(111, 120);
            this.grb_logging.TabIndex = 15;
            this.grb_logging.TabStop = false;
            this.grb_logging.Text = "Logging Cycle";
            // 
            // rdo1Min
            // 
            this.rdo1Min.AutoSize = true;
            this.rdo1Min.Location = new System.Drawing.Point(16, 93);
            this.rdo1Min.Name = "rdo1Min";
            this.rdo1Min.Size = new System.Drawing.Size(44, 20);
            this.rdo1Min.TabIndex = 3;
            this.rdo1Min.TabStop = true;
            this.rdo1Min.Text = "1M";
            this.rdo1Min.UseVisualStyleBackColor = true;
            // 
            // rdo10Sec
            // 
            this.rdo10Sec.AutoSize = true;
            this.rdo10Sec.Location = new System.Drawing.Point(16, 71);
            this.rdo10Sec.Name = "rdo10Sec";
            this.rdo10Sec.Size = new System.Drawing.Size(67, 20);
            this.rdo10Sec.TabIndex = 2;
            this.rdo10Sec.TabStop = true;
            this.rdo10Sec.Text = "10SEC";
            this.rdo10Sec.UseVisualStyleBackColor = true;
            // 
            // rdo3Sec
            // 
            this.rdo3Sec.AutoSize = true;
            this.rdo3Sec.Location = new System.Drawing.Point(16, 49);
            this.rdo3Sec.Name = "rdo3Sec";
            this.rdo3Sec.Size = new System.Drawing.Size(60, 20);
            this.rdo3Sec.TabIndex = 1;
            this.rdo3Sec.TabStop = true;
            this.rdo3Sec.Text = "3SEC";
            this.rdo3Sec.UseVisualStyleBackColor = true;
            // 
            // rdo1Sec
            // 
            this.rdo1Sec.AutoSize = true;
            this.rdo1Sec.Location = new System.Drawing.Point(16, 27);
            this.rdo1Sec.Name = "rdo1Sec";
            this.rdo1Sec.Size = new System.Drawing.Size(60, 20);
            this.rdo1Sec.TabIndex = 0;
            this.rdo1Sec.TabStop = true;
            this.rdo1Sec.Text = "1SEC";
            this.rdo1Sec.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(28, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Table Name : ";
            // 
            // txtTableName
            // 
            this.txtTableName.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtTableName.Enabled = false;
            this.txtTableName.Location = new System.Drawing.Point(124, 35);
            this.txtTableName.Name = "txtTableName";
            this.txtTableName.Size = new System.Drawing.Size(150, 21);
            this.txtTableName.TabIndex = 0;
            // 
            // txtCreatedAt
            // 
            this.txtCreatedAt.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.txtCreatedAt.Enabled = false;
            this.txtCreatedAt.Location = new System.Drawing.Point(124, 259);
            this.txtCreatedAt.Name = "txtCreatedAt";
            this.txtCreatedAt.Size = new System.Drawing.Size(149, 21);
            this.txtCreatedAt.TabIndex = 17;
            this.txtCreatedAt.Visible = false;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.dataGridView1, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.groupBox3, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 85F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(848, 555);
            this.tableLayoutPanel3.TabIndex = 3;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbSearchSystem);
            this.groupBox3.Controls.Add(this.cmbSavingPeriod);
            this.groupBox3.Controls.Add(this.cmbSearchLogging);
            this.groupBox3.Controls.Add(this.txtSearchDesc);
            this.groupBox3.Controls.Add(this.txtSearchName);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.searchMenu);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Silver;
            this.groupBox3.Location = new System.Drawing.Point(3, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(832, 79);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Table Search";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Silver;
            this.label10.Location = new System.Drawing.Point(403, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 15);
            this.label10.TabIndex = 27;
            this.label10.Text = "System :";
            // 
            // cmbSearchSystem
            // 
            this.cmbSearchSystem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchSystem.FormattingEnabled = true;
            this.cmbSearchSystem.Items.AddRange(new object[] {
            "C2_HVAC_S1",
            "C2_HVAC_S2",
            "C2_HVAC_S3",
            "C2_HVAC_S4",
            "ALL"});
            this.cmbSearchSystem.Location = new System.Drawing.Point(470, 19);
            this.cmbSearchSystem.Name = "cmbSearchSystem";
            this.cmbSearchSystem.Size = new System.Drawing.Size(130, 23);
            this.cmbSearchSystem.TabIndex = 26;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(239, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 16);
            this.label6.TabIndex = 24;
            this.label6.Text = "Saving :";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(237, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 15);
            this.label7.TabIndex = 25;
            this.label7.Text = "Logging :";
            // 
            // cmbSavingPeriod
            // 
            this.cmbSavingPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSavingPeriod.FormattingEnabled = true;
            this.cmbSavingPeriod.Items.AddRange(new object[] {
            "A",
            "B",
            "C",
            "ALL"});
            this.cmbSavingPeriod.Location = new System.Drawing.Point(302, 46);
            this.cmbSavingPeriod.Name = "cmbSavingPeriod";
            this.cmbSavingPeriod.Size = new System.Drawing.Size(84, 23);
            this.cmbSavingPeriod.TabIndex = 23;
            // 
            // cmbSearchLogging
            // 
            this.cmbSearchLogging.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSearchLogging.FormattingEnabled = true;
            this.cmbSearchLogging.Items.AddRange(new object[] {
            "1SEC",
            "3SEC",
            "10SEC",
            "1MIN",
            "ALL"});
            this.cmbSearchLogging.Location = new System.Drawing.Point(302, 19);
            this.cmbSearchLogging.Name = "cmbSearchLogging";
            this.cmbSearchLogging.Size = new System.Drawing.Size(84, 23);
            this.cmbSearchLogging.TabIndex = 22;
            // 
            // searchMenu
            // 
            windowsUIButtonImageOptions3.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions3.SvgImage")));
            this.searchMenu.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Search", true, windowsUIButtonImageOptions3, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1, false)});
            this.searchMenu.Dock = System.Windows.Forms.DockStyle.Right;
            this.searchMenu.Location = new System.Drawing.Point(754, 17);
            this.searchMenu.Name = "searchMenu";
            this.searchMenu.Size = new System.Drawing.Size(75, 59);
            this.searchMenu.TabIndex = 21;
            this.searchMenu.Text = "windowsUIButtonPanel1";
            this.searchMenu.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.searchMenu_ButtonClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(14, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 16);
            this.label5.TabIndex = 20;
            this.label5.Text = "Desc : ";
            // 
            // txtSearchDesc
            // 
            this.txtSearchDesc.Location = new System.Drawing.Point(68, 47);
            this.txtSearchDesc.Name = "txtSearchDesc";
            this.txtSearchDesc.Size = new System.Drawing.Size(155, 21);
            this.txtSearchDesc.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(13, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 15);
            this.label4.TabIndex = 20;
            this.label4.Text = "Name : ";
            // 
            // txtSearchName
            // 
            this.txtSearchName.Location = new System.Drawing.Point(68, 20);
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(155, 21);
            this.txtSearchName.TabIndex = 19;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 2;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Controls.Add(this.tableMenu, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.detailMenu, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 570);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1204, 74);
            this.tableLayoutPanel4.TabIndex = 4;
            // 
            // detailMenu
            // 
            this.detailMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.detailMenu.ButtonInterval = 30;
            windowsUIButtonImageOptions4.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions4.SvgImage")));
            windowsUIButtonImageOptions5.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions5.SvgImage")));
            this.detailMenu.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Save", true, windowsUIButtonImageOptions4, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Save", -1, true, null, true, false, true, null, -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Restart", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Restart", -1, true, null, true, false, true, null, -1, false)});
            this.detailMenu.Location = new System.Drawing.Point(1050, 3);
            this.detailMenu.Name = "detailMenu";
            this.detailMenu.Size = new System.Drawing.Size(151, 68);
            this.detailMenu.TabIndex = 1;
            this.detailMenu.Text = "windowsUIButtonPanel1";
            this.detailMenu.ButtonClick += new DevExpress.XtraBars.Docking2010.ButtonEventHandler(this.detailMenu_ButtonClick);
            // 
            // FormPopupCreateTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this.ClientSize = new System.Drawing.Size(1210, 647);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "FormPopupCreateTable";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CREATE TREND TABLE";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormCreateTable_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormCreateTable_FormClosed);
            this.Load += new System.EventHandler(this.FormCreateTable_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.grb_saving.ResumeLayout(false);
            this.grb_saving.PerformLayout();
            this.grb_logging.ResumeLayout(false);
            this.grb_logging.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel tableMenu;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTableName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTableDesc;
        private System.Windows.Forms.GroupBox grb_saving;
        private System.Windows.Forms.RadioButton rdoC;
        private System.Windows.Forms.RadioButton rdoB;
        private System.Windows.Forms.RadioButton rdoA;
        private System.Windows.Forms.GroupBox grb_logging;
        private System.Windows.Forms.RadioButton rdo1Min;
        private System.Windows.Forms.RadioButton rdo10Sec;
        private System.Windows.Forms.RadioButton rdo3Sec;
        private System.Windows.Forms.RadioButton rdo1Sec;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCreatedAt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel searchMenu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtSearchDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ComboBox cmbSavingPeriod;
        private System.Windows.Forms.ComboBox cmbSearchLogging;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel detailMenu;
        private System.Windows.Forms.TextBox txtUpdatedAt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbSystem;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbSearchSystem;
        private System.Windows.Forms.DataGridViewTextBoxColumn seq;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn table_desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn SYSTEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn LOGGING;
        private System.Windows.Forms.DataGridViewTextBoxColumn SAVING;
    }
}