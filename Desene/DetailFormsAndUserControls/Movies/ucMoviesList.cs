using Common.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Utils.DataGridViewAutoFilter;

namespace Desene.DetailFormsAndUserControls.Movies
{
    public partial class ucMoviesList : UserControl
    {
        private FrmMain _parent;
        private DataTable _gridData;
        private DataTable _unfilteredData;
        private Timer _genericTimer;
        private bool _preventEvent;

        //private BindingList<MovieForWeb> _gridData;
        //private BindingList<MovieForWeb> _unfilteredData;

        public ucMoviesList(FrmMain parent)
        {
            InitializeComponent();

            _parent = parent;

            dgvMoviesList.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(dgvMoviesList, true, null);
            dgvMoviesList.AutoGenerateColumns = false;
                    //    dgvMoviesList.EnableHeadersVisualStyles = false;
        }

        private void ucMoviesList_Load(object sender, System.EventArgs e)
        {
            _gridData = DAL.GetMoviesForWeb().ToDataTable();
            _gridData.PrimaryKey = new[] {_gridData.Columns["Id"] };

            var dataSource = new BindingSource(_gridData, null);
            dgvMoviesList.DataSource = dataSource;
        }

        private void dgvMoviesList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            var filterStatus = DataGridViewAutoFilterColumnHeaderCell.GetFilterStatus(dgvMoviesList);

            var currentFilter = (dgvMoviesList.DataSource as BindingSource).Filter;

            if (string.IsNullOrEmpty(currentFilter))
            {
                currentFilter = string.Empty;

                if (_unfilteredData == null)
                    _parent.SetStatistics(true, "No filters applied.");

                tbFilter.Enabled = true;
            }

            var headerCaptionWithFilters = new List<string>();

            foreach (DataGridViewTextBoxColumn gridCol in dgvMoviesList.Columns)
            {
                var fieldNameInFilter = string.Format("[{0}]=", gridCol.DataPropertyName);
                if (currentFilter.IndexOf(fieldNameInFilter) >= 0)
                {
                    //gridCol.HeaderCell.Style.BackColor = Color.Red;
                    gridCol.HeaderCell.Style.Font = new Font(DefaultFont, FontStyle.Bold);
                    headerCaptionWithFilters.Add(gridCol.HeaderText);
                }
                else
                {
                    if (gridCol.HeaderCell.Style.Font != null && gridCol.HeaderCell.Style.Font.Bold)
                    {
                        gridCol.HeaderCell.Style.Font = new Font(DefaultFont, FontStyle.Regular);
                    }
                }
            }

            if (!string.IsNullOrEmpty(currentFilter))
            {
                tbFilter.Enabled = false;

                _parent.SetStatistics(true,
                    headerCaptionWithFilters.Count > 1
                        ? string.Format("Filters applied on the following columns: {0}. {1}'", string.Join(", ", headerCaptionWithFilters), filterStatus)
                        : string.Format("Filters applied on the {0} column. {1}'", headerCaptionWithFilters[0], filterStatus)
                    );
            }

            //"[Y]='2017'"
        }

        private void tbFilter_TextChanged(object sender, System.EventArgs e)
        {
            if (_preventEvent) return;

            if (_genericTimer != null)
            {
                _genericTimer.Enabled = false;
                _genericTimer = null;
            }

            _genericTimer = new Timer
            {
                Interval = 1000
            };

            _genericTimer.Tick += FilterMovieTiles;
            _genericTimer.Enabled = true;
        }

        private void FilterMovieTiles(object sender, EventArgs e)
        {
            SetUnsetFilterOnAllColumns();
        }

        private void SetUnsetFilterOnAllColumns()
        {
            if (_genericTimer != null)
            {
                _genericTimer.Enabled = false;
                _genericTimer = null;
            }

            if (!string.IsNullOrEmpty(tbFilter.Text))
            {
                if (_unfilteredData == null)
                {
                    _unfilteredData = _gridData.Copy();
                }

                var filterAll = new List<string>();

                foreach (DataGridViewTextBoxColumn gridCol in dgvMoviesList.Columns)
                {
                    if (gridCol.DataPropertyName == "Id") continue;

                    filterAll.Add(string.Format("{0} like '%{1}%'", gridCol.DataPropertyName, tbFilter.Text));
                }

                _gridData = _unfilteredData.Select(string.Join(" OR ", filterAll)).CopyToDataTable();
                _parent.SetStatistics(true, string.Format("Filter applied on all columns. {0} of {1} records found", _gridData.Rows.Count, _unfilteredData.Rows.Count));
            }
            else
            {
                _gridData = _unfilteredData.Copy();
                _unfilteredData = null;
                _parent.SetStatistics(true, "No filters applied.");
            }

            var dataSource = new BindingSource(_gridData, null);
            dgvMoviesList.DataSource = dataSource;
        }

        private void tbFilter_ButtonClick(object sender, EventArgs e)
        {
            try
            {
                _preventEvent = true;

                tbFilter.Text = "";
                SetUnsetFilterOnAllColumns();
            }
            finally
            {
                _preventEvent = false;
            }

            var dataSource = new BindingSource(_gridData, null);
            dgvMoviesList.DataSource = dataSource;
        }
    }
}
