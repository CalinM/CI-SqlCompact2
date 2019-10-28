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
    }
}
