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



        private float[] pos = new float[3];
        private int[] idx = new int[6];

        private int row = 20;
        private int column = 20;
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

                GL.Begin(BeginMode.LineStrip);

                rad = _size.X;

                //頂点位置計算
                for (var i = 0; i <= row; i++)
                {
                        //経度
                        var r = Math.PI / row * i;
                        var ry = Math.Cos(r);
                        var rr = Math.Sin(r);
                        for (int ii = 0; ii <= column; ii++)
                        {
                            //緯度
                            var tr = Math.PI * 2 / column * ii;
                            float tx = (float)rr * rad * (float)Math.Cos(tr);
                            float ty = (float)ry * rad;
                            float tz = (float)rr * rad * (float)Math.Sin(tr);

                            pos[0] = tx;
                            pos[1] = ty;
                            pos[2] = tz;
                            GL.Vertex3(pos);

                        }
                    
                }

                //頂点インデックス
                var r2 = 0;
                for (int i = 0; i < row; i++)
                {
                    for (int ii = 0; ii < column; ii++)
                    {
                        r2 = (column + 1) * i + ii;
                        idx[0] = r2;
                        idx[1] = r2 + 1;
                        idx[2] = r2 + column + 2;
                        idx[3] = r2;
                        idx[4] = r2 + column + 2;
                        idx[5] = r2 + column + 1;
                        //idx.push(r2, r2 + column + 2, r2 + column + 1);
                    }
                }

                
                
                GL.End();

                ////バインドされているVBOの情報をattributeとしてシェーダーに送る(GPUに送る)
                ////Attribute Pointers
                //GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);
                //GL.EnableVertexAttribArray(0);

                //GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3 * sizeof(float));
                //GL.EnableVertexAttribArray(1);

                //GL.VertexAttribPointer(2, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 6 * sizeof(float));
                //GL.EnableVertexAttribArray(2);

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
