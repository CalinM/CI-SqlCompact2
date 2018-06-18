using System;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Desene.DetailFormsAndUserControls
{
    //private
    public partial class ucSeriesEpisodes : UserControl
    {
        private BindingSource _bsEpisodesGridData;
        private ucSeries _parent;

        public ucSeriesEpisodes()
        {
            InitializeComponent();
        }

        public ucSeriesEpisodes(int seriesId, ucSeries parent)
        {
            InitializeComponent();

            _parent = parent;

            dgvEpisodes.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvEpisodes, true, null);
            dgvEpisodes.AutoGenerateColumns = false;

            _bsEpisodesGridData = new BindingSource();
            dgvEpisodes.DataSource = _bsEpisodesGridData;

            LoadControls(seriesId);
        }

        public void LoadControls(int seriesId)
        {
            var episodesInSeries = DAL.GetEpisodesInSeries(seriesId);

            if (episodesInSeries.Rows.Count > 0)
            {
                _bsEpisodesGridData.DataSource = episodesInSeries;
                _bsEpisodesGridData.ResetBindings(false);
                dgvEpisodes.Visible = true;
                lbNoEpisodeWarning.Visible = false;
            }
            else
            {
                dgvEpisodes.Visible = false;
                lbNoEpisodeWarning.Visible = true;
            }
        }

        private void dgvEpisodes_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            //https://stackoverflow.com/questions/7412098/fit-datagridview-size-to-rows-and-columnss-total-size
            dgvEpisodes.Height = dgvEpisodes.Rows.GetRowsHeight(DataGridViewElementStates.None) + dgvEpisodes.ColumnHeadersHeight + 6;

            Size = new Size
            {
                Height = dgvEpisodes.Height + dgvEpisodes.Location.Y + 10,
                Width = 150
            };
        }

        private void dgvEpisodes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) {
                var row = (DataRowView)dgvEpisodes.Rows[e.RowIndex].DataBoundItem;

                if (row != null) {
                    _parent.TryLocateEpisodeInTree((int)row["Id"]);
                }
            }
        }
    }
}
