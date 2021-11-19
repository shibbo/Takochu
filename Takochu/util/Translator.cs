using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;

namespace Takochu.util
{
    public interface IMessageBoxLanguage
    {

        string GetMessage(MessageBoxTranslator.MessageBoxName messageBoxName);

    }

    // This class is for translating message boxes.
    // In order to support multiple languages in the future,
    // all languages inherit from the "IMessageBoxLanguage" interface.
    public class MessageBoxTranslator
    {
        private readonly IMessageBoxLanguage _messageBoxLanguage;
        public enum MessageBoxName 
        {
            InvalidFolder
        }
        public MessageBoxTranslator()
        {
            var TranslateName = 
                (Translator.Language)System.Enum.Parse(typeof(Translator.Language), Properties.Settings.Default.Translation);
            switch(TranslateName)
            {
                case Translator.Language.Japanese:
                    _messageBoxLanguage = new MessageBoxJapanese();
                    break;
                case Translator.Language.English:
                    _messageBoxLanguage = new MessageBoxEnglish();
                    break;
                case Translator.Language.German:
                case Translator.Language.French:
                case Translator.Language.Korean:
                case Translator.Language.Spanish:
                default:
                    _messageBoxLanguage = new MessageBoxEnglish();
                    break;
            }
        }

        public string GetMessage(MessageBoxTranslator.MessageBoxName messageBoxName)
        {
            return _messageBoxLanguage.GetMessage(messageBoxName);
        }
    }

    public class MessageBoxEnglish : IMessageBoxLanguage
    {
        private readonly string[]
            MessageArray =
            {
                "Invalid folder. If you have already selected a correct folder, that will continue to be your base folder."
            };
        public string GetMessage(MessageBoxTranslator.MessageBoxName messageBoxName)
        {
            return MessageArray[((int)messageBoxName)];
        }
    }

    public class MessageBoxJapanese : IMessageBoxLanguage
    {
        private readonly string[]
            MessageArray =
            {
                "無効なフォルダです。すでに正しいフォルダを選択している場合は、そのフォルダが引き続きベースフォルダとなります。"
            };
        public string GetMessage(MessageBoxTranslator.MessageBoxName messageBoxName)
        {
            return MessageArray[((int)messageBoxName)];
        }
    }

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
            de,
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

        public Translator()
        {
            mLanguage = sStringToLang[Properties.Settings.Default.Translation];
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
                    path = JointSimpleGalaxyName(LangageCode.de);
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
            string[] lines = File.ReadAllLines(path);

            Dictionary<string, string> ret = new Dictionary<string, string>();

            foreach (string line in lines)
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
