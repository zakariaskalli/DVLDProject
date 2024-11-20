namespace DVLDProject
{
    partial class frmRenewLocalDrivingLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRenewLocalDrivingLicense));
            this.label1 = new System.Windows.Forms.Label();
            this.lnkLbl2 = new System.Windows.Forms.LinkLabel();
            this.lnkLbl1 = new System.Windows.Forms.LinkLabel();
            this.btnRenew = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlApplicationNewLicenseInfo1 = new DVLDProject.ctrlApplicationNewLicenseInfo();
            this.ctrlFilterDriverLicenseInfo1 = new DVLDProject.ctrlFilterDriverLicenseInfo();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Brown;
            this.label1.Location = new System.Drawing.Point(299, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 31);
            this.label1.TabIndex = 3;
            this.label1.Text = "Renew License Application";
            // 
            // lnkLbl2
            // 
            this.lnkLbl2.AutoSize = true;
            this.lnkLbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLbl2.Location = new System.Drawing.Point(220, 649);
            this.lnkLbl2.Name = "lnkLbl2";
            this.lnkLbl2.Size = new System.Drawing.Size(183, 20);
            this.lnkLbl2.TabIndex = 18;
            this.lnkLbl2.TabStop = true;
            this.lnkLbl2.Text = "Show New Licenses Info";
            this.lnkLbl2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl2_LinkClicked);
            // 
            // lnkLbl1
            // 
            this.lnkLbl1.AutoSize = true;
            this.lnkLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLbl1.Location = new System.Drawing.Point(12, 649);
            this.lnkLbl1.Name = "lnkLbl1";
            this.lnkLbl1.Size = new System.Drawing.Size(169, 20);
            this.lnkLbl1.TabIndex = 17;
            this.lnkLbl1.TabStop = true;
            this.lnkLbl1.Text = "Show Licenses History";
            this.lnkLbl1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl1_LinkClicked);
            // 
            // btnRenew
            // 
            this.btnRenew.BackColor = System.Drawing.Color.White;
            this.btnRenew.Enabled = false;
            this.btnRenew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRenew.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRenew.Image = ((System.Drawing.Image)(resources.GetObject("btnRenew.Image")));
            this.btnRenew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRenew.Location = new System.Drawing.Point(781, 641);
            this.btnRenew.Name = "btnRenew";
            this.btnRenew.Size = new System.Drawing.Size(118, 38);
            this.btnRenew.TabIndex = 16;
            this.btnRenew.Text = "     Renew";
            this.btnRenew.UseVisualStyleBackColor = false;
            this.btnRenew.Click += new System.EventHandler(this.btnRenew_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(648, 639);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 38);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "     Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlApplicationNewLicenseInfo1
            // 
            this.ctrlApplicationNewLicenseInfo1.Location = new System.Drawing.Point(12, 378);
            this.ctrlApplicationNewLicenseInfo1.Name = "ctrlApplicationNewLicenseInfo1";
            this.ctrlApplicationNewLicenseInfo1.Size = new System.Drawing.Size(807, 255);
            this.ctrlApplicationNewLicenseInfo1.TabIndex = 5;
            // 
            // ctrlFilterDriverLicenseInfo1
            // 
            this.ctrlFilterDriverLicenseInfo1.Location = new System.Drawing.Point(12, 27);
            this.ctrlFilterDriverLicenseInfo1.Name = "ctrlFilterDriverLicenseInfo1";
            this.ctrlFilterDriverLicenseInfo1.Size = new System.Drawing.Size(898, 345);
            this.ctrlFilterDriverLicenseInfo1.TabIndex = 4;
            this.ctrlFilterDriverLicenseInfo1._OnClickToSearch += new System.Action<int, bool>(this.ctrlFilterDriverLicenseInfo1__OnClickToSearch);
            this.ctrlFilterDriverLicenseInfo1.Load += new System.EventHandler(this.ctrlFilterDriverLicenseInfo1_Load);
            // 
            // frmRenewLocalDrivingLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 689);
            this.Controls.Add(this.lnkLbl2);
            this.Controls.Add(this.lnkLbl1);
            this.Controls.Add(this.btnRenew);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlApplicationNewLicenseInfo1);
            this.Controls.Add(this.ctrlFilterDriverLicenseInfo1);
            this.Controls.Add(this.label1);
            this.Name = "frmRenewLocalDrivingLicense";
            this.Text = "Renew Local Driving License";
            this.Load += new System.EventHandler(this.frmRenewLocalDrivingLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private ctrlFilterDriverLicenseInfo ctrlFilterDriverLicenseInfo1;
        private ctrlApplicationNewLicenseInfo ctrlApplicationNewLicenseInfo1;
        private System.Windows.Forms.LinkLabel lnkLbl2;
        private System.Windows.Forms.LinkLabel lnkLbl1;
        private System.Windows.Forms.Button btnRenew;
        private System.Windows.Forms.Button btnClose;
    }
}