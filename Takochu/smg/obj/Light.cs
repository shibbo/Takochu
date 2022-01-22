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
    public class Light : AbstractObj
    {
        public Light(BCSV.Entry e, string parent) : base(e)
        {
            mName = mEntry.Get<string>("AreaLightName");
            mLightNo = mEntry.Get<int>("LightID");
            mParent = parent;
        }

        public override void Save()
        {
            mEntry.Set("AreaLightName", mName);
            mEntry.Set("LightID", mLightNo);
        }

        public override string ToString()
        {
            return $"[{mLightNo}] {mName} [{mParent}]";
        }

        public string mParent;

        public int mLightNo;

    }
}
