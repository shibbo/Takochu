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
    public class ColorWireCylinder : IColorWireShape
    {
        private Vector3 _size;
        private Vector4 _fillColor;
        private Color _color;


        private List<Vector3> _pos;
        private float[] pos = new float[3];
        private int[] idx = new int[6];

        private int row = 20;
        private int column = 20;
        private float rad = 500;

        public ColorWireCylinder()
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
                    //row個の角
                    double r = Math.PI * 2 / row * i;
                    //半径1の時の横幅
                    double rx = Math.Cos(r);
                    //半径1の時の奥行き
                    double rz = Math.Sin(r);

                    //実際の横幅
                    double x = rx * rad;
                    //実際の奥行き
                    double z = rz * rad;
                    Vector3 hpos = new Vector3((float)x, _size.Y, (float)z);
                    Vector3 lpos = new Vector3((float)x, -_size.Y, (float)z);


                    double nr = Math.PI * 2 / row * (i + 1);
                    double nrx = Math.Cos(nr);
                    double nrz = Math.Sin(nr);

                    double nx = nrx * rad;
                    double nz = nrz * rad;
                    Vector3 nhpos = new Vector3((float)nx, _size.Y, (float)nz);
                    Vector3 nlpos = new Vector3((float)nx, -_size.Y, (float)nz);


                    GL.Vertex3(hpos);
                    GL.Vertex3(nhpos);

                    GL.Vertex3(nhpos);
                    GL.Vertex3(nlpos);

                    GL.Vertex3(nlpos);
                    GL.Vertex3(lpos);



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
