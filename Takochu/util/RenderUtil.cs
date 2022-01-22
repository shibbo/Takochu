using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using Takochu.smg.obj;
using OpenTK.Graphics;

namespace Takochu.util
{
    /// <summary>
    /// レンダリング時に使える便利関数
    /// </summary>
    public static class RenderUtil
    {
        public static Matrix4 SRTToMatrix(Vector3 scale, Vector3 rot, Vector3 trans)
        {
            Matrix4 ret = Matrix4.Identity;

            Matrix4 mscale = Matrix4.Scale(scale);
            Matrix4 mxrot = Matrix4.CreateRotationX(rot.X);
            Matrix4 myrot = Matrix4.CreateRotationY(rot.Y);
            Matrix4 mzrot = Matrix4.CreateRotationZ(rot.Z);
            Matrix4 mtrans = Matrix4.CreateTranslation(trans);

            Matrix4.Mult(ref ret, ref mscale, out ret);
            Matrix4.Mult(ref ret, ref mxrot, out ret);
            Matrix4.Mult(ref ret, ref myrot, out ret);
            Matrix4.Mult(ref ret, ref mzrot, out ret);
            Matrix4.Mult(ref ret, ref mtrans, out ret);

            return ret;
        }

        public static Vector3 ApplyZoneRotation(StageObj stageObj, Vector3 delta)
        {
            float rotationY = stageObj.mRotation.Y;
            float xcos = (float)Math.Cos(-((int)stageObj.mRotation.Z * Math.PI) / 180f);
            float xsin = (float)Math.Sin(-((int)stageObj.mRotation.Z * Math.PI) / 180f);
            float ycos = (float)Math.Cos(-((int)rotationY * Math.PI) / 180f);
            float ysin = (float)Math.Sin(-((int)rotationY * Math.PI) / 180f);
            float zcos = (float)Math.Cos(-((int)stageObj.mRotation.X * Math.PI) / 180f);
            float zsin = (float)Math.Sin(-((int)stageObj.mRotation.X * Math.PI) / 180f);

            float x1 = (delta.X * zcos) - (delta.Y * zsin);
            float y1 = (delta.X * zsin) + (delta.Y * zcos);
            float x2 = (x1 * ycos) + (delta.Z * ysin);
            float z2 = -(x1 * ysin) + (delta.Z * ycos);
            float y3 = (y1 * xcos) - (z2 * xsin);
            float z3 = (y1 * xsin) + (z2 * xcos);

            delta.X = x2;
            delta.Y = y3;
            delta.Z = z3;

            return delta;
        }

        public static void AssignColors()
        {
            sColors = new Color4[8];
            sColors[0] = sRedColor;
            sColors[1] = sOrangeColor;
            sColors[2] = sYellowColor;
            sColors[3] = sGreenColor;
            sColors[4] = sLightBlue;
            sColors[5] = sBlue;
            sColors[6] = sPurple;
            sColors[7] = sPink;
        }

        public static Color4 GenerateRandomColor()
        {
            if (sCurrentColor == sColors.Length)
                sCurrentColor = 0;

            return sColors[sCurrentColor++];
        }

        public class Ray
        {
            public Vector3 Origin;
            public Vector3 Direction;
            public Ray(Vector3 origin, Vector3 dir)
            {
                Origin = origin;
                Direction = dir;
            }
        }

        static int sCurrentColor = 0;

        static Color4[] sColors;

        static Color4 sRedColor = new Color4(254, 45, 0, 100);
        static Color4 sOrangeColor = new Color4(253, 130, 0, 100);
        static Color4 sYellowColor = new Color4(253, 244, 0, 100);
        static Color4 sGreenColor = new Color4(0, 253, 17, 100);
        static Color4 sLightBlue = new Color4(0, 236, 253, 100);
        static Color4 sBlue = new Color4(0, 54, 253, 100);
        static Color4 sPurple = new Color4(164, 0, 253, 100);
        static Color4 sPink = new Color4(253, 0, 219, 100);

    }
}
