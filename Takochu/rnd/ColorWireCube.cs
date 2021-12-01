using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Takochu.rnd
{
    public class ColorWireCube : RendererBase
    {
        public ColorWireCube(Vector3 size, Vector4 border, Vector4 fill, bool axes)
        {
            m_Size = size;
            m_BorderColor = border;
            m_FillColor = fill;
            m_ShowAxes = axes;
        }

        public override bool GottaRender(RenderInfo info)
        {
            return info.Mode != RenderMode.Translucent;
        }

        public override void Render(RenderInfo info)
        {
            if (info.Mode == RenderMode.Translucent) return;

            var sx = m_Size.X;
            var sy = m_Size.Y;
            var sz = m_Size.Z;
            var s = m_Size;
            if (info.Mode != RenderMode.Picking)
            {
                for (int i = 0; i < 8; i++)
                {
                    GL.ActiveTexture(TextureUnit.Texture0 + i);
                    GL.Disable(EnableCap.Texture2D);
                }

                GL.DepthFunc(DepthFunction.Lequal);
                GL.DepthMask(true);
                GL.Color4(m_FillColor);
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
                GL.Color4(/*m_BorderColor*/System.Drawing.Color.Red);

                

                GL.Begin(BeginMode.LineStrip);
                GL.Vertex3(s);
                GL.Vertex3(-sx,sy,sz);
                GL.Vertex3(-sx, sy, -sz);
                GL.Vertex3(sx, sy, -sz);
                GL.Vertex3(sx, sy, sz);
                GL.Vertex3(sx, -sy, sz);
                GL.Vertex3(-sx, -sy, sz);
                GL.Vertex3(-sx, -sy, -sz);
                GL.Vertex3(sx, -sy, -sz);
                GL.Vertex3(sx, -sy, sz);
                GL.End();

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


        private Vector3 m_Size;
        private Vector4 m_BorderColor, m_FillColor;
        private bool m_ShowAxes;
    }
    
    
}
