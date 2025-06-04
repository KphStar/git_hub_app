namespace git_hub_app
{
    partial class User_Dashboard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User_Dashboard));
            this.BtnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnExit
            // 
            this.BtnExit.Location = new System.Drawing.Point(529, 346);
            this.BtnExit.Name = "BtnExit";
            this.BtnExit.Size = new System.Drawing.Size(259, 42);
            this.BtnExit.TabIndex = 0;
            this.BtnExit.Text = "EXIT";
            this.BtnExit.UseVisualStyleBackColor = true;
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // User_Dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BtnExit);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "User_Dashboard";
            this.Text = "Dashboard";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.User_Dashboard_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.User_Dashboard_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.User_Dashboard_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnExit;
    }
}