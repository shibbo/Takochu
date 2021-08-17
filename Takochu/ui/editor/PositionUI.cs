using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.smg.obj;

namespace Takochu.ui.editor
{
    public class PositionUI
    {
        AbstractObj obj;
        bool useScale;

        public PositionUI(AbstractObj obj, bool usesScale = true)
        {
            this.obj = obj;
            useScale = usesScale;
        }
    }
}
