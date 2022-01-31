using System.Collections.Generic;
using Takochu.fmt;
using Takochu.util;

namespace Takochu.smg
{
    public class Scenario
    {
        public Scenario(BCSV.Entry entry, List<string> zoneList)
        {
            mEntry = entry;

            mScenarioNo = mEntry.Get<int>("ScenarioNo");
            mScenarioName = mEntry.Get<string>("ScenarioName");
            mPowerStarID = mEntry.Get<int>("PowerStarId");
            mAppearPowerStar = mEntry.Get<string>("AppearPowerStarObj");

            if (GameUtil.IsSMG2())
            {
                mPowerStarType = mEntry.Get<string>("PowerStarType");
                mCometLimitTimer = mEntry.Get<int>("CometLimitTimer");
            }

            if (!mEntry.ContainsKey("Comet"))
                mComet = "";
            else
                mComet = mEntry.Get<string>("Comet");
            
            mZoneMasks = new Dictionary<string, int>();

            foreach (string zone in zoneList)
                mZoneMasks.Add(zone, mEntry.Get<int>(zone));
        }

        public void RemoveZone(string zoneName)
        {
            mZoneMasks.Remove(zoneName);
        }

        public BCSV.Entry mEntry { get; private set; }
        public int mScenarioNo { get; private set; }
        public string mScenarioName { get; private set; }
        public int mPowerStarID { get; private set; }
        public string mAppearPowerStar { get; private set; }
        public string mPowerStarType { get; private set; }
        public string mComet { get; private set; }
        public int mCometLimitTimer { get; private set; }

        public Dictionary<string, int> mZoneMasks { get; private set; }

        /// <summary>
        /// SMG2で使用されるコメットの一覧
        /// </summary>
        public enum SMG2Comets 
        {
            None,
            Red,
            Purple,
            Quick,
            Dark,
            Exterminate,
            Mimic
        }

        public enum StarType 
        {
            Normal,
            Hidden,
            Green
        }


        /// <summary>
        /// SMG1で使用されるコメットの一覧
        /// </summary>
        public enum SMG1Comets 
        {
            None,
            Red,
            Purple,
            Quick,
            Dark,
            Black,
            Ghost
        }
    }
}
