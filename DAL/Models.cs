using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

//using System.Web.Script.Serialization;

namespace DAL
{
    public class MovieShortInfo
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string Quality { get; set; }
        //public byte[] Cover { get; set; }
        public bool HasPoster { get; set; }
        public bool HasThumbnails { get; set; }
        public bool HasSynopsis { get; set; }
        public bool ThumbnailGenerated { get; set;}

        public MovieShortInfo()
        {
            HasThumbnails = true;
        }
    }

    public class SeriesEpisodesShortInfo
    {
        public int Id { get; set; }
        public string FileName { get; set; }

        public string Theme { get; set; }
        public string AudioLanguages { get; set; }
        public string Composed1
        {
            get
            {
                return IsEpisode ? Theme : AudioLanguages;
            }
        }

        public string Quality { get; set; }
        public string Season { get; set; }
        public int SeriesId { get; set; }

        public bool IsSeries { get; set; }
        public bool IsSeason { get; set; }
        public bool IsEpisode { get; set; }

        public int SectionType { get; set; } //collections only
        public string Notes { get; set; } //collections only
        public byte[] Poster { get; set; } //collections only
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
        public string Season2 { get; set; }

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
        public string Trailer { get; set; }
        public string AudioLanguages { get; set; }
        public string SubtitleLanguages { get; set; }
        public string Synopsis { get; set; }

        public List<VideoStreamInfo> VideoStreams { get; set; }
        public List<AudioStreamInfo> AudioStreams { get; set; }
        public List<SubtitleStreamInfo> SubtitleStreams { get; set; }
        public List<byte[]> MovieStills { get; set; }


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

        public string Index_Display
        {
            get { return string.Format("{0} / Source", Index); }
        }
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
        public int AudioSource { get; set; }
        public bool Default { get; set; }
        public bool Forced { get; set; }

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
        public bool Default { get; set; }
        public bool Forced { get; set; }

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
        public bool ForceAddMissingEntries { get; set; }
        public bool PreserveManuallySetData { get; set; }
        public bool DisplayInfoOnly { get; set; }
        public string RecordingAudio { get; set; }
        public bool SkipMultiVersion { get; set; }
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

    #region objects that will be serialized to provide data for the webpage

    //not using the main classes to keep the data as small as possible

    public class MovieForWeb
    {
        public int Id { get; set; }
        public string FN { get; set; }   //FileName  *        ~   Titlu
        public string R { get; set; }   //Recommended  *     ~   Recomandat
        public string RL { get; set; }  //RecommendedLink *  ~   RecomandatLink
        public string Y { get; set; }   //Year  *            ~   An
        public string Q { get; set; }   //Quality  *         ~   Calitate
        //public string RZ { get; set; }  //?                 ~   Rezolutie
        //public string F { get; set; }   //Format            ~   Format
        public string S { get; set; }   //FileSize2  *       ~   Dimensiune
        public string B { get; set; }   //BitRate  *         ~   Bitrate
        public string L { get; set; }   //DurationFormatted* ~   DurataStr
        public string A { get; set; }   //AudioLanguages *   ~   Audio
        public string SU { get; set; }  //SubtitleLanguages* ~   Subtitrari
        public string DL { get; set; }  //DescriptionLink *  ~   MoreInfo
        public string T { get; set; }   //Theme *            ~   Tematica
        public string N { get; set; }   //Notes *            ~   Obs
        public string Nl { get; set; }  //?   *              ~   NLSource
        public string Tr { get; set; }  //Trailer           ~   TrailerVideoId
        public int Th { get; set; }     //Thumbnail present? (Bit like ~ 0-1)
        public int ISP { get; set; }     //Inserted Sort position
        public int USP { get; set; }     //LastUpdate Sort position

        [ScriptIgnore]
        public byte[] Cover { get; set; }

        [ScriptIgnore]
        public bool HasPoster { get; set; }

        [ScriptIgnore]
        public DateTime InsertedDate { get; set; }

        [ScriptIgnore]
        public DateTime LastChangeDate { get; set; }

        [ScriptIgnore]
        public bool HasThumbnails { get; set; }
    }

    public class MoviesDetails2ForWeb
    {
        public int Id { get; set; }
        public string Syn { get; set; }
        public List<string> Vtd { get; set; } //VideoTrackDetails
        public List<string> Ats { get; set; } //AudioTrackSummary
        public List<string> Sts { get; set; } //SubtitleTracksSummary
        public List<string> Fd { get; set; } //FileDetails

        public MoviesDetails2ForWeb()
        {
            Vtd = new List<string>();
            Ats = new List<string>();
            Sts = new List<string>();
            Fd = new List<string>();
        }
    }

    public class SeriesForWeb
    {
        public int Id { get; set; }
        public string FN { get; set; }   //FileName          ~   Titlu
        public string R { get; set; }   //Recommended       ~   Recomandat
        public string RL { get; set; }  //RecommendedLink   ~   RecomandatLink
        public string DL { get; set; }  //DescriptionLink   ~   MoreInfo
        public string N { get; set; }   //Notes             ~   Obs

        //calculated based on Episodes data
        public string Y { get; set; }   //Year              ~   An
        public string S { get; set; }   //Size
        public string Q { get; set; }   //Quality
        public string A { get; set; }   //Audio
        public string Ec { get; set; }  //EpisodeCount
        public int T { get; set; }      //CollectionsSiteSectionType

        [ScriptIgnore]
        public byte[] Cover { get; set; }

        [ScriptIgnore]
        public bool HasPoster { get; set; }
    }

    public class EpisodesForWeb
    {
        public int Id { get; set; }
        public int SId { get; set; }    //ParentId          ~   SerialId
        public string FN { get; set; }   //FileName          ~   Titlu
        public string SZ { get; set; }      //Season            ~   Sezon
        public string Y { get; set; }   //Year              ~   An
        public string Q { get; set; }   //Quality           ~   Calitate
        public string L { get; set; }   //DurationFormatted ~   Durata
        //public string RZ { get; set; }  //?                 ~   Rezolutie
        //public string F { get; set; }   //Format            ~   Format
        public string S { get; set; }   //FileSize2         ~   Dimensiune

        [ScriptIgnore]
        public long Si { get; set; }    //FileSize          ~   DimensiuneInt
        public string A { get; set; }   //AudioLanguages    ~   Audio
        public string SU { get; set; }  //SubtitleLanguages ~   Subtitrari
        public string T { get; set; }   //Theme             ~   Tematica
        public string N { get; set; }   //Notes             ~   Obs

        public int Th { get; set; }     //Thumbnail present? (Bit like ~ 0-1)

        [ScriptIgnore]
        public DateTime InsertedDate { get; set; }

        [ScriptIgnore]
        public DateTime LastChangeDate { get; set; }

        public EpisodesForWeb()
        {
        }
    }

    //combination of MoviesForWeb and EpisodesForWeb
    public class CollectionElementForWeb
    {
        public int Id { get; set; }
        public int CId { get; set; }    //CollectionId
        public string FN { get; set; }  //FileName
        public string R { get; set; }   //Recommended
        public string RL { get; set; }  //RecommendedLink
        public string Y { get; set; }   //Year
        public string Q { get; set; }   //Quality
        public string S { get; set; }   //FileSize2

        [ScriptIgnore]
        public long Si { get; set; }    //FileSize          ~   DimensiuneIn

        public string B { get; set; }   //BitRate
        public string L { get; set; }   //DurationFormatted
        public string A { get; set; }   //AudioLanguages
        public string SU { get; set; }  //SubtitleLanguages
        public string DL { get; set; }  //DescriptionLink
        public string T { get; set; }   //Theme
        public string N { get; set; }   //Notes
        public string Nl { get; set; }  //NLSource
        public string Tr { get; set; }  //Trailer
        public int Th { get; set; }     //Thumbnail present? (Bit like ~ 0-1)

        [ScriptIgnore]
        public DateTime InsertedDate { get; set; }

        [ScriptIgnore]
        public DateTime LastChangeDate { get; set; }

        [ScriptIgnore]
        public bool HasPoster { get; set; }

    }

    #endregion

    public class SelectableElement
    {
        public object Value { get; set; }

        public string Description { get; set; }

        public SelectableElement(object value, string description)
        {
            Value = value;
            Description = description;
        }
    }

    public class SiteGenParams
    {
        public string Location { get; set; }
        public bool SavePosters { get; set; }
        public bool SaveMoviesThumbnals { get; set; }
        public bool SaveEpisodesThumbnals { get; set; }
        public bool PreserveMarkesForExistingThumbnails { get; set; }
        public bool MinifyScriptFiles { get; set; }
        public bool MinifyDataFiles { get; set; }
    }

    public class BulkEditField
    {
        public string Caption { get; set; }

        public string FieldName { get; set; }

        public string Value { get; set; }

        public bool RequireRefresh { get; set; }
    }

    public class StatisticEl
    {
        public string Quality { get; set; }
        public int Count { get; set; }
        public decimal Size { get; set; }
        public int Runtime_dd { get; set; }
        public int Runtime_hh { get; set; }
        public int Runtime_mm { get; set; }
        public int Runtime_ss { get; set; }
    }

    public class SectionStatistics
    {
        public List<StatisticEl> SectionElements { get; set; }

        //public string
    }

    public class PdfGenParams
    {
        public string FileName { get; set; }
        public PDFGenType PDFGenType { get; set; }
        public bool ForMovies { get; set; }
    }

    public enum PDFGenType
    {
        All = 0,
        Christmas = 1,
        Helloween = 2
    }

    public class GeneratedJSData
    {
        public string MoviesData { get; set; }

        public string SeriesData { get; set; }

        public string RecordingsData { get; set; }

        public string MoviesDetails2 { get; set; }

        public string CollectionsData { get; set; }
        public string CollectionsDetails2 { get; set; }

        public GeneratedJSData() { }

        public GeneratedJSData(string moviesData, string seriesData, string recordingsData, string moviesDetails2,
            string collectionsData, string collectionsDetails2)
        {
            MoviesData = moviesData;
            SeriesData = seriesData;
            RecordingsData = recordingsData;
            MoviesDetails2 = moviesDetails2;
            CollectionsData = collectionsData;
            CollectionsDetails2 = collectionsDetails2;
        }
    }

    public class SynopsisImportMovieData
    {
        public int MovieId { get; set; }
        public string FileName { get; set; }
        public string DescriptionLink { get; set; }

        //public string Synopsis { get; set; }
        //public string SkipReason { get; set; }
    }

    public class BgwArgument_Work
    {
        public string SiteGenLocation { get; set; }
        public string SubFolder { get; set; }
        public List<MovieShortInfo> MSI { get; set; }
    }
}