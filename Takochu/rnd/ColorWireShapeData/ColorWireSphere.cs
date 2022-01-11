using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace Takochu.rnd.ColorWireShapeData
{
    public class ColorWireSphere : IColorWireShape
    {
        private Vector3 _size;
        private Vector4 _fillColor;
        private Color _color;



        private int row = 20;
        private int column = 40;
        private float rad = 500;

        public ColorWireSphere()
        {
            _size = Vector3.One;
            _fillColor = Vector4.One;
            _color = Color.Black;
        }

        public void Render(RenderInfo info)
        {
            if (info.Mode == RenderMode.Translucent) return;

            var sx = _size.X;
            var sy = _size.Y;
            var sz = _size.Z;
            var s = _size;
            if (info.Mode != RenderMode.Picking)
            {
                for (int i = 0; i < 8; i++)
                {
                    GL.ActiveTexture(TextureUnit.Texture0 + i);
                    GL.Disable(EnableCap.Texture2D);
                }

                GL.DepthFunc(DepthFunction.Lequal);
                GL.DepthMask(true);
                GL.Color4(_fillColor);
                GL.Disable(EnableCap.Lighting);
                GL.Disable(EnableCap.Blend);
                GL.Disable(EnableCap.ColorLogicOp);
                GL.Disable(EnableCap.AlphaTest);
                GL.CullFace(CullFaceMode.Front);
                try { GL.UseProgram(0); } catch { }
            }



            if (info.Mode != RenderMode.Picking)
            {
                GL.LineWidth(2.5f);
                GL.Color4(_color);

                GL.Begin(BeginMode.Lines);

                rad = _size.X;

                //頂点位置計算
                for (var i = 0; i <= row; i++)
                {
                    //経度
                    var r = Math.PI / row * i;
                    var rx = Math.Cos(r);
                    var rz = Math.Sin(r);

                    var nr = Math.PI / row * (i + 1);
                    var nrx = Math.Cos(nr);
                    var nrz = Math.Sin(nr);



                    for (int ii = 0; ii <= column; ii++)
                    {
                        //緯度
                        var vr = Math.PI * 2 / column * ii;
                        var vrx = Math.Cos(vr);
                        var vry = Math.Sin(vr);


                        var nvr = Math.PI * 2 / column * (ii + 1);
                        var nvrx = Math.Cos(nvr);
                        var nvry = Math.Sin(nvr);


                        double tx = rx * rad * vrx;
                        double ty = vry * rad ;
                        double tz = rz * rad * vrx;


                        double ntx = nrx * rad * vrx;
                        double nty = vry * rad;
                        double ntz = nrz * rad * vrx;


                        double hntx = nrx * rad * nvrx;
                        double hnty = nvry * rad;
                        double hntz = nrz * rad * nvrx;

                        double htx = rx * rad * nvrx;
                        double hty = nvry * rad;
                        double htz = rz * rad * nvrx;

                        Vector3 pos = new Vector3((float)tx, (float)ty, (float)tz);
                        Vector3 npos = new Vector3((float)ntx, (float)nty, (float)ntz);
                        Vector3 hnpos = new Vector3((float)hntx, (float)hnty, (float)hntz);
                        Vector3 hpos = new Vector3((float)htx, (float)hty, (float)htz);
                        GL.Vertex3(pos);
                        GL.Vertex3(npos);

                        GL.Vertex3(npos);
                        GL.Vertex3(hnpos);

                        GL.Vertex3(hnpos);
                        GL.Vertex3(hpos);
                    }
                    
                }

                
                
                GL.End();

                

                //if (m_ShowAxes)
                //{
                //    GL.Begin(BeginMode.Lines);
                //    GL.Color3(1.0f, 0.0f, 0.0f);
                //    GL.Vertex3(0.0f, 0.0f, 0.0f);
                //    GL.Color3(1.0f, 0.0f, 0.0f);
                //    GL.Vertex3(s * 2.0f, 0.0f, 0.0f);
                //    GL.Color3(0.0f, 1.0f, 0.0f);
                //    GL.Vertex3(0.0f, 0.0f, 0.0f);
                //    GL.Color3(0.0f, 1.0f, 0.0f);
                //    GL.Vertex3(0.0f, s * 2.0f, 0.0f);
                //    GL.Color3(0.0f, 0.0f, 1.0f);
                //    GL.Vertex3(0.0f, 0.0f, 0.0f);
                //    GL.Color3(0.0f, 0.0f, 1.0f);
                //    GL.Vertex3(0.0f, 0.0f, s * 2.0f);
                //    GL.End();
                //}
            }
        }

        public void SetColor(AreaType areaType)
        {
            Color color = _color;
            switch (areaType)
            {
                case AreaType.Normal:
                    color = Color.Aqua;
                    break;
                case AreaType.Camera:
                    color = Color.Red;
                    break;
                case AreaType.Gravity:
                    color = Color.DarkOliveGreen;
                    break;
                default:

                    break;
            }

            _color = color;
        }

        public void SetFillColor(Vector4 fillColor)
        {
            _fillColor = fillColor;
        }

        public void SetSize(Vector3 size)
        {
            _size = size;
        }

       

    }
}
