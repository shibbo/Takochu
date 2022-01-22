using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.io;
using Takochu.util;

namespace Takochu.smg
{
    class LightData
    {
        public static void Initialize()
        {
            mLights = new List<LightEntry>();
            var a = Program.sGame.mFilesystem.OpenFile(GameUtil.IsSMG1() ? "/ObjectData/LightData.arc" : "/LightData/LightData.arc");
            mFilesystem = new RARCFilesystem(a);
            //a.Close();
            mBCSV = new BCSV(mFilesystem.OpenFile("/LightData/LightData.bcsv"));

            mBCSV.mEntries.ForEach(e => mLights.Add(new LightEntry(e)));
        }

        public static LightEntry Get(string name)
        {
            return mLights.Find(l => l.mAreaLightName == name);
        }

        public static List<string> GetNames()
        {
            List<string> names = new List<string>();
            mLights.ForEach(l => names.Add(l.mAreaLightName));
            return names;
        }

        public static void Save()
        {
            mBCSV.Save();   
            mFilesystem.Save();
        }

        public static void Close() 
        {
            /*if (mBCSV != null)*/ mBCSV.Close();
            mFilesystem.Close();
        }

        static List<LightEntry> mLights;
        static RARCFilesystem mFilesystem;
        static BCSV mBCSV;
    }

    class LightEntry
    {
        public LightEntry(BCSV.Entry entry)
        {
            mEntry = entry;
            mAreaLightName = mEntry.Get<string>("AreaLightName");
        }

        public T Get<T>(string key)
        {
            return mEntry.Get<T>(key);
        }

        public void Set(string key, object val)
        {
            mEntry.Set(key, val);
        }


        BCSV.Entry mEntry;
        public string mAreaLightName;
    }
}
