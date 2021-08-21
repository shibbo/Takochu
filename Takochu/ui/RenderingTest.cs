using OpenTK;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.io;
using Takochu.rnd;

namespace Takochu.ui
{
    public partial class RenderingTest : Form
    {
        public RenderingTest()
        {
            InitializeComponent();
        }

        private void UpdateViewport()
        {
            GL.Viewport(glLevelView.ClientRectangle);

            m_AspectRatio = (float)glLevelView.Width / (float)glLevelView.Height;
            GL.MatrixMode(MatrixMode.Projection);
            Matrix4 projmtx = Matrix4.CreatePerspectiveFieldOfView(k_FOV, m_AspectRatio, k_zNear, k_zFar);
            GL.LoadMatrix(ref projmtx);

            m_PixelFactorX = ((2f * (float)Math.Tan(k_FOV / 2f) * m_AspectRatio) / (float)(glLevelView.Width));
            m_PixelFactorY = ((2f * (float)Math.Tan(k_FOV / 2f)) / (float)(glLevelView.Height));
        }

        private void UpdateCamera()
        {
            Vector3 up;

            if (Math.Cos(m_CamRotation.Y) < 0)
            {
                m_UpsideDown = true;
                up = new Vector3(0.0f, -1.0f, 0.0f);
            }
            else
            {
                m_UpsideDown = false;
                up = new Vector3(0.0f, 1.0f, 0.0f);
            }

            m_CamPosition.X = m_CamDistance * (float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            m_CamPosition.Y = m_CamDistance * (float)Math.Sin(m_CamRotation.Y);
            m_CamPosition.Z = m_CamDistance * (float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

            Vector3 skybox_target;
            skybox_target.X = -(float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
            skybox_target.Y = -(float)Math.Sin(m_CamRotation.Y);
            skybox_target.Z = -(float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

            Vector3.Add(ref m_CamPosition, ref m_CamTarget, out m_CamPosition);

            m_CamMatrix = Matrix4.LookAt(m_CamPosition, m_CamTarget, up);
            m_SkyboxMatrix = Matrix4.LookAt(Vector3.Zero, skybox_target, up);

            //m_CamMatrix = Matrix4.Mult(Matrix4.Scale(0.0001f), m_CamMatrix);
            m_CamMatrix = Matrix4.Mult(Matrix4.CreateScale(0.0001f), m_CamMatrix);

            //GL.NewList(m_RenderInfo.BillboardDL, ListMode.Compile);
            //GL.Rotate(m_CamRotation.Y, 0f, 1f, 0f);
            //GL.EndList();
        }

        private const float k_FOV = (float)((70f * Math.PI) / 180f);
        private const float k_zNear = 0.01f;
        private const float k_zFar = 1000f;

        private bool m_GLLoaded;
        private float m_AspectRatio;
        private Vector2 m_CamRotation;
        private Vector3 m_CamPosition;
        private Vector3 m_CamTarget;
        private float m_CamDistance;
        private bool m_UpsideDown;
        private Matrix4 m_CamMatrix, m_SkyboxMatrix;
        private RenderInfo m_RenderInfo;

        private MouseButtons m_MouseDown;
        private Point m_LastMouseMove, m_LastMouseClick;

        int mainlist = 0;

        private void glLevelView_Paint(object sender, PaintEventArgs e)
        {
            if (!m_GLLoaded) return;
            glLevelView.MakeCurrent();

            GL.DepthMask(true); // ensures that GL.Clear() will successfully clear the buffers

            GL.ClearColor(0f, 0f, 0.125f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            //GL.PolygonMode(MaterialFace.FrontAndBack, m_MouseDown != MouseButtons.None ? PolygonMode.Line : PolygonMode.Fill);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref m_CamMatrix);

            GL.Disable(EnableCap.Texture2D);

            GL.Begin(PrimitiveType.Lines);
            GL.Color4(1f, 0f, 0f, 1f);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(100000f, 0f, 0f);
            GL.Color4(0f, 1f, 0f, 1f);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0, 100000f, 0f);
            GL.Color4(0f, 0f, 1f, 1f);
            GL.Vertex3(0f, 0f, 0f);
            GL.Vertex3(0f, 0f, 100000f);
            GL.End();

            GL.Color4(1f, 1f, 1f, 1f);

            /*m_RenderInfo.Mode = RenderMode.Opaque;
            testrenderer.Render(m_RenderInfo);
            m_RenderInfo.Mode = RenderMode.Translucent;
            testrenderer.Render(m_RenderInfo);*/
            GL.CallList(mainlist);

            glLevelView.SwapBuffers();
        }

        private void glLevelView_MouseMove(object sender, MouseEventArgs e)
        {
            float xdelta = (float)(e.X - m_LastMouseMove.X);
            float ydelta = (float)(e.Y - m_LastMouseMove.Y);

            m_LastMouseMove = e.Location;

            if (m_MouseDown != MouseButtons.None)
            {
                if (m_MouseDown == MouseButtons.Right)
                {
                    /*if (btnReverseRot.Checked)
                    {
                        xdelta = -xdelta;
                        ydelta = -ydelta;
                    }*/

                    if (m_UpsideDown)
                        xdelta = -xdelta;

                    m_CamRotation.X -= xdelta * 0.002f;
                    m_CamRotation.Y -= ydelta * 0.002f;
                    //m_CamRotation.X -= (float)Math.Tan((xdelta * m_PixelFactorX) / m_PickingDepth);//xdelta * m_PixelFactorX * m_PickingDepth;
                    //m_CamRotation.Y -= ydelta * m_PixelFactorY * m_PickingDepth;

                    //ClampRotation(ref m_CamRotation.X, (float)Math.PI * 2.0f);
                    //ClampRotation(ref m_CamRotation.Y, (float)Math.PI * 2.0f);
                }
                else if (m_MouseDown == MouseButtons.Left)
                {
                    xdelta *= 0.005f;
                    ydelta *= 0.005f;
                    //xdelta *= m_PixelFactorX * m_PickingDepth;
                    //ydelta *= m_PixelFactorY * m_PickingDepth;

                    m_CamTarget.X -= xdelta * (float)Math.Sin(m_CamRotation.X);
                    m_CamTarget.X -= ydelta * (float)Math.Cos(m_CamRotation.X) * (float)Math.Sin(m_CamRotation.Y);
                    m_CamTarget.Y += ydelta * (float)Math.Cos(m_CamRotation.Y);
                    m_CamTarget.Z += xdelta * (float)Math.Cos(m_CamRotation.X);
                    m_CamTarget.Z -= ydelta * (float)Math.Sin(m_CamRotation.X) * (float)Math.Sin(m_CamRotation.Y);
                }

                UpdateCamera();
            }

            glLevelView.Refresh();
        }

        private void glLevelView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != m_MouseDown) return;

            m_MouseDown = MouseButtons.None;
            m_LastMouseMove = e.Location;
        }

        private void glLevelView_MouseDown(object sender, MouseEventArgs e)
        {
            if (m_MouseDown != MouseButtons.None) return;

            m_MouseDown = e.Button;
            m_LastMouseMove = m_LastMouseClick = e.Location;
        }

        private void glLevelView_Resize(object sender, EventArgs e)
        {
            if (!m_GLLoaded) return;
            glLevelView.MakeCurrent();

            UpdateViewport();
        }

        private void glLevelView_Load(object sender, EventArgs e)
        {
            glLevelView.MakeCurrent();

            GL.Enable(EnableCap.DepthTest);
            GL.ClearDepth(1f);

            //GL.Enable(EnableCap.AlphaTest);
            //GL.AlphaFunc(AlphaFunction.Gequal, 0.1f);
            //GL.Enable(EnableCap.Blend);
            //GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            GL.FrontFace(FrontFaceDirection.Cw);

            // values taken from SM64DSe
            // todo tweak (possibly assign dynamically depending on the model's size)
            // m_CamRotation = new Vector2(0.0f, (float)Math.PI / 8.0f);
            m_CamRotation = new Vector2(0.0f, 0.0f);
            m_CamTarget = new Vector3(0.0f, 0.0f, 0.0f);
            m_CamDistance = 1f;// 700.0f;

            m_RenderInfo = new RenderInfo();
            //m_RenderInfo.BillboardDL = GL.GenLists(2);
            //m_RenderInfo.YBillboardDL = m_RenderInfo.BillboardDL + 1;

            UpdateViewport();
            UpdateCamera();

            mainlist = GL.GenLists(1);

            GL.NewList(mainlist, ListMode.Compile);

            RARCFilesystem rarc = new RARCFilesystem(Program.sGame.mFilesystem.OpenFile("/ObjectData/CloudSky.arc"));
            BMD bmd = new BMD(rarc.OpenFile("/asdf/CloudSky.bdl"));

            GL.PushMatrix();

            GL.Scale(1, 1, 1);
            RenderInfo inf = new RenderInfo();
            inf.Mode = RenderMode.Opaque;
            BmdRenderer rend = new BmdRenderer(bmd);
            rend.Render(inf);

            GL.PopMatrix();

            GL.EndList();

            bmd.Close();
            rarc.Close();

            m_GLLoaded = true;
        }

        private void glLevelView_MouseWheel(object sender, MouseEventArgs e)
        {
            /*if ((m_MouseDown == MouseButtons.Left) && ((m_Selected >> 28) != 0xF) && (m_LastClicked == m_Selected))
            {
                float delta = -(e.Delta / 120f);
                delta = ((delta < 0f) ? -1f : 1f) * (float)Math.Pow(delta, 2f) * 0.05f;
                m_SelectedObject.Position.X += delta * (float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
                m_SelectedObject.Position.Y += delta * (float)Math.Sin(m_CamRotation.Y);
                m_SelectedObject.Position.Z += delta * (float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
                float xdist = delta * (m_MouseCoords.X - (glLevelView.Width / 2f)) * m_PixelFactorX;
                float ydist = delta * (m_MouseCoords.Y - (glLevelView.Height / 2f)) * m_PixelFactorY;
                m_SelectedObject.Position.X -= (xdist * (float)Math.Sin(m_CamRotation.X)) + (ydist * (float)Math.Sin(m_CamRotation.Y) * (float)Math.Cos(m_CamRotation.X));
                m_SelectedObject.Position.Y += ydist * (float)Math.Cos(m_CamRotation.Y);
                m_SelectedObject.Position.Z += (xdist * (float)Math.Cos(m_CamRotation.X)) - (ydist * (float)Math.Sin(m_CamRotation.Y) * (float)Math.Sin(m_CamRotation.X));
                UpdateSelection();
            }
            else*/
            {
                float delta = -((e.Delta / 120f) * 0.1f);
                m_CamTarget.X += delta * (float)Math.Cos(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);
                m_CamTarget.Y += delta * (float)Math.Sin(m_CamRotation.Y);
                m_CamTarget.Z += delta * (float)Math.Sin(m_CamRotation.X) * (float)Math.Cos(m_CamRotation.Y);

                UpdateCamera();
            }

            glLevelView.Refresh();
        }

        private float m_PixelFactorX, m_PixelFactorY;
    }
}
