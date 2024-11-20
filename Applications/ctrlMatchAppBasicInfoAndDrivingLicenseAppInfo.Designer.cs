namespace DVLDProject
{
    partial class ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.applicationBasicInfo1 = new DVLDProject.ApplicationBasicInfo();
            this.drivingLicenseApplicationInfo1 = new DVLDProject.DrivingLicenseApplicationInfo();
            this.SuspendLayout();
            // 
            // applicationBasicInfo1
            // 
            this.applicationBasicInfo1.Location = new System.Drawing.Point(5, 105);
            this.applicationBasicInfo1.Name = "applicationBasicInfo1";
            this.applicationBasicInfo1.Size = new System.Drawing.Size(787, 192);
            this.applicationBasicInfo1.TabIndex = 0;
            // 
            // drivingLicenseApplicationInfo1
            // 
            this.drivingLicenseApplicationInfo1.Location = new System.Drawing.Point(4, 3);
            this.drivingLicenseApplicationInfo1.Name = "drivingLicenseApplicationInfo1";
            this.drivingLicenseApplicationInfo1.Size = new System.Drawing.Size(788, 102);
            this.drivingLicenseApplicationInfo1.TabIndex = 1;
            // 
            // ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drivingLicenseApplicationInfo1);
            this.Controls.Add(this.applicationBasicInfo1);
            this.Name = "ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo";
            this.Size = new System.Drawing.Size(793, 300);
            this.Load += new System.EventHandler(this.ctrlMatchAppBasicInfoAndDrivingLicenseAppInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ApplicationBasicInfo applicationBasicInfo1;
        private DrivingLicenseApplicationInfo drivingLicenseApplicationInfo1;
    }
}
