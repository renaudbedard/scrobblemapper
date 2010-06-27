using System.Collections.Generic;
using System.Windows.Forms;
using ScrobbleMapper.Library;

namespace ScrobbleMapper.Forms
{
    partial class ErrorReporter : Form
    {
        public ErrorReporter(IEnumerable<UpdateError> errors)
        {
            InitializeComponent();

            foreach (var error in errors)
                ErrorList.Items.Add(new ListViewItem(new[] { error.Track.ToString(), error.Error }));
        }
    }
}
