namespace DVLDProject
{
    partial class frmReleaseDetainedLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReleaseDetainedLicense));
            this.label1 = new System.Windows.Forms.Label();
            this.ctrlFilterDriverLicenseInfo1 = new DVLDProject.ctrlFilterDriverLicenseInfo();
            this.ctrlReleaseDetainedLicense1 = new DVLDProject.ctrlReleaseDetainedLicense();
            this.lnkLbl2 = new System.Windows.Forms.LinkLabel();
            this.lnkLbl1 = new System.Windows.Forms.LinkLabel();
            this.btnRelease = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(306, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 31);
            this.label1.TabIndex = 2;
            this.label1.Text = "Release Detained License";
            // 
            // ctrlFilterDriverLicenseInfo1
            // 
            this.ctrlFilterDriverLicenseInfo1.Location = new System.Drawing.Point(1, 43);
            this.ctrlFilterDriverLicenseInfo1.Name = "ctrlFilterDriverLicenseInfo1";
            this.ctrlFilterDriverLicenseInfo1.Size = new System.Drawing.Size(898, 356);
            this.ctrlFilterDriverLicenseInfo1.TabIndex = 3;
            this.ctrlFilterDriverLicenseInfo1._OnClickToSearch += new System.Action<int, bool>(this.ctrlFilterDriverLicenseInfo1__OnClickToSearch);
            // 
            // ctrlReleaseDetainedLicense1
            // 
            this.ctrlReleaseDetainedLicense1.Location = new System.Drawing.Point(1, 405);
            this.ctrlReleaseDetainedLicense1.Name = "ctrlReleaseDetainedLicense1";
            this.ctrlReleaseDetainedLicense1.Size = new System.Drawing.Size(839, 165);
            this.ctrlReleaseDetainedLicense1.TabIndex = 4;
            this.ctrlReleaseDetainedLicense1.Load += new System.EventHandler(this.ctrlReleaseDetainedLicense1_Load);
            // 
            // lnkLbl2
            // 
            this.lnkLbl2.AutoSize = true;
            this.lnkLbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLbl2.Location = new System.Drawing.Point(229, 573);
            this.lnkLbl2.Name = "lnkLbl2";
            this.lnkLbl2.Size = new System.Drawing.Size(148, 20);
            this.lnkLbl2.TabIndex = 22;
            this.lnkLbl2.TabStop = true;
            this.lnkLbl2.Text = "Show Licenses Info";
            this.lnkLbl2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl2_LinkClicked);
            // 
            // lnkLbl1
            // 
            this.lnkLbl1.AutoSize = true;
            this.lnkLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLbl1.Location = new System.Drawing.Point(24, 573);
            this.lnkLbl1.Name = "lnkLbl1";
            this.lnkLbl1.Size = new System.Drawing.Size(169, 20);
            this.lnkLbl1.TabIndex = 21;
            this.lnkLbl1.TabStop = true;
            this.lnkLbl1.Text = "Show Licenses History";
            this.lnkLbl1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl1_LinkClicked);
            // 
            // btnRelease
            // 
            this.btnRelease.BackColor = System.Drawing.Color.White;
            this.btnRelease.Enabled = false;
            this.btnRelease.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelease.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRelease.Image = ((System.Drawing.Image)(resources.GetObject("btnRelease.Image")));
            this.btnRelease.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelease.Location = new System.Drawing.Point(759, 573);
            this.btnRelease.Name = "btnRelease";
            this.btnRelease.Size = new System.Drawing.Size(120, 36);
            this.btnRelease.TabIndex = 20;
            this.btnRelease.Text = "     Release";
            this.btnRelease.UseVisualStyleBackColor = false;
            this.btnRelease.Click += new System.EventHandler(this.btnRelease_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(622, 571);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 38);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "     Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmReleaseDetainedLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(891, 616);
            this.Controls.Add(this.lnkLbl2);
            this.Controls.Add(this.lnkLbl1);
            this.Controls.Add(this.btnRelease);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlReleaseDetainedLicense1);
            this.Controls.Add(this.ctrlFilterDriverLicenseInfo1);
            this.Controls.Add(this.label1);
            this.Name = "frmReleaseDetainedLicense";
            this.Text = "frmReleaseDetainedLicense";
            this.Load += new System.EventHandler(this.frmReleaseDetainedLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ctrlFilterDriverLicenseInfo ctrlFilterDriverLicenseInfo1;
        private ctrlReleaseDetainedLicense ctrlReleaseDetainedLicense1;
        private System.Windows.Forms.LinkLabel lnkLbl2;
        private System.Windows.Forms.LinkLabel lnkLbl1;
        private System.Windows.Forms.Button btnRelease;
        private System.Windows.Forms.Button btnClose;
    }
}