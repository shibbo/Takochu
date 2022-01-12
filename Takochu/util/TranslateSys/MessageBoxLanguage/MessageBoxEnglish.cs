using System.Windows.Forms;

namespace Takochu.util.TranslateSys.MessageBoxLanguage
{
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
                "Changes are not saved.\n\rDo you want to close the window?"
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
}
