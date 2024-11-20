namespace DVLDProject
{
    partial class frmUserInfo
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
            this.ctrlShowPersonDetails1 = new DVLDProject.ctrlShowPersonDetails();
            this.ctrlLoginInformation1 = new DVLDProject.ctrlLoginInformation();
            this.SuspendLayout();
            // 
            // ctrlShowPersonDetails1
            // 
            this.ctrlShowPersonDetails1.Location = new System.Drawing.Point(2, 2);
            this.ctrlShowPersonDetails1.Name = "ctrlShowPersonDetails1";
            this.ctrlShowPersonDetails1.Size = new System.Drawing.Size(770, 290);
            this.ctrlShowPersonDetails1.TabIndex = 0;
            // 
            // ctrlLoginInformation1
            // 
            this.ctrlLoginInformation1.Location = new System.Drawing.Point(2, 298);
            this.ctrlLoginInformation1.Name = "ctrlLoginInformation1";
            this.ctrlLoginInformation1.Size = new System.Drawing.Size(764, 84);
            this.ctrlLoginInformation1.TabIndex = 1;
            // 
            // frmUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 394);
            this.Controls.Add(this.ctrlLoginInformation1);
            this.Controls.Add(this.ctrlShowPersonDetails1);
            this.Name = "frmUserInfo";
            this.Text = "frmUserInfo";
            this.Load += new System.EventHandler(this.frmUserInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlShowPersonDetails ctrlShowPersonDetails1;
        private ctrlLoginInformation ctrlLoginInformation1;
    
    }
}