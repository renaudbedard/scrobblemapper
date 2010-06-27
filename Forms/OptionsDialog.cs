using System;
using System.Windows.Forms;

namespace ScrobbleMapper.Forms
{
    public partial class OptionsDialog : Form
    {
        public OptionsDialog()
        {
            InitializeComponent();
        }

        public float MinimumEditDistance
        {
            get { return (int) MinimumEditDistanceChooser.Value / 100f; }
            set { MinimumEditDistanceChooser.Value = (int) Math.Round(value * 100); }
        }
    }
}
