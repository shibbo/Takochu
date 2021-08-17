using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.ui;

namespace Takochu.smg.obj
{
    public class Light
    {
        public Light(BCSV.Entry e, string parent)
        {
            mEntry = e;
            mLightName = mEntry.Get<string>("AreaLightName");
            mLightNo = mEntry.Get<int>("LightID");
            mParent = parent;
        }

        public void Save()
        {
            mEntry.Set("AreaLightName", mLightName);
            mEntry.Set("LightID", mLightNo);
        }

        public override string ToString()
        {
            return $"[{mLightNo}] {mLightName} [{mParent}]";
        }

        public BCSV.Entry mEntry;
        public string mParent;

        public string mLightName;
        public int mLightNo;

    }
}
