using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ScrobbleMapper.LastFm;
using ScrobbleMapper.Library;
using ScrobbleMapper.Properties;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ScrobbleMapper.Forms
{
    partial class MainForm : Form
    {
        readonly ScrobbleFetcher fetcher;

        bool hasiTunes = true, hasWmp = true;

        WmpLibraryManager wmpLibrary;
        ItunesLibraryManager iTunesLibrary;

        ScrobbleArchive scrobbleData;

        public MainForm()
        {
            InitializeComponent();
            fetcher = new ScrobbleFetcher();

            ExportMenuItem.Enabled = false;
        }

        #region Form Events

        void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Settings.Default.Save();

            if (wmpLibrary != null)
                wmpLibrary.Dispose();

            if (iTunesLibrary != null)
                iTunesLibrary.Dispose();
        }

        void UsernameText_TextChanged(object sender, EventArgs eventArgs)
        {
            FetchButton.Enabled = UsernameText.Text != string.Empty;
        }

        void FetchButton_Click(object sender, EventArgs eventArgs)
        {
            var fromWeek = scrobbleData == null ? DateTime.MinValue : scrobbleData.LastWeekFetched;

            if (scrobbleData != null && UsernameText.Text != scrobbleData.Account)
            {
                if (MessageBox.Show(this,
                    string.Format("Username '{0}' doesn't match the Last.fm account whose scrobbles were last fetched ('{1}').\nFetching from account '{0}' will clear currently loaded data and start over from scratch.\n\nContinue?",
                        UsernameText.Text, scrobbleData.Account),
                    "Username mismatch", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                else
                    scrobbleData = null;
            }

            TaskUtil.PerformForegroundTask(this, fetcher.FetchAsync(UsernameText.Text, fromWeek), result =>
            {
                if (result.Errors.Count() > 0)
                    new ErrorReporter(result.Errors) { AlternateTaskName = "fetching the user's scrobbles.", AlternateItemName = "Weekly chart" }.ShowDialog();

                if (scrobbleData == null)
                {
                    scrobbleData = new ScrobbleArchive { Scrobbles = result.Scrobbles, Account = UsernameText.Text };
                    ExportMenuItem.Enabled = true;
                }
                else
                {
                    // merge scrobbles
                    var currentScrobbleSet = new Dictionary<ScrobbledTrack, ScrobbledTrack>(scrobbleData.Scrobbles.Count);
                    foreach (var scrobble in scrobbleData.Scrobbles)
                        currentScrobbleSet.Add(scrobble, scrobble);

                    foreach (var fetchedScrobble in result.Scrobbles)
                    {
                        ScrobbledTrack currentScrobble;
                        if (currentScrobbleSet.TryGetValue(fetchedScrobble, out currentScrobble))
                        {
                            currentScrobble.PlayCount += fetchedScrobble.PlayCount;
                            if (currentScrobble.WeekLastPlayed < fetchedScrobble.WeekLastPlayed)
                                currentScrobble.WeekLastPlayed = fetchedScrobble.WeekLastPlayed;
                        }
                        else
                            scrobbleData.Scrobbles.Add(fetchedScrobble);
                    }
                }
                scrobbleData.LastWeekFetched = result.LastWeekFetched;

                TracksView.DataSource = new ScrobbledTrackBindingList(scrobbleData.Scrobbles);
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
            TaskUtil.PerformForegroundTask(this, library.MapAsync(scrobbleData.Scrobbles),
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
            else
                ShowSummary(result ?? new MapResult(null, 0, 0, 0, 0, null), new ChooseFuzzyMatchesResult(0, 0, 0, null));
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

        void ImportMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "JSON (*.json)|*.json|XML (*.xml)|*.xml",
                CheckFileExists = true
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            TaskUtil.PerformForegroundTask(this, new ReportingTask<bool>
            {
                Task = Task.Factory.StartNew(() => ImportScrobbles(dialog.FileName)),
            },
            _ =>
            {
                TracksView.DataSource = new ScrobbledTrackBindingList(scrobbleData.Scrobbles);
                MapToWmpButton.Enabled = hasWmp;
                MapToITunesButton.Enabled = hasiTunes;
                ExportMenuItem.Enabled = true;
            },
            error =>
            {
                MessageBox.Show("An error occured while importing scrobbles." +
                                         Environment.NewLine + Environment.NewLine +
                                         "Details : " + error.Message, "Error!",
                                         MessageBoxButtons.OK, MessageBoxIcon.Error);
            });
        }

        bool ImportScrobbles(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);

            switch (fileInfo.Extension)
            {
                case ".xml":
                    var document = XDocument.Load(filename);
                    var documentElement = document.Element("ScrobbleArchive");

                    scrobbleData = new ScrobbleArchive
                    {
                        Account = documentElement.Element("Account").Value,
                        LastWeekFetched = DateTime.Parse(documentElement.Element("LastWeekFetched").Value),
                        Scrobbles = (
                            from element in documentElement.Element("Scrobbles").Elements("ScrobbledTrack")
                            select new ScrobbledTrack(
                                element.Element("Artist").Value,
                                element.Element("Title").Value,
                                int.Parse(element.Element("PlayCount").Value),
                                DateTime.Parse(element.Element("WeekLastPlayed").Value))
                        ).ToList()
                    };
                    break;

                case ".json":
                    using (var reader = File.OpenText(filename))
                    {
                        var root = JObject.Load(new JsonTextReader(reader));

                        scrobbleData = new ScrobbleArchive
                        {
                            Account = root["account"].Value<string>(),
                            LastWeekFetched = root["lastWeekFetched"].Value<DateTime>(),
                            Scrobbles = (
                                from item in root["scrobbles"].Children()
                                select new ScrobbledTrack(
                                    item["artist"].Value<string>(),
                                    item["title"].Value<string>(),
                                    item["playCount"].Value<int>(),
                                    item["weekLastPlayed"].Value<DateTime>())
                            ).ToList()
                        };
                    }
                    break;
            }

            return true;
        }

        void ExportMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "JSON (*.json)|*.json|XML (*.xml)|*.xml"
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
                return;

            TaskUtil.PerformForegroundTask(this, new ReportingTask<bool>
            {
                Task = Task.Factory.StartNew(() => ExportScrobbles(dialog.FileName)),
            },
            ActionUtil.NullAction,
            error => MessageBox.Show("An error occured while exporting scrobbles." +
                                     Environment.NewLine + Environment.NewLine +
                                     "Details : " + error.Message, "Error!",
                                     MessageBoxButtons.OK, MessageBoxIcon.Error));
        }

        bool ExportScrobbles(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);

            switch (fileInfo.Extension)
            {
                case ".xml":
                    var document = new XDocument(
                        new XDeclaration("1.0", "UTF-8", "yes"),
                        new XElement("ScrobbleArchive",
                            new XElement("Account", scrobbleData.Account),
                            new XElement("LastWeekFetched", scrobbleData.LastWeekFetched),
                            new XElement("Scrobbles",
                                from scrobble in scrobbleData.Scrobbles
                                select new XElement("ScrobbledTrack",
                                    new XElement("Artist", scrobble.Artist),
                                    new XElement("Title", scrobble.Title),
                                    new XElement("PlayCount", scrobble.PlayCount),
                                    new XElement("WeekLastPlayed", scrobble.WeekLastPlayed)))));

                    using (var stream = File.CreateText(filename))
                    using (var writer = XmlWriter.Create(stream, new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true }))
                    {
                        document.WriteTo(writer);
                    }
                    break;

                case ".json":
                    var data = new JObject(
                        new JProperty("account", scrobbleData.Account),
                        new JProperty("lastWeekFetched", scrobbleData.LastWeekFetched),
                        new JProperty("scrobbles",
                            new JArray(
                                from scrobble in scrobbleData.Scrobbles
                                select new JObject(
                                    new JProperty("artist", scrobble.Artist),
                                    new JProperty("title", scrobble.Title),
                                    new JProperty("playCount", scrobble.PlayCount),
                                    new JProperty("weekLastPlayed", scrobble.WeekLastPlayed)))));

                    using (var stream = File.CreateText(filename))
                    using (var writer = new JsonTextWriter(stream) { Formatting = Newtonsoft.Json.Formatting.Indented })
                    {
                        data.WriteTo(writer);
                    }
                    break;
            }

            return true;
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

        void AboutMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        #endregion
    }
}