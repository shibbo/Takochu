using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.calc;
using Takochu.fmt;
using Takochu.io;
using Takochu.rnd;
using Takochu.util;
using static Takochu.util.EditorUtil;

namespace Takochu.smg.obj
{
    public class PathObj : AbstractObj
    {
        public PathObj(BCSV.Entry entry, Zone parentZone, RARCFilesystem filesystem) : base(entry)
        {
            mFilesystem = filesystem;
            mEntry = entry;
            mParentZone = parentZone;
            mName = mEntry.Get<string>("name");
            mNo = mEntry.Get<short>("no");
            mType = mEntry.Get<string>("type");
            mClosed = mEntry.Get<string>("closed");
            mNumPoint = mEntry.Get<int>("num_pnt");
            mID = mEntry.Get<int>("l_id");

            mPathArgs = new int[8];
            mType = "PathObj";

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

            mPathColor = RenderUtil.GenerateRandomColor();
        }

        public void RemovePathPointAtIndex(int idx)
        {
            mPathPointObjs.RemoveAt(idx);
        }

        public override void Reload_mValues()
        {
            mNo = ObjectTypeChange.ToInt16(mEntry.Get("no"));
            mNumPoint = ObjectTypeChange.ToInt32(mEntry.Get("num_pnt"));

            for (int i = 0; i < 8; i++)
            {
                mPathArgs[i] = ObjectTypeChange.ToInt32(mEntry.Get($"path_arg{i}"));
            }
        }

        public override void Save()
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

        public override void Render(RenderMode mode)
        {
            foreach (PathPointObj pp in mPathPointObjs)
            {
                pp.Render(1, mPathColor, mode);
                pp.Render(2, mPathColor, mode);
                pp.Render(0, mPathColor, mode);

                if (mode != RenderMode.Picking)
                {
                    GL.Color4(mPathColor);
                }

                GL.LineWidth(1.0f);
                GL.Begin(BeginMode.LineStrip);

                GL.Vertex3(pp.mPoint1);
                GL.Vertex3(pp.mPoint0);
                GL.Vertex3(pp.mPoint2);
                GL.End();
            }

            GL.Color4(mPathColor);

            if (mPathPointObjs.Count != 0)
            {
                GL.LineWidth(1.5f);
                GL.Begin(BeginMode.LineStrip);

                int end = mPathPointObjs.Count;

                if (mClosed == "CLOSE")
                    end++;

                IEnumerator<PathPointObj> thepoints = mPathPointObjs.GetEnumerator();
                thepoints.MoveNext();
                PathPointObj curpoint = thepoints.Current;

                Vector3 start = curpoint.mPoint0;

                GL.Vertex3(start);
               
                for (int p = 1; p < end; p++)
                {
                    Vector3 p1 = curpoint.mPoint0;
                    Vector3 p2 = curpoint.mPoint2;

                    if (!thepoints.MoveNext())
                    {
                        thepoints = mPathPointObjs.GetEnumerator();
                        thepoints.MoveNext();
                    }

                    curpoint = thepoints.Current;

                    Vector3 p3 = curpoint.mPoint1;
                    Vector3 p4 = curpoint.mPoint0;

                    float step = 0.01f;

                    for (float t = step; t < 1.0f; t += step)
                    {
                        float p1t = (1.0f - t) * (1.0f - t) * (1.0f - t);
                        float p2t = 3.0f * t * (1.0f - t) * (1.0f - t);
                        float p3t = 3.0f * t * t * (1.0f - t);
                        float p4t = t * t * t;

                        GL.Vertex3(p1.X * p1t + p2.X * p2t + p3.X * p3t + p4.X * p4t, p1.Y * p1t + p2.Y * p2t + p3.Y * p3t + p4.Y * p4t, p1.Z * p1t + p2.Z * p2t + p3.Z * p3t + p4.Z * p4t);
                    }
                }

                GL.End();
            }
        }

        public override string ToString()
        {
            return $"{mName}";
        }

        public RARCFilesystem mFilesystem;
        public short mNo;
        public string mClosed;
        public int mNumPoint;
        public int mID;
        public string mUsage;
        public short mPathID;

        public int[] mPathArgs;
        public Zone mZone;

        public List<PathPointObj> mPathPointObjs;
        public OpenTK.Graphics.Color4 mPathColor;
    }
}
