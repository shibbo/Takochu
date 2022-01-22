using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;

namespace Takochu.fmt
{
    public class BRK
    {
        public BRK(FileBase file)
        {
            mFile = file;

            if (mFile.ReadString(8) != "J3D1brk1")
            {
                throw new Exception("BRK::BRK() -- Invalid BRK file!");
            }

            uint fileSize = mFile.ReadUInt32();
            uint sectionCount = mFile.ReadUInt32();

            // SVR1 section is useless and can be ignored
            mFile.Skip(0x10);

            for (int i = 0; i < sectionCount; i++)
            {
                string magic = mFile.ReadString(4);

                switch(magic)
                {
                    case "TRK1":
                        mTRK1 = new TRK1(ref mFile);
                        break;
                    default:
                        throw new Exception($"BRK::BRK() -- Unknown BRK section {magic}!");
                }
            }
        }

        FileBase mFile;
        TRK1 mTRK1;
    }

    class TRK1
    {
        public TRK1(ref FileBase file)
        {
            long sectionStart = file.Position();
            int length = file.ReadInt32();
            mLoopMode = file.ReadByte();
            file.Skip(0x1);
            mTime = file.ReadUInt16();
            mRegCount = file.ReadUInt16();
            mConstCount = file.ReadUInt16();
            mParts = new ushort[8];
            mOffsets = new uint[14];

            for (int i = 0; i < 8; i++)
                mParts[i] = file.ReadUInt16();

            for (int i = 0; i < 14; i++)
                mOffsets[i] = file.ReadUInt32();

            // skip to nearest 0x10th byte, "This is padding data"
            while (file.Position() % 0x10 != 0)
                file.Skip(0x1);

            mRegisters = new ushort[mRegCount];

            for (int i = 0; i < mRegCount; i++)
                mRegisters[i] = file.ReadUInt16();

            mConstants = new ushort[mConstCount];

            for (int i = 0; i < mConstCount; i++)
                mConstants[i] = file.ReadUInt16();
        }

        byte mLoopMode;
        ushort mTime;
        ushort mRegCount;
        ushort mConstCount;
        ushort[] mParts;
        uint[] mOffsets;

        ushort[] mRegisters;
        ushort[] mConstants;
    }
    
    struct TrackData
    {
        public short mCount;
        public short mIndex;
        public short mTangentMode;
    }

    class AnimationTable
    {
        public AnimationTable(ref FileBase file)
        {

        }

        TrackData mRed;
        TrackData mGreen;
        TrackData mBlue;
        TrackData mAlpha;
    }
}
