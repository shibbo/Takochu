using System.Windows.Forms;

namespace Takochu.util.TranslateSys.MessageBoxLanguage
{
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
                "変更は保存されていません\n\rウィンドウを閉じますか？"
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
            return MessageBox.Show(s1, s2);
        }

        public DialogResult Show(MessageBoxText messageBoxName, MessageBoxCaption messageBoxCaption, MessageBoxButtons messageButton)
        {
            var s1 = MessageArray[((int)messageBoxName)];
            var s2 = MessageCaption[((int)messageBoxCaption)];
            return MessageBox.Show(s1, s2, messageButton);
        }
    }
}
