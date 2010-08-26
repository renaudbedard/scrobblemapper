using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using ScrobbleMapper.LastFm;
using ScrobbleMapper.Library;
using ScrobbleMapper.Properties;

namespace ScrobbleMapper.Forms
{
    partial class MainForm : Form
    {
        readonly ScrobbleFetcher fetcher;

        bool hasiTunes = true, hasWmp = true;

        WmpLibraryManager wmpLibrary;
        ItunesLibraryManager iTunesLibrary;

        public IEnumerable<ScrobbledTrack> scrobbles;

        public MainForm()
        {
            InitializeComponent();
            UsernameText.Text = Settings.Default.LastFmUsername;
            fetcher = new ScrobbleFetcher();
        }

        #region Form Events

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();
        }

        void UsernameText_TextChanged(object sender, EventArgs eventArgs)
        {
            FetchButton.Enabled = UsernameText.Text != string.Empty;
        }

        void FetchButton_Click(object sender, EventArgs eventArgs)
        {
            Settings.Default.LastFmUsername = UsernameText.Text;

            TaskUtil.PerformForegroundTask(this, fetcher.FetchAsync(UsernameText.Text), result =>
            {
                if (result.Errors.Count() > 0)
                    new ErrorReporter(result.Errors) { AlternateTaskName = "fetching the user's scrobbles.", AlternateItemName = "Weekly chart" }.ShowDialog();

                scrobbles = result.Scrobbles;
                TracksView.DataSource = new ScrobbledTrackBindingList(scrobbles);
                MapToWmpButton.Enabled = hasWmp;
                MapToITunesButton.Enabled = hasiTunes;
            },
            error =>
            {
                string errorMessage = "";
                if (error is AggregateException)
                    foreach (var innerError in (error as AggregateException).InnerExceptions)
                        errorMessage += innerError.Message + Environment.NewLine;
                else
                    errorMessage = error.Message;

                MessageBox.Show("There was an error fetching this user's scrobbles." +
                                Environment.NewLine + Environment.NewLine +
                                "Details : " + errorMessage, "Error.",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            });
        }

        void MapToWmpButton_Click(object sender, EventArgs eventArgs)
        {
            if (wmpLibrary == null)
            {
                try
                {
                    wmpLibrary = new WmpLibraryManager();
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "The Windows Media Player interface could not be loaded." +
                        Environment.NewLine + Environment.NewLine +
                        "Either it could not be found or your version is not supported.",
                        "Could not load Windows Media player",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    hasWmp = false;
                    MapToWmpButton.Enabled = false;
                    return;
                }
            }
            MapToLibrary(wmpLibrary);
        }

        void MapToITunesButton_Click(object sender, EventArgs eventArgs)
        {
            if (iTunesLibrary == null)
            {
                try
                {
                    iTunesLibrary = new ItunesLibraryManager();
                }
                catch (Exception)
                {
                    MessageBox.Show(
                        "The iTunes interface could not be loaded." +
                        Environment.NewLine + Environment.NewLine +
                        "Either it could not be found or your version is not supported.",
                        "Could not load iTunes",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    hasiTunes = false;
                    MapToITunesButton.Enabled = false;
                    return;
                }
            }
            MapToLibrary(iTunesLibrary);
        }

        #endregion

        #region Actions

        void MapToLibrary(LibraryManager library)
        {
            TaskUtil.PerformForegroundTask(this, library.MapAsync(scrobbles),
                result => ChooseFuzzyMatches(library, result),
                error => MessageBox.Show("An error occured when updating the library." +
                                         Environment.NewLine + Environment.NewLine +
                                         "Details : " + error.Message, "Error!",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        void ChooseFuzzyMatches(LibraryManager library, MapResult result)
        {
            if (result != null && result.FuzzyMatches.Count() > 0)
            {
                TaskUtil.PerformForegroundTask(this, library.ChooseFuzzyMatchesAsync(this, result.FuzzyMatches),
                                fuzzyResult => ShowSummary(result, fuzzyResult),
                                error => MessageBox.Show("An error occured when updating the library." +
                                                         Environment.NewLine + Environment.NewLine +
                                                         "Details : " + error.Message, "Error!",
                                                         MessageBoxButtons.OK, MessageBoxIcon.Error));
            }
        }

        void ShowSummary(MapResult mapResult, ChooseFuzzyMatchesResult fuzzyResult)
        {
            var totalUpdated = mapResult.Updated + fuzzyResult.Updated;
            var totalUpToDate = mapResult.AlreadyUpToDate + fuzzyResult.AlreadyUpToDate;
            var totalFailed = mapResult.UpdateFailed + fuzzyResult.UpdateFailed;
            var totalSkipped = totalFailed + mapResult.NotFound;

            if (totalFailed > 0)
                new ErrorReporter(mapResult.Errors.Concat(fuzzyResult.Errors)).ShowDialog(this);

            MessageBox.Show(totalUpdated + " entries updated, " +
                            totalUpToDate + " were already up to date, " +
                            totalSkipped + " were skipped.",
                            "Mapping complete", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        #endregion

        #region Main Menu Events

        void AboutMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        void OptionsMenuItem_Click(object sender, EventArgs e)
        {
            using (var optionsDialog = new OptionsDialog { MinimumEditDistance = Settings.Default.MinimumEditDistance })
            {
                if (optionsDialog.ShowDialog(this) == DialogResult.OK)
                    Settings.Default.MinimumEditDistance = optionsDialog.MinimumEditDistance;
            }
        }

        #endregion
    }
}