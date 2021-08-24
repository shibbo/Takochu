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
                mPacketIndex = file.ReadByte();
                mPacketLength = file.ReadByte();
                mFlags = file.ReadInt16();

                if ((mFlags & 0x1) != 0)
                {
                    mTranslation = new Vector3(file.ReadInt16(), file.ReadInt16(), file.ReadInt16());
                }

                if ((mFlags & 0x800) != 0)
                {
                    mVelocity = new Vector3(file.ReadByte(), file.ReadByte(), file.ReadByte());
                }

                if ((mFlags & 0x400) != 0)
                {
                    mScale = new Vector3(file.ReadByte(), file.ReadByte(), file.ReadByte());
                }

                if ((mFlags & 0x2) != 0)
                {
                    _unkFlag2 = file.ReadByte();
                }

                if ((mFlags & 0x4) != 0)
                {
                    _unkFlag4 = file.ReadByte();
                }

                if ((mFlags & 0x8) != 0)
                {
                    _unkFlag8 = file.ReadByte();
                }

                if ((mFlags & 0xE) != 0)
                {
                    // makeMtxRotate
                }

                if ((mFlags & 0x10) != 0)
                {
                    mCurrentAnimation = file.ReadString();
                }

                if ((mFlags & 0x2000) != 0)
                {
                    mAnimationHash = file.ReadUInt32();
                }

                if ((mFlags & 0x20) != 0)
                {
                    mBCKRelated = file.ReadInt16();
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

                    mWeight = other;
                }

                if ((mFlags & 0x1000) != 0)
                {
                    mBCKRate = file.ReadByte();
                }

                //file.ReadBytes(mPacketLength - 0x4);
            }

            byte mPacketIndex;
            byte mPacketLength;

            short mFlags;

            Vector3 mTranslation;
            Vector3 mVelocity;
            Vector3 mScale;

            float _unkFlag2;
            float _unkFlag4;
            float _unkFlag8;

            string mCurrentAnimation;
            uint mAnimationHash;

            float mBCKRelated;
            float mWeight;
            float mBCKRate;
        }
    }
}
