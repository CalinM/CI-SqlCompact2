using Aga.Controls.Tree;

using DAL;

namespace Desene
{
    public class SeriesTreeModel: TreeModelBase
	{
        private bool _showingCollections;

        public SeriesTreeModel(bool b = false)
        {
            _showingCollections = b;
        }

        public override System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            if (_showingCollections)
            {
                if (treePath.IsEmpty())
                {
                    return DAL.GetCollectionsInfo();
                }

                var n = (SeriesEpisodesShortInfo)treePath.LastNode;

                return DAL.GetElementsInCollection(n.Id, n.SectionType); //changed to another root (Collection)
            }
            else
            {
                if (treePath.IsEmpty())
                {
                    return DAL.GetSeriesInfo();
                }

                var n = (SeriesEpisodesShortInfo)treePath.LastNode;

                return
                    string.IsNullOrEmpty(n.Season)
                        ? DAL.GetSeasonsForSeries(n.Id) //changed to another root (Series)
                        : DAL.GetEpisodesInSeason(n.SeriesId, n.Season);
            }
        }

        public override bool IsLeaf(TreePath treePath)
        {
            var n = (SeriesEpisodesShortInfo)treePath.LastNode;

            return n != null && n.IsEpisode;
        }
    }
}
