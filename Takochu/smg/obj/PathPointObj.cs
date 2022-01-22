using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.rnd;

namespace Takochu.smg.obj
{
    public class PathPointObj : AbstractObj
    {
        public PathPointObj(PathObj parent, BCSV.Entry entry) : base(entry)
        {
            mParent = parent;
            mParentZone = parent.mParentZone;

            mID = mEntry.Get<short>("id");

            mPoint0 = new Vector3
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

        public override void Save()
        {
            mEntry.Set("id", mID);

            mEntry.Set("pnt0_x", mPoint0.X);
            mEntry.Set("pnt0_y", mPoint0.Y);
            mEntry.Set("pnt0_z", mPoint0.Z);

            mEntry.Set("pnt1_x", mPoint1.X);
            mEntry.Set("pnt1_y", mPoint1.Y);
            mEntry.Set("pnt1_z", mPoint1.Z);

            mEntry.Set("pnt2_x", mPoint2.X);
            mEntry.Set("pnt2_y", mPoint2.Y);
            mEntry.Set("pnt2_z", mPoint2.Z);
        }

        public void Render(int pointNo, OpenTK.Graphics.Color4 color, RenderMode mode)
        {
            Vector3 point = Vector3.Zero;

            switch (pointNo)
            {
                case 0:
                    point = mPoint0;
                    break;
                case 1:
                    point = mPoint1;
                    break;
                default:
                    point = mPoint2;
                    break;
            }

            GL.PushMatrix();

            GL.Translate(point);

            ColorCubeRenderer rend = new ColorCubeRenderer(pointNo == 0 ? 200f : 100f, new Vector4(1f, 1f, 1f, 1f), new Vector4(color.R, color.G, color.B, color.A), true);

            RenderInfo inf = new RenderInfo();
            inf.Mode = mode;
            rend.Render(inf);

            GL.PopMatrix();
        }

        public override string ToString()
        {
            return $"[{mParent.mID}] {mParent.mName} (Point {mID}) ({mParent.mZone.mZoneName})";
        }

        PathObj mParent;
        short mID;
        public Vector3 mPoint0;
        public Vector3 mPoint1;
        public Vector3 mPoint2;

        int[] mPointArgs;
    }
}
