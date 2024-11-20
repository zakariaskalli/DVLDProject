namespace DVLDProject
{
    partial class frmReplacementForDamagedOrLostLicenses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReplacementForDamagedOrLostLicenses));
            this.lblTextTitle = new System.Windows.Forms.Label();
            this.gbReplacementFor = new System.Windows.Forms.GroupBox();
            this.rbLostLicense = new System.Windows.Forms.RadioButton();
            this.rbDamagedLicense = new System.Windows.Forms.RadioButton();
            this.lnkLbl2 = new System.Windows.Forms.LinkLabel();
            this.lnkLbl1 = new System.Windows.Forms.LinkLabel();
            this.btnIssueReplacement = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlAppInfoForLicReplacement1 = new DVLDProject.ctrlAppInfoForLicReplacement();
            this.ctrlFilterDriverLicenseInfo1 = new DVLDProject.ctrlFilterDriverLicenseInfo();
            this.gbReplacementFor.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTextTitle
            // 
            this.lblTextTitle.AutoSize = true;
            this.lblTextTitle.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTextTitle.ForeColor = System.Drawing.Color.Brown;
            this.lblTextTitle.Location = new System.Drawing.Point(284, 9);
            this.lblTextTitle.Name = "lblTextTitle";
            this.lblTextTitle.Size = new System.Drawing.Size(501, 31);
            this.lblTextTitle.TabIndex = 3;
            this.lblTextTitle.Text = "Replacement For Damaged Or Lost License";
            // 
            // gbReplacementFor
            // 
            this.gbReplacementFor.Controls.Add(this.rbLostLicense);
            this.gbReplacementFor.Controls.Add(this.rbDamagedLicense);
            this.gbReplacementFor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbReplacementFor.Location = new System.Drawing.Point(601, 43);
            this.gbReplacementFor.Name = "gbReplacementFor";
            this.gbReplacementFor.Size = new System.Drawing.Size(200, 74);
            this.gbReplacementFor.TabIndex = 5;
            this.gbReplacementFor.TabStop = false;
            this.gbReplacementFor.Text = "Replacement For:";
            // 
            // rbLostLicense
            // 
            this.rbLostLicense.AutoSize = true;
            this.rbLostLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbLostLicense.Location = new System.Drawing.Point(30, 46);
            this.rbLostLicense.Name = "rbLostLicense";
            this.rbLostLicense.Size = new System.Drawing.Size(110, 22);
            this.rbLostLicense.TabIndex = 1;
            this.rbLostLicense.Text = "Lost License";
            this.rbLostLicense.UseVisualStyleBackColor = true;
            this.rbLostLicense.CheckedChanged += new System.EventHandler(this.rbLostLicense_CheckedChanged);
            // 
            // rbDamagedLicense
            // 
            this.rbDamagedLicense.AutoSize = true;
            this.rbDamagedLicense.Checked = true;
            this.rbDamagedLicense.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDamagedLicense.Location = new System.Drawing.Point(30, 20);
            this.rbDamagedLicense.Name = "rbDamagedLicense";
            this.rbDamagedLicense.Size = new System.Drawing.Size(145, 22);
            this.rbDamagedLicense.TabIndex = 0;
            this.rbDamagedLicense.TabStop = true;
            this.rbDamagedLicense.Text = "Damaged License";
            this.rbDamagedLicense.UseVisualStyleBackColor = true;
            this.rbDamagedLicense.CheckedChanged += new System.EventHandler(this.rbDamagedLicense_CheckedChanged);
            // 
            // lnkLbl2
            // 
            this.lnkLbl2.AutoSize = true;
            this.lnkLbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLbl2.Location = new System.Drawing.Point(213, 556);
            this.lnkLbl2.Name = "lnkLbl2";
            this.lnkLbl2.Size = new System.Drawing.Size(148, 20);
            this.lnkLbl2.TabIndex = 18;
            this.lnkLbl2.TabStop = true;
            this.lnkLbl2.Text = "Show Licenses Info";
            this.lnkLbl2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl2_LinkClicked);
            // 
            // lnkLbl1
            // 
            this.lnkLbl1.AutoSize = true;
            this.lnkLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLbl1.Location = new System.Drawing.Point(8, 556);
            this.lnkLbl1.Name = "lnkLbl1";
            this.lnkLbl1.Size = new System.Drawing.Size(169, 20);
            this.lnkLbl1.TabIndex = 17;
            this.lnkLbl1.TabStop = true;
            this.lnkLbl1.Text = "Show Licenses History";
            this.lnkLbl1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl1_LinkClicked);
            // 
            // btnIssueReplacement
            // 
            this.btnIssueReplacement.BackColor = System.Drawing.Color.White;
            this.btnIssueReplacement.Enabled = false;
            this.btnIssueReplacement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIssueReplacement.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIssueReplacement.Image = ((System.Drawing.Image)(resources.GetObject("btnIssueReplacement.Image")));
            this.btnIssueReplacement.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnIssueReplacement.Location = new System.Drawing.Point(685, 548);
            this.btnIssueReplacement.Name = "btnIssueReplacement";
            this.btnIssueReplacement.Size = new System.Drawing.Size(200, 36);
            this.btnIssueReplacement.TabIndex = 16;
            this.btnIssueReplacement.Text = "     Issue Replacement";
            this.btnIssueReplacement.UseVisualStyleBackColor = false;
            this.btnIssueReplacement.Click += new System.EventHandler(this.btnIssueReplacement_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(561, 548);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 38);
            this.btnClose.TabIndex = 15;
            this.btnClose.Text = "     Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlAppInfoForLicReplacement1
            // 
            this.ctrlAppInfoForLicReplacement1.Location = new System.Drawing.Point(12, 417);
            this.ctrlAppInfoForLicReplacement1.Name = "ctrlAppInfoForLicReplacement1";
            this.ctrlAppInfoForLicReplacement1.Size = new System.Drawing.Size(860, 127);
            this.ctrlAppInfoForLicReplacement1.TabIndex = 6;
            // 
            // ctrlFilterDriverLicenseInfo1
            // 
            this.ctrlFilterDriverLicenseInfo1.Location = new System.Drawing.Point(12, 55);
            this.ctrlFilterDriverLicenseInfo1.Name = "ctrlFilterDriverLicenseInfo1";
            this.ctrlFilterDriverLicenseInfo1.Size = new System.Drawing.Size(898, 356);
            this.ctrlFilterDriverLicenseInfo1.TabIndex = 4;
            this.ctrlFilterDriverLicenseInfo1._OnClickToSearch += new System.Action<int, bool>(this.ctrlFilterDriverLicenseInfo1__OnClickToSearch);
            // 
            // frmReplacementForDamagedOrLostLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(897, 594);
            this.Controls.Add(this.lnkLbl2);
            this.Controls.Add(this.lnkLbl1);
            this.Controls.Add(this.btnIssueReplacement);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlAppInfoForLicReplacement1);
            this.Controls.Add(this.gbReplacementFor);
            this.Controls.Add(this.ctrlFilterDriverLicenseInfo1);
            this.Controls.Add(this.lblTextTitle);
            this.Name = "frmReplacementForDamagedOrLostLicenses";
            this.Text = "Replacement For Damaged Or Lost License";
            this.Load += new System.EventHandler(this.frmReplacementForDamagedOrLostLicenses_Load);
            this.gbReplacementFor.ResumeLayout(false);
            this.gbReplacementFor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTextTitle;
        private System.Windows.Forms.GroupBox gbReplacementFor;
        private System.Windows.Forms.RadioButton rbLostLicense;
        private System.Windows.Forms.RadioButton rbDamagedLicense;
        private ctrlAppInfoForLicReplacement ctrlAppInfoForLicReplacement1;
        private System.Windows.Forms.LinkLabel lnkLbl2;
        private System.Windows.Forms.LinkLabel lnkLbl1;
        private System.Windows.Forms.Button btnIssueReplacement;
        private System.Windows.Forms.Button btnClose;
        private ctrlFilterDriverLicenseInfo ctrlFilterDriverLicenseInfo1;
    }
}