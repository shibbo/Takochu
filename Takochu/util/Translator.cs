using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System;
using System.Windows.Forms;

namespace Takochu.util
{
    public interface IMessageBoxLanguage
    {
        /// <summary>
        /// 翻訳されたメッセージボックスのテキストを取得します。<br/>
        /// Get the text of the translated message box.
        /// </summary>
        /// <param name="messageBoxName">テンプレートメッセージの名前</param>
        /// <returns>翻訳された文字列</returns>
        //string Text(MessageBoxText messageBoxName);
        //string CaptionText(MessageBoxCaption messageBoxCaption);
        DialogResult Show(MessageBoxText messageBoxName, MessageBoxCaption messageBoxCaption);
        DialogResult Show(MessageBoxText messageBoxName, MessageBoxCaption messageBoxCaption, MessageBoxButtons messageButton);
        string[] MessageArray { get; }
        string[] MessageCaption { get; }
    }

    public enum MessageBoxText
    {
        InvalidGameFolder,
        FolderPathCorrectly,
        UnimplementedFeatures,
        InitialPathSettings,
        ChangesNotSaved,
        ShapeNoNotValid
    }

    public enum MessageBoxCaption
    {
        Error,
        Info
    }

    public enum Language
    {
        English,
        German,
        French,
        Japanese,
        Korean,
        Spanish
    }

    public enum LangageCode
    {
        en,
        de,
        jp
    }

    // This class is for translating message boxes.
    // In order to support multiple languages in the future,
    // all languages inherit from the "IMessageBoxLanguage" interface.

    /// <summary>
    /// 翻訳するためのクラス
    /// </summary>
    public class Translate
    {
        /// <summary>
        /// メッセージボックステキストを翻訳
        /// </summary>
        public static IMessageBoxLanguage GetMessageBox
        {
            get => SetLanguage();
        }

        public static readonly Dictionary<string, Type> t = new Dictionary<string, Type>()
        {
            {"English",typeof(Properties.PropertyGrid_EN)},
            {"German",typeof(Properties.PropertyGrid_EN)},
            {"French",typeof(Properties.PropertyGrid_EN)},
            {"Japanese",typeof(Properties.PropertyGrid_JP)},
            {"Korean",typeof(Properties.PropertyGrid_EN)},
            {"Spanish",typeof(Properties.PropertyGrid_EN)}
        };

        private static Dictionary<Language, IMessageBoxLanguage> testinterface = new Dictionary<Language, IMessageBoxLanguage>()
        {
            { Language.English   , new MessageBoxEnglish()  },
            { Language.German    , new MessageBoxEnglish()  },
            { Language.French    , new MessageBoxEnglish()  },
            { Language.Japanese  , new MessageBoxJapanese() },
            { Language.Korean    , new MessageBoxEnglish()  },
            { Language.Spanish   , new MessageBoxEnglish()  }
        };

        private static IMessageBoxLanguage SetLanguage()
        {

            var TranslateName = GetLanguage();

            ArraySizeChecker(TranslateName, testinterface[TranslateName]);

            return testinterface[TranslateName];
        }

        private static Language GetLanguage() 
        {
            var LanguageType = typeof(Language);
            var SettingParser = Enum.Parse(LanguageType, Properties.Settings.Default.Translation);
            return (Language)SettingParser;
        }

        private static void ArraySizeChecker(Language lang ,IMessageBoxLanguage imbl) 
        {
            var EnumTextArrayLength = typeof(MessageBoxText).GetEnumValues().Length;
            var TextArrayLength = testinterface[lang].MessageArray.Length;

            var EnumCaptionTextArrayLength = typeof(MessageBoxCaption).GetEnumValues().Length;
            var CaptionArrayLength = testinterface[lang].MessageCaption.Length;

            if (EnumTextArrayLength != TextArrayLength)
                throw new Exception("MessageArrayまたはMessageBoxTextの配列サイズが不足しています");
            if (EnumCaptionTextArrayLength != CaptionArrayLength)
                throw new Exception("MessageCaptionまたはMessageBoxCaptionの配列サイズが不足しています");
            return;
        }

        public static Dictionary<string, string> GetGalaxyNames()
        {
            var TranslateName = GetLanguage();
            string path;
            switch (TranslateName)
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

            PropertySave(TranslateName);

            string[] lines = File.ReadAllLines(path);
            
            Dictionary<string, string> ret = new Dictionary<string, string>();

            foreach (string line in lines)
            {
                string[] split = line.Split('=');
                ret.Add(split[0], split[1]);
            }

            return ret;
        }

        private static void PropertySave(Language lang) 
        {
            Properties.Settings.Default.Translation = lang.ToString();
            Properties.Settings.Default.Save();
        }

        private static string JointSimpleGalaxyName(LangageCode lc)
        {
            return $"res/translations/{lc}/SimpleGalaxyNames_{(GameUtil.IsSMG1() ? "SMG1.txt" : "SMG2.txt")}";
        }


    }

    public class MessageBoxEnglish : IMessageBoxLanguage
    {
        public string[] MessageArray
        {
            get => new string[]
            {
                "Invalid folder. If you have already selected a correct folder, that will continue to be your base folder.",
                "Path set successfully! You may now use Takochu.",
                "Some or all of these features have not been implemented.",
                "Please select a path that contains the dump of your SMG1 / SMG2 copy.",
                "Changes are not saved.\n\rDo you want to close the window?",
                "Invalid AreaShapeNo value. AreaShapeNo cannot be less than 0."
            };
        }

        public string[] MessageCaption
        {
            get => new string[]
            {
                "Error",
                "Info"
            };
        }

        public DialogResult Show(MessageBoxText messageBoxName, MessageBoxCaption messageBoxCaption)
        {
            var s1 = MessageArray[((int)messageBoxName)];
            var s2 = MessageCaption[((int)messageBoxCaption)];
            return MessageBox.Show(s1, s2);
        }

        public DialogResult Show(MessageBoxText messageBoxName, MessageBoxCaption messageBoxCaption, MessageBoxButtons messageButton)
        {
            var s1 = MessageArray[((int)messageBoxName)];
            var s2 = MessageCaption[((int)messageBoxCaption)];
            return MessageBox.Show(s1, s2, messageButton);
        }
    }

    public class MessageBoxJapanese : IMessageBoxLanguage
    {
        public string[] MessageArray
        {
            get => new string[]
            {
                "スーパーマリオギャラクシー1,2以外のフォルダです。\n\r「StageData」と「ObjectData」が入っているフォルダを指定してください。",
                "新しくゲームフォルダをセットしました。\n\rこれでTacochuを使用できます。",
                "この機能の一部、または全ての機能が実装されていません。",
                "SMG1 / SMG2のディスクからコピーされたデータが\n\r入っているパスを選択してください",
                "変更は保存されていません\n\rウィンドウを閉じますか？",
                ""
            };
        }

        public string[] MessageCaption 
        {
            get => new string[] 
            {
                "エラー",
                "情報"
            };
        }

        public DialogResult Show(MessageBoxText messageBoxName, MessageBoxCaption messageBoxCaption) 
        {
            var s1 = MessageArray[((int)messageBoxName)];
            var s2 = MessageCaption[((int)messageBoxCaption)];
            return MessageBox.Show(s1,s2);
        }

        public DialogResult Show(MessageBoxText messageBoxName, MessageBoxCaption messageBoxCaption, MessageBoxButtons messageButton)
        {
            var s1 = MessageArray[((int)messageBoxName)];
            var s2 = MessageCaption[((int)messageBoxCaption)];
            return MessageBox.Show(s1, s2,messageButton);
        }
    }



    public class Translator
    {
        //public static Dictionary<string, Language> sStringToLang = new Dictionary<string, Language>()
        //{
        //    { "English", Language.English },
        //    { "German", Language.German },
        //    { "French", Language.French },
        //    { "Japanese", Language.Japanese },
        //    { "Korean", Language.Korean },
        //    { "Spanish", Language.Spanish }
        //};

        public Translator()
        {
            //mLanguage = sStringToLang[Properties.Settings.Default.Translation];
        }

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
