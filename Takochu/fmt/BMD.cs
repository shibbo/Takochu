using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;
using Takochu.io;
using Takochu.util;

// BMD/BDL file parser
// heavily inspired from bmdview2's code, big thanks to its author

namespace Takochu.fmt
{
    public class BMD
    {
        public BMD(FileBase file)
        {
            m_File = file;
            m_File.SetBigEndian(true);

            m_File.Seek(0xC);
            uint numsections = m_File.ReadUInt32();
            m_File.Skip(0x10);
            for (uint i = 0; i < numsections; i++)
            {
                uint sectiontag = m_File.ReadUInt32();
                switch (sectiontag)
                {
                    case 0x494E4631: ReadINF1(); break;
                    case 0x56545831: ReadVTX1(); break;
                    case 0x45565031: ReadEVP1(); break;
                    case 0x44525731: ReadDRW1(); break;
                    case 0x4A4E5431: ReadJNT1(); break;
                    case 0x53485031: ReadSHP1(); break;
                    case 0x4D415433: ReadMAT3(); break;
                    case 0x4D444C33: ReadMDL3(); break;
                    case 0x54455831: ReadTEX1(); break;

                    default: throw new NotImplementedException("Unsupported BMD section " + sectiontag.ToString("X8"));
                }
            }

            BBoxMin = new Vector3(0, 0, 0);
            BBoxMax = new Vector3(0, 0, 0);
            foreach (Vector3 vec in PositionArray)
            {
                if (vec.X < BBoxMin.X) BBoxMin.X = vec.X;
                if (vec.Y < BBoxMin.Y) BBoxMin.Y = vec.Y;
                if (vec.Z < BBoxMin.Z) BBoxMin.Z = vec.Z;
                if (vec.X > BBoxMax.X) BBoxMax.X = vec.X;
                if (vec.Y > BBoxMax.Y) BBoxMax.Y = vec.Y;
                if (vec.Z > BBoxMax.Z) BBoxMax.Z = vec.Z;
            }
        }

        public void Close()
        {
            m_File.Close();
        }


        // wee
        private delegate float ReadArrayValueFunc(byte fixedpoint);
        private delegate Vector4 ReadColorValueFunc();

        private float ReadArrayValue_s16(byte fixedpoint)
        {
            short val = m_File.ReadInt16();
            return (float)(val / (float)(1 << fixedpoint));
        }

        private float ReadArrayValue_f32(byte fixedpoint)
        {
            return m_File.ReadSingle();
        }

        private Vector4 ReadColorValue_RGBA8()
        {
            byte r = m_File.ReadByte();
            byte g = m_File.ReadByte();
            byte b = m_File.ReadByte();
            byte a = m_File.ReadByte();
            return new Vector4(r / 255f, g / 255f, b / 255f, a / 255f);
        }


        // support functions for reading sections
        private void ReadINF1()
        {
            long sectionstart = m_File.Position() - 4;
            uint sectionsize = m_File.ReadUInt32();

            SceneGraph = new List<SceneGraphNode>();

            Stack<ushort> matstack = new Stack<ushort>();
            Stack<int> nodestack = new Stack<int>();
            matstack.Push(0xFFFF);
            nodestack.Push(-1);

            m_File.Skip(8);
            NumVertices = m_File.ReadUInt32();

            uint datastart = m_File.ReadUInt32();
            m_File.Skip((int)(datastart - 0x18));

            ushort curtype = 0;
            while ((curtype = m_File.ReadUInt16()) != 0)
            {
                ushort arg = m_File.ReadUInt16();

                switch (curtype)
                {
                    case 0x01:
                        matstack.Push(matstack.Peek());
                        nodestack.Push(SceneGraph.Count - 1);
                        break;

                    case 0x02:
                        matstack.Pop();
                        nodestack.Pop();
                        break;


                    case 0x11:
                        matstack.Pop();
                        matstack.Push(arg);
                        break;

                    case 0x10:
                    case 0x12:
                        {
                            int parentnode = nodestack.Peek();
                            SceneGraphNode node = new SceneGraphNode();
                            node.MaterialID = matstack.Peek();
                            node.NodeID = arg;
                            node.NodeType = (curtype == 0x12) ? 0 : 1;
                            node.ParentIndex = parentnode;
                            SceneGraph.Add(node);
                        }
                        break;
                }
            }

            m_File.Seek((int)(sectionstart + sectionsize));
        }

        private void ReadVTX1()
        {
            long sectionstart = m_File.Position() - 4;
            uint sectionsize = m_File.ReadUInt32();

            ArrayMask = 0;
            ColorArray = new Vector4[2][];
            TexcoordArray = new Vector2[8][];

            List<uint> arrayoffsets = new List<uint>();

            uint arraydefoffset = m_File.ReadUInt32();
            for (int i = 0; i < 13; i++)
            {
                m_File.Seek((int)(sectionstart + 0xC + (i * 0x4)));
                uint dataoffset = m_File.ReadUInt32();
                if (dataoffset == 0) continue;

                arrayoffsets.Add(dataoffset);
            }

            for (int i = 0; i < arrayoffsets.Count; i++)
            {
                m_File.Seek((int)(sectionstart + arraydefoffset + (i * 0x10)));
                uint arraytype = m_File.ReadUInt32();
                uint compsize = m_File.ReadUInt32();
                uint datatype = m_File.ReadUInt32();
                byte fp = m_File.ReadByte();

                // apparently, arrays may contain more elements than specified in the INF1 section
                // so we have to rely on bmdview2's way to know the array's exact size
                int arraysize = 0;
                if (i == arrayoffsets.Count - 1)
                    arraysize = (int)(sectionsize - arrayoffsets[i]);
                else
                    arraysize = (int)(arrayoffsets[i + 1] - arrayoffsets[i]);

                ReadArrayValueFunc readval = null;
                ReadColorValueFunc readcolor = null;

                if (arraytype == 11 || arraytype == 12)
                {
                    if ((datatype < 3) ^ (compsize == 0))
                        throw new Exception("Bmd: component count mismatch in color array; DataType=" + datatype.ToString() + ", CompSize=" + compsize.ToString());

                    switch (datatype)
                    {
                        case 5: readcolor = ReadColorValue_RGBA8; arraysize /= 4; break;
                        default: throw new NotImplementedException("Bmd: unsupported color DataType " + datatype.ToString());
                    }
                }
                else
                {
                    switch (datatype)
                    {
                        case 3: readval = ReadArrayValue_s16; arraysize /= 2; break;
                        case 4: readval = ReadArrayValue_f32; arraysize /= 4; break;
                        default: throw new NotImplementedException("Bmd: unsupported DataType " + datatype.ToString());
                    }
                }

                m_File.Seek((int)(sectionstart + arrayoffsets[i]));

                ArrayMask |= (uint)(1 << (int)arraytype);
                switch (arraytype)
                {
                    case 9:
                        {
                            switch (compsize)
                            {
                                case 0:
                                    PositionArray = new Vector3[arraysize / 2];
                                    for (int j = 0; j < arraysize / 2; j++) PositionArray[j] = new Vector3(readval(fp), readval(fp), 0f);
                                    break;
                                case 1:
                                    PositionArray = new Vector3[arraysize / 3];
                                    for (int j = 0; j < arraysize / 3; j++) PositionArray[j] = new Vector3(readval(fp), readval(fp), readval(fp));
                                    break;
                                default: throw new NotImplementedException("Bmd: unsupported position CompSize " + compsize.ToString());
                            }
                        }
                        break;

                    case 10:
                        {
                            switch (compsize)
                            {
                                case 0:
                                    NormalArray = new Vector3[arraysize / 3];
                                    for (int j = 0; j < arraysize / 3; j++) NormalArray[j] = new Vector3(readval(fp), readval(fp), readval(fp));
                                    break;
                                default: throw new NotImplementedException("Bmd: unsupported normal CompSize " + compsize.ToString());
                            }
                        }
                        break;

                    case 11:
                    case 12:
                        {
                            uint cid = arraytype - 11;
                            ColorArray[cid] = new Vector4[arraysize];
                            for (int j = 0; j < arraysize; j++) ColorArray[cid][j] = readcolor();
                        }
                        break;

                    case 13:
                    case 14:
                    case 15:
                    case 16:
                    case 17:
                    case 18:
                    case 19:
                    case 20:
                        {
                            uint tid = arraytype - 13;
                            switch (compsize)
                            {
                                case 0:
                                    TexcoordArray[tid] = new Vector2[arraysize];
                                    for (int j = 0; j < arraysize; j++) TexcoordArray[tid][j] = new Vector2(readval(fp), 0f);
                                    break;
                                case 1:
                                    TexcoordArray[tid] = new Vector2[arraysize / 2];
                                    for (int j = 0; j < arraysize / 2; j++) TexcoordArray[tid][j] = new Vector2(readval(fp), readval(fp));
                                    break;
                                default: throw new NotImplementedException("Bmd: unsupported texcoord CompSize " + compsize.ToString());
                            }
                        }
                        break;

                    default: throw new NotImplementedException("Bmd: unsupported ArrayType " + arraytype.ToString());
                }
            }

            m_File.Seek((int)(sectionstart + sectionsize));
        }

        private void ReadEVP1()
        {
            long sectionstart = m_File.Position() - 4;
            uint sectionsize = m_File.ReadUInt32();

            ushort count = m_File.ReadUInt16();
            m_File.Skip(2);

            MultiMatrices = new MultiMatrix[count];

            uint offset0 = m_File.ReadUInt32();
            uint offset1 = m_File.ReadUInt32();
            uint offset2 = m_File.ReadUInt32();
            uint offset3 = m_File.ReadUInt32();

            uint position1 = 0, position2 = 0;

            for (int i = 0; i < count; i++)
            {
                m_File.Seek((int)(sectionstart + offset0 + i));
                byte subcount = m_File.ReadByte();

                MultiMatrix mm = new MultiMatrix();
                MultiMatrices[i] = mm;
                mm.NumMatrices = subcount;
                mm.MatrixIndices = new ushort[subcount];
                mm.Matrices = new Matrix4[subcount];
                mm.MatrixWeights = new float[subcount];

                for (int j = 0; j < subcount; j++)
                {
                    m_File.Seek((int)(sectionstart + offset1 + position1));
                    mm.MatrixIndices[j] = m_File.ReadUInt16();
                    position1 += 2;

                    m_File.Seek((int)(sectionstart + offset2 + position2));
                    mm.MatrixWeights[j] = m_File.ReadSingle();
                    position2 += 4;

                    m_File.Seek((int)(sectionstart + offset3 + (mm.MatrixIndices[j] * 48)));
                    mm.Matrices[j].M11 = m_File.ReadSingle(); mm.Matrices[j].M12 = m_File.ReadSingle();
                    mm.Matrices[j].M13 = m_File.ReadSingle(); mm.Matrices[j].M14 = m_File.ReadSingle();
                    mm.Matrices[j].M21 = m_File.ReadSingle(); mm.Matrices[j].M22 = m_File.ReadSingle();
                    mm.Matrices[j].M23 = m_File.ReadSingle(); mm.Matrices[j].M24 = m_File.ReadSingle();
                    mm.Matrices[j].M31 = m_File.ReadSingle(); mm.Matrices[j].M32 = m_File.ReadSingle();
                    mm.Matrices[j].M33 = m_File.ReadSingle(); mm.Matrices[j].M34 = m_File.ReadSingle();
                    mm.Matrices[j].M41 = mm.Matrices[j].M42 = mm.Matrices[j].M43 = 0f; mm.Matrices[j].M44 = 1f;
                }
            }

            m_File.Seek((int)(sectionstart + sectionsize));
        }

        private void ReadDRW1()
        {
            long sectionstart = m_File.Position() - 4;
            uint sectionsize = m_File.ReadUInt32();

            ushort count = m_File.ReadUInt16();
            m_File.Skip(2);

            MatrixTypes = new MatrixType[count];

            uint offset0 = m_File.ReadUInt32();
            uint offset1 = m_File.ReadUInt32();

            for (int i = 0; i < count; i++)
            {
                MatrixType mt = new MatrixType();
                MatrixTypes[i] = mt;

                m_File.Seek((int)(sectionstart + offset0 + i));
                mt.IsWeighted = (m_File.ReadByte() != 0);

                m_File.Seek((int)(sectionstart + offset1 + (i * 2)));
                mt.Index = m_File.ReadUInt16();
            }

            m_File.Seek((int)(sectionstart + sectionsize));
        }

        private void ReadJNT1()
        {
            long sectionstart = m_File.Position() - 4;
            uint sectionsize = m_File.ReadUInt32();

            ushort numjoints = m_File.ReadUInt16();
            m_File.Skip(2);

            Joints = new Joint[numjoints];

            uint jointsoffset = m_File.ReadUInt32();
            uint unkoffset = m_File.ReadUInt32();
            uint stringsoffset = m_File.ReadUInt32();

            for (int i = 0; i < numjoints; i++)
            {
                m_File.Seek((int)(sectionstart + jointsoffset + (i * 0x40)));

                Joint jnt = new Joint();
                Joints[i] = jnt;

                jnt.Unk1 = m_File.ReadUInt16();
                jnt.Unk2 = m_File.ReadByte();
                m_File.Skip(1);

                jnt.Scale.X = m_File.ReadSingle();
                jnt.Scale.Y = m_File.ReadSingle();
                jnt.Scale.Z = m_File.ReadSingle();
                jnt.Rotation.X = (float)((m_File.ReadInt16() * Math.PI) / 32768f);
                jnt.Rotation.Y = (float)((m_File.ReadInt16() * Math.PI) / 32768f);
                jnt.Rotation.Z = (float)((m_File.ReadInt16() * Math.PI) / 32768f);
                m_File.Skip(2);
                jnt.Translation.X = m_File.ReadSingle();
                jnt.Translation.Y = m_File.ReadSingle();
                jnt.Translation.Z = m_File.ReadSingle();

                jnt.Matrix = RenderUtil.SRTToMatrix(jnt.Scale, jnt.Rotation, jnt.Translation);

                foreach (SceneGraphNode node in SceneGraph)
                {
                    if (node.NodeType != 1) continue;
                    if (node.NodeID != i) continue;

                    SceneGraphNode parentnode = node;
                    do
                    {
                        if (parentnode.ParentIndex == -1)
                        {
                            parentnode = null;
                            break;
                        }

                        parentnode = SceneGraph[parentnode.ParentIndex];

                    } while (parentnode.NodeType != 1);

                    if (parentnode != null)
                        Matrix4.Mult(ref jnt.Matrix, ref Joints[parentnode.NodeID].FinalMatrix, out jnt.FinalMatrix);
                    else
                        jnt.FinalMatrix = jnt.Matrix;

                    break;
                }
            }

            m_File.Seek((int)(sectionstart + sectionsize));
        }

        private void ReadSHP1()
        {
            long sectionstart = m_File.Position() - 4;
            uint sectionsize = m_File.ReadUInt32();

            ushort numbatches = m_File.ReadUInt16();
            m_File.Skip(2);
            uint batchesoffset = m_File.ReadUInt32();
            m_File.Skip(8);
            uint batchattribsoffset = m_File.ReadUInt32();
            uint mtxtableoffset = m_File.ReadUInt32();
            uint dataoffset = m_File.ReadUInt32();
            uint mtxdataoffset = m_File.ReadUInt32();
            uint pktlocationsoffset = m_File.ReadUInt32();

            Batches = new Batch[numbatches];

            for (int i = 0; i < numbatches; i++)
            {
                Batch batch = new Batch();
                Batches[i] = batch;

                m_File.Seek((int)(sectionstart + batchesoffset + (i * 0x28)));

                batch.MatrixType = m_File.ReadByte();
                m_File.Skip(1);
                ushort numpackets = m_File.ReadUInt16();
                ushort attribsoffset = m_File.ReadUInt16();
                ushort firstmtxindex = m_File.ReadUInt16();
                ushort firstpktindex = m_File.ReadUInt16();

                m_File.Skip(2);
                batch.Unk = m_File.ReadSingle();

                List<ushort> attribs = new List<ushort>();
                m_File.Seek((int)(sectionstart + batchattribsoffset + attribsoffset));

                uint arraymask = 0;
                for (; ; )
                {
                    uint arraytype = m_File.ReadUInt32();
                    uint datatype = m_File.ReadUInt32();

                    if (arraytype == 0xFF) break;

                    ushort attrib = (ushort)((arraytype & 0xFF) | ((datatype & 0xFF) << 8));
                    attribs.Add(attrib);

                    arraymask |= (uint)(1 << (int)arraytype);
                }

                batch.Packets = new Batch.Packet[numpackets];
                for (int j = 0; j < numpackets; j++)
                {
                    Batch.Packet packet = new Batch.Packet();
                    packet.Primitives = new List<Batch.Packet.Primitive>();
                    batch.Packets[j] = packet;

                    m_File.Seek((int)(sectionstart + mtxdataoffset + ((firstmtxindex + j) * 0x8)));

                    m_File.Skip(2);
                    ushort mtxtablesize = m_File.ReadUInt16();
                    uint mtxtablefirstindex = m_File.ReadUInt32();

                    packet.MatrixTable = new ushort[mtxtablesize];
                    m_File.Seek((int)(sectionstart + mtxtableoffset + (mtxtablefirstindex * 0x2)));
                    for (int k = 0; k < mtxtablesize; k++)
                        packet.MatrixTable[k] = m_File.ReadUInt16();

                    m_File.Seek((int)(sectionstart + pktlocationsoffset + ((firstpktindex + j) * 0x8)));

                    uint pktsize = m_File.ReadUInt32();
                    uint pktoffset = m_File.ReadUInt32();

                    m_File.Seek((int)(sectionstart + dataoffset + pktoffset));
                    long packetend = m_File.Position() + pktsize;

                    for (; ; )
                    {
                        if (m_File.Position() >= packetend) break;

                        byte primtype = m_File.ReadByte();
                        if (primtype == 0) break;
                        ushort numvertices = m_File.ReadUInt16();

                        Batch.Packet.Primitive prim = new Batch.Packet.Primitive();
                        packet.Primitives.Add(prim);

                        prim.ColorIndices = new int[2][];
                        prim.TexcoordIndices = new int[8][];
                        prim.ArrayMask = arraymask;

                        prim.NumIndices = numvertices;
                        if ((arraymask & (1 << 0)) != 0) prim.PosMatrixIndices = new int[numvertices];
                        if ((arraymask & (1 << 9)) != 0) prim.PositionIndices = new int[numvertices];
                        if ((arraymask & (1 << 10)) != 0) prim.NormalIndices = new int[numvertices];
                        if ((arraymask & (1 << 11)) != 0) prim.ColorIndices[0] = new int[numvertices];
                        if ((arraymask & (1 << 12)) != 0) prim.ColorIndices[1] = new int[numvertices];
                        if ((arraymask & (1 << 13)) != 0) prim.TexcoordIndices[0] = new int[numvertices];
                        if ((arraymask & (1 << 14)) != 0) prim.TexcoordIndices[1] = new int[numvertices];
                        if ((arraymask & (1 << 15)) != 0) prim.TexcoordIndices[2] = new int[numvertices];
                        if ((arraymask & (1 << 16)) != 0) prim.TexcoordIndices[3] = new int[numvertices];
                        if ((arraymask & (1 << 17)) != 0) prim.TexcoordIndices[4] = new int[numvertices];
                        if ((arraymask & (1 << 18)) != 0) prim.TexcoordIndices[5] = new int[numvertices];
                        if ((arraymask & (1 << 19)) != 0) prim.TexcoordIndices[6] = new int[numvertices];
                        if ((arraymask & (1 << 20)) != 0) prim.TexcoordIndices[7] = new int[numvertices];

                        prim.PrimitiveType = primtype;

                        for (int k = 0; k < numvertices; k++)
                        {
                            foreach (ushort attrib in attribs)
                            {
                                int val = 0;

                                switch (attrib & 0xFF00)
                                {
                                    case 0x0000:
                                    case 0x0100:
                                        val = (int)m_File.ReadByte();
                                        break;

                                    case 0x0200:
                                    case 0x0300:
                                        val = (int)m_File.ReadUInt16();
                                        break;

                                    default: throw new NotImplementedException("Bmd: unsupported index attrib " + attrib.ToString("X4"));
                                }

                                switch (attrib & 0xFF)
                                {
                                    case 0: prim.PosMatrixIndices[k] = val / 3; break;
                                    case 9: prim.PositionIndices[k] = val; break;
                                    case 10: prim.NormalIndices[k] = val; break;
                                    case 11:
                                    case 12: prim.ColorIndices[(attrib & 0xFF) - 11][k] = val; break;
                                    case 13:
                                    case 14:
                                    case 15:
                                    case 16:
                                    case 17:
                                    case 18:
                                    case 19:
                                    case 20: prim.TexcoordIndices[(attrib & 0xFF) - 13][k] = val; break;

                                    default: throw new NotImplementedException("Bmd: unsupported index attrib " + attrib.ToString("X4"));
                                }
                            }
                        }
                    }
                }
            }

            m_File.Seek((int)(sectionstart + sectionsize));
        }

        private void ReadMAT3()
        {
            long sectionstart = m_File.Position() - 4;
            uint sectionsize = m_File.ReadUInt32();

            ushort nummaterials = m_File.ReadUInt16();
            m_File.Skip(2);

            Materials = new Material[nummaterials];

            // uh yeah let's create 30 separate variables
            uint[] offsets = new uint[30];
            for (int i = 0; i < 30; i++) offsets[i] = m_File.ReadUInt32();

            for (int i = 0; i < nummaterials; i++)
            {
                Material mat = new Material();
                Materials[i] = mat;

                // idk if that's right
                m_File.Seek((int)(sectionstart + offsets[2] + 4 + (i * 4) + 2));
                ushort nameoffset = m_File.ReadUInt16();
                m_File.Seek((int)(sectionstart + offsets[2] + nameoffset));
                mat.Name = m_File.ReadString();

                m_File.Seek((int)(sectionstart + offsets[1] + (i * 2)));
                ushort matindex = m_File.ReadUInt16();

                m_File.Seek((int)(sectionstart + offsets[0] + (matindex * 0x14C)));

                // giant chunk of crap here.
                // why everything has to be an index into some silly array, this
                // is beyond me
                mat.DrawFlag = m_File.ReadByte();
                byte cull_id = m_File.ReadByte();
                byte numchans_id = m_File.ReadByte();
                byte numtexgens_id = m_File.ReadByte();
                byte numtev_id = m_File.ReadByte();
                m_File.Skip(1); // index into matData6 -- 27
                byte zmode_id = m_File.ReadByte();
                m_File.Skip(1); // index into matData7 -- 28
                m_File.Skip(4); // color1 -- 5
                m_File.Skip(8); // chanControls -- 7?
                m_File.Skip(4); // color2 -- 8
                m_File.Skip(16); // lights -- 9
                ushort[] texgen_id = new ushort[8];
                for (int j = 0; j < 8; j++) texgen_id[j] = m_File.ReadUInt16();
                m_File.Skip(16); // texGenInfo2 -- 12
                m_File.Skip(20); // texMatrices -- 13?
                m_File.Skip(40); // dttMatrices -- 14?
                ushort[] texstage_id = new ushort[8];
                for (int j = 0; j < 8; j++) texstage_id[j] = m_File.ReadUInt16();
                ushort[] constcolor_id = new ushort[4];
                for (int j = 0; j < 4; j++) constcolor_id[j] = m_File.ReadUInt16();
                mat.ConstColorSel = new byte[16];
                for (int j = 0; j < 16; j++) mat.ConstColorSel[j] = m_File.ReadByte();
                mat.ConstAlphaSel = new byte[16];
                for (int j = 0; j < 16; j++) mat.ConstAlphaSel[j] = m_File.ReadByte();
                ushort[] tevorder_id = new ushort[16];
                for (int j = 0; j < 16; j++) tevorder_id[j] = m_File.ReadUInt16();
                ushort[] colors10_id = new ushort[4];
                for (int j = 0; j < 4; j++) colors10_id[j] = m_File.ReadUInt16();
                ushort[] tevstage_id = new ushort[16];
                for (int j = 0; j < 16; j++) tevstage_id[j] = m_File.ReadUInt16();
                ushort[] tevswap_id = new ushort[16];
                for (int j = 0; j < 16; j++) tevswap_id[j] = m_File.ReadUInt16();
                ushort[] tevswaptbl_id = new ushort[4];
                for (int j = 0; j < 4; j++) tevswaptbl_id[j] = m_File.ReadUInt16();
                m_File.Skip(24);
                ushort fog_id = m_File.ReadUInt16();
                ushort alphacomp_id = m_File.ReadUInt16();
                ushort blendmode_id = m_File.ReadUInt16();

                m_File.Seek((int)(sectionstart + offsets[4] + (cull_id * 4)));
                mat.CullMode = (byte)m_File.ReadUInt32();

                m_File.Seek((int)(sectionstart + offsets[6] + numchans_id));
                mat.NumChans = m_File.ReadByte();

                m_File.Seek((int)(sectionstart + offsets[10] + numtexgens_id));
                mat.NumTexgens = m_File.ReadByte();

                m_File.Seek((int)(sectionstart + offsets[19] + numtev_id));
                mat.NumTevStages = m_File.ReadByte();

                m_File.Seek((int)(sectionstart + offsets[26] + (zmode_id * 4)));
                mat.ZMode.EnableZTest = m_File.ReadByte() != 0;
                mat.ZMode.Func = m_File.ReadByte();
                mat.ZMode.EnableZWrite = m_File.ReadByte() != 0;

                //

                mat.TexGen = new Material.TexGenInfo[mat.NumTexgens];
                for (int j = 0; j < mat.NumTexgens; j++)
                {
                    m_File.Seek((int)(sectionstart + offsets[11] + (texgen_id[j] * 4)));

                    mat.TexGen[j].Type = m_File.ReadByte();
                    mat.TexGen[j].Src = m_File.ReadByte();
                    mat.TexGen[j].Matrix = m_File.ReadByte();
                }

                // with some luck we don't need to support texgens2
                // SMG models don't seem to use it

                //

                mat.TexStages = new ushort[8];
                for (int j = 0; j < 8; j++)
                {
                    if (texstage_id[j] == 0xFFFF)
                    {
                        mat.TexStages[j] = 0xFFFF;
                        continue;
                    }

                    m_File.Seek((int)(sectionstart + offsets[15] + (texstage_id[j] * 2)));
                    mat.TexStages[j] = m_File.ReadUInt16();
                }

                mat.ConstColors = new Material.Color8Bit[4];
                for (int j = 0; j < 4; j++)
                {
                    if (constcolor_id[j] == 0xFFFF)
                    {
                        mat.ConstColors[j].R = 0; mat.ConstColors[j].G = 0;
                        mat.ConstColors[j].B = 0; mat.ConstColors[j].A = 0;
                    }
                    else
                    {
                        m_File.Seek((int)(sectionstart + offsets[18] + (constcolor_id[j] * 4)));
                        mat.ConstColors[j].R = m_File.ReadByte();
                        mat.ConstColors[j].G = m_File.ReadByte();
                        mat.ConstColors[j].B = m_File.ReadByte();
                        mat.ConstColors[j].A = m_File.ReadByte();
                    }
                }

                mat.TevOrder = new Material.TevOrderInfo[mat.NumTevStages];
                for (int j = 0; j < mat.NumTevStages; j++)
                {
                    m_File.Seek((int)(sectionstart + offsets[16] + (tevorder_id[j] * 4)));

                    mat.TevOrder[j].TexcoordId = m_File.ReadByte();
                    mat.TevOrder[j].TexMap = m_File.ReadByte();
                    mat.TevOrder[j].ChanID = m_File.ReadByte();
                }

                mat.ColorS10 = new Material.Color10Bit[4];
                for (int j = 0; j < 4; j++)
                {
                    if (colors10_id[j] == 0xFFFF)
                    {
                        mat.ColorS10[j].R = 255; mat.ColorS10[j].G = 0;
                        mat.ColorS10[j].B = 255; mat.ColorS10[j].A = 255;
                    }
                    else
                    {
                        m_File.Seek((int)(sectionstart + offsets[17] + (colors10_id[j] * 8)));
                        mat.ColorS10[j].R = m_File.ReadInt16();
                        mat.ColorS10[j].G = m_File.ReadInt16();
                        mat.ColorS10[j].B = m_File.ReadInt16();
                        mat.ColorS10[j].A = m_File.ReadInt16();
                    }
                }

                mat.TevStage = new Material.TevStageInfo[mat.NumTevStages];
                for (int j = 0; j < mat.NumTevStages; j++)
                {
                    m_File.Seek((int)(sectionstart + offsets[20] + (tevstage_id[j] * 20) + 1));

                    mat.TevStage[j].ColorIn = new byte[4];
                    for (int k = 0; k < 4; k++) mat.TevStage[j].ColorIn[k] = m_File.ReadByte();
                    mat.TevStage[j].ColorOp = m_File.ReadByte();
                    mat.TevStage[j].ColorBias = m_File.ReadByte();
                    mat.TevStage[j].ColorScale = m_File.ReadByte();
                    mat.TevStage[j].ColorClamp = m_File.ReadByte();
                    mat.TevStage[j].ColorRegID = m_File.ReadByte();

                    mat.TevStage[j].AlphaIn = new byte[4];
                    for (int k = 0; k < 4; k++) mat.TevStage[j].AlphaIn[k] = m_File.ReadByte();
                    mat.TevStage[j].AlphaOp = m_File.ReadByte();
                    mat.TevStage[j].AlphaBias = m_File.ReadByte();
                    mat.TevStage[j].AlphaScale = m_File.ReadByte();
                    mat.TevStage[j].AlphaClamp = m_File.ReadByte();
                    mat.TevStage[j].AlphaRegID = m_File.ReadByte();
                }

                mat.TevSwapMode = new Material.TevSwapModeInfo[mat.NumTevStages];
                for (int j = 0; j < mat.NumTevStages; j++)
                {
                    if (tevswap_id[j] == 0xFFFF)
                    {
                        mat.TevSwapMode[j].RasSel = 0;
                        mat.TevSwapMode[j].TexSel = 0;
                    }
                    else
                    {
                        m_File.Seek((int)(sectionstart + offsets[21] + (tevswap_id[j] * 4)));

                        mat.TevSwapMode[j].RasSel = m_File.ReadByte();
                        mat.TevSwapMode[j].TexSel = m_File.ReadByte();
                    }
                }

                mat.TevSwapTable = new Material.TevSwapModeTable[4];
                for (int j = 0; j < 4; j++)
                {
                    if (tevswaptbl_id[j] == 0xFFFF) continue; // safety
                    m_File.Seek((int)(sectionstart + offsets[22] + (tevswaptbl_id[j] * 4)));

                    mat.TevSwapTable[j].R = m_File.ReadByte();
                    mat.TevSwapTable[j].G = m_File.ReadByte();
                    mat.TevSwapTable[j].B = m_File.ReadByte();
                    mat.TevSwapTable[j].A = m_File.ReadByte();
                }

                m_File.Seek((int)(sectionstart + offsets[24] + (alphacomp_id * 8)));
                mat.AlphaComp.Func0 = m_File.ReadByte();
                mat.AlphaComp.Ref0 = m_File.ReadByte();
                mat.AlphaComp.MergeFunc = m_File.ReadByte();
                mat.AlphaComp.Func1 = m_File.ReadByte();
                mat.AlphaComp.Ref1 = m_File.ReadByte();

                m_File.Seek((int)(sectionstart + offsets[25] + (blendmode_id * 4)));
                mat.BlendMode.BlendMode = m_File.ReadByte();
                mat.BlendMode.SrcFactor = m_File.ReadByte();
                mat.BlendMode.DstFactor = m_File.ReadByte();
                mat.BlendMode.BlendOp = m_File.ReadByte();

                if (mat.DrawFlag != 1 && mat.DrawFlag != 4)
                    throw new Exception("Unknown DrawFlag " + mat.DrawFlag.ToString() + " for material " + mat.Name);
            }

            m_File.Seek((int)(sectionstart + sectionsize));
        }

        private void ReadMDL3()
        {
            long sectionstart = m_File.Position() - 4;
            uint sectionsize = m_File.ReadUInt32();

            // TODO: figure out what the fuck this section is about
            // bmdview2 has no code about it
            // the section doesn't seem important for rendering the model, but it
            // may have relations with animations or something else
            // and more importantly, can we generate a .bdl file without that
            // section and expect SMG to render it correctly?

            m_File.Seek((int)(sectionstart + sectionsize));
        }

        private void ReadTEX1()
        {
            long sectionstart = m_File.Position() - 4;
            uint sectionsize = m_File.ReadUInt32();

            ushort numtextures = m_File.ReadUInt16();
            m_File.Skip(2);

            Textures = new Texture[numtextures];

            uint entriesoffset = m_File.ReadUInt32();

            for (int i = 0; i < numtextures; i++)
            {
                Texture tex = new Texture();
                Textures[i] = tex;

                m_File.Seek((int)(sectionstart + entriesoffset + (i * 32)));

                tex.Format = m_File.ReadByte();
                m_File.Skip(1);
                tex.Width = m_File.ReadUInt16();
                tex.Height = m_File.ReadUInt16();

                tex.WrapS = m_File.ReadByte();
                tex.WrapT = m_File.ReadByte();

                m_File.Skip(1);

                tex.PaletteFormat = m_File.ReadByte();
                ushort palnumentries = m_File.ReadUInt16();
                uint paloffset = m_File.ReadUInt32();

                m_File.Skip(4);

                tex.MinFilter = m_File.ReadByte();
                tex.MagFilter = m_File.ReadByte();

                m_File.Skip(2);

                tex.MipmapCount = m_File.ReadByte();

                m_File.Skip(3);

                uint dataoffset = m_File.ReadUInt32();

                m_File.Seek((int)(sectionstart + dataoffset + 0x20 + (0x20 * i)));
                tex.Image = new byte[tex.MipmapCount][];
                int width = tex.Width, height = tex.Height;

                for (int mip = 0; mip < tex.MipmapCount; mip++)
                {
                    byte[] image = null;

                    switch (tex.Format)
                    {
                        case 0: // I4
                            {
                                image = new byte[width * height];

                                for (int by = 0; by < height; by += 8)
                                {
                                    for (int bx = 0; bx < width; bx += 8)
                                    {
                                        for (int y = 0; y < 8; y++)
                                        {
                                            for (int x = 0; x < 8; x += 2)
                                            {
                                                byte b = m_File.ReadByte();

                                                int outp = (((by + y) * width) + (bx + x));
                                                image[outp++] = (byte)((b & 0xF0) | (b >> 4));
                                                image[outp] = (byte)((b << 4) | (b & 0x0F));
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case 1: // I8
                            {
                                image = new byte[width * height];

                                for (int by = 0; by < height; by += 4)
                                {
                                    for (int bx = 0; bx < width; bx += 8)
                                    {
                                        for (int y = 0; y < 4; y++)
                                        {
                                            for (int x = 0; x < 8; x++)
                                            {
                                                byte b = m_File.ReadByte();

                                                int outp = (((by + y) * width) + (bx + x));
                                                image[outp] = b;
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case 2: // I4A4
                            {
                                image = new byte[width * height * 2];

                                for (int by = 0; by < height; by += 4)
                                {
                                    for (int bx = 0; bx < width; bx += 8)
                                    {
                                        for (int y = 0; y < 4; y++)
                                        {
                                            for (int x = 0; x < 8; x++)
                                            {
                                                byte b = m_File.ReadByte();

                                                int outp = (((by + y) * width) + (bx + x)) * 2;
                                                image[outp++] = (byte)((b << 4) | (b & 0x0F));
                                                image[outp] = (byte)((b & 0xF0) | (b >> 4));
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case 3: // I8A8
                            {
                                image = new byte[width * height * 2];

                                for (int by = 0; by < height; by += 4)
                                {
                                    for (int bx = 0; bx < width; bx += 4)
                                    {
                                        for (int y = 0; y < 4; y++)
                                        {
                                            for (int x = 0; x < 4; x++)
                                            {
                                                byte a = m_File.ReadByte();
                                                byte l = m_File.ReadByte();

                                                int outp = (((by + y) * width) + (bx + x)) * 2;
                                                image[outp++] = l;
                                                image[outp] = a;
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        case 4:
                                image = ImageUtil.DecodeRGB565(ref m_File, height, width);
                            break;
                        case 5:
                            image = ImageUtil.DecodeRGB5A3(ref m_File, height, width);
                            break;

                        case 14: // DXT1
                            {
                                image = new byte[width * height * 4];

                                for (int by = 0; by < height; by += 8)
                                {
                                    for (int bx = 0; bx < width; bx += 8)
                                    {
                                        for (int sby = 0; sby < 8; sby += 4)
                                        {
                                            for (int sbx = 0; sbx < 8; sbx += 4)
                                            {
                                                ushort c1 = m_File.ReadUInt16();
                                                ushort c2 = m_File.ReadUInt16();
                                                uint block = m_File.ReadUInt32();

                                                byte r1 = (byte)((c1 & 0xF800) >> 8);
                                                byte g1 = (byte)((c1 & 0x07E0) >> 3);
                                                byte b1 = (byte)((c1 & 0x001F) << 3);
                                                byte r2 = (byte)((c2 & 0xF800) >> 8);
                                                byte g2 = (byte)((c2 & 0x07E0) >> 3);
                                                byte b2 = (byte)((c2 & 0x001F) << 3);

                                                byte[,] colors = new byte[4, 4];
                                                colors[0, 0] = 255; colors[0, 1] = r1; colors[0, 2] = g1; colors[0, 3] = b1;
                                                colors[1, 0] = 255; colors[1, 1] = r2; colors[1, 2] = g2; colors[1, 3] = b2;
                                                if (c1 > c2)
                                                {
                                                    int r3 = ((r1 << 1) + r2) / 3;
                                                    int g3 = ((g1 << 1) + g2) / 3;
                                                    int b3 = ((b1 << 1) + b2) / 3;

                                                    int r4 = (r1 + (r2 << 1)) / 3;
                                                    int g4 = (g1 + (g2 << 1)) / 3;
                                                    int b4 = (b1 + (b2 << 1)) / 3;

                                                    colors[2, 0] = 255; colors[2, 1] = (byte)r3; colors[2, 2] = (byte)g3; colors[2, 3] = (byte)b3;
                                                    colors[3, 0] = 255; colors[3, 1] = (byte)r4; colors[3, 2] = (byte)g4; colors[3, 3] = (byte)b4;
                                                }
                                                else
                                                {
                                                    colors[2, 0] = 255;
                                                    colors[2, 1] = (byte)((r1 + r2) / 2);
                                                    colors[2, 2] = (byte)((g1 + g2) / 2);
                                                    colors[2, 3] = (byte)((b1 + b2) / 2);
                                                    colors[3, 0] = 0; colors[3, 1] = r2; colors[3, 2] = g2; colors[3, 3] = b2;
                                                }

                                                for (int y = 0; y < 4; y++)
                                                {
                                                    for (int x = 0; x < 4; x++)
                                                    {
                                                        int c = (int)(block >> 30);
                                                        int outp = (((by + sby + y) * width) + (bx + sbx + x)) * 4;
                                                        image[outp++] = (byte)(colors[c, 3] | (colors[c, 3] >> 5));
                                                        image[outp++] = (byte)(colors[c, 2] | (colors[c, 2] >> 5));
                                                        image[outp++] = (byte)(colors[c, 1] | (colors[c, 1] >> 5));
                                                        image[outp] = colors[c, 0];
                                                        block <<= 2;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            break;

                        default: throw new NotImplementedException("Bmd: unsupported texture format " + tex.Format.ToString());
                    }

#if asfads
                    System.Drawing.Bitmap lol = new System.Drawing.Bitmap((int)width, (int)height);
                    switch (tex.Format)
                    {
                        case 0:
                        case 1:
                            for (int y = 0; y < (int)height; y++)
                                for (int x = 0; x < (int)width; x++)
                                    lol.SetPixel(x, y, System.Drawing.Color.FromArgb(image[((y * width) + x)],
                                        image[((y * width) + x)],
                                        image[((y * width) + x)],
                                        image[((y * width) + x)]));
                            break;

                        case 2:
                        case 3:
                            for (int y = 0; y < (int)height; y++)
                                for (int x = 0; x < (int)width; x++)
                                    lol.SetPixel(x, y, System.Drawing.Color.FromArgb(image[((y * width) + x) * 2 + 1],
                                        image[((y * width) + x) * 2],
                                        image[((y * width) + x) * 2],
                                        image[((y * width) + x) * 2]));
                            break;

                        default:
                            for (int y = 0; y < (int)height; y++)
                                for (int x = 0; x < (int)width; x++)
                                    lol.SetPixel(x, y, System.Drawing.Color.FromArgb(image[((y * width) + x) * 4 + 3],
                                        image[((y * width) + x) * 4 + 2],
                                        image[((y * width) + x) * 4 + 1],
                                        image[((y * width) + x) * 4]));
                            break;
                    }
                    lol.Save("loltex/vanish" + lolz.ToString() + "_" + i.ToString() + "_mip" + mip.ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
#endif

                    tex.Image[mip] = image;
                    width /= 2; height /= 2;
                }
            }

            m_File.Seek((int)(sectionstart + sectionsize));
            lolz++;
        }
        static int lolz = 0;


        public class SceneGraphNode
        {
            public ushort MaterialID;

            public int ParentIndex;
            public int NodeType; // 0: shape, 1: joint
            public ushort NodeID;
        }

        public class Batch
        {
            public class Packet
            {
                public class Primitive
                {
                    public int NumIndices;
                    public byte PrimitiveType;

                    public uint ArrayMask;
                    public int[] PosMatrixIndices;
                    public int[] PositionIndices;
                    public int[] NormalIndices;
                    public int[][] ColorIndices;
                    public int[][] TexcoordIndices;
                }


                public List<Primitive> Primitives;
                public ushort[] MatrixTable;
            }


            public byte MatrixType;

            public Packet[] Packets;

            public float Unk;
        }

        public class MultiMatrix
        {
            public int NumMatrices;
            public ushort[] MatrixIndices;
            public Matrix4[] Matrices;
            public float[] MatrixWeights;
        }

        public class MatrixType
        {
            public bool IsWeighted;
            public ushort Index;
        }

        public class Joint
        {
            public ushort Unk1;
            public byte Unk2;

            public Vector3 Scale, Rotation, Translation;
            public Matrix4 Matrix;
            public Matrix4 FinalMatrix; // matrix with parents' transforms applied
        }

        public class Material
        {
            public struct ZModeInfo
            {
                public bool EnableZTest;
                public byte Func;
                public bool EnableZWrite;
            }

            public struct TevOrderInfo
            {
                public byte TexcoordId;
                public byte TexMap;
                public byte ChanID;
            }

            public struct Color8Bit
            {
                public byte R, G, B, A;
            }

            public struct Color10Bit
            {
                public short R, G, B, A;
            }

            public struct TexGenInfo
            {
                public byte Type;
                public byte Src;
                public byte Matrix;
            }

            public struct TevStageInfo
            {
                public byte[] ColorIn;
                public byte ColorOp;
                public byte ColorBias;
                public byte ColorScale;
                public byte ColorClamp;
                public byte ColorRegID;

                public byte[] AlphaIn;
                public byte AlphaOp;
                public byte AlphaBias;
                public byte AlphaScale;
                public byte AlphaClamp;
                public byte AlphaRegID;
            }

            public struct TevSwapModeInfo
            {
                public byte RasSel;
                public byte TexSel;
            }

            public struct TevSwapModeTable
            {
                public byte R, G, B, A;
            }

            public struct AlphaCompInfo
            {
                public byte Func0, Ref0;
                public byte MergeFunc;
                public byte Func1, Ref1;
            }

            public struct BlendModeInfo
            {
                public byte BlendMode;
                public byte SrcFactor, DstFactor;
                public byte BlendOp;
            }


            public string Name;

            public byte DrawFlag; // apparently: 1=opaque, 4=translucent, 253=???
            public byte CullMode;
            public int NumChans;
            public int NumTexgens;
            public int NumTevStages;
            // matData6
            public ZModeInfo ZMode;
            // matData7

            // lights

            public TexGenInfo[] TexGen;
            // texGenInfo2

            // texMatrices
            // dttMatrices

            public ushort[] TexStages;
            public Color8Bit[] ConstColors;
            public byte[] ConstColorSel;
            public byte[] ConstAlphaSel;
            public TevOrderInfo[] TevOrder;
            public Color10Bit[] ColorS10;
            public TevStageInfo[] TevStage;
            public TevSwapModeInfo[] TevSwapMode;
            public TevSwapModeTable[] TevSwapTable;
            // fog
            public AlphaCompInfo AlphaComp;
            public BlendModeInfo BlendMode;
        }

        public class Texture
        {
            public byte Format;
            public ushort Width, Height;

            public byte WrapS, WrapT;

            public byte PaletteFormat;
            public byte[] Palette; // ARGB palette for palettized textures, null otherwise

            public byte MinFilter;
            public byte MagFilter;

            public byte MipmapCount;

            public byte[][] Image; // texture data converted to ARGB
        }


        private FileBase m_File;

        public Vector3 BBoxMin, BBoxMax;

        // INF1
        public uint NumVertices;
        public List<SceneGraphNode> SceneGraph;

        // VTX1
        public uint ArrayMask;
        public Vector3[] PositionArray;
        public Vector3[] NormalArray;
        public Vector4[][] ColorArray;
        public Vector2[][] TexcoordArray;

        // SHP1
        public Batch[] Batches;

        // EVP1
        public MultiMatrix[] MultiMatrices;

        // DRW1
        public MatrixType[] MatrixTypes;

        // JNT1
        public Joint[] Joints;

        // MAT3
        public Material[] Materials;

        // TEX1
        public Texture[] Textures;
    }
}
