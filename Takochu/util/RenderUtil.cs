using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takochu.util
{
    public static class RenderUtil
    {
        public static Matrix4 SRTToMatrix(Vector3 scale, Vector3 rot, Vector3 trans)
        {
            Matrix4 ret = Matrix4.Identity;

            Matrix4 mscale = Matrix4.CreateScale(scale);
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
    }
}
