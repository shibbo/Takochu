using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;

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
            mPowerStarType = mEntry.Get<string>("PowerStarType");
            mComet = mEntry.Get<string>("Comet");
            mCometLimitTimer = mEntry.Get<int>("CometLimitTimer");

            mZoneMasks = new int[zoneList.Count];
            int curIdx = 0;

            foreach (string zone in zoneList)
                mZoneMasks[curIdx++] = mEntry.Get<int>(zone);
        }

        public BCSV.Entry mEntry;
        public int mScenarioNo;
        public string mScenarioName;
        public int mPowerStarID;
        public string mAppearPowerStar;
        public string mPowerStarType;
        public string mComet;
        public int mCometLimitTimer;

        public int[] mZoneMasks;
    }
}
