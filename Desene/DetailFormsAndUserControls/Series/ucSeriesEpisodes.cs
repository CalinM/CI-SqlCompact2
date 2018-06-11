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
        public ucSeriesEpisodes()
        {
            InitializeComponent();
        }

        public ucSeriesEpisodes(int seriesId)
        {
            InitializeComponent();

            dgvEpisodes.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvEpisodes, true, null);
            dgvEpisodes.AutoGenerateColumns = false;
            LoadControls(seriesId);
        }

        public void LoadControls(int seriesId)
        {
            //var rows = DAL.Series.Select("ParentId = " + seriesId);// + " and Id <> " + id);//.OrderBy(u => u["FileName"]).ToArray();
            //var seasonsList = new List<string>();

            //var seasons = rows.CopyToDataTable().DefaultView.ToTable(true, "Season");

            //for (var i = 0; i < seasons.Rows.Count; i++)
            //{
            //    seasonsList.Add(seasons.Rows[i]["Season"].ToString());
            //}

            //foreach (var season in seasonsList.OrderBy(o => o))
            //{
            //    var episodesInSeason = DAL.Series.Select("ParentId = " + seriesId + " and Season = " + season).OrderBy(u => u["FileName"]).CopyToDataTable();
            //}

            //var rows = DAL.Series.Select("ParentId = " + seriesId).OrderBy(u => u["FileName"]).ToArray();
            //foreach (var row in rows)
            //{
            //    Controls.Add(new ucEpisodeLine(row) { Dock = DockStyle.Top });
            //}

            try
            {
                dgvEpisodes.DataSource = DAL.Series.Select("ParentId = " + seriesId).OrderBy(u => u["Season"]).ThenBy(u => u["FileName"]).CopyToDataTable();
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
