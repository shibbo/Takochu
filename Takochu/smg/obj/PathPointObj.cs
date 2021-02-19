using GL_EditorFramework.EditorDrawables;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;

namespace Takochu.smg.obj
{
    public class PathPointObj : TransformableObject
    {
        public PathPointObj(PathObj parent, BCSV.Entry entry) : base(Vector3.Zero, Vector3.Zero, Vector3.One)
        {
            mEntry = entry;
            mParent = parent;

            mID = mEntry.Get<short>("id");

            mPosition = new Vector3
            {
                X = mEntry.Get<float>("pnt0_x"),
                Y = mEntry.Get<float>("pnt0_y"),
                Z = mEntry.Get<float>("pnt0_z")
            };

            mPoint1 = new Vector3
            {
                X = mEntry.Get<float>("pnt1_x"),
                Y = mEntry.Get<float>("pnt1_y"),
                Z = mEntry.Get<float>("pnt1_z")
            };

            mPoint2 = new Vector3
            {
                X = mEntry.Get<float>("pnt2_x"),
                Y = mEntry.Get<float>("pnt2_y"),
                Z = mEntry.Get<float>("pnt2_z")
            };

            mPointArgs = new int[8];

            for (int i = 0; i < 8; i++)
            {
                mPointArgs[i] = mEntry.Get<int>($"point_arg{i}");
            }
        }

        public void Save()
        {
            mEntry.Set("id", mID);

            mEntry.Set("pnt0_x", mPosition.X);
            mEntry.Set("pnt0_y", mPosition.Y);
            mEntry.Set("pnt0_z", mPosition.Z);

            mEntry.Set("pnt0_x", mPoint1.X);
            mEntry.Set("pnt0_y", mPoint1.Y);
            mEntry.Set("pnt0_z", mPoint1.Z);

            mEntry.Set("pnt0_x", mPoint2.X);
            mEntry.Set("pnt0_y", mPoint2.Y);
            mEntry.Set("pnt0_z", mPoint2.Z);
        }

        public override string ToString()
        {
            return $"[{mParent.mID}] {mParent.mName} (Point {mID}) ({mParent.mZone.mZoneName})";
        }

        public BCSV.Entry mEntry;
        PathObj mParent;
        short mID;
        Vector3 mPosition;
        Vector3 mPoint1;
        Vector3 mPoint2;

        int[] mPointArgs;
    }
}
