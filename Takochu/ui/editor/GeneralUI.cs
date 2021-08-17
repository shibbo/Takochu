using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.smg.obj;

namespace Takochu.ui.editor
{
    class GeneralUI
    {
        AbstractObj obj;

        List<string> zones;

        public GeneralUI(AbstractObj obj)
        {
            this.obj = obj;

            zones = new List<string>();
            zones.AddRange(obj.mParentZone.mGalaxy.GetZones().Keys);
        }
    }
}