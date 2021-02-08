using GL_EditorFramework.EditorDrawables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.io;

namespace Takochu.smg.obj
{
    public class PathObj
    {
        public PathObj(BCSV.Entry entry, Zone parentZone, RARCFilesystem filesystem)
        {
            mName = entry.Get<string>("name");
            mID = entry.Get<short>("no");
            mZone = parentZone;

            mPathPointObjs = new List<PathPointObj>();

            BCSV b = new BCSV(filesystem.OpenFile($"/Stage/jmp/Path/CommonPathPointInfo.{mID}"));

            foreach(BCSV.Entry e in b.mEntries)
            {
                mPathPointObjs.Add(new PathPointObj(this, e));
            }
        }

        public override string ToString()
        {
            return $"{mName}";
        }

        public string mName;
        public short mID;
        public Zone mZone;

        public List<PathPointObj> mPathPointObjs;
    }
}
