using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using Aga.Controls.Tree;
using DAL;

namespace Desene
{
    public class FilteredTreeModel: TreeModelBase
    {
        private List<SeriesEpisodesShortInfo> _filterResult;
        private readonly string _filterBy;
        private bool _showingCollections;

        public FilteredTreeModel(string filterBy, bool b)
        {
            _filterBy = filterBy;
            _showingCollections = b;
        }

        public override System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            if (_filterResult == null)
                _filterResult = DAL.GetFilteredFileNames(_filterBy, _showingCollections);

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