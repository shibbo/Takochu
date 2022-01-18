using System.Windows.Forms;

namespace Takochu.util.TranslateSys
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
}
