using System;

using DAL;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace Desene.DetailFormsAndUserControls.Shared
{
    public partial class ucGenericStreamsWrapper : UserControl
    {
        public ucGenericStreamsWrapper()
        {
            InitializeComponent();
        }
        public ucGenericStreamsWrapper(List<VideoStreamInfo> videoStreams)
        {
            InitializeComponent();
            LoadControlsForVideoStreams(videoStreams);
        }

        public ucGenericStreamsWrapper(List<AudioStreamInfo> audioStreams)
        {
            InitializeComponent();
            LoadControlsForAudioStreams(audioStreams);

        }

        public ucGenericStreamsWrapper(List<SubtitleStreamInfo> subtitleStreams)
        {
            InitializeComponent();
            LoadControlsForSubtitleStreams(subtitleStreams);

        }

        public void LoadControlsForVideoStreams(List<VideoStreamInfo> videoStreams)
        {
            if (videoStreams.Count < flpWrapper.Controls.Count)
            {
                for (var i = flpWrapper.Controls.Count - 1; i >= 0; i--)
                {
                    if (!int.TryParse(flpWrapper.Controls[i].Tag.ToString(), out var idx)) continue;

                    if (idx > videoStreams.Count)
                        flpWrapper.Controls.Remove(flpWrapper.Controls[i]);
                }
            }

            for (var i = 0; i < videoStreams.Count; i++)
            {
                var vsData = videoStreams[i];
                var vsDetUC = flpWrapper.Controls.OfType<UserControl>().FirstOrDefault(uc => uc.Tag != null && uc.Tag.ToString() == vsData.Index.ToString());

                if (vsDetUC == null)
                {
                    flpWrapper.Controls.Add(new ucVideoStreamDetail(vsData) { Tag = vsData.Index.ToString() });
                }
                else
                {
                    //((ucVideoStreamDetail)vsDetUC).LoadControls(vsData);
                    ((ucVideoStreamDetail)vsDetUC).RefreshControls(vsData);
                }
            }
        }

        public void LoadControlsForAudioStreams(List<AudioStreamInfo> audioStreams)
        {
            //var watch = System.Diagnostics.Stopwatch.StartNew();

            if (audioStreams.Count < flpWrapper.Controls.Count)
            {
                for (var i = flpWrapper.Controls.Count - 1; i >= 0; i--)
                {
                    if (!int.TryParse(flpWrapper.Controls[i].Tag.ToString(), out var idx)) continue;

                    if (idx > audioStreams.Count)
                        flpWrapper.Controls.Remove(flpWrapper.Controls[i]);
                }
            }

            for (var i = 0; i < audioStreams.Count; i++)
            {
                var asData = audioStreams[i];
                var asDetUC = flpWrapper.Controls.OfType<UserControl>().FirstOrDefault(uc => uc.Tag != null && uc.Tag.ToString() == asData.Index.ToString());

                if (asDetUC == null)
                {
                    flpWrapper.Controls.Add(new ucAudioStreamDetail(asData) { Tag = asData.Index.ToString() });
                }
                else
                {
                    ((ucAudioStreamDetail)asDetUC).RefreshControls(asData);
                }
            }

            //watch.Stop();
            //Debug.WriteLine(watch.ElapsedMilliseconds);
        }

        public void LoadControlsForSubtitleStreams(List<SubtitleStreamInfo> subtitleStreams)
        {
            //flpWrapper.Controls.Clear();

            //foreach (var ssData in subtitleStreams)
            //    flpWrapper.Controls.Add(new ucSubtitleStreamDetail(ssData));


            if (subtitleStreams.Count < flpWrapper.Controls.Count)
            {
                for (var i = flpWrapper.Controls.Count - 1; i >= 0; i--)
                {
                    if (!int.TryParse(flpWrapper.Controls[i].Tag.ToString(), out var idx)) continue;

                    if (idx > subtitleStreams.Count)
                        flpWrapper.Controls.Remove(flpWrapper.Controls[i]);
                }
            }

            for (var i = 0; i < subtitleStreams.Count; i++)
            {
                var ssData = subtitleStreams[i];
                var ssDetUC = flpWrapper.Controls.OfType<UserControl>().FirstOrDefault(uc => uc.Tag != null && uc.Tag.ToString() == ssData.Index.ToString());

                if (ssDetUC == null)
                {
                    flpWrapper.Controls.Add(new ucSubtitleStreamDetail(ssData) { Tag = ssData.Index.ToString() });
                }
                else
                {
                    ((ucSubtitleStreamDetail)ssDetUC).RefreshControls(ssData);
                }
            }
        }
    }
}
