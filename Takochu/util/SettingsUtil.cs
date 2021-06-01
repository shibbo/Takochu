using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takochu.util
{
    public static class SettingsUtil
    {
        public static void SetSetting(string setting, object value)
        {
            switch(setting)
            {
                case "GameFolder":
                    Properties.Settings.Default.GamePath = Convert.ToString(value);
                    break;
                case "ShowArgs":
                    Properties.Settings.Default.ShowArgs = Convert.ToBoolean(value);
                    break;
                case "Translation":
                    Properties.Settings.Default.Translation = Convert.ToString(value);
                    break;
            }

            Properties.Settings.Default.Save();
        }

        public static object GetSetting(string setting)
        {
            object val = null;

            switch (setting)
            {
                case "GameFolder":
                    val = Properties.Settings.Default.GamePath;
                    break;
                case "ShowArgs":
                    val = Properties.Settings.Default.ShowArgs;
                    break;
                case "Translation":
                    val = Properties.Settings.Default.Translation;
                    break;
            }

            return val;
        }
    }
}
