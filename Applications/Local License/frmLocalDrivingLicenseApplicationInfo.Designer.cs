namespace DVLDProject.Applications.Local_License
{
    partial class frmLocalDrivingLicenseApplicationInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLocalDrivingLicenseApplicationInfo));
            this.button2 = new System.Windows.Forms.Button();
            this.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1 = new DVLDProject.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(688, 307);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 38);
            this.button2.TabIndex = 11;
            this.button2.Text = "     Close";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1
            // 
            this.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.Location = new System.Drawing.Point(3, 1);
            this.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.Name = "ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1";
            this.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.Size = new System.Drawing.Size(794, 300);
            this.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.TabIndex = 3;
            this.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1.Load += new System.EventHandler(this.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1_Load);
            // 
            // frmLocalDrivingLicenseApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 351);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmLocalDrivingLicenseApplicationInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Local Driving License Application Info";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseApplicationInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo1;
        private System.Windows.Forms.Button button2;
    }
}