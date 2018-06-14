using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Desene.DetailFormsAndUserControls
{
    //private
    public partial class ucSeriesEpisodes : UserControl
    {
        private BindingSource _bsEpisodesGridData;

        public ucSeriesEpisodes()
        {
            InitializeComponent();
        }

        public ucSeriesEpisodes(int seriesId)
        {
            InitializeComponent();

            dgvEpisodes.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvEpisodes, true, null);
            dgvEpisodes.AutoGenerateColumns = false;

            _bsEpisodesGridData = new BindingSource();
            dgvEpisodes.DataSource = _bsEpisodesGridData;

            LoadControls(seriesId);
        }

        public void LoadControls(int seriesId)
        {
            try
            {
                _bsEpisodesGridData.DataSource
                    = DAL.Series
                         .Select("ParentId = " + seriesId)
                         .OrderBy(u => u["Season"])
                         .ThenBy(u => u["FileName"])
                         .CopyToDataTable();

                _bsEpisodesGridData.ResetBindings(false);
            }
            catch (Exception e)
            {
                //the series contains no episodes!
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
    }
}
