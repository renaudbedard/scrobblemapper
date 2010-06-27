namespace ScrobbleMapper.Forms
{
    partial class TrackChooser
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.DiscardButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ArtistLabel = new System.Windows.Forms.Label();
            this.TitleLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.FilesLeftLabel = new System.Windows.Forms.Label();
            this.MatchesList = new System.Windows.Forms.ListView();
            this.ArtistColumn = new System.Windows.Forms.ColumnHeader();
            this.TitleColumn = new System.Windows.Forms.ColumnHeader();
            this.AlbumColumn = new System.Windows.Forms.ColumnHeader();
            this.RelevanceColumn = new System.Windows.Forms.ColumnHeader();
            this.AutoUpdateSingleMatchesCheck = new System.Windows.Forms.CheckBox();
            this.AutoUpdateFullyRelevantCheck = new System.Windows.Forms.CheckBox();
            this.RelevanceBoundChooser = new System.Windows.Forms.NumericUpDown();
            this.DiscardReminaingCheck = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.RelevanceBoundChooser)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(614, 28);
            this.label1.TabIndex = 0;
            this.label1.Text = "Multiple or possibly inaccurate matches were found for this media file. Please ch" +
                "oose the one(s) you wish to update.";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Last.fm Artist :";
            // 
            // DiscardButton
            // 
            this.DiscardButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.DiscardButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.DiscardButton.Location = new System.Drawing.Point(535, 284);
            this.DiscardButton.Name = "DiscardButton";
            this.DiscardButton.Size = new System.Drawing.Size(91, 23);
            this.DiscardButton.TabIndex = 3;
            this.DiscardButton.Text = "Discard";
            this.DiscardButton.UseVisualStyleBackColor = true;
            // 
            // UpdateButton
            // 
            this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UpdateButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.UpdateButton.Location = new System.Drawing.Point(535, 255);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(91, 23);
            this.UpdateButton.TabIndex = 3;
            this.UpdateButton.Text = "Update";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Last.fm Title :";
            // 
            // ArtistLabel
            // 
            this.ArtistLabel.AutoSize = true;
            this.ArtistLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ArtistLabel.Location = new System.Drawing.Point(12, 54);
            this.ArtistLabel.Name = "ArtistLabel";
            this.ArtistLabel.Size = new System.Drawing.Size(43, 13);
            this.ArtistLabel.TabIndex = 1;
            this.ArtistLabel.Text = "[Artist]";
            // 
            // TitleLabel
            // 
            this.TitleLabel.AutoSize = true;
            this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleLabel.Location = new System.Drawing.Point(206, 54);
            this.TitleLabel.Name = "TitleLabel";
            this.TitleLabel.Size = new System.Drawing.Size(37, 13);
            this.TitleLabel.TabIndex = 1;
            this.TitleLabel.Text = "[Title]";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(496, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Files left to match :";
            // 
            // FilesLeftLabel
            // 
            this.FilesLeftLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.FilesLeftLabel.AutoSize = true;
            this.FilesLeftLabel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FilesLeftLabel.Location = new System.Drawing.Point(601, 54);
            this.FilesLeftLabel.Name = "FilesLeftLabel";
            this.FilesLeftLabel.Size = new System.Drawing.Size(25, 13);
            this.FilesLeftLabel.TabIndex = 4;
            this.FilesLeftLabel.Text = " [#]";
            // 
            // MatchesList
            // 
            this.MatchesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.MatchesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ArtistColumn,
            this.TitleColumn,
            this.AlbumColumn,
            this.RelevanceColumn});
            this.MatchesList.FullRowSelect = true;
            this.MatchesList.HideSelection = false;
            this.MatchesList.Location = new System.Drawing.Point(12, 77);
            this.MatchesList.Name = "MatchesList";
            this.MatchesList.ShowGroups = false;
            this.MatchesList.Size = new System.Drawing.Size(614, 164);
            this.MatchesList.TabIndex = 5;
            this.MatchesList.UseCompatibleStateImageBehavior = false;
            this.MatchesList.View = System.Windows.Forms.View.Details;
            this.MatchesList.SelectedIndexChanged += new System.EventHandler(this.MatchesList_SelectedIndexChanged);
            this.MatchesList.DoubleClick += new System.EventHandler(this.MatchesList_DoubleClick);
            // 
            // ArtistColumn
            // 
            this.ArtistColumn.Text = "Artist";
            this.ArtistColumn.Width = 173;
            // 
            // TitleColumn
            // 
            this.TitleColumn.Text = "Title";
            this.TitleColumn.Width = 171;
            // 
            // AlbumColumn
            // 
            this.AlbumColumn.Text = "Album";
            this.AlbumColumn.Width = 203;
            // 
            // RelevanceColumn
            // 
            this.RelevanceColumn.Text = "Relevance";
            this.RelevanceColumn.Width = 63;
            // 
            // AutoUpdateSingleMatchesCheck
            // 
            this.AutoUpdateSingleMatchesCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoUpdateSingleMatchesCheck.AutoSize = true;
            this.AutoUpdateSingleMatchesCheck.Location = new System.Drawing.Point(15, 253);
            this.AutoUpdateSingleMatchesCheck.Name = "AutoUpdateSingleMatchesCheck";
            this.AutoUpdateSingleMatchesCheck.Size = new System.Drawing.Size(408, 17);
            this.AutoUpdateSingleMatchesCheck.TabIndex = 6;
            this.AutoUpdateSingleMatchesCheck.Text = "Automatically update all single matches, even if they\'re possibly inaccurate";
            this.AutoUpdateSingleMatchesCheck.UseVisualStyleBackColor = true;
            // 
            // AutoUpdateFullyRelevantCheck
            // 
            this.AutoUpdateFullyRelevantCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.AutoUpdateFullyRelevantCheck.AutoSize = true;
            this.AutoUpdateFullyRelevantCheck.Location = new System.Drawing.Point(15, 272);
            this.AutoUpdateFullyRelevantCheck.Name = "AutoUpdateFullyRelevantCheck";
            this.AutoUpdateFullyRelevantCheck.Size = new System.Drawing.Size(348, 17);
            this.AutoUpdateFullyRelevantCheck.TabIndex = 6;
            this.AutoUpdateFullyRelevantCheck.Text = "Automatically update all matches with a relevance greater than";
            this.AutoUpdateFullyRelevantCheck.UseVisualStyleBackColor = true;
            this.AutoUpdateFullyRelevantCheck.CheckedChanged += new System.EventHandler(this.AutoUpdateFullyRelevantCheck_CheckedChanged);
            // 
            // RelevanceBoundChooser
            // 
            this.RelevanceBoundChooser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.RelevanceBoundChooser.Location = new System.Drawing.Point(361, 271);
            this.RelevanceBoundChooser.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.RelevanceBoundChooser.Minimum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.RelevanceBoundChooser.Name = "RelevanceBoundChooser";
            this.RelevanceBoundChooser.Size = new System.Drawing.Size(35, 22);
            this.RelevanceBoundChooser.TabIndex = 7;
            this.RelevanceBoundChooser.Value = new decimal(new int[] {
            85,
            0,
            0,
            0});
            // 
            // DiscardReminaingCheck
            // 
            this.DiscardReminaingCheck.AutoSize = true;
            this.DiscardReminaingCheck.Location = new System.Drawing.Point(15, 291);
            this.DiscardReminaingCheck.Name = "DiscardReminaingCheck";
            this.DiscardReminaingCheck.Size = new System.Drawing.Size(204, 17);
            this.DiscardReminaingCheck.TabIndex = 8;
            this.DiscardReminaingCheck.Text = "Discard all remaining fuzzy choices";
            this.DiscardReminaingCheck.UseVisualStyleBackColor = true;
            // 
            // TrackChooser
            // 
            this.AcceptButton = this.UpdateButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.DiscardButton;
            this.ClientSize = new System.Drawing.Size(638, 319);
            this.Controls.Add(this.DiscardReminaingCheck);
            this.Controls.Add(this.RelevanceBoundChooser);
            this.Controls.Add(this.AutoUpdateFullyRelevantCheck);
            this.Controls.Add(this.AutoUpdateSingleMatchesCheck);
            this.Controls.Add(this.MatchesList);
            this.Controls.Add(this.FilesLeftLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.DiscardButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TitleLabel);
            this.Controls.Add(this.ArtistLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TrackChooser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Multiple/Inaccurate Matches";
            ((System.ComponentModel.ISupportInitialize)(this.RelevanceBoundChooser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button DiscardButton;
        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ArtistLabel;
        private System.Windows.Forms.Label TitleLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label FilesLeftLabel;
        private System.Windows.Forms.ListView MatchesList;
        private System.Windows.Forms.ColumnHeader ArtistColumn;
        private System.Windows.Forms.ColumnHeader TitleColumn;
        private System.Windows.Forms.ColumnHeader AlbumColumn;
        private System.Windows.Forms.CheckBox AutoUpdateSingleMatchesCheck;
        private System.Windows.Forms.ColumnHeader RelevanceColumn;
        private System.Windows.Forms.CheckBox AutoUpdateFullyRelevantCheck;
        private System.Windows.Forms.NumericUpDown RelevanceBoundChooser;
        private System.Windows.Forms.CheckBox DiscardReminaingCheck;
    }
}