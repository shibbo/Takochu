using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using Takochu.fmt;
using Takochu.util;

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
        private void UploadTexture(int id)
        {
            TextureWrapMode[] wrapmodes = { TextureWrapMode.ClampToEdge, TextureWrapMode.Repeat, TextureWrapMode.MirroredRepeat };
            TextureMinFilter[] minfilters = { TextureMinFilter.Nearest, TextureMinFilter.Linear,
                                                TextureMinFilter.NearestMipmapNearest, TextureMinFilter.LinearMipmapNearest,
                                                TextureMinFilter.NearestMipmapLinear, TextureMinFilter.LinearMipmapLinear };
            TextureMagFilter[] magfilters = { TextureMagFilter.Nearest, TextureMagFilter.Linear,
                                                TextureMagFilter.Nearest, TextureMagFilter.Linear,
                                                TextureMagFilter.Nearest, TextureMagFilter.Linear };

            BMD.Texture tex = m_Model.Textures[id];
            int texid = GL.GenTexture();
            m_Textures[id] = texid;

            GL.BindTexture(TextureTarget.Texture2D, texid);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, tex.MipmapCount - 1);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)minfilters[tex.MinFilter]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)magfilters[tex.MagFilter]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)wrapmodes[tex.WrapS]);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)wrapmodes[tex.WrapT]);

            PixelInternalFormat ifmt;
            PixelFormat fmt;
            switch (tex.Format)
            {
                case 0:
                case 1: ifmt = PixelInternalFormat.Intensity; fmt = PixelFormat.Luminance; break;

                case 2:
                case 3: ifmt = PixelInternalFormat.Luminance8Alpha8; fmt = PixelFormat.LuminanceAlpha; break;

                default: ifmt = PixelInternalFormat.Four; fmt = PixelFormat.Bgra; break;
            }

            int width = tex.Width, height = tex.Height;
            for (int mip = 0; mip < tex.MipmapCount; mip++)
            {
                GL.TexImage2D(TextureTarget.Texture2D, mip, ifmt, width, height, 0, fmt, PixelType.UnsignedByte, tex.Image[mip]);
                width /= 2; height /= 2;
            }
        }

        public string debugshaders;

        private void GenerateShaders(int matid)
        {
            CultureInfo forceusa = new CultureInfo("en-US");

            string[] texgensrc = { "normalize(gl_Vertex)", "vec4(gl_Normal,1.0)", "argh", "argh",
                                     "gl_MultiTexCoord0", "gl_MultiTexCoord1", "gl_MultiTexCoord2", "gl_MultiTexCoord3",
                                     "gl_MultiTexCoord4", "gl_MultiTexCoord5", "gl_MultiTexCoord6", "gl_MultiTexCoord7" };

            string[] outputregs = { "rprev", "r0", "r1", "r2" };

            string[] c_inputregs = { "truncc3(rprev.rgb)", "truncc3(rprev.aaa)", "truncc3(r0.rgb)", "truncc3(r0.aaa)",
                                        "truncc3(r1.rgb)", "truncc3(r1.aaa)", "truncc3(r2.rgb)", "truncc3(r2.aaa)",
                                       "texcolor.rgb", "texcolor.aaa", "rascolor.rgb", "rascolor.aaa",
                                       "vec3(1.0,1.0,1.0)", "vec3(0.5,0.5,0.5)", "konst.rgb", "vec3(0.0,0.0,0.0)" };
            string[] c_inputregsD = { "rprev.rgb", "rprev.aaa", "r0.rgb", "r0.aaa",
                                        "r1.rgb", "r1.aaa", "r2.rgb", "r2.aaa",
                                       "texcolor.rgb", "texcolor.aaa", "rascolor.rgb", "rascolor.aaa",
                                       "vec3(1.0,1.0,1.0)", "vec3(0.5,0.5,0.5)", "konst.rgb", "vec3(0.0,0.0,0.0)" };
            string[] c_konstsel = { "vec3(1.0,1.0,1.0)", "vec3(0.875,0.875,0.875)", "vec3(0.75,0.75,0.75)", "vec3(0.625,0.625,0.625)",
                                      "vec3(0.5,0.5,0.5)", "vec3(0.375,0.375,0.375)", "vec3(0.25,0.25,0.25)", "vec3(0.125,0.125,0.125)",
                                      "", "", "", "", "k0.rgb", "k1.rgb", "k2.rgb", "k3.rgb",
                                      "k0.rrr", "k1.rrr", "k2.rrr", "k3.rrr", "k0.ggg", "k1.ggg", "k2.ggg", "k3.ggg",
                                      "k0.bbb", "k1.bbb", "k2.bbb", "k3.bbb", "k0.aaa", "k1.aaa", "k2.aaa", "k3.aaa" };

            string[] a_inputregs = { "truncc1(rprev.a)", "truncc1(r0.a)", "truncc1(r1.a)", "truncc1(r2.a)",
                                       "texcolor.a", "rascolor.a", "konst.a", "0.0" };
            string[] a_inputregsD = { "rprev.a", "r0.a", "r1.a", "r2.a",
                                       "texcolor.a", "rascolor.a", "konst.a", "0.0" };
            string[] a_konstsel = { "1.0", "0.875", "0.75", "0.625", "0.5", "0.375", "0.25", "0.125",
                                      "", "", "", "", "", "", "", "",
                                      "k0.r", "k1.r", "k2.r", "k3.r", "k0.g", "k1.g", "k2.g", "k3.g",
                                      "k0.b", "k1.b", "k2.b", "k3.b", "k0.a", "k1.a", "k2.a", "k3.a" };

            string[] tevbias = { "0.0", "0.5", "-0.5" };
            string[] tevscale = { "1.0", "2.0", "4.0", "0.5" };

            string[] alphacompare = { "{0} != {0}", "{0} < {1}", "{0} == {1}", "{0} <= {1}", "{0} > {1}", "{0} != {1}", "{0} >= {1}", "{0} == {0}" };
            // string[] alphacombine = { "all(bvec2({0},{1}))", "any(bvec2({0},{1}))", "any(bvec2(all(bvec2({0},!{1})),all(bvec2(!{0},{1}))))", "any(bvec2(all(bvec2({0},{1})),all(bvec2(!{0},!{1}))))" };
            string[] alphacombine = { "({0}) && ({1})", "({0}) || ({1})", "(({0}) && (!({1}))) || ((!({0})) && ({1}))", "(({0}) && ({1})) || ((!({0})) && (!({1})))" };

            // yes, oldstyle shaders
            // I would use version 130 or above but there are certain
            // of their new designs I don't agree with. Namely, what's
            // up with removing texture coordinates. That's just plain
            // retarded.

            int success = 0;
            BMD.Material mat = m_Model.Materials[matid];

            StringBuilder vert = new StringBuilder();
            vert.AppendLine("#version 120");
            vert.AppendLine("");
            vert.AppendLine("void main()");
            vert.AppendLine("{");
            vert.AppendLine("    gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;");
            vert.AppendLine("    gl_FrontColor = gl_Color;");
            vert.AppendLine("    gl_FrontSecondaryColor = gl_SecondaryColor;");
            for (int i = 0; i < mat.NumTexgens; i++)
            {
                /*if (mat.TexGen[i].Src == 1) vert.AppendFormat("    gl_TexCoord[{0}].st = gl_Normal.xy;\n", i);
                else if (mat.TexGen[i].Src != 4 + i) throw new Exception("!texgen " + mat.TexGen[i].Src.ToString());
                else
                vert.AppendFormat("    gl_TexCoord[{0}] = gl_MultiTexCoord{0};\n", i);*/
                // TODO matrices
                vert.AppendFormat("    gl_TexCoord[{0}] = {1};\n", i, texgensrc[mat.TexGen[i].Src]);
            }
            vert.AppendLine("}");

            int vertid = GL.CreateShader(ShaderType.VertexShader);
            m_Shaders[matid].VertexShader = vertid;
            GL.ShaderSource(vertid, vert.ToString());
            GL.CompileShader(vertid);

            GL.GetShader(vertid, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string log = GL.GetShaderInfoLog(vertid);
                throw new Exception("!Failed to compile vertex shader: " + log);
                // TODO: better error reporting/logging?
            }

            StringBuilder frag = new StringBuilder();
            frag.AppendLine("#version 120");
            frag.AppendLine("");

            for (int i = 0; i < 8; i++)
            {
                if (mat.TexStages[i] == 0xFFFF) continue;
                frag.AppendLine("uniform sampler2D texture" + i.ToString() + ";");
            }

            frag.AppendLine("");
            frag.AppendLine("float truncc1(float c)");
            frag.AppendLine("{");
            frag.AppendLine("    return (c == 0.0) ? 0.0 : ((fract(c) == 0.0) ? 1.0 : fract(c));");
            frag.AppendLine("}");
            frag.AppendLine("");
            frag.AppendLine("vec3 truncc3(vec3 c)");
            frag.AppendLine("{");
            frag.AppendLine("    return vec3(truncc1(c.r), truncc1(c.g), truncc1(c.b));");
            frag.AppendLine("}");
            frag.AppendLine("");
            frag.AppendLine("void main()");
            frag.AppendLine("{");

            for (int i = 0; i < 4; i++)
            {
                int _i = (i == 0) ? 3 : i - 1; // ???
                frag.AppendFormat(forceusa, "    vec4 {0} = vec4({1}, {2}, {3}, {4});\n",
                    outputregs[i],
                    (float)mat.ColorS10[_i].R / 255f, (float)mat.ColorS10[_i].G / 255f,
                    (float)mat.ColorS10[_i].B / 255f, (float)mat.ColorS10[_i].A / 255f);
            }

            for (int i = 0; i < 4; i++)
            {
                frag.AppendFormat(forceusa, "    vec4 k{0} = vec4({1}, {2}, {3}, {4});\n",
                    i,
                    (float)mat.ConstColors[i].R / 255f, (float)mat.ConstColors[i].G / 255f,
                    (float)mat.ConstColors[i].B / 255f, (float)mat.ConstColors[i].A / 255f);
            }

            frag.AppendLine("    vec4 texcolor, rascolor, konst;");

            for (int i = 0; i < mat.NumTevStages; i++)
            {
                frag.AppendLine("\n    // TEV stage " + i.ToString());

                // TEV inputs
                // for registers prev/0/1/2: use fract() to emulate truncation
                // if they're selected into a, b or c
                string rout, a, b, c, d, operation = "";

                frag.AppendLine("    konst.rgb = " + c_konstsel[mat.ConstColorSel[i]] + ";");
                frag.AppendLine("    konst.a = " + a_konstsel[mat.ConstAlphaSel[i]] + ";");
                if (mat.TevOrder[i].TexMap != 0xFF && mat.TevOrder[i].TexcoordId != 0xFF)
                    frag.AppendFormat("    texcolor = texture2D(texture{0}, gl_TexCoord[{1}].st);\n",
                        mat.TevOrder[i].TexMap, mat.TevOrder[i].TexcoordId);
                frag.AppendLine("    rascolor = gl_Color;");
                // TODO: take mat.TevOrder[i].ChanId into account
                // TODO: tex/ras swizzle? (important or not?)
                //mat.TevSwapMode[0].

                if (mat.TevOrder[i].ChanID != 4)
                    throw new Exception("!UNSUPPORTED CHANID " + mat.TevOrder[i].ChanID.ToString());

                rout = outputregs[mat.TevStage[i].ColorRegID] + ".rgb";
                a = c_inputregs[mat.TevStage[i].ColorIn[0]];
                b = c_inputregs[mat.TevStage[i].ColorIn[1]];
                c = c_inputregs[mat.TevStage[i].ColorIn[2]];
                d = c_inputregsD[mat.TevStage[i].ColorIn[3]];

                switch (mat.TevStage[i].ColorOp)
                {
                    case 0:
                        operation = "    {0} = ({4} + mix({1},{2},{3}) + vec3({5},{5},{5})) * vec3({6},{6},{6});";
                        if (mat.TevStage[i].ColorClamp != 0) operation += "\n    {0} = clamp({0}, vec3(0.0,0.0,0.0), vec3(1.0,1.0,1.0));";
                        break;

                    case 1:
                        operation = "    {0} = ({4} - mix({1},{2},{3}) + vec3({5},{5},{5})) * vec3({6},{6},{6});";
                        if (mat.TevStage[i].ColorClamp != 0) operation += "\n    {0} = clamp({0}, vec3(0.0,0.0,0.0), vec3(1.0,1.0,1.0));";
                        break;

                    case 8:
                        operation = "    {0} = {4} + ((({1}).r > ({2}).r) ? {3} : vec(0.0,0.0,0.0));";
                        break;

                    default:
                        operation = "    {0} = vec3(1.0,0.0,1.0);";
                        throw new Exception("!colorop " + mat.TevStage[i].ColorOp.ToString());
                }

                operation = string.Format(operation,
                    rout, a, b, c, d, tevbias[mat.TevStage[i].ColorBias],
                    tevscale[mat.TevStage[i].ColorScale]);
                frag.AppendLine(operation);

                rout = outputregs[mat.TevStage[i].AlphaRegID] + ".a";
                a = a_inputregs[mat.TevStage[i].AlphaIn[0]];
                b = a_inputregs[mat.TevStage[i].AlphaIn[1]];
                c = a_inputregs[mat.TevStage[i].AlphaIn[2]];
                d = a_inputregsD[mat.TevStage[i].AlphaIn[3]];

                switch (mat.TevStage[i].AlphaOp)
                {
                    case 0:
                        operation = "    {0} = ({4} + mix({1},{2},{3}) + {5}) * {6};";
                        if (mat.TevStage[i].AlphaClamp != 0) operation += "\n   {0} = clamp({0}, 0.0, 1.0);";
                        break;

                    case 1:
                        operation = "    {0} = ({4} - mix({1},{2},{3}) + {5}) * {6};";
                        if (mat.TevStage[i].AlphaClamp != 0) operation += "\n   {0} = clamp({0}, 0.0, 1.0);";
                        break;

                    default:
                        operation = "    {0} = 1.0;";
                        throw new Exception("!alphaop " + mat.TevStage[i].AlphaOp.ToString());
                }

                operation = string.Format(operation,
                    rout, a, b, c, d, tevbias[mat.TevStage[i].AlphaBias],
                    tevscale[mat.TevStage[i].AlphaScale]);
                frag.AppendLine(operation);
            }

            frag.AppendLine("");
            frag.AppendLine("   gl_FragColor.rgb = truncc3(rprev.rgb);");
            frag.AppendLine("   gl_FragColor.a = truncc1(rprev.a);");
            frag.AppendLine("");

            frag.AppendLine("    // Alpha test");
            if (mat.AlphaComp.MergeFunc == 1 && (mat.AlphaComp.Func0 == 7 || mat.AlphaComp.Func1 == 7))
            {
                // always pass -- do nothing :)
            }
            else if (mat.AlphaComp.MergeFunc == 0 && (mat.AlphaComp.Func0 == 0 || mat.AlphaComp.Func1 == 0))
            {
                // never pass
                // (we did all those color/alpha calculations for uh, nothing ;_; )
                frag.AppendLine("    discard;");
            }
            else
            {
                string compare0 = string.Format(forceusa, alphacompare[mat.AlphaComp.Func0], "gl_FragColor.a", (float)mat.AlphaComp.Ref0 / 255f);
                string compare1 = string.Format(forceusa, alphacompare[mat.AlphaComp.Func1], "gl_FragColor.a", (float)mat.AlphaComp.Ref1 / 255f);
                string fullcompare = "";

                if (mat.AlphaComp.MergeFunc == 1)
                {
                    if (mat.AlphaComp.Func0 == 0) fullcompare = compare1;
                    else if (mat.AlphaComp.Func1 == 0) fullcompare = compare0;
                }
                else if (mat.AlphaComp.MergeFunc == 0)
                {
                    if (mat.AlphaComp.Func0 == 7) fullcompare = compare1;
                    else if (mat.AlphaComp.Func1 == 7) fullcompare = compare0;
                }

                if (fullcompare == "") fullcompare = string.Format(alphacombine[mat.AlphaComp.MergeFunc], compare0, compare1);

                frag.AppendLine("    if (!(" + fullcompare + ")) discard;");
            }

            frag.AppendLine("}");

            int fragid = GL.CreateShader(ShaderType.FragmentShader);
            m_Shaders[matid].FragmentShader = fragid;
            GL.ShaderSource(fragid, frag.ToString());
            GL.CompileShader(fragid);

            GL.GetShader(fragid, ShaderParameter.CompileStatus, out success);
            if (success == 0)
            {
                string log = GL.GetShaderInfoLog(fragid);
                throw new Exception("!Failed to compile fragment shader: " + log);
                // TODO: better error reporting/logging?
            }

            int sid = GL.CreateProgram();
            m_Shaders[matid].Program = sid;

            GL.AttachShader(sid, vertid);
            GL.AttachShader(sid, fragid);

            GL.LinkProgram(sid);
            GL.GetProgram(sid, ProgramParameter.LinkStatus, out success);
            if (success == 0)
            {
                string log = GL.GetProgramInfoLog(sid);
                throw new Exception("!Failed to link shader program: " + log);
                // TODO: better error reporting/logging?
            }

            //debugshaders += "-----------------------------------------------------------\n" + frag.ToString();
        }


        public BmdRenderer(BMD model)
        {
            m_Model = model;

            string[] extensions = GL.GetString(StringName.Extensions).Split(' ');
            m_HasShaders = extensions.Contains("GL_ARB_shading_language_100") &&
                extensions.Contains("GL_ARB_shader_objects") &&
                extensions.Contains("GL_ARB_vertex_shader") &&
                extensions.Contains("GL_ARB_fragment_shader");
            // TODO: setting for turning shaders on/off

            m_Textures = new int[model.Textures.Length];
            for (int i = 0; i < model.Textures.Length; i++)
                UploadTexture(i);

            if (m_HasShaders)
            {
                m_Shaders = new Shader[model.Materials.Length];
                for (int i = 0; i < model.Materials.Length; i++)
                {
                    try { GenerateShaders(i); }
                    catch (Exception ex)
                    {
                        // really ugly hack
                        if (ex.Message[0] == '!')
                        {
                            StringBuilder src = new StringBuilder(10000); int lolz;
                            string str = src.ToString();
                            GL.GetShaderSource(m_Shaders[i].FragmentShader, 10000, out lolz, out str);
                            System.Windows.Forms.MessageBox.Show(ex.Message + "\n" + src.ToString());
                            throw ex;
                        }

                        m_Shaders[i].Program = 0;
                    }
                }
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
                    CullFaceMode[] cullmodes = { CullFaceMode.Front, CullFaceMode.Back, CullFaceMode.Front };
                    DepthFunction[] depthfuncs = { DepthFunction.Never, DepthFunction.Less, DepthFunction.Equal, DepthFunction.Lequal,
                                                     DepthFunction.Greater, DepthFunction.Notequal, DepthFunction.Gequal, DepthFunction.Always };

                    BMD.Material mat = m_Model.Materials[node.MaterialID];

                    if ((mat.DrawFlag == 4) ^ (info.Mode == RenderMode.Translucent))
                        continue;

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

                    SuperBMDLib.Materials.BlendMode m = new SuperBMDLib.Materials.BlendMode();
                    m.Type = RenderUtil.GetBlendModeType(mat.BlendMode.BlendMode);
                    m.SourceFact = RenderUtil.GetBlendModeCtrl(mat.BlendMode.SrcFactor);
                    m.DestinationFact = RenderUtil.GetBlendModeCtrl(mat.BlendMode.DstFactor);

                    SuperBMDLib.Materials.ZMode z_mode = new SuperBMDLib.Materials.ZMode();
                    z_mode.Enable = mat.ZMode.EnableZTest;
                    z_mode.Function = RenderUtil.GetCompareType(mat.ZMode.Func);
                    z_mode.UpdateEnable = mat.ZMode.EnableZWrite;
                    RenderUtil.SetBlendState(m);
                    RenderUtil.SetCullState(RenderUtil.GetCullMode(mat.CullMode));
                    RenderUtil.SetDepthState(z_mode, false);
                    RenderUtil.SetDitherEnabled(false);

                    /*switch (mat.BlendMode.BlendMode)
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

                            GL.BlendFunc((BlendingFactor)blendsrc[mat.BlendMode.SrcFactor], (BlendingFactor)blenddst[mat.BlendMode.DstFactor]);
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

                    GL.DepthMask(mat.ZMode.EnableZWrite);*/
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
                        //BeginMode[] primtypes = { BeginMode.Quads, BeginMode.Points, BeginMode.Triangles, BeginMode.TriangleStrip,
                        //                            BeginMode.TriangleFan, BeginMode.Lines, BeginMode.LineStrip, BeginMode.Points };

                        PrimitiveType[] primtypes = { PrimitiveType.Quads, PrimitiveType.Points, PrimitiveType.Triangles, PrimitiveType.TriangleStrip,
                                                        PrimitiveType.TriangleFan, PrimitiveType.Lines, PrimitiveType.LineStrip, PrimitiveType.Points };


                        //GL.Begin((PrimitiveType)primtypes[(prim.PrimitiveType - 0x80) / 8]);
                        GL.Begin(primtypes[(prim.PrimitiveType - 0x80) / 8]);

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
                            if ((prim.ArrayMask & (1 << 0)) != 0) 
                                Vector3.TransformPosition(pos, mtxtable[prim.PosMatrixIndices[i]]);
                            else 
                                Vector3.TransformPosition(pos, mtxtable[0]);

                            GL.Vertex3(pos);
                        }

                        GL.End();
                    }
                }

                //if (batch.MatrixType == 1 || batch.MatrixType == 2)
                //     GL.PopMatrix();
            }
        }


        private struct Shader
        {
            public int Program, VertexShader, FragmentShader;
        }

        private BMD m_Model;
        private int[] m_Textures;
        private bool m_HasShaders;
        private Shader[] m_Shaders;
    }
}
