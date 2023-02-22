namespace HIS.PopUp
{
    partial class PopUpSearchTrendGroup
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PopUpSearchTrendGroup));
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions6 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnTrend = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.txtGroup = new System.Windows.Forms.TextBox();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDistinct = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(4, 114);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(453, 239);
            this.dataGridView1.TabIndex = 7;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DoubleClick += new System.EventHandler(this.dataGridView1_DoubleClick);
            // 
            // btnTrend
            // 
            this.btnTrend.ButtonInterval = 15;
            windowsUIButtonImageOptions5.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions5.SvgImage")));
            windowsUIButtonImageOptions6.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions6.SvgImage")));
            this.btnTrend.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Search", true, windowsUIButtonImageOptions5, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1, false),
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("Show Trend", true, windowsUIButtonImageOptions6, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "", -1, true, null, true, false, true, null, -1, false)});
            this.btnTrend.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.btnTrend.Location = new System.Drawing.Point(289, 41);
            this.btnTrend.Name = "btnTrend";
            this.btnTrend.Size = new System.Drawing.Size(154, 65);
            this.btnTrend.TabIndex = 8;
            this.btnTrend.Text = "windowsUIButtonPanel1";
            // 
            // txtGroup
            // 
            this.txtGroup.Location = new System.Drawing.Point(79, 78);
            this.txtGroup.Name = "txtGroup";
            this.txtGroup.Size = new System.Drawing.Size(146, 21);
            this.txtGroup.TabIndex = 11;
            // 
            // txtPart
            // 
            this.txtPart.Location = new System.Drawing.Point(79, 51);
            this.txtPart.Name = "txtPart";
            this.txtPart.Size = new System.Drawing.Size(146, 21);
            this.txtPart.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(9, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "GROUP :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(19, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "PART :";
            // 
            // lblDistinct
            // 
            this.lblDistinct.AutoSize = true;
            this.lblDistinct.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDistinct.ForeColor = System.Drawing.Color.Aqua;
            this.lblDistinct.Location = new System.Drawing.Point(194, 9);
            this.lblDistinct.Name = "lblDistinct";
            this.lblDistinct.Size = new System.Drawing.Size(59, 19);
            this.lblDistinct.TabIndex = 13;
            this.lblDistinct.Text = "label1";
            this.lblDistinct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PopUpSearchTrendGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(460, 360);
            this.Controls.Add(this.lblDistinct);
            this.Controls.Add(this.txtGroup);
            this.Controls.Add(this.txtPart);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnTrend);
            this.Controls.Add(this.dataGridView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopUpSearchTrendGroup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Search Trend Group";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel btnTrend;
        private System.Windows.Forms.TextBox txtGroup;
        private System.Windows.Forms.TextBox txtPart;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDistinct;
    }
}