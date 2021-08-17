using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.smg;
using Takochu.smg.obj;
using static Takochu.smg.ObjectDB;

namespace Takochu.ui.editor
{
    public class ParameterUI
    {
        AbstractObj obj;
        int arg_count;

        public ParameterUI(AbstractObj obj, int arg)
        {
            this.obj = obj;
            this.arg_count = arg;
        }
    }
}
