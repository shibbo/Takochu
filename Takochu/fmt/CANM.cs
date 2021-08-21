using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.io;

namespace Takochu.fmt
{
    public class CANM
    {
        private static readonly string[] sNames = new string[0x8]{ "pos_x", "pos_y", "pos_z", "dir_x", "dir_y", "dir_z", "twist", "fovy" };

        public CANM(FileBase file)
        {
            mFile = file;

            if (file.ReadString(0x8) != "ANDOCKAN")
            {
                throw new Exception("CANM::CANM -- Invalid CANM file!");
            }

            _08 = mFile.ReadInt32();
            _0C = mFile.ReadInt32();
            _10 = mFile.ReadInt32();
            _14 = mFile.ReadInt32();
            _18 = mFile.ReadInt32();
            mTableOffs = mFile.ReadInt32();

            mKeyFrameIndicies = new Dictionary<string, KeyFrameIndexer>();

            foreach(string s in sNames)
            {
                KeyFrameIndexer idxer = new KeyFrameIndexer
                {
                    mKeyFrameCount = mFile.ReadInt32(),
                    mStartIdx = mFile.ReadInt32()
                };

                mFile.Skip(0x4);

                mKeyFrameIndicies.Add(s, idxer);
            }

            int tableCount = mFile.ReadInt32() / 4;

            mTable = new float[tableCount];

            for (int i = 0; i < tableCount; i++)
                mTable[i] = mFile.ReadSingle();

            mKeyFrames = new Dictionary<string, KeyFrames>();

            foreach(KeyValuePair<string, KeyFrameIndexer> idx in mKeyFrameIndicies)
            {
                KeyFrameIndexer thing = idx.Value;

                float[] input;

                if (thing.mKeyFrameCount == 1)
                    input = new float[]{ 0, mTable[thing.mStartIdx], 0 };
                else
                {
                    input = new float[thing.mKeyFrameCount * 0x3];

                    for (int i = 0; i < thing.mKeyFrameCount * 0x3; i++)
                        input[i] = mTable[thing.mStartIdx + i];
                }

                mKeyFrames.Add(idx.Key, new KeyFrames(input));
            }
        }

        public KeyFrames GetKeyFrames(string name)
        {
            return mKeyFrames[name];
        }

        public struct KeyFrameIndexer
        {
            public int mKeyFrameCount;
            public int mStartIdx;
        }

        private FileBase mFile;
        private Dictionary<string, KeyFrameIndexer> mKeyFrameIndicies;
        private float[] mTable;
        public Dictionary<string, KeyFrames> mKeyFrames;

        public int _08;
        public int _0C;
        public int _10;
        public int _14;
        public int _18;
        public int mTableOffs;
    }

    public class KeyFrames
    {
        public KeyFrames(float[] values)
        {
            mValues = values;
        }

        public int GetCount()
        {
            return mValues.Length;
        }

        public float[] GetData()
        {
            return mValues;
        }

        float[] mValues;
    }

}