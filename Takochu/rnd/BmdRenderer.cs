using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Takochu.fmt;
using Takochu.rnd.BmdRendererSys;

// BMD renderer TODO list
// * finish TEV/material emulation
// * texgens
// * certain texture formats (texture decoder is in Bmd.cs)
// * perhaps not keep the whole model stored in RAM all the time?
// * check transform support (especially weighted matrices)

namespace Takochu.rnd
{
    public class BmdRenderer : RendererBase
    {
        public string debugshaders;

        public BmdRenderer(BMD model)
        {
            m_Model = model;

            string[] extensions = GL.GetString(StringName.Extensions).Split(' ');
            m_HasShaders = 
                extensions.Contains("GL_ARB_shading_language_100") &&
                extensions.Contains("GL_ARB_shader_objects") &&
                extensions.Contains("GL_ARB_vertex_shader") &&
                extensions.Contains("GL_ARB_fragment_shader");
            // TODO: setting for turning shaders on/off

            UploadTextures();

            if (m_HasShaders)
            {
                m_Shaders = new Shader[model.Materials.Length];
                for (int i = 0; i < model.Materials.Length; i++)
                {
                    try 
                    {
                        ShaderSetting shaderSetting = new ShaderSetting(model, i);
                        shaderSetting.GenerateShader(ref m_Shaders);
                        //GenerateShaders(i);
                    }
                    catch (Exception ex)
                    {
                        // really ugly hack
                        if (ex.Message[0] == '!')
                        {
                            //StringBuilder src = new StringBuilder(10000); int lolz;
                            //GL.GetShaderSource(m_Shaders[i].FragmentShader, 10000, out lolz, src);
                            //System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + src.ToString());
                            //throw ex;
                        }

                        m_Shaders[i].Program = 0;
                    }
                }
            }
        }

        private void UploadTextures()
        {
            m_Textures = new int[m_Model.Textures.Length];

            for (int id = 0; id < m_Model.Textures.Length; id++)
            {
                int TexID = GL.GenTexture();
                m_Textures[id] = TexID;
                TextureSetting TexSetting = new TextureSetting(m_Model.Textures, id, TexID);
                TexSetting.UploadTexture();
            }
        }

        public override void Close()
        {
            if (m_HasShaders)
            {
                foreach (Shader shader in m_Shaders)
                {
                    if (shader.VertexShader > 0)
                    {
                        GL.DetachShader(shader.Program, shader.VertexShader);
                        GL.DeleteShader(shader.VertexShader);
                    }

                    if (shader.FragmentShader > 0)
                    {
                        GL.DetachShader(shader.Program, shader.FragmentShader);
                        GL.DeleteShader(shader.FragmentShader);
                    }

                    if (shader.Program > 0)
                        GL.DeleteProgram(shader.Program);

                }
            }

            foreach (int tex in m_Textures)
                GL.DeleteTexture(tex);

            m_Model.Close();
        }

        public override bool GottaRender(RenderInfo info)
        {
            foreach (BMD.Material mat in m_Model.Materials)
            {
                if (!((mat.DrawFlag == 4) ^ (info.Mode == RenderMode.Translucent)))
                    return true;
            }

            return false;
        }

        public override void Render(RenderInfo info)
        {
            BlendingFactorSrc[] blendsrc = { BlendingFactorSrc.Zero, BlendingFactorSrc.One,
                                               BlendingFactorSrc.One, BlendingFactorSrc.Zero, // um...
                                               BlendingFactorSrc.SrcAlpha, BlendingFactorSrc.OneMinusSrcAlpha,
                                               BlendingFactorSrc.DstAlpha, BlendingFactorSrc.OneMinusDstAlpha,
                                               BlendingFactorSrc.DstColor, BlendingFactorSrc.OneMinusDstColor };
            BlendingFactorDest[] blenddst = { BlendingFactorDest.Zero, BlendingFactorDest.One,
                                                BlendingFactorDest.SrcColor, BlendingFactorDest.OneMinusSrcColor,
                                                BlendingFactorDest.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha,
                                                BlendingFactorDest.DstAlpha, BlendingFactorDest.OneMinusDstAlpha,
                                                BlendingFactorDest.DstColor, BlendingFactorDest.OneMinusDstColor };
            LogicOp[] logicop = { LogicOp.Clear, LogicOp.And, LogicOp.AndReverse, LogicOp.Copy,
                                    LogicOp.AndInverted, LogicOp.Noop, LogicOp.Xor, LogicOp.Or,
                                    LogicOp.Nor, LogicOp.Equiv, LogicOp.Invert, LogicOp.OrReverse,
                                    LogicOp.CopyInverted, LogicOp.OrInverted, LogicOp.Nand, LogicOp.Set };

            Matrix4[] lastmatrixtable = null;

            foreach (BMD.SceneGraphNode node in m_Model.SceneGraph)
            {
                if (node.NodeType != 0) continue;
                int shape = node.NodeID;

                if (node.MaterialID != 0xFFFF)
                {
                    CullFaceMode[] cullmodes = { CullFaceMode.Front, CullFaceMode.Back, CullFaceMode.FrontAndBack };
                    DepthFunction[] depthfuncs = { DepthFunction.Never, DepthFunction.Less, DepthFunction.Equal, DepthFunction.Lequal,
                                                     DepthFunction.Greater, DepthFunction.Notequal, DepthFunction.Gequal, DepthFunction.Always };

                    BMD.Material mat = m_Model.Materials[node.MaterialID];

                    if ((mat.DrawFlag == 4) ^ (info.Mode == RenderMode.Translucent))
                    {
                        //Console.WriteLine("drawFlag "+((mat.DrawFlag == 4) ^ (info.Mode == RenderMode.Translucent)));
                        continue;
                    }
                    //Console.WriteLine("false");
                    //Console.WriteLine("hasShaders"+m_HasShaders);
                    if (m_HasShaders)
                    {
                        // shader: handles multitexturing, color combination, alpha test
                        GL.UseProgram(m_Shaders[node.MaterialID].Program);

                        // do multitexturing
                        for (int i = 0; i < 8; i++)
                        {
                            GL.ActiveTexture(TextureUnit.Texture0 + i);

                            if (mat.TexStages[i] == 0xFFFF)
                            {
                                GL.Disable(EnableCap.Texture2D);
                                continue;
                            }

                            int loc = GL.GetUniformLocation(m_Shaders[node.MaterialID].Program, "texture" + i.ToString());
                            GL.Uniform1(loc, i);

                            int texid = m_Textures[mat.TexStages[i]];
                            GL.Enable(EnableCap.Texture2D);
                            GL.BindTexture(TextureTarget.Texture2D, texid);
                        }
                    }
                    else
                    {
                        AlphaFunction[] alphafunc = { AlphaFunction.Never, AlphaFunction.Less, AlphaFunction.Equal, AlphaFunction.Lequal,
                                                        AlphaFunction.Greater, AlphaFunction.Notequal, AlphaFunction.Gequal, AlphaFunction.Always };

                        // texturing -- texture 0 will be used
                        if (mat.TexStages[0] != 0xFFFF)
                        {
                            int texid = m_Textures[mat.TexStages[0]];
                            GL.Enable(EnableCap.Texture2D);
                            GL.BindTexture(TextureTarget.Texture2D, texid);
                        }
                        else
                            GL.Disable(EnableCap.Texture2D);

                        // alpha test -- only one comparison can be done
                        if (mat.AlphaComp.MergeFunc == 1 && (mat.AlphaComp.Func0 == 7 || mat.AlphaComp.Func1 == 7))
                            GL.Disable(EnableCap.AlphaTest);
                        else if (mat.AlphaComp.MergeFunc == 0 && (mat.AlphaComp.Func0 == 0 || mat.AlphaComp.Func1 == 0))
                        {
                            GL.Enable(EnableCap.AlphaTest);
                            GL.AlphaFunc(AlphaFunction.Never, 0f);
                        }
                        else
                        {
                            GL.Enable(EnableCap.AlphaTest);

                            if ((mat.AlphaComp.MergeFunc == 1 && mat.AlphaComp.Func0 == 0) || (mat.AlphaComp.MergeFunc == 0 && mat.AlphaComp.Func0 == 7))
                                GL.AlphaFunc(alphafunc[mat.AlphaComp.Func1], (float)mat.AlphaComp.Ref1 / 255f);
                            else
                                GL.AlphaFunc(alphafunc[mat.AlphaComp.Func0], (float)mat.AlphaComp.Ref0 / 255f);
                        }
                    }
                    //Console.WriteLine(mat.BlendMode.BlendMode);
                    switch (mat.BlendMode.BlendMode)
                    {
                        case 0:
                            GL.Disable(EnableCap.Blend);
                            GL.Disable(EnableCap.ColorLogicOp);
                            break;

                        case 1:
                        case 3:
                            GL.Enable(EnableCap.Blend);
                            GL.Disable(EnableCap.ColorLogicOp);

                            if (mat.BlendMode.BlendMode == 3)
                                GL.BlendEquation(BlendEquationMode.FuncSubtract);
                            else
                                GL.BlendEquation(BlendEquationMode.FuncAdd);

                            GL.BlendFunc(blendsrc[mat.BlendMode.SrcFactor], blenddst[mat.BlendMode.DstFactor]);
                            break;

                        case 2:
                            GL.Disable(EnableCap.Blend);
                            GL.Enable(EnableCap.ColorLogicOp);
                            GL.LogicOp(logicop[mat.BlendMode.BlendOp]);
                            break;
                    }


                    if (mat.CullMode == 0)
                        GL.Disable(EnableCap.CullFace);
                    else
                    {
                        GL.Enable(EnableCap.CullFace);
                        GL.CullFace(cullmodes[mat.CullMode - 1]);
                    }

                    if (mat.ZMode.EnableZTest)
                    {
                        GL.Enable(EnableCap.DepthTest);
                        GL.DepthFunc(depthfuncs[mat.ZMode.Func]);
                    }
                    else
                        GL.Disable(EnableCap.DepthTest);

                    GL.DepthMask(mat.ZMode.EnableZWrite);
                }
                else
                {
                    //if (info.Mode != RenderMode.Opaque) continue;
                    // if (m_HasShaders) GL.UseProgram(0);
                    throw new Exception("Material-less geometry node " + node.NodeID.ToString());
                }


                BMD.Batch batch = m_Model.Batches[shape];

                /*if (batch.MatrixType == 1)
                {
                    GL.PushMatrix();
                    GL.CallList(info.BillboardDL);
                }
                else if (batch.MatrixType == 2)
                {
                    GL.PushMatrix();
                    GL.CallList(info.YBillboardDL);
                }*/

                foreach (BMD.Batch.Packet packet in batch.Packets)
                {
                    Matrix4[] mtxtable = new Matrix4[packet.MatrixTable.Length];
                    int[] mtx_debug = new int[packet.MatrixTable.Length];

                    for (int i = 0; i < packet.MatrixTable.Length; i++)
                    {
                        if (packet.MatrixTable[i] == 0xFFFF)
                        {
                            mtxtable[i] = lastmatrixtable[i];
                            mtx_debug[i] = 2;
                        }
                        else
                        {
                            BMD.MatrixType mtxtype = m_Model.MatrixTypes[packet.MatrixTable[i]];

                            if (mtxtype.IsWeighted)
                            {
                                //throw new NotImplementedException("weighted matrix");

                                // code inspired from bmdview2, except doesn't work right
                                /*Matrix4 mtx = new Matrix4();
                                Bmd.MultiMatrix mm = m_Model.MultiMatrices[mtxtype.Index];
                                for (int j = 0; j < mm.NumMatrices; j++)
                                {
                                    Matrix4 wmtx = mm.Matrices[j];
                                    float weight = mm.MatrixWeights[j];

                                    Matrix4.Mult(ref wmtx, ref m_Model.Joints[mm.MatrixIndices[j]].Matrix, out wmtx);

                                    Vector4.Mult(ref wmtx.Row0, weight, out wmtx.Row0);
                                    Vector4.Mult(ref wmtx.Row1, weight, out wmtx.Row1);
                                    Vector4.Mult(ref wmtx.Row2, weight, out wmtx.Row2);
                                    //Vector4.Mult(ref wmtx.Row3, weight, out wmtx.Row3);

                                    Vector4.Add(ref mtx.Row0, ref wmtx.Row0, out mtx.Row0);
                                    Vector4.Add(ref mtx.Row1, ref wmtx.Row1, out mtx.Row1);
                                    Vector4.Add(ref mtx.Row2, ref wmtx.Row2, out mtx.Row2);
                                    //Vector4.Add(ref mtx.Row3, ref wmtx.Row3, out mtx.Row3);
                                }
                                mtx.M44 = 1f;
                                mtxtable[i] = mtx;*/

                                // seems fine in most cases
                                // but hey, certainly not right, that data has to be used in some way
                                mtxtable[i] = Matrix4.Identity;

                                mtx_debug[i] = 1;
                            }
                            else
                            {
                                mtxtable[i] = m_Model.Joints[mtxtype.Index].FinalMatrix;
                                mtx_debug[i] = 0;
                            }
                        }
                    }

                    lastmatrixtable = mtxtable;

                    foreach (BMD.Batch.Packet.Primitive prim in packet.Primitives)
                    {
                        BeginMode[] primtypes = { BeginMode.Quads, BeginMode.Points, BeginMode.Triangles, BeginMode.TriangleStrip,
                                                    BeginMode.TriangleFan, BeginMode.Lines, BeginMode.LineStrip, BeginMode.Points };
                        GL.Begin(primtypes[(prim.PrimitiveType - 0x80) / 8]);
                        //GL.Begin(BeginMode.Points);

                        for (int i = 0; i < prim.NumIndices; i++)
                        {

                            if ((prim.ArrayMask & (1 << 11)) != 0) GL.Color4(m_Model.ColorArray[0][prim.ColorIndices[0][i]]);

                            if (m_HasShaders)
                            {
                                if ((prim.ArrayMask & (1 << 12)) != 0)
                                {
                                    Vector4 color2 = m_Model.ColorArray[1][prim.ColorIndices[1][i]];
                                    GL.SecondaryColor3(color2.X, color2.Y, color2.Z);
                                    throw new Exception("color2 detected");
                                }

                                if ((prim.ArrayMask & (1 << 13)) != 0) GL.MultiTexCoord2(TextureUnit.Texture0, ref m_Model.TexcoordArray[0][prim.TexcoordIndices[0][i]]);
                                if ((prim.ArrayMask & (1 << 14)) != 0) GL.MultiTexCoord2(TextureUnit.Texture1, ref m_Model.TexcoordArray[1][prim.TexcoordIndices[1][i]]);
                                if ((prim.ArrayMask & (1 << 15)) != 0) GL.MultiTexCoord2(TextureUnit.Texture2, ref m_Model.TexcoordArray[2][prim.TexcoordIndices[2][i]]);
                                if ((prim.ArrayMask & (1 << 16)) != 0) GL.MultiTexCoord2(TextureUnit.Texture3, ref m_Model.TexcoordArray[3][prim.TexcoordIndices[3][i]]);
                                if ((prim.ArrayMask & (1 << 17)) != 0) GL.MultiTexCoord2(TextureUnit.Texture4, ref m_Model.TexcoordArray[4][prim.TexcoordIndices[4][i]]);
                                if ((prim.ArrayMask & (1 << 18)) != 0) GL.MultiTexCoord2(TextureUnit.Texture5, ref m_Model.TexcoordArray[5][prim.TexcoordIndices[5][i]]);
                                if ((prim.ArrayMask & (1 << 19)) != 0) GL.MultiTexCoord2(TextureUnit.Texture6, ref m_Model.TexcoordArray[6][prim.TexcoordIndices[6][i]]);
                                if ((prim.ArrayMask & (1 << 20)) != 0) GL.MultiTexCoord2(TextureUnit.Texture7, ref m_Model.TexcoordArray[7][prim.TexcoordIndices[7][i]]);
                            }
                            else
                            {
                                if ((prim.ArrayMask & (1 << 13)) != 0) GL.TexCoord2(m_Model.TexcoordArray[0][prim.TexcoordIndices[0][i]]);
                            }
                            //if ((prim.ArrayMask & (1 << 0)) != 0) GL.Color4(debug[prim.PosMatrixIndices[i]]);

                            if ((prim.ArrayMask & (1 << 10)) != 0) GL.Normal3(m_Model.NormalArray[prim.NormalIndices[i]]);

                            Vector3 pos = m_Model.PositionArray[prim.PositionIndices[i]];
                            if ((prim.ArrayMask & (1 << 0)) != 0) Vector3.Transform(ref pos, ref mtxtable[prim.PosMatrixIndices[i]], out pos);
                            else Vector3.Transform(ref pos, ref mtxtable[0], out pos);
                            GL.Vertex3(pos);
                        }

                        GL.End();
                    }
                }

                //if (batch.MatrixType == 1 || batch.MatrixType == 2)
                //     GL.PopMatrix();
            }
        }


        public struct Shader
        {
            public int Program, VertexShader, FragmentShader;
        }

        private BMD m_Model;
        private int[] m_Textures;
        private bool m_HasShaders;
        private Shader[] m_Shaders;
    }
}
