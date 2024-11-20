namespace DVLDProject
{
    partial class ctrlFilterAndMakePersonInfo
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
            this.ctrlFilterBy1 = new DVLDProject.ctrlFilterBy();
            this.ctrlShowPersonDetails1 = new DVLDProject.ctrlShowPersonDetails();
            this.SuspendLayout();
            // 
            // ctrlFilterBy1
            // 
            this.ctrlFilterBy1.Location = new System.Drawing.Point(3, 3);
            this.ctrlFilterBy1.Name = "ctrlFilterBy1";
            this.ctrlFilterBy1.Size = new System.Drawing.Size(770, 60);
            this.ctrlFilterBy1.TabIndex = 0;
            this.ctrlFilterBy1.OnFilterBtn += new System.Action<string, string>(this.ctrlFilterBy1_OnFilterBtn);
            this.ctrlFilterBy1.OnNewPeronAdd += new System.Action<int>(this.ctrlFilterBy1_OnNewPeronAdd);
            this.ctrlFilterBy1.Load += new System.EventHandler(this.ctrlFilterBy1_Load);
            // 
            // ctrlShowPersonDetails1
            // 
            this.ctrlShowPersonDetails1.Location = new System.Drawing.Point(3, 69);
            this.ctrlShowPersonDetails1.Name = "ctrlShowPersonDetails1";
            this.ctrlShowPersonDetails1.Size = new System.Drawing.Size(770, 290);
            this.ctrlShowPersonDetails1.TabIndex = 1;
            this.ctrlShowPersonDetails1.Load += new System.EventHandler(this.ctrlShowPersonDetails1_Load);
            // 
            // ctrlFilterAndMakePersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ctrlShowPersonDetails1);
            this.Controls.Add(this.ctrlFilterBy1);
            this.Name = "ctrlFilterAndMakePersonInfo";
            this.Size = new System.Drawing.Size(779, 360);
            this.ResumeLayout(false);

        }

        #endregion

        private ctrlFilterBy ctrlFilterBy1;
        private ctrlShowPersonDetails ctrlShowPersonDetails1;
    }
}
