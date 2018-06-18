using Aga.Controls.Tree;

using DAL;

namespace Desene
{
    public class SeriesTreeModel: TreeModelBase
	{
        public override System.Collections.IEnumerable GetChildren(TreePath treePath)
        {
            if (treePath.IsEmpty() )
            {
                return DAL.GetSeriesInfo();
            }

            var n = (SeriesEpisodesShortInfo)treePath.LastNode;

            return
                n.Season == -1
                    ? DAL.GetSeasonsForSeries(n.Id) //changed to another root
                    : DAL.GetEpisodesInSeason(n.SeriesId, n.Season);

        }

        public override bool IsLeaf(TreePath treePath)
        {
            var n = (SeriesEpisodesShortInfo)treePath.LastNode;

            return n != null && n.IsEpisode;
        }
    }
}
