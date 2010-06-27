namespace ScrobbleMapper.Forms
{
    partial class ProgressReporter
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
            this.ProgressBar = new System.Windows.Forms.ProgressBar();
            this.ProgressCaption = new System.Windows.Forms.Label();
            this.ProgressText = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.DetailsCaption = new System.Windows.Forms.Label();
            this.DetailsLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ProgressBar
            // 
            this.ProgressBar.Location = new System.Drawing.Point(12, 12);
            this.ProgressBar.Name = "ProgressBar";
            this.ProgressBar.Size = new System.Drawing.Size(497, 23);
            this.ProgressBar.TabIndex = 0;
            // 
            // ProgressCaption
            // 
            this.ProgressCaption.AutoSize = true;
            this.ProgressCaption.Location = new System.Drawing.Point(12, 38);
            this.ProgressCaption.Name = "ProgressCaption";
            this.ProgressCaption.Size = new System.Drawing.Size(57, 13);
            this.ProgressCaption.TabIndex = 1;
            this.ProgressCaption.Text = "Progress :";
            // 
            // ProgressText
            // 
            this.ProgressText.AutoSize = true;
            this.ProgressText.Location = new System.Drawing.Point(75, 38);
            this.ProgressText.Name = "ProgressText";
            this.ProgressText.Size = new System.Drawing.Size(101, 13);
            this.ProgressText.TabIndex = 2;
            this.ProgressText.Text = "Preparing action...";
            // 
            // StopButton
            // 
            this.StopButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.StopButton.Location = new System.Drawing.Point(421, 42);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(88, 23);
            this.StopButton.TabIndex = 3;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // DetailsCaption
            // 
            this.DetailsCaption.AutoSize = true;
            this.DetailsCaption.Location = new System.Drawing.Point(12, 55);
            this.DetailsCaption.Name = "DetailsCaption";
            this.DetailsCaption.Size = new System.Drawing.Size(48, 13);
            this.DetailsCaption.TabIndex = 4;
            this.DetailsCaption.Text = "Details :";
            // 
            // DetailsLabel
            // 
            this.DetailsLabel.AutoEllipsis = true;
            this.DetailsLabel.Location = new System.Drawing.Point(75, 55);
            this.DetailsLabel.Name = "DetailsLabel";
            this.DetailsLabel.Size = new System.Drawing.Size(340, 13);
            this.DetailsLabel.TabIndex = 2;
            this.DetailsLabel.Text = "(none)";
            // 
            // ProgressReporter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.StopButton;
            this.ClientSize = new System.Drawing.Size(521, 77);
            this.ControlBox = false;
            this.Controls.Add(this.DetailsCaption);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.DetailsLabel);
            this.Controls.Add(this.ProgressText);
            this.Controls.Add(this.ProgressCaption);
            this.Controls.Add(this.ProgressBar);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ProgressReporter";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Action In Progress...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar ProgressBar;
        private System.Windows.Forms.Label ProgressCaption;
        private System.Windows.Forms.Label ProgressText;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Label DetailsCaption;
        private System.Windows.Forms.Label DetailsLabel;
    }
}