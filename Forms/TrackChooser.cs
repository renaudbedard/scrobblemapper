using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ScrobbleMapper.Library;

namespace ScrobbleMapper.Forms
{
    partial class TrackChooser : Form
    {
        public IEnumerable<ILibraryTrack> ChosenLibraryTracks { get; private set; }

        public TrackChooser()
        {
            InitializeComponent();
        }

        public bool DiscardRemaining
        {
            get { return DiscardReminaingCheck.Checked; }
            set { DiscardReminaingCheck.Checked = value; }
        }

        public bool AutoUpdateSingleMatches
        {
            get { return AutoUpdateSingleMatchesCheck.Checked; }
            set { AutoUpdateSingleMatchesCheck.Checked = value; }
        }

        public bool AutoUpdateFullyRelevant
        {
            get { return AutoUpdateFullyRelevantCheck.Checked; }
            set { AutoUpdateFullyRelevantCheck.Checked = value; }
        }

        FuzzyMatch fuzzyMatch;
        public FuzzyMatch FuzzyMatch
        {
            set
            {
                fuzzyMatch = value;
                ChosenLibraryTracks = Enumerable.Empty<ILibraryTrack>();

                ArtistLabel.Text = fuzzyMatch.Scrobble.Artist;
                TitleLabel.Text = fuzzyMatch.Scrobble.Title;
                UpdateButton.Enabled = false;

                MatchesList.Items.Clear();
                foreach (var match in fuzzyMatch.Matches)
                    MatchesList.Items.Add(new ListViewItem(new[] {
                                                                    match.Track.Artist, 
                                                                    match.Track.Title, 
                                                                    match.Track.Album, 
                                                                    match.Relevance.ToString("##0%")
                                                                 }));
                AutoSelect();
            }
        }

        public int FilesLeft
        {
            set { FilesLeftLabel.Text = value.ToString(); }
        }

        void UpdateButton_Click(object sender, EventArgs e)
        {
            ChooseTracks();
        }

        void MatchesList_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateButton.Enabled = MatchesList.SelectedIndices.Count > 0;
        }

        void AutoUpdateFullyRelevantCheck_CheckedChanged(object sender, EventArgs e)
        {
            RelevanceBoundChooser.Enabled = AutoUpdateFullyRelevant;
            AutoSelect();
        }

        void MatchesList_DoubleClick(object sender, EventArgs e)
        {
            ChooseTracks();
            DialogResult = DialogResult.OK;
        }

        void ChooseTracks()
        {
            ChosenLibraryTracks = fuzzyMatch.Matches
                .Where((x, i) => MatchesList.SelectedIndices.Contains(i))
                .Select(x => x.Track);
        }

        void AutoSelect()
        {
            MatchesList.SelectedIndices.Clear();

            bool foundAny = false;
            if (AutoUpdateFullyRelevant)
            {
                var bound = (int)RelevanceBoundChooser.Value / 100f;

                foreach (var index in fuzzyMatch.Matches
                        .Where(x => x.Relevance > bound)
                        .Select((x, i) => i))
                {
                    MatchesList.SelectedIndices.Add(index);
                    foundAny = true;
                }
            }

            if (foundAny)
                ChooseTracks();
            else
                MatchesList.SelectedIndices.Add(0);
        }
    }
}