using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Common
{
    public enum Quality
    {
        NotSet = -1,

        FullHD = 0,

        HD = 1,

        SD = 2,

        HD_up = 3, //series

        Mix = 4 //series
    }

    public enum AudioSource
    {
        Unknown = 0,

        BDRip = 1,

        DVD = 2,

        DVDRip = 3,

        StreamDl = 4, //iptv

        ScreenRecording = 5,
    }

    public enum MovieTheme
    {
        None = 0,

        Christmas = 1,

        Halloween = 2,

        Winter = 3,

        Easter = 4,

        Sinterklaas = 5,

        [Description("Valentine's Day")]
        ValentinesDay = 6
    }

    public class EnumSelectorHelper
    {
        public int Index { get; set; }

        public string Text { get; set; }
    }

    public enum StartPosition2
    {
        TopLeft = 0,
        TopRight = 1,
        BottomLeft = 2,
        BottomRight = 3
    }

    public enum MessageType
    {
        Information = 0,
        Warning = 1,
        Error = 2,
        Confirmation = 3
    }

    public enum Sections
    {
        Movies = 0,
        Series = 1,
        Recordings = 2,
        Collections = 3
    }

    public enum SeriesType
    {
        Final = -1,
        Recordings = -100,
        Collection = -10 //only used to get data for web
    }

    public enum CollectionsSiteSectionType
    {
        MovieType = 0,
        SeriesType = 1
    }

    public enum EpisodeParentType
    {
        Series = 0,
        Collection = 1
    }

    public enum NamesMix_Ext
    {
        [Description(".mkv")]
        mkv = 0,

        [Description(".mp4")]
        mp4 = 1
    }

    public enum NamesMix_NameType
    {
        [Description(" - ")]
        dash = 0,

        [Description(". ")]
        dot = 1
    }

    public enum NamesMix_ProcessFN
    {
        [Description("no filename processing")]
        none = 0,

        [Description("filename ToSentenceCase")]
        ToSentenceCase = 1,

        [Description("filename ToTitleCase")]
        ToTitleCase = 2
    }

    public enum ALotOrAlittleElements
    {
        [Description("Educational Value")]
        EducationalValue = 0,

        [Description("Positive Messages")]
        PositiveMessages = 1,

        [Description("Positive Role Models")]
        PositiveRoleModelsAndRepresentations = 2,

        [Description("Violence & Scariness")]
        ViolenceAndScariness = 3,

        [Description("Sexy Stuff")]
        SexyStuff = 4,

        [Description("Language")]
        Language = 5,

        [Description("Consumerism")]
        Consumerism = 6,

        [Description("Drinking, Drugs & Smoking")]
        DrinkingDrugsAndSmoking = 7
    }

    public enum MoviesGridsHighlights
    {
        //None = 0,
        //UnkownQuality = 1,
        NoSynopsis = 2,
        NoPoster = 3,
        NoCSM = 4
    }



    public class EnumHelpers
    {
        public static List<EnumSelectorHelper> EnumToList<T>() where T : struct
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();

            return Enum.GetValues(typeof(T))
                .Cast<T>()
                .Select(x => new EnumSelectorHelper
                {
                    Index = Convert.ToInt32(x),
                    Text = x.ToString()
                })
                .ToList();
        }

        public static List<EnumSelectorHelper> EnumDescToList<T>() where T : struct
        {
            var result = new List<EnumSelectorHelper>();

            foreach (var value in Enum.GetValues(typeof(T)).Cast<T>())
            {
                var fieldInfo = value.GetType().GetField(value.ToString());
                var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(fieldInfo, typeof(DescriptionAttribute));

                result.Add(new EnumSelectorHelper
                {
                    Index = Convert.ToInt32(value),
                    Text = attribute != null && attribute.Description.Length > 0 ? attribute.Description : value.ToString()
                });
            }

            return result;
        }

        public static string GetEnumDescription(Enum enumElement)
        {
            if (enumElement == null)
                return string.Empty;

            var fi = enumElement.GetType().GetField(enumElement.ToString());
            if (fi == null)
                return string.Empty;

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0)
                ? attributes[0].Description
                : enumElement.ToString();
        }

    }
}
