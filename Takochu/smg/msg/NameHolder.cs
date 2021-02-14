using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.io;

namespace Takochu.smg.msg
{
    public class NameHolder
    {
        public static void Initialize()
        {
            mFilesystem = new RARCFilesystem(Program.sGame.mFilesystem.OpenFile("/LocalizeData/UsEnglish/MessageData/SystemMessage.arc"));

            mGalaxyNames = new MSBT(mFilesystem.OpenFile("/boop/GalaxyName.msbt"));
            mScenarioNames = new MSBT(mFilesystem.OpenFile("/boop/ScenarioName.msbt"));
        }

        public static bool HasGalaxyName(string galaxy)
        {
            return mGalaxyNames.HasGalaxyName(galaxy);
        }

        public static string GetGalaxyName(string galaxy)
        {
            string name = $"GalaxyName_{galaxy}";
            return mGalaxyNames.GetStringFromLabelNoTag(name);
        }

        public static string GetScenarioName(string galaxy, int scenarioNo)
        {
            string name = $"ScenarioName_{galaxy}{scenarioNo}";
            return mScenarioNames.GetStringFromLabelNoTag(name);
        }

        public static void Close()
        {
            mFilesystem.Close();
        }

        public static void CloseAll()
        {
            mGalaxyNames.Close();
            mScenarioNames.Close();
            mFilesystem.Close();
        }

        public static void Save()
        {
            mGalaxyNames.Save();
            mScenarioNames.Save();
            mFilesystem.Save();
        }

        private static RARCFilesystem mFilesystem;
        private static MSBT mGalaxyNames;
        private static MSBT mScenarioNames;
    }
}
