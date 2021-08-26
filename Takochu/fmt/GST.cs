using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;

namespace Takochu.fmt
{
    public class GST
    {

        public GST(FileBase file)
        {
            mFile = file;
            mPackets = new List<GhostPacket>();

            while(mFile.Position() != mFile.GetLength())
            {
                mPackets.Add(new GhostPacket(mFile));
            }
        }

        private FileBase mFile;
        List<GhostPacket> mPackets;


        class GhostPacket
        {
            public GhostPacket(FileBase file)
            {
                mValues = new Dictionary<string, object>();

                mPacketIndex = file.ReadByte();
                mPacketLength = file.ReadByte();
                mFlags = file.ReadInt16();

                if ((mFlags & 0x1) != 0)
                {
                    mValues.Add("Translation", new Vector3(file.ReadInt16(), file.ReadInt16(), file.ReadInt16()));
                }

                if ((mFlags & 0x800) != 0)
                {
                    mValues.Add("Velocity", new Vector3(file.ReadByte(), file.ReadByte(), file.ReadByte()));
                }

                if ((mFlags & 0x400) != 0)
                {
                    mValues.Add("Scale", new Vector3(file.ReadByte(), file.ReadByte(), file.ReadByte()));
                }

                if ((mFlags & 0x2) != 0)
                {
                    mValues.Add("Unk2", (float)file.ReadByte());
                }

                if ((mFlags & 0x4) != 0)
                {
                    mValues.Add("Unk4", (float)file.ReadByte());
                }

                if ((mFlags & 0x8) != 0)
                {
                    mValues.Add("Unk8", (float)file.ReadByte());
                }

                if ((mFlags & 0xE) != 0)
                {
                    // makeMtxRotate
                }

                if ((mFlags & 0x10) != 0)
                {
                    mValues.Add("CurrentAnimation", file.ReadString());
                }

                if ((mFlags & 0x2000) != 0)
                {
                    mValues.Add("AnimationHash", file.ReadUInt32());
                }

                if ((mFlags & 0x20) != 0)
                {
                    mValues.Add("BCKRelated", (float)file.ReadInt16());
                }

                for (int i = 0; i < 4; i++)
                {
                    int val = 0x40 << i;
                    val &= mFlags;

                    if (val == 0)
                        continue;

                    byte other = file.ReadByte();

                    if (other == 0x80)
                        other = 0x7F;


                    mValues.Add($"Weight_{i}", (float)other);
                }

                if ((mFlags & 0x1000) != 0)
                {
                    mValues.Add("BCKRate", (float)file.ReadByte());
                }
            }

            Dictionary<string, object> mValues;

            byte mPacketIndex;
            byte mPacketLength;

            short mFlags;
        }
    }
}
