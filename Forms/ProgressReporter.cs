using System;
using System.Windows.Forms;

namespace ScrobbleMapper.Forms
{
    partial class ProgressReporter : Form
    {
        readonly Action UpdateProgress;
        readonly Action UpdateDescription;

        readonly IReportingTask TaskContext;

        public ProgressReporter(IReportingTask taskContext)
        {
            InitializeComponent();

            TaskContext = taskContext;

            // Change the visual controls when the task's progress changes
            // This needs to be done in this form's thread, hence the BeginInvoke
            UpdateProgress = () => BeginInvoke((Action)delegate
            {
                var roundedValue = (int)Math.Round(TaskContext.Progress * 100);

                if (roundedValue == 0)
                    ProgressText.Text = "Preparing action...";
                else if (roundedValue == 100)
                    ProgressText.Text = "Finalizing action...";
                else
                    ProgressText.Text = roundedValue + "%";

                ProgressBar.Value = roundedValue;
            });
            TaskContext.ProgressChanged += UpdateProgress;

            UpdateDescription = () => BeginInvoke((Action)delegate
            {
                if (TaskContext.Description != null)
                    DetailsLabel.Text = TaskContext.Description;
            });
            TaskContext.DescriptionChanged += UpdateDescription;
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            // BeginInvoke cannot be called before the handle is created,...
            UpdateProgress();
            UpdateDescription();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            // Clean up events
            TaskContext.ProgressChanged -= UpdateProgress;
            TaskContext.DescriptionChanged -= UpdateDescription;
        }

        void StopButton_Click(object sender, EventArgs e)
        {
            StopButton.Enabled = false;
            StopButton.Text = "Please wait...";

            // This will schedule a canceling, it does NOT force it
            TaskContext.CancellationTokenSource.Cancel();
        }
    }
}