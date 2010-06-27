using System.Diagnostics;
using System.Windows.Forms;

namespace ScrobbleMapper.Forms
{
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            HomepageLink.Links.Add(0, HomepageLink.Text.Length, HomepageLink.Text);
        }

        void HomepageLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }
    }
}
