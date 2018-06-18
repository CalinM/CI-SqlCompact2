using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Aga.Controls.Tree;

using Common.ExtensionMethods;

using DAL;

namespace Desene
{
    public class FilteredTreeModel: TreeModelBase
    {
        private List<SeriesEpisodesShortInfo> _filterResult;
        private readonly string _filterBy;

        public FilteredTreeModel(string filterBy)
        {
            _filterBy = filterBy;
        }

        public override System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            if (_filterResult == null)
                _filterResult = DAL.GetFilteredSeriesEpisodes(_filterBy);

            if (!_filterResult.Any())
                return _filterResult;

            if (treePath.IsEmpty())
                return _filterResult.Where(r => r.IsSeries).ToList();

            var n = (SeriesEpisodesShortInfo)treePath.LastNode;

            return n.IsSeries
                ? _filterResult.Where(r => r.IsSeason && r.SeriesId == n.Id).ToList()
                : _filterResult.Where(r => r.IsEpisode && r.Season == n.Season && r.SeriesId == n.Id).ToList();
        }

        public override bool IsLeaf(TreePath treePath)
        {
            var n = (SeriesEpisodesShortInfo)treePath.LastNode;

            return n != null && n.IsEpisode;
        }
    }
}
/*
{
    public class DataTableTreeModel: TreeModelBase
	{
        //private readonly List<FileInfoNode> _roots = new List<FileInfoNode>();
        private DataTable _filteredSeries;


		public DataTableTreeModel(string filterBy)
		{
            _filteredSeries = DAL.GetFilteredSeriesEpisodes(filterBy);

            using (var conn = new SqlCeConnection(Constants.ConnectionString))
            {
                conn.Open();

                //loading Series AND Episodes!
                var cmd = new SqlCeCommand("select * from FileDetail where ParentId is not null", conn);
                var commandSource = new SqlCeCommand(string.Format("SELECT * FROM FileDetail WHERE ParentId = {0} AND Season = {1} ORDER BY FileName", seriesId, seasonVal), conn);

                using (var reader = cmd.ExecuteReader())

                var rows = DAL.Series.Select("ParentId = -1").OrderBy(u => u["FileName"]).ToArray();

		    foreach (var row in rows)
		    {
                var rootElement = new FileInfoNode(Convert.ToInt32(row["Id"]), row["FileName"].ToString(), row["Theme"].ToString(),
                    row["Quality"].ToString(), -1, -1);
                rootElement.IsSeries = true;

		        _roots.Add(rootElement);
		    }
		}

        public override System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            var items = new List<FileInfoNode>();

            if (treePath.IsEmpty() )
            {
                items.AddRange(_roots);
            }
            else
            {
                var n = (FileInfoNode)treePath.LastNode;

                if (n.Season == -1)
                {
                    var rows = DAL.Series.Select("ParentId = " + n.Id + " and Id <> " + n.Id);//.OrderBy(u => u["FileName"]).ToArray();
                    if (rows.Length <= 0) return items;

                    var seasons = rows.CopyToDataTable().DefaultView.ToTable(true, "Season");

                    for (var i = 0; i < seasons.Rows.Count; i++)
                    {
                        var seasonId = int.Parse(seasons.Rows[i]["Season"].ToString());

                        //same Id as the Series node !
                        var node = new FileInfoNode(n.Id, string.Format("Season {0}", seasonId), string.Empty, string.Empty,
                            seasonId, n.Id);
                        node.IsSeason = true;

                        items.Add(node);
                    }
                }
                else
                {
                    var rows = DAL.Series.Select("ParentId = " + n.Id + " and Id <> " + n.Id + " and Season = " + n.Season).OrderBy(u => u["FileName"]).ToArray();
                    if (rows.Length <= 0) return items;

                    foreach (var row in rows)
                    {
                        var episodeId = int.Parse(row["Id"].ToString());

                        var node = new FileInfoNode(episodeId, row["FileName"].ToString(), row["Theme"].ToString(), row["Quality"].ToString(),
                            -1, n.Id);
                        node.IsEpisode = true;

                        items.Add(node);
                    }
                }
            }

            return items;
        }

        public override bool IsLeaf(TreePath treePath)
        {
            var n = (FileInfoNode)treePath.LastNode;

            return n != null && n.IsEpisode;
        }
    }

}
*/