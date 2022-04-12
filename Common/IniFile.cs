using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace Common
{
    public class IniFile   // revision 11
    {
        string _path;
        string _fileName = "Desene";

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern long WritePrivateProfileString(string section, string key, string value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string section, string key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile()
        {
            _path = new FileInfo(_fileName + ".ini").FullName;
        }

        public string ReadString(string key, string section, string defaultValue = "")
        {
            var retVal = new StringBuilder(255);
            GetPrivateProfileString(section, key, "", retVal, 255, _path);

            return string.IsNullOrEmpty(retVal.ToString()) ? defaultValue : retVal.ToString();
        }

        public int ReadInt(string key, string section, int defaultValue = 0)
        {
            try
            {
                return int.Parse(ReadString(key, section, defaultValue.ToString()));
            }
            catch
            {
                return defaultValue;
            }
        }

        public bool ReadBool(string key, string section, bool defaultValue = false)
        {
            try
            {
                return bool.Parse(ReadString(key, section, defaultValue.ToString()));
            }
            catch
            {
                return defaultValue;
            }
        }

        public void Write(string key, string value, string section)
        {
            WritePrivateProfileString(section, key, value, _path);
        }

        public void DeleteKey(string key, string section)
        {
            Write(key, null, section);
        }

        public void DeleteSection(string section)
        {
            Write(null, null, section);
        }

        public bool KeyExists(string key, string section)
        {
            return ReadString(key, section, string.Empty).Length > 0;
        }
    }
}