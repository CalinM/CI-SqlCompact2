using DAL;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace Desene
{
    public partial class ucMovies : UserControl
    {
        private BindingList<MovieShortInfo> _moviesData;
        public ucMovies()
        {
            InitializeComponent();

            dgvMoviesList.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvMoviesList, true, null);
            dgvMoviesList.AutoGenerateColumns = false;
        }

        private void ucMovies_Load(object sender, EventArgs e)
        {
            _moviesData = DAL.GetMoviesGridData();

            //dgvMoviesList.Columns.Clear();
            dgvMoviesList.DataSource = _moviesData;
            dgvMoviesList.Invalidate();
        }

        private void dgvMoviesList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            var rowObj = ((DataGridView)sender).Rows[e.RowIndex].DataBoundItem;
            if (rowObj == null) return;

            dgvMoviesList.Rows[e.RowIndex].DefaultCellStyle.BackColor = ((MovieShortInfo)rowObj).HasPoster ? Color.White : Color.LightPink;
            //var rowItem = ((DataRowView)rowObj).Row;
            //if (rowItem["Poster"] != DBNull.Value) return;

            //dgvMoviesList.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightPink;
        }
    }
}
