using System;
using System.Collections.Generic;
//using System.Web.Script.Serialization;

namespace DAL
{
    public class MovieShortInfo
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public byte[] Cover { get; set; }

        public bool HasPoster { get; set; }
    }

    public class SeriesEpisodesShortInfo
    {
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

    public class MovieTechnicalDetails
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string FileName { get; set; }
        public string InitialPath { get; set; }
        public string Year { get; set; }
        public string Format { get; set; }
        public string Encoded_Application { get; set; }
        public string FileSize { get; set; } //totals
        public string FileSize2 { get; set; }

        public string Duration { get; set; }
        public int DurationAsInt
        {
            get
            {
                return int.TryParse(Duration, out var tmpDbl) ? tmpDbl : 0;
            }
        }

        public DateTime DurationAsDateTime
        {
            get
            {
                return new DateTime(1970, 1, 1).AddMilliseconds(DurationAsInt);
            }
        }

        private string _durationFormatted = string.Empty;

        public string DurationFormatted
        {
            get
            {
                return string.IsNullOrEmpty(_durationFormatted)
                    ? DurationAsDateTime.ToString("HH:mm:ss")
                    : _durationFormatted;
            }
            set
            {
                _durationFormatted = value;
            }
        }

        //duration from format in DB on save !!!

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                HasTitle = !string.IsNullOrEmpty(value);
            }
        }
        public bool HasTitle { get; set; }

        private string _cover;
        public string Cover
        {
            get
            {
                return _cover;
            }
            set
            {
                _cover = value;
                HasCover = !string.IsNullOrEmpty(value);
            }
        }
        public bool HasCover { get; set; }

        public string Season { get; set; }
        public string Theme { get; set; }
        public string StreamLink { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public string Quality { get; set; }
        public string Recommended { get; set; }
        public string RecommendedLink { get; set; }
        public string DescriptionLink { get; set; }
        public string Notes { get; set; }
        public byte[] Poster { get; set; }
        public string AudioLanguages { get; set; }
        public string SubtitleLanguages { get; set; }


        public List<VideoStreamInfo> VideoStreams { get; set; }
        public List<AudioStreamInfo> AudioStreams { get; set; }
        public List<SubtitleStreamInfo> SubtitleStreams { get; set; }
        public List<byte[]> MovieStills {get; set;}


        public MovieTechnicalDetails()
        {
            Quality = "NotSet";
            VideoStreams = new List<VideoStreamInfo>();
            AudioStreams = new List<AudioStreamInfo>();
            SubtitleStreams = new List<SubtitleStreamInfo>();
            MovieStills = new List<byte[]>();
        }
    }

    public class VideoStreamInfo
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Format { get; set; }
        public string Format_Profile { get; set; }
        public string BitRateMode { get; set; }
        public string BitRate { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
        public string FrameRate_Mode { get; set; }
        public string FrameRate { get; set; }
        public string Delay { get; set; }
        public string StreamSize { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                HasTitle = !string.IsNullOrEmpty(value);
            }
        }

        public bool HasTitle { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return $"{Index}. {Width}x{Height} - {BitRate}";
        }
    }

    public class AudioStreamInfo
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Format { get; set; }

        // (VBR, CBR)
        public string BitRate { get; set; }
        public string Channel { get; set; }
        public string ChannelPosition { get; set; }
        public string SamplingRate { get; set; }
        public string Resolution { get; set; }
        public string Delay { get; set; }
        public string Video_Delay { get; set; }
        public string StreamSize { get; set; }

        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                HasTitle = !string.IsNullOrEmpty(value);
            }
        }

        public bool HasTitle { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return $"{Index}. {Language} - {Format} - {BitRate} - {Channel}";
        }
    }

    public class SubtitleStreamInfo
    {
        public int Id { get; set; }
        public int Index { get; set; }
        public string Format { get; set; }
        public string StreamSize { get; set; }
        private string _title;
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                HasTitle = !string.IsNullOrEmpty(value);
            }
        }

        public bool HasTitle { get; set; }
        public string Language { get; set; }

        public override string ToString()
        {
            return $"{Index}. {Language} - {Format}";
        }
    }

    public class FilesImportParams
    {
        public string Location { get; set; }
        public string FilesExtension { get; set; }
        public int? ParentId { get; set; }
        public string Season { get; set; }
        public string Year { get; set; }
        public bool GenerateThumbnail { get; set; }
    }

    public class CachedMovieStills
    {
        public int FileDetailId { get; set; }
        public List<byte[]> MovieStills { get; set; }

        public CachedMovieStills()
        {
            MovieStills = new List<byte[]>();
        }
    }
}