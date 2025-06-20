﻿namespace git_hub_app
{
    partial class User_Profile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(User_Profile));
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.textBoxBio = new System.Windows.Forms.TextBox();
            this.labelWelcome = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.BtnDash = new System.Windows.Forms.Button();
            this.flowPanelRepos = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnForward = new System.Windows.Forms.Button();
            this.BtnBackward = new System.Windows.Forms.Button();
            this.pictureBoxAvatar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).BeginInit();
            this.SuspendLayout();
            // 
            // lblUsername
            // 
            this.lblUsername.AutoSize = true;
            this.lblUsername.BackColor = System.Drawing.Color.Transparent;
            this.lblUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.lblUsername.Location = new System.Drawing.Point(12, 434);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(124, 29);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Username";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.BackColor = System.Drawing.Color.Transparent;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.lblName.Location = new System.Drawing.Point(12, 405);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(78, 29);
            this.lblName.TabIndex = 1;
            this.lblName.Text = "Name";
            // 
            // lblEmail
            // 
            this.lblEmail.AutoSize = true;
            this.lblEmail.BackColor = System.Drawing.Color.Transparent;
            this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail.ForeColor = System.Drawing.Color.MediumTurquoise;
            this.lblEmail.Location = new System.Drawing.Point(12, 463);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(74, 29);
            this.lblEmail.TabIndex = 2;
            this.lblEmail.Text = "Email";
            // 
            // textBoxBio
            // 
            this.textBoxBio.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBoxBio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxBio.ForeColor = System.Drawing.Color.Black;
            this.textBoxBio.Location = new System.Drawing.Point(15, 495);
            this.textBoxBio.Multiline = true;
            this.textBoxBio.Name = "textBoxBio";
            this.textBoxBio.ReadOnly = true;
            this.textBoxBio.Size = new System.Drawing.Size(360, 120);
            this.textBoxBio.TabIndex = 5;
            // 
            // labelWelcome
            // 
            this.labelWelcome.AutoSize = true;
            this.labelWelcome.BackColor = System.Drawing.Color.Transparent;
            this.labelWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelWelcome.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelWelcome.Location = new System.Drawing.Point(125, 9);
            this.labelWelcome.Name = "labelWelcome";
            this.labelWelcome.Size = new System.Drawing.Size(144, 20);
            this.labelWelcome.TabIndex = 6;
            this.labelWelcome.Text = "GITHUB PROFILE";
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnExit.Location = new System.Drawing.Point(228, 621);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(147, 27);
            this.btnExit.TabIndex = 7;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // BtnDash
            // 
            this.BtnDash.BackColor = System.Drawing.Color.Transparent;
            this.BtnDash.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDash.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnDash.Location = new System.Drawing.Point(12, 621);
            this.BtnDash.Name = "BtnDash";
            this.BtnDash.Size = new System.Drawing.Size(205, 27);
            this.BtnDash.TabIndex = 8;
            this.BtnDash.Text = "Dashboard";
            this.BtnDash.UseVisualStyleBackColor = false;
            this.BtnDash.Click += new System.EventHandler(this.BtnDash_Click);
            // 
            // flowPanelRepos
            // 
            this.flowPanelRepos.AutoScroll = true;
            this.flowPanelRepos.BackColor = System.Drawing.Color.Turquoise;
            this.flowPanelRepos.Location = new System.Drawing.Point(381, 56);
            this.flowPanelRepos.Name = "flowPanelRepos";
            this.flowPanelRepos.Size = new System.Drawing.Size(754, 242);
            this.flowPanelRepos.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(381, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "REPOSITORIES\r\n";
            // 
            // BtnForward
            // 
            this.BtnForward.BackColor = System.Drawing.Color.Transparent;
            this.BtnForward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnForward.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnForward.Location = new System.Drawing.Point(801, 329);
            this.BtnForward.Name = "BtnForward";
            this.BtnForward.Size = new System.Drawing.Size(288, 27);
            this.BtnForward.TabIndex = 11;
            this.BtnForward.Text = "<<<<NEXT>>>>";
            this.BtnForward.UseVisualStyleBackColor = false;
            this.BtnForward.Click += new System.EventHandler(this.BtnForward_Click);
            // 
            // BtnBackward
            // 
            this.BtnBackward.BackColor = System.Drawing.Color.Transparent;
            this.BtnBackward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBackward.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.BtnBackward.Location = new System.Drawing.Point(429, 329);
            this.BtnBackward.Name = "BtnBackward";
            this.BtnBackward.Size = new System.Drawing.Size(288, 27);
            this.BtnBackward.TabIndex = 12;
            this.BtnBackward.Text = "<<<<PREVIOUS>>>>";
            this.BtnBackward.UseVisualStyleBackColor = false;
            this.BtnBackward.Click += new System.EventHandler(this.BtnBackward_Click);
            // 
            // pictureBoxAvatar
            // 
            this.pictureBoxAvatar.BackColor = System.Drawing.Color.Transparent;
            this.pictureBoxAvatar.Location = new System.Drawing.Point(17, 33);
            this.pictureBoxAvatar.Name = "pictureBoxAvatar";
            this.pictureBoxAvatar.Size = new System.Drawing.Size(358, 369);
            this.pictureBoxAvatar.TabIndex = 4;
            this.pictureBoxAvatar.TabStop = false;
            // 
            // User_Profile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::git_hub_app.Properties.Resources.GHimage;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1147, 685);
            this.Controls.Add(this.BtnBackward);
            this.Controls.Add(this.BtnForward);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flowPanelRepos);
            this.Controls.Add(this.BtnDash);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.labelWelcome);
            this.Controls.Add(this.textBoxBio);
            this.Controls.Add(this.pictureBoxAvatar);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.lblUsername);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "User_Profile";
            this.Text = "User_Profile";
            this.Load += new System.EventHandler(this.User_Profile_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.User_Profile_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.User_Profile_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.User_Profile_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxAvatar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.PictureBox pictureBoxAvatar;
        private System.Windows.Forms.TextBox textBoxBio;
        private System.Windows.Forms.Label labelWelcome;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button BtnDash;
        private System.Windows.Forms.FlowLayoutPanel flowPanelRepos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnForward;
        private System.Windows.Forms.Button BtnBackward;
    }
}