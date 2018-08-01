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
    //public class Film
    //{
    //    public int Id { get; set; }

    //    public string Titlu { get; set; }

    //    private string _recomandat = string.Empty;
    //    public string Recomandat
    //    {
    //        get { return _recomandat; }
    //        set
    //        {
    //            _recomandat = value;

    //            if (string.IsNullOrEmpty(value))
    //            {
    //                RecomandatInt = 0;
    //            }
    //            else
    //            {
    //                var str = Helpers.StripNonNumeric(value);

    //                RecomandatInt = string.IsNullOrEmpty(str) ? 0 : int.Parse(str);
    //            }
    //        }
    //    }

    //    public string RecomandatLink { get; set; }

    //    [ScriptIgnore]
    //    public int RecomandatInt { get; set;  }

    //    public string An { get; set; }

    //    public Calitate Calitate { get; set; }

    //    public string Rezolutie { get; set; }

    //    public string Format { get; set; }

    //    private string _dimensiune = string.Empty;

    //    public string Dimensiune
    //    {
    //        get { return _dimensiune; }
    //        set
    //        {
    //            _dimensiune = value;

    //            if (!string.IsNullOrEmpty(value))
    //            {
    //                var isGb = value.ToLower().IndexOf("gb") > 0;

    //                var numValue = decimal.Parse(Helpers.StripNonNumeric(value).Replace(",", "."));
    //                if (isGb) numValue = numValue * 1024;

    //                DimensiuneInt = (int)numValue;
    //            }
    //        }
    //    }

    //    [ScriptIgnore]
    //    public long DimensiuneInt { get; set; }

    //    public string Bitrate { get; set; }

    //    [ScriptIgnore]
    //    public DateTime Durata { get; set; }

    //    public string DurataStr => Durata.ToString("HH:mm:ss");

    //    public string Audio { get; set; }

    //    public string Subtitrari { get; set; }

    //    public string MoreInfo { get; set; }

    //    public string Tematica { get; set; }

    //    public string Obs { get; set; }

    //    [ScriptIgnore]
    //    public string Obs2 { get; set; }

    //    [ScriptIgnore]
    //    public string Obs3 { get; set; }

    //    public string NLSource { get; set; }

    //    [ScriptIgnore]
    //    public string Md5 { get; set; }

    //    [ScriptIgnore]
    //    public byte[] Cover { get; set; }

    //    [ScriptIgnore]
    //    public bool HasCover { get; set; }

    //    [ScriptIgnore]
    //    public string Trailer { get; set; }

    //    //[ScriptIgnore]
    //    //public string TrailerEmbeded
    //    //{
    //    //    get
    //    //    {
    //    //        var embedableLink = string.Empty;

    //    //        try
    //    //        {
    //    //            if (string.IsNullOrEmpty(Trailer))
    //    //                return string.Empty;

    //    //            var s = Trailer.IndexOf("youtu.be") >= 0
    //    //                ? "https://www.youtube.com/watch?v=" + Trailer.Substring(Trailer.LastIndexOf("/") + 1) //no param => takes all from LastIndexOf !
    //    //                : Trailer;

    //    //            return s.Replace("watch?v=", "embed/");
    //    //        }
    //    //        catch (Exception)
    //    //        {
    //    //            return string.Empty;
    //    //        }
    //    //    }
    //    //}

    //    public string TrailerVideoId
    //    {
    //        get
    //        {
    //            try
    //            {
    //                if (string.IsNullOrEmpty(Trailer))
    //                    return string.Empty;

    //                var s = Trailer.IndexOf("youtu.be") >= 0
    //                    ? "https://www.youtube.com/watch?v=" + Trailer.Substring(Trailer.LastIndexOf("/") + 1) //no param => takes all from LastIndexOf !
    //                    : Trailer;

    //                return s.Replace("https://www.youtube.com/watch?v=", "");
    //            }
    //            catch (Exception)
    //            {
    //                return string.Empty;
    //            }
    //        }
    //    }
    //}

    //public class Serial
    //{
    //    public int Id { get; set; }

    //    public string Titlu { get; set; }

    //    private string _recomandat = string.Empty;
    //    public string Recomandat
    //    {
    //        get { return _recomandat; }
    //        set
    //        {
    //            _recomandat = value;

    //            if (string.IsNullOrEmpty(value))
    //            {
    //                RecomandatInt = 0;
    //            }
    //            else
    //            {
    //                var str = Helpers.StripNonNumeric(value);

    //                RecomandatInt = string.IsNullOrEmpty(str) ? 0 : int.Parse(str);
    //            }
    //        }
    //    }

    //    public string RecomandatLink { get; set; }

    //    [ScriptIgnore]
    //    public int RecomandatInt { get; set; }

    //    public string An { get; set; }

    //    public Calitate Calitate { get; set; }

    //    public long DimensiuneInt { get; set; }

    //    public string Audio { get; set; }

    //    //true daca unele episoade nu au dublaj
    //    public bool DifferentAudio { get; set; }

    //    public string MoreInfo { get; set; }

    //    public string Obs { get; set; }

    //    [ScriptIgnore]
    //    public byte[] Cover { get; set; }

    //    public bool HasCover { get; set; }

    //    [ScriptIgnore]
    //    public string Trailer { get; set; }

    //    //[ScriptIgnore]
    //    //public string TrailerEmbeded
    //    //{
    //    //    get
    //    //    {
    //    //        var embedableLink = string.Empty;

    //    //        try
    //    //        {
    //    //            if (string.IsNullOrEmpty(Trailer))
    //    //                return string.Empty;

    //    //            var s = Trailer.IndexOf("youtu.be") >= 0
    //    //                ? "https://www.youtube.com/watch?v=" + Trailer.Substring(Trailer.LastIndexOf("/") + 1) //no param => takes all from LastIndexOf !
    //    //                : Trailer;

    //    //            return s.Replace("watch?v=", "embed/");
    //    //        }
    //    //        catch (Exception)
    //    //        {
    //    //            return string.Empty;
    //    //        }
    //    //    }
    //    //}

    //    public string TrailerVideoId
    //    {
    //        get
    //        {
    //            try
    //            {
    //                if (string.IsNullOrEmpty(Trailer))
    //                    return string.Empty;

    //                var s = Trailer.IndexOf("youtu.be") >= 0
    //                    ? "https://www.youtube.com/watch?v=" + Trailer.Substring(Trailer.LastIndexOf("/") + 1) //no param => takes all from LastIndexOf !
    //                    : Trailer;

    //                return s.Replace("https://www.youtube.com/watch?v=", "");
    //            }
    //            catch (Exception)
    //            {
    //                return string.Empty;
    //            }
    //        }
    //    }
    //}

    //public class Episod
    //{
    //    public int Id { get; set; }

    //    public int SerialId { get; set; }

    //    public string Sezon { get; set; }

    //    public string Titlu { get; set; }

    //    public string An { get; set; }

    //    public Calitate Calitate { get; set; }

    //    [ScriptIgnore]
    //    public DateTime Durata { get; set; }

    //    public string DurataStr => Durata.ToString("HH:mm:ss");

    //    public string Rezolutie { get; set; }

    //    public string Format { get; set; }

    //    private string _dimensiune = string.Empty;
    //    public string Dimensiune
    //    {
    //        get { return _dimensiune; }
    //        set
    //        {
    //            _dimensiune = value;

    //            if (!string.IsNullOrEmpty(value))
    //            {
    //                var isGb = value.ToLower().IndexOf("gb") > 0;

    //                var numValue = decimal.Parse(Helpers.StripNonNumeric(value).Replace(",", "."));
    //                if (isGb) numValue = numValue * 1024;

    //                DimensiuneInt = (int)numValue;
    //            }
    //        }
    //    }

    //    [ScriptIgnore]
    //    public long DimensiuneInt { get; set; }

    //    public string Audio { get; set; }

    //    [ScriptIgnore]
    //    public string Subtitrari { get; set; }

    //    public string Obs { get; set; }

    //    public string Tematica { get; set; }
    //}
}