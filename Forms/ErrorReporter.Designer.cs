namespace ScrobbleMapper.Forms
{
    partial class ErrorReporter
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.ErrorsLabel = new System.Windows.Forms.Label();
            this.ErrorList = new System.Windows.Forms.ListView();
            this.TrackColumn = new System.Windows.Forms.ColumnHeader();
            this.ErrorColumn = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CloseButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.CloseButton.Location = new System.Drawing.Point(532, 230);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(75, 23);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // ErrorsLabel
            // 
            this.ErrorsLabel.AutoSize = true;
            this.ErrorsLabel.Location = new System.Drawing.Point(12, 9);
            this.ErrorsLabel.Name = "ErrorsLabel";
            this.ErrorsLabel.Size = new System.Drawing.Size(357, 13);
            this.ErrorsLabel.TabIndex = 2;
            this.ErrorsLabel.Text = "One or more error(s) has/have occured while updating library tracks.";
            // 
            // ErrorList
            // 
            this.ErrorList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ErrorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TrackColumn,
            this.ErrorColumn});
            this.ErrorList.FullRowSelect = true;
            this.ErrorList.Location = new System.Drawing.Point(12, 25);
            this.ErrorList.Name = "ErrorList";
            this.ErrorList.Size = new System.Drawing.Size(595, 199);
            this.ErrorList.TabIndex = 3;
            this.ErrorList.UseCompatibleStateImageBehavior = false;
            this.ErrorList.View = System.Windows.Forms.View.Details;
            // 
            // TrackColumn
            // 
            this.TrackColumn.Text = "Track";
            this.TrackColumn.Width = 219;
            // 
            // ErrorColumn
            // 
            this.ErrorColumn.Text = "Error";
            this.ErrorColumn.Width = 371;
            // 
            // ErrorReporter
            // 
            this.AcceptButton = this.CloseButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CloseButton;
            this.ClientSize = new System.Drawing.Size(619, 265);
            this.Controls.Add(this.ErrorList);
            this.Controls.Add(this.ErrorsLabel);
            this.Controls.Add(this.CloseButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ErrorReporter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Error Log";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.Label ErrorsLabel;
        private System.Windows.Forms.ListView ErrorList;
        private System.Windows.Forms.ColumnHeader TrackColumn;
        private System.Windows.Forms.ColumnHeader ErrorColumn;
    }
}