namespace DVLDProject
{
    partial class frmPersonDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPersonDetails));
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.lblAddOrUpdate = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.ctrlShowPersonDetails1 = new DVLDProject.ctrlShowPersonDetails();
            this.SuspendLayout();
            // 
            // lblAddOrUpdate
            // 
            this.lblAddOrUpdate.AutoSize = true;
            this.lblAddOrUpdate.Font = new System.Drawing.Font("Microsoft Tai Le", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAddOrUpdate.ForeColor = System.Drawing.Color.Brown;
            this.lblAddOrUpdate.Location = new System.Drawing.Point(272, 20);
            this.lblAddOrUpdate.Name = "lblAddOrUpdate";
            this.lblAddOrUpdate.Size = new System.Drawing.Size(199, 34);
            this.lblAddOrUpdate.TabIndex = 8;
            this.lblAddOrUpdate.Text = "Person Details";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.White;
            this.btnClose.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = ((System.Drawing.Image)(resources.GetObject("btnClose.Image")));
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(634, 362);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(120, 38);
            this.btnClose.TabIndex = 39;
            this.btnClose.Text = "     Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ctrlShowPersonDetails1
            // 
            this.ctrlShowPersonDetails1.Location = new System.Drawing.Point(2, 66);
            this.ctrlShowPersonDetails1.Name = "ctrlShowPersonDetails1";
            this.ctrlShowPersonDetails1.Size = new System.Drawing.Size(770, 290);
            this.ctrlShowPersonDetails1.TabIndex = 0;
            this.ctrlShowPersonDetails1.Load += new System.EventHandler(this.ctrlShowPersonDetails1_Load);
            // 
            // frmPersonDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 410);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblAddOrUpdate);
            this.Controls.Add(this.ctrlShowPersonDetails1);
            this.Name = "frmPersonDetails";
            this.Text = "frmPersonDetails";
            this.Load += new System.EventHandler(this.frmPersonDetails_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private ctrlShowPersonDetails ctrlShowPersonDetails1;
        private System.Windows.Forms.Label lblAddOrUpdate;
        private System.Windows.Forms.Button btnClose;
    }
}