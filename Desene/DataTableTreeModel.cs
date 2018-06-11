using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Aga.Controls.Tree;

namespace Desene
{
    public class DataTableTreeModel: TreeModelBase
	{
        private readonly List<FileInfoNode> _roots = new List<FileInfoNode>();

		public DataTableTreeModel()
		{
            var rows = DAL.Series.Select("ParentId = -1").OrderBy(u => u["FileName"]).ToArray();

		    foreach (var row in rows)
		    {
                var rootElement = new FileInfoNode(row , row["FileName"].ToString(), row["Theme"].ToString(), row["Quality"].ToString(),
                    -1, -1); //Convert.ToInt32(row["Id"])
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

                var selectedNodeDataRow = n.Row;
                var id = Convert.ToInt32(selectedNodeDataRow["Id"]);

                if (n.Season == -1)
                {
                    var rows = DAL.Series.Select("ParentId = " + id + " and Id <> " + id);//.OrderBy(u => u["FileName"]).ToArray();
                    if (rows.Length <= 0) return items;

                    var seasons = rows.CopyToDataTable().DefaultView.ToTable(true, "Season");

                    for (var i = 0; i < seasons.Rows.Count; i++)
                    {
                        var seasonId = int.Parse(seasons.Rows[i]["Season"].ToString());

                        var node = new FileInfoNode(selectedNodeDataRow, string.Format("Season {0}", seasonId), string.Empty, string.Empty,
                            seasonId, id);
                        node.IsSeason = true;

                        items.Add(node);
                    }
                }
                else
                {
                    var rows = DAL.Series.Select("ParentId = " + id + " and Id <> " + id + " and Season = " + n.Season).OrderBy(u => u["FileName"]).ToArray();
                    if (rows.Length <= 0) return items;

                    foreach (var row in rows)
                    {
                        var node = new FileInfoNode(row, row["FileName"].ToString(), row["Theme"].ToString(), row["Quality"].ToString(),
                            -1, id);
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

            return n != null && (int)n.Row["ParentId"] > 0;
        }
    }

    public class FileInfoNode
    {
        //not taking the data from "row", because the rest of the parameters can hold different values while creating the fake nodes for seasons
        public FileInfoNode(DataRow row, string fileName, string theme, string quality, int season, int seriesId)
        {
            Row = row;
            FileName = fileName;
            Theme = theme;
            Quality = quality;
            Season = season;
            SeriesId = seriesId; //WRONG for episodes?
        }

        private DataRow _row;
        public DataRow Row
        {
            get { return _row; }
            set
            {
                _row = value;
                Id = (int)_row["Id"];
            }
        }

        public int Id { get; set; }
        public string FileName { get; set; }
        public string Theme { get; set; }
        public string Quality { get; set; }
        public int Season { get; set; }
        public int SeriesId {get; set;}

        public bool IsSeries { get; set; }
        public bool IsSeason { get; set; }
        public bool IsEpisode { get; set; }
    }
}
