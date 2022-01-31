using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.io;

namespace Takochu.smg
{
    public static class BGMInfo
    {
        public static void Initialize()
        {
            mFilesystem = new RARCFilesystem(Program.sGame.mFilesystem.OpenFile("/AudioRes/Info/StageBgmInfo.arc"));

            //StageBGMInfoを読み込む
            BCSV stageBgm = new BCSV(mFilesystem.OpenFile("/StageBgmInfo/StageBgmInfo.bcsv"));
            mStageEntries = new Dictionary<string, BGMInfoEntry>();
            foreach(BCSV.Entry e in stageBgm.mEntries)
            {
                BGMInfoEntry bgmEntry = new BGMInfoEntry
                {
                    Entry = e,
                    StageName = e.Get<string>("StageName"),

                    ChangeBGMIDName = new string[5],
                    ChangeBGMState = new int[5]
                };

                for (int i = 0; i < 5; i++)
                    bgmEntry.ChangeBGMIDName[i] = e.Get<string>($"ChangeBgmIdName{i}");

                for (int i = 0; i < 5; i++)
                    bgmEntry.ChangeBGMState[i] = e.Get<int>($"ChangeBgmState{i}");

                mStageEntries.Add(bgmEntry.StageName, bgmEntry);
            }

            //ScenarioBGMInfoを読み込む
            BCSV scenarioBgm = new BCSV(mFilesystem.OpenFile("/StageBgmInfo/ScenarioBgmInfo.bcsv"));
            mScenarioEntries = new List<ScenarioBGMEntry>();
            foreach(BCSV.Entry e in scenarioBgm.mEntries)
            {
                ScenarioBGMEntry scenarioEntry = new ScenarioBGMEntry
                {
                    Entry = e,
                    StageName = e.Get<string>("StageName"),
                    ScenarioNo = e.Get<int>("ScenarioNo"),
                    BGMName = e.Get<string>("BgmIdName"),
                    StartType = e.Get<int>("StartType"),
                    StartFrame = e.Get<int>("StartFrame"),
                    IsPrepare = e.Get<int>("IsPrepare")
                };

                mScenarioEntries.Add(scenarioEntry);
            }
        }

        public static bool HasBGMInfo(string name)
        {
            if (mStageEntries == null)
                return false;

            return mStageEntries.ContainsKey(name);
        }

        public static void GetBGMInfo(string name, out BGMInfoEntry stageEntry, out List<ScenarioBGMEntry> scenarioEntry)
        {
            stageEntry = mStageEntries[name];
            scenarioEntry = mScenarioEntries.FindAll(e => e.StageName == name);
        }

        public static void Save()
        {
            BCSV stageBgm = new BCSV(mFilesystem.OpenFile("/StageBgmInfo/StageBgmInfo.bcsv"));
            stageBgm.mEntries.Clear();

            foreach (KeyValuePair<string, BGMInfoEntry> entry in mStageEntries)
            {
                stageBgm.mEntries.Add(entry.Value.Entry);
            }

            stageBgm.Save();

            BCSV scenarioBgm = new BCSV(mFilesystem.OpenFile("/StageBgmInfo/ScenarioBgmInfo.bcsv"));
            scenarioBgm.mEntries.Clear();

            foreach (ScenarioBGMEntry entry in mScenarioEntries)
            {
                scenarioBgm.mEntries.Add(entry.Entry);
            }

            scenarioBgm.Save();
            mFilesystem.Save();
        }

        public static void Close() 
        {
            if(mFilesystem != null)
            mFilesystem.Close();
        }

        public class BGMInfoEntry
        {
            public BCSV.Entry Entry;
            public string StageName;
            public string[] ChangeBGMIDName;
            public int[] ChangeBGMState;
        };

        public class ScenarioBGMEntry
        {
            public BCSV.Entry Entry;
            public string StageName;
            public int ScenarioNo;
            public string BGMName;
            public int StartType;
            public int StartFrame;
            public int IsPrepare;
        };

        static RARCFilesystem mFilesystem;
        public static Dictionary<string, BGMInfoEntry> mStageEntries;
        public static List<ScenarioBGMEntry> mScenarioEntries;
    }
}
