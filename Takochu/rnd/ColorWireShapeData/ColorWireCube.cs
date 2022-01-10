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
    public class ColorWireCube: IColorWireShape
    {
        private Vector3 _size;
        private Vector4 _fillColor;
        private Color _color;

        public ColorWireCube() 
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

            //GL.Begin(BeginMode.TriangleStrip);
            //GL.Vertex3(-s, -s, -s);
            //GL.Vertex3(-s, s, -s);
            //GL.Vertex3(s, -s, -s);
            //GL.Vertex3(s, s, -s);
            //GL.Vertex3(s, -s, s);
            //GL.Vertex3(s, s, s);
            //GL.Vertex3(-s, -s, s);
            //GL.Vertex3(-s, s, s);
            //GL.Vertex3(-s, -s, -s);
            //GL.Vertex3(-s, s, -s);
            //GL.End();

            //GL.Begin(BeginMode.TriangleStrip);
            //GL.Vertex3(-s, s, -s);
            //GL.Vertex3(-s, s, s);
            //GL.Vertex3(s, s, -s);
            //GL.Vertex3(s, s, s);
            //GL.End();

            //GL.Begin(BeginMode.TriangleStrip);
            //GL.Vertex3(-s, -s, -s);
            //GL.Vertex3(s, -s, -s);
            //GL.Vertex3(-s, -s, s);
            //GL.Vertex3(s, -s, s);
            //GL.End();

            if (info.Mode != RenderMode.Picking)
            {
                GL.LineWidth(2.5f);
                GL.Color4(_color);



                GL.Begin(BeginMode.LineStrip);
                //上面
                GL.Vertex3(sx, sy, sz);
                GL.Vertex3(-sx, sy, sz);
                GL.Vertex3(-sx, sy, -sz);
                GL.Vertex3(sx, sy, -sz);
                GL.Vertex3(sx, sy, sz);

                //下面
                GL.Vertex3(sx, -sy, sz);
                GL.Vertex3(-sx, -sy, sz);
                GL.Vertex3(-sx, -sy, -sz);
                GL.Vertex3(sx, -sy, -sz);
                GL.Vertex3(sx, -sy, sz);
                GL.End();

                //縦の線
                GL.Begin(BeginMode.Lines);
                GL.Vertex3(-sx, sy, sz);
                GL.Vertex3(-sx, -sy, sz);
                GL.Vertex3(-sx, sy, -sz);
                GL.Vertex3(-sx, -sy, -sz);
                GL.Vertex3(sx, sy, -sz);
                GL.Vertex3(sx, -sy, -sz);
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
