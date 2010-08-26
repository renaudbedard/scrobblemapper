using System.Collections.Generic;
using System.Windows.Forms;
using ScrobbleMapper.Library;

namespace ScrobbleMapper.Forms
{
    partial class ErrorReporter : Form
    {
        public ErrorReporter(IEnumerable<QualifiedError> errors)
        {
            InitializeComponent();

            foreach (var error in errors)
                ErrorList.Items.Add(new ListViewItem(new[] { error.Item, error.Error }));
        }

        public string AlternateTaskName
        {
            set { ErrorsLabel.Text = "One or more error(s) has/have occurred while " + value; }
        }
        public string AlternateItemName
        {
            set { ErrorList.Columns[0].Name = value; }
        }
    }

    internal struct QualifiedError
    {
        public readonly string Item;
        public readonly string Error;

        public QualifiedError(string item, string error)
        {
            Item = item;
            Error = error;
        }
    }
}
