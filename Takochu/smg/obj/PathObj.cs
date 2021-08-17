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
            mFilesystem = filesystem;
            mEntry = entry;
            mName = mEntry.Get<string>("name");
            mNo = mEntry.Get<short>("no");
            mType = mEntry.Get<string>("type");
            mClosed = mEntry.Get<string>("closed");
            mNumPoint = mEntry.Get<int>("num_pnt");
            mID = mEntry.Get<int>("l_id");

            mPathArgs = new int[8];

            for (int i = 0; i < 8; i++)
                mPathArgs[i] = mEntry.Get<int>($"path_arg{i}");

            mUsage = mEntry.Get<string>("usage");
            mPathID = mEntry.Get<short>("Path_ID");
            mZone = parentZone;

            mPathPointObjs = new List<PathPointObj>();

            BCSV b = new BCSV(mFilesystem.OpenFile($"/Stage/jmp/Path/CommonPathPointInfo.{mNo}"));

            foreach(BCSV.Entry e in b.mEntries)
            {
                mPathPointObjs.Add(new PathPointObj(this, e));
            }
        }

        public void Save()
        {
            mEntry.Set("name", mName);
            mEntry.Set("no", mNo);
            mEntry.Set("type", mType);
            mEntry.Set("closed", mClosed);
            mEntry.Set("num_pnt", mNumPoint);
            mEntry.Set("l_id", mID);

            for (int i = 0; i < 8; i++)
                mEntry.Set($"path_arg{i}", mPathArgs[i]);

            mEntry.Set("usage", mUsage);
            mEntry.Set("Path_ID", mPathID);

            BCSV b = new BCSV(mFilesystem.OpenFile($"/Stage/jmp/Path/CommonPathPointInfo.{mNo}"));
            b.mEntries.Clear();

            foreach (PathPointObj p in mPathPointObjs)
            {
                p.Save();
                b.mEntries.Add(p.mEntry);
            }

            b.Save();
        }

        public override string ToString()
        {
            return $"{mName}";
        }

        public RARCFilesystem mFilesystem;
        public BCSV.Entry mEntry;
        public string mName;
        public short mNo;
        public string mType;
        public string mClosed;
        public int mNumPoint;
        public int mID;
        public string mUsage;
        public short mPathID;

        public int[] mPathArgs;
        public Zone mZone;

        public List<PathPointObj> mPathPointObjs;
    }
}
