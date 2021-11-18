using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;

namespace Takochu.util
{
    public class Translator
    {
        public enum Language
        {
            English,
            German,
            French,
            Japanese,
            Korean,
            Spanish
        }

        private enum LangageCode
        {
            en,
            du,
            jp
        }

        public static Dictionary<string, Language> sStringToLang = new Dictionary<string, Language>()
        {
            { "English", Language.English },
            { "German", Language.German },
            { "French", Language.French },
            { "Japanese", Language.Japanese },
            { "Korean", Language.Korean },
            { "Spanish", Language.Spanish }
        };

        public Translator(string lang)
        {
            mLanguage = sStringToLang[lang];
        }

        /*public string GetTranslation(string where, string what)
        {
            string[] lines = File.ReadAllLines()
        }*/

        public Dictionary<string, string> GetGalaxyNames()
        {
            string path;
            switch (mLanguage)
            {
                case Language.English:
                    path = JointSimpleGalaxyName(LangageCode.en);
                    break;

                case Language.German:
                    path = JointSimpleGalaxyName(LangageCode.du);
                    break;

                case Language.Japanese:
                    path = JointSimpleGalaxyName(LangageCode.jp);
                    break;

                default:
                    path = JointSimpleGalaxyName(0);
                    break;
            }
            Properties.Settings.Default.Translation = mLanguage.ToString();
            Properties.Settings.Default.Save();
            System.Console.WriteLine(Properties.Settings.Default.Translation);
            System.Console.WriteLine( path);
            System.Console.WriteLine(mLanguage.ToString());
            string[] lines = File.ReadAllLines(path);

            Dictionary<string, string> ret = new Dictionary<string, string>();

            foreach(string line in lines)
            {
                string[] split = line.Split('=');
                ret.Add(split[0], split[1]);
            }

            return ret;
        }
        
        private string JointSimpleGalaxyName(LangageCode lc) 
        {
            return $"res/translations/{lc}/SimpleGalaxyNames_{(GameUtil.IsSMG1() ? "SMG1.txt" : "SMG2.txt")}";
        }

        private Language mLanguage;
    }
}
