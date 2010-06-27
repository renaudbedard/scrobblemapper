namespace ScrobbleMapper.Forms
{
    partial class MainForm
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
            this.UsernameText = new System.Windows.Forms.TextBox();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.TracksView = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OptionsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FetchButton = new System.Windows.Forms.Button();
            this.MapToITunesButton = new System.Windows.Forms.Button();
            this.MapToWmpButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TracksView)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // UsernameText
            // 
            this.UsernameText.Location = new System.Drawing.Point(12, 56);
            this.UsernameText.Name = "UsernameText";
            this.UsernameText.Size = new System.Drawing.Size(210, 22);
            this.UsernameText.TabIndex = 0;
            this.UsernameText.TextChanged += new System.EventHandler(this.UsernameText_TextChanged);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.UsernameLabel.Location = new System.Drawing.Point(12, 40);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(97, 13);
            this.UsernameLabel.TabIndex = 1;
            this.UsernameLabel.Text = "Last.fm Username";
            // 
            // TracksView
            // 
            this.TracksView.AllowUserToAddRows = false;
            this.TracksView.AllowUserToDeleteRows = false;
            this.TracksView.AllowUserToOrderColumns = true;
            this.TracksView.AllowUserToResizeRows = false;
            this.TracksView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TracksView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TracksView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TracksView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TracksView.Location = new System.Drawing.Point(12, 91);
            this.TracksView.Name = "TracksView";
            this.TracksView.ReadOnly = true;
            this.TracksView.RowHeadersVisible = false;
            this.TracksView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TracksView.Size = new System.Drawing.Size(920, 461);
            this.TracksView.TabIndex = 2;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenuItem,
            this.ToolsMenuItem,
            this.HelpMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(944, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileMenuItem
            // 
            this.FileMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExitMenuItem});
            this.FileMenuItem.Name = "FileMenuItem";
            this.FileMenuItem.Size = new System.Drawing.Size(37, 20);
            this.FileMenuItem.Text = "&File";
            // 
            // ExitMenuItem
            // 
            this.ExitMenuItem.Name = "ExitMenuItem";
            this.ExitMenuItem.Size = new System.Drawing.Size(92, 22);
            this.ExitMenuItem.Text = "E&xit";
            this.ExitMenuItem.Click += new System.EventHandler(this.ExitMenuItem_Click);
            // 
            // ToolsMenuItem
            // 
            this.ToolsMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OptionsMenuItem});
            this.ToolsMenuItem.Name = "ToolsMenuItem";
            this.ToolsMenuItem.Size = new System.Drawing.Size(48, 20);
            this.ToolsMenuItem.Text = "&Tools";
            // 
            // OptionsMenuItem
            // 
            this.OptionsMenuItem.Name = "OptionsMenuItem";
            this.OptionsMenuItem.Size = new System.Drawing.Size(152, 22);
            this.OptionsMenuItem.Text = "&Options...";
            this.OptionsMenuItem.Click += new System.EventHandler(this.OptionsMenuItem_Click);
            // 
            // HelpMenuItem
            // 
            this.HelpMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutMenuItem});
            this.HelpMenuItem.Name = "HelpMenuItem";
            this.HelpMenuItem.Size = new System.Drawing.Size(44, 20);
            this.HelpMenuItem.Text = "&Help";
            // 
            // AboutMenuItem
            // 
            this.AboutMenuItem.Name = "AboutMenuItem";
            this.AboutMenuItem.Size = new System.Drawing.Size(116, 22);
            this.AboutMenuItem.Text = "&About...";
            this.AboutMenuItem.Click += new System.EventHandler(this.AboutMenuItem_Click);
            // 
            // FetchButton
            // 
            this.FetchButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FetchButton.Enabled = false;
            this.FetchButton.Image = global::ScrobbleMapper.Properties.Resources.lfm;
            this.FetchButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.FetchButton.Location = new System.Drawing.Point(228, 54);
            this.FetchButton.Name = "FetchButton";
            this.FetchButton.Size = new System.Drawing.Size(140, 26);
            this.FetchButton.TabIndex = 3;
            this.FetchButton.Text = "Fetch Scrobbles";
            this.FetchButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.FetchButton.UseVisualStyleBackColor = true;
            this.FetchButton.Click += new System.EventHandler(this.FetchButton_Click);
            // 
            // MapToITunesButton
            // 
            this.MapToITunesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MapToITunesButton.Enabled = false;
            this.MapToITunesButton.Image = global::ScrobbleMapper.Properties.Resources.itunes;
            this.MapToITunesButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MapToITunesButton.Location = new System.Drawing.Point(672, 27);
            this.MapToITunesButton.Name = "MapToITunesButton";
            this.MapToITunesButton.Size = new System.Drawing.Size(260, 26);
            this.MapToITunesButton.TabIndex = 3;
            this.MapToITunesButton.Text = "Map To iTunes Library";
            this.MapToITunesButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.MapToITunesButton.UseVisualStyleBackColor = true;
            this.MapToITunesButton.Click += new System.EventHandler(this.MapToITunesButton_Click);
            // 
            // MapToWmpButton
            // 
            this.MapToWmpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MapToWmpButton.Enabled = false;
            this.MapToWmpButton.Image = global::ScrobbleMapper.Properties.Resources.wmp;
            this.MapToWmpButton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.MapToWmpButton.Location = new System.Drawing.Point(672, 59);
            this.MapToWmpButton.Name = "MapToWmpButton";
            this.MapToWmpButton.Size = new System.Drawing.Size(260, 26);
            this.MapToWmpButton.TabIndex = 3;
            this.MapToWmpButton.Text = "Map To Windows Media Player Library";
            this.MapToWmpButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.MapToWmpButton.UseVisualStyleBackColor = true;
            this.MapToWmpButton.Click += new System.EventHandler(this.MapToWmpButton_Click);
            // 
            // MainForm
            // 
            this.AcceptButton = this.FetchButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 564);
            this.Controls.Add(this.MapToITunesButton);
            this.Controls.Add(this.MapToWmpButton);
            this.Controls.Add(this.FetchButton);
            this.Controls.Add(this.TracksView);
            this.Controls.Add(this.UsernameLabel);
            this.Controls.Add(this.UsernameText);
            this.Controls.Add(this.menuStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Scrobble Fetcher & Mapper";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.TracksView)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox UsernameText;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.DataGridView TracksView;
        private System.Windows.Forms.Button FetchButton;
        private System.Windows.Forms.Button MapToWmpButton;
        private System.Windows.Forms.Button MapToITunesButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ToolsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OptionsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HelpMenuItem;
        private System.Windows.Forms.ToolStripMenuItem AboutMenuItem;
    }
}