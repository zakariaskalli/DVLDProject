namespace DVLDProject
{
    partial class ctrlFilterBy
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
            System.Windows.Forms.Button btnAddUser;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlFilterBy));
            this.FilterBy = new System.Windows.Forms.GroupBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.tbData = new System.Windows.Forms.TextBox();
            this.cbSearchBy = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            btnAddUser = new System.Windows.Forms.Button();
            this.FilterBy.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnAddUser
            // 
            btnAddUser.BackColor = System.Drawing.Color.White;
            btnAddUser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnAddUser.Image = ((System.Drawing.Image)(resources.GetObject("btnAddUser.Image")));
            btnAddUser.Location = new System.Drawing.Point(543, 15);
            btnAddUser.Name = "btnAddUser";
            btnAddUser.Size = new System.Drawing.Size(40, 35);
            btnAddUser.TabIndex = 8;
            btnAddUser.UseVisualStyleBackColor = false;
            btnAddUser.Click += new System.EventHandler(this.btnAddUser_Click);
            // 
            // FilterBy
            // 
            this.FilterBy.Controls.Add(btnAddUser);
            this.FilterBy.Controls.Add(this.btnSearch);
            this.FilterBy.Controls.Add(this.tbData);
            this.FilterBy.Controls.Add(this.cbSearchBy);
            this.FilterBy.Controls.Add(this.label1);
            this.FilterBy.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilterBy.Location = new System.Drawing.Point(3, 0);
            this.FilterBy.Name = "FilterBy";
            this.FilterBy.Size = new System.Drawing.Size(894, 59);
            this.FilterBy.TabIndex = 0;
            this.FilterBy.TabStop = false;
            this.FilterBy.Text = "Filter";
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(485, 15);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(40, 35);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.UseVisualStyleBackColor = false;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // tbData
            // 
            this.tbData.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbData.Location = new System.Drawing.Point(310, 22);
            this.tbData.Name = "tbData";
            this.tbData.Size = new System.Drawing.Size(152, 24);
            this.tbData.TabIndex = 7;
            this.tbData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbData_KeyPress);
            // 
            // cbSearchBy
            // 
            this.cbSearchBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSearchBy.Font = new System.Drawing.Font("Microsoft Tai Le", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbSearchBy.FormattingEnabled = true;
            this.cbSearchBy.Location = new System.Drawing.Point(118, 22);
            this.cbSearchBy.Name = "cbSearchBy";
            this.cbSearchBy.Size = new System.Drawing.Size(173, 24);
            this.cbSearchBy.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(41, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Filter By:";
            // 
            // ctrlFilterBy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.FilterBy);
            this.Name = "ctrlFilterBy";
            this.Size = new System.Drawing.Size(900, 60);
            this.Load += new System.EventHandler(this.ctrlFilterBy_Load);
            this.FilterBy.ResumeLayout(false);
            this.FilterBy.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FilterBy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbSearchBy;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox tbData;
    }
}
