namespace ScrobbleMapper.Forms
{
    partial class OptionsDialog
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
            this.AcceptChangesButton = new System.Windows.Forms.Button();
            this.CancelChangesButton = new System.Windows.Forms.Button();
            this.Label = new System.Windows.Forms.Label();
            this.MinimumEditDistanceChooser = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.MinimumEditDistanceChooser)).BeginInit();
            this.SuspendLayout();
            // 
            // AcceptChangesButton
            // 
            this.AcceptChangesButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.AcceptChangesButton.Location = new System.Drawing.Point(219, 68);
            this.AcceptChangesButton.Name = "AcceptChangesButton";
            this.AcceptChangesButton.Size = new System.Drawing.Size(75, 23);
            this.AcceptChangesButton.TabIndex = 0;
            this.AcceptChangesButton.Text = "OK";
            this.AcceptChangesButton.UseVisualStyleBackColor = true;
            // 
            // CancelChangesButton
            // 
            this.CancelChangesButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelChangesButton.Location = new System.Drawing.Point(300, 68);
            this.CancelChangesButton.Name = "CancelChangesButton";
            this.CancelChangesButton.Size = new System.Drawing.Size(75, 23);
            this.CancelChangesButton.TabIndex = 0;
            this.CancelChangesButton.Text = "Cancel";
            this.CancelChangesButton.UseVisualStyleBackColor = true;
            // 
            // Label
            // 
            this.Label.Location = new System.Drawing.Point(12, 20);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(306, 31);
            this.Label.TabIndex = 1;
            this.Label.Text = "Minimum track title/artist relevance when fuzzy-matching : (high is strict, low i" +
                "s tolerant)";
            // 
            // MinimumEditDistanceChooser
            // 
            this.MinimumEditDistanceChooser.Location = new System.Drawing.Point(324, 18);
            this.MinimumEditDistanceChooser.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.MinimumEditDistanceChooser.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MinimumEditDistanceChooser.Name = "MinimumEditDistanceChooser";
            this.MinimumEditDistanceChooser.Size = new System.Drawing.Size(51, 22);
            this.MinimumEditDistanceChooser.TabIndex = 2;
            this.MinimumEditDistanceChooser.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // OptionsDialog
            // 
            this.AcceptButton = this.AcceptChangesButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.CancelChangesButton;
            this.ClientSize = new System.Drawing.Size(387, 103);
            this.Controls.Add(this.MinimumEditDistanceChooser);
            this.Controls.Add(this.Label);
            this.Controls.Add(this.CancelChangesButton);
            this.Controls.Add(this.AcceptChangesButton);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "OptionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Options";
            ((System.ComponentModel.ISupportInitialize)(this.MinimumEditDistanceChooser)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button AcceptChangesButton;
        private System.Windows.Forms.Button CancelChangesButton;
        private System.Windows.Forms.Label Label;
        private System.Windows.Forms.NumericUpDown MinimumEditDistanceChooser;
    }
}