namespace HIS.Forms
{
    partial class FormAlarmMain
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
            DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions windowsUIButtonImageOptions2 = new DevExpress.XtraBars.Docking2010.WindowsUIButtonImageOptions();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlarmMain));
            this.menuPanel = new DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel();
            this.SuspendLayout();
            // 
            // menuPanel
            // 
            this.menuPanel.ButtonInterval = 30;
            windowsUIButtonImageOptions2.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("windowsUIButtonImageOptions2.SvgImage")));
            this.menuPanel.Buttons.AddRange(new DevExpress.XtraEditors.ButtonPanel.IBaseButton[] {
            new DevExpress.XtraBars.Docking2010.WindowsUIButton("History", true, windowsUIButtonImageOptions2, DevExpress.XtraBars.Docking2010.ButtonStyle.PushButton, "Alarm History", -1, true, null, true, false, true, null, -1, false)});
            this.menuPanel.ContentAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.menuPanel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuPanel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuPanel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.menuPanel.Location = new System.Drawing.Point(0, 0);
            this.menuPanel.Name = "menuPanel";
            this.menuPanel.Padding = new System.Windows.Forms.Padding(20, 10, 10, 10);
            this.menuPanel.Size = new System.Drawing.Size(1144, 97);
            this.menuPanel.TabIndex = 2;
            this.menuPanel.Text = "windowsUIButtonPanel1";
            // 
            // FormAlarmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(33)))), ((int)(((byte)(74)))));
            this.ClientSize = new System.Drawing.Size(1144, 568);
            this.Controls.Add(this.menuPanel);
            this.Name = "FormAlarmMain";
            this.Text = "ALARM MANAGER";
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.Docking2010.WindowsUIButtonPanel menuPanel;
    }
}