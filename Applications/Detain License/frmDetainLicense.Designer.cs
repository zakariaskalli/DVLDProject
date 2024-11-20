namespace DVLDProject
{
    partial class frmDetainLicense
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDetainLicense));
            this.label2 = new System.Windows.Forms.Label();
            this.lnkLbl2 = new System.Windows.Forms.LinkLabel();
            this.lnkLbl1 = new System.Windows.Forms.LinkLabel();
            this.btnDetain = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlDetainInfo1 = new DVLDProject.ctrlDetainInfo();
            this.ctrlFilterDriverLicenseInfo1 = new DVLDProject.ctrlFilterDriverLicenseInfo();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Brown;
            this.label2.Location = new System.Drawing.Point(344, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(179, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Detain License";
            // 
            // lnkLbl2
            // 
            this.lnkLbl2.AutoSize = true;
            this.lnkLbl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLbl2.Location = new System.Drawing.Point(226, 548);
            this.lnkLbl2.Name = "lnkLbl2";
            this.lnkLbl2.Size = new System.Drawing.Size(183, 20);
            this.lnkLbl2.TabIndex = 22;
            this.lnkLbl2.TabStop = true;
            this.lnkLbl2.Text = "Show New Licenses Info";
            this.lnkLbl2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl2_LinkClicked);
            // 
            // lnkLbl1
            // 
            this.lnkLbl1.AutoSize = true;
            this.lnkLbl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnkLbl1.Location = new System.Drawing.Point(12, 548);
            this.lnkLbl1.Name = "lnkLbl1";
            this.lnkLbl1.Size = new System.Drawing.Size(169, 20);
            this.lnkLbl1.TabIndex = 21;
            this.lnkLbl1.TabStop = true;
            this.lnkLbl1.Text = "Show Licenses History";
            this.lnkLbl1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLbl1_LinkClicked);
            // 
            // btnDetain
            // 
            this.btnDetain.BackColor = System.Drawing.Color.White;
            this.btnDetain.Enabled = false;
            this.btnDetain.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetain.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetain.Image = ((System.Drawing.Image)(resources.GetObject("btnDetain.Image")));
            this.btnDetain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDetain.Location = new System.Drawing.Point(778, 540);
            this.btnDetain.Name = "btnDetain";
            this.btnDetain.Size = new System.Drawing.Size(118, 38);
            this.btnDetain.TabIndex = 20;
            this.btnDetain.Text = "     Detain";
            this.btnDetain.UseVisualStyleBackColor = false;
            this.btnDetain.Click += new System.EventHandler(this.btnDetain_Click);
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(647, 540);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(118, 38);
            this.btnClose.TabIndex = 19;
            this.btnClose.Text = "     Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlDetainInfo1
            // 
            this.ctrlDetainInfo1.Location = new System.Drawing.Point(-2, 405);
            this.ctrlDetainInfo1.Name = "ctrlDetainInfo1";
            this.ctrlDetainInfo1.Size = new System.Drawing.Size(835, 132);
            this.ctrlDetainInfo1.TabIndex = 5;
            // 
            // ctrlFilterDriverLicenseInfo1
            // 
            this.ctrlFilterDriverLicenseInfo1.Location = new System.Drawing.Point(-2, 43);
            this.ctrlFilterDriverLicenseInfo1.Name = "ctrlFilterDriverLicenseInfo1";
            this.ctrlFilterDriverLicenseInfo1.Size = new System.Drawing.Size(898, 356);
            this.ctrlFilterDriverLicenseInfo1.TabIndex = 4;
            this.ctrlFilterDriverLicenseInfo1._OnClickToSearch += new System.Action<int, bool>(this.ctrlFilterDriverLicenseInfo1__OnClickToSearch);
            // 
            // frmDetainLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(908, 589);
            this.Controls.Add(this.lnkLbl2);
            this.Controls.Add(this.lnkLbl1);
            this.Controls.Add(this.btnDetain);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ctrlDetainInfo1);
            this.Controls.Add(this.ctrlFilterDriverLicenseInfo1);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDetainLicense";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Detain License";
            this.Load += new System.EventHandler(this.frmDetainLicense_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private ctrlFilterDriverLicenseInfo ctrlFilterDriverLicenseInfo1;
        private ctrlDetainInfo ctrlDetainInfo1;
        private System.Windows.Forms.LinkLabel lnkLbl2;
        private System.Windows.Forms.LinkLabel lnkLbl1;
        private System.Windows.Forms.Button btnDetain;
        private System.Windows.Forms.Button btnClose;
    }
}