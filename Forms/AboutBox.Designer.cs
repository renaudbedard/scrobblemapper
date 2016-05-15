namespace ScrobbleMapper.Forms
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.HomepageLink = new System.Windows.Forms.LinkLabel();
            this.AuthorNameLabel = new System.Windows.Forms.Label();
            this.AuthorCaption = new System.Windows.Forms.Label();
            this.LicenseLabel = new System.Windows.Forms.Label();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.UsedModulesLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CloseButton.Location = new System.Drawing.Point(289, 193);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // HomepageLink
            // 
            this.HomepageLink.AutoSize = true;
            this.HomepageLink.Location = new System.Drawing.Point(68, 128);
            this.HomepageLink.Name = "HomepageLink";
            this.HomepageLink.Size = new System.Drawing.Size(159, 13);
            this.HomepageLink.TabIndex = 1;
            this.HomepageLink.TabStop = true;
            this.HomepageLink.Text = "http://theinstructionlimit.com";
            this.HomepageLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.HomepageLink_LinkClicked);
            // 
            // AuthorNameLabel
            // 
            this.AuthorNameLabel.AutoSize = true;
            this.AuthorNameLabel.Location = new System.Drawing.Point(68, 115);
            this.AuthorNameLabel.Name = "AuthorNameLabel";
            this.AuthorNameLabel.Size = new System.Drawing.Size(87, 13);
            this.AuthorNameLabel.TabIndex = 2;
            this.AuthorNameLabel.Text = "Renaud Bédard";
            // 
            // AuthorCaption
            // 
            this.AuthorCaption.AutoSize = true;
            this.AuthorCaption.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AuthorCaption.Location = new System.Drawing.Point(12, 115);
            this.AuthorCaption.Name = "AuthorCaption";
            this.AuthorCaption.Size = new System.Drawing.Size(50, 13);
            this.AuthorCaption.TabIndex = 3;
            this.AuthorCaption.Text = "Author :";
            // 
            // LicenseLabel
            // 
            this.LicenseLabel.Location = new System.Drawing.Point(12, 160);
            this.LicenseLabel.Name = "LicenseLabel";
            this.LicenseLabel.Size = new System.Drawing.Size(352, 30);
            this.LicenseLabel.TabIndex = 4;
            this.LicenseLabel.Text = "Licensed under the MIT License. See the license.txt file for more information.";
            // 
            // VersionLabel
            // 
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Location = new System.Drawing.Point(12, 47);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(63, 13);
            this.VersionLabel.TabIndex = 4;
            this.VersionLabel.Text = "Version 3.0";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Corbel", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(12, 9);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(291, 29);
            this.TitleLabel.TabIndex = 5;
            this.TitleLabel.Text = "Scrobble Fetcher && Mapper";
            // 
            // UsedModulesLabel
            // 
            this.UsedModulesLabel.Location = new System.Drawing.Point(12, 74);
            this.UsedModulesLabel.Name = "UsedModulesLabel";
            this.UsedModulesLabel.Size = new System.Drawing.Size(352, 32);
            this.UsedModulesLabel.TabIndex = 4;
            this.UsedModulesLabel.Text = "Uses the Last.fm 2.0 RESTful XML API, the iTunes COM Interface and the Windows Me" +
    "dia Player 11 COM Interface";
            // 
            // AboutBox
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(376, 228);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.UsedModulesLabel);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.LicenseLabel);
            this.Controls.Add(this.AuthorCaption);
            this.Controls.Add(this.AuthorNameLabel);
            this.Controls.Add(this.HomepageLink);
            this.Controls.Add(this.CloseButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutBox";
            this.Padding = new System.Windows.Forms.Padding(9);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About the Scrobble Fetcher & Mapper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.LinkLabel HomepageLink;
        private System.Windows.Forms.Label AuthorNameLabel;
        private System.Windows.Forms.Label AuthorCaption;
        private System.Windows.Forms.Label LicenseLabel;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label UsedModulesLabel;

    }
}
