using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;

namespace Takochu.fmt
{
    public class MSBT
    {
        public MSBT(FileBase file)
        {
            if (file.ReadString(8) != "MsgStdBn")
            {
                throw new Exception("MSBT::MSBT() -- Invalid MSBT file!");
            }

            file.Skip(0x18);

            while (file.Position() != file.GetLength())
            {
                string magic = file.ReadString(4);

                switch (magic)
                {
                    case "LBL1":
                        mLabels = new LBL1(ref file);
                        break;
                    case "ATR1":
                        mAttributes = new ATR1(ref file);
                        break;
                    case "TXT2":
                        mText = new TXT2(ref file);
                        break;
                }
            }
        }

        public Dictionary<string, List<MessageBase>> GetMessages()
        {
            Dictionary<string, List<MessageBase>> msgs = new Dictionary<string, List<MessageBase>>();

            foreach(LBL1.LabelEntry e in mLabels.mEntries)
            {
                e.Pairs.ForEach(p => msgs.Add(p.Label, mText.GetMessageFromID(p.TextOffset)));
            }

            return msgs; 
        }

        public List<MessageBase> GetMessageFromLabel(string label)
        {
            return GetMessages()[label];
        }

        public string GetStringFromLabelNoTag(string label)
        {
            string ret = "";
            List<MessageBase> msgs = GetMessageFromLabel(label);

            foreach(MessageBase m in msgs)
            {
                if (m is Character)
                {
                    ret += m.ToString();
                }
            }

            return ret;
        }

        LBL1 mLabels;
        ATR1 mAttributes;
        TXT2 mText;
    }

    public class LBL1
    {
        public LBL1(ref FileBase file)
        {
            int start = file.ReadInt32();
            file.Skip(0x8);

            int baseOffs = file.Position();
            uint count = file.ReadUInt32();

            mEntries = new List<LabelEntry>();

            for (int i = 0; i < count; i++)
            {
                LabelEntry e = new LabelEntry();
                e.PairCount = file.ReadUInt32();

                int offs = file.ReadInt32();

                List<LabelPair> pairs = new List<LabelPair>();

                int pos = file.Position();
                file.Seek(baseOffs + offs);

                for (int j = 0; j < e.PairCount; j++)
                {   
                    LabelPair p = new LabelPair();
                    p.Label = file.ReadStringLenPrefix();
                    p.TextOffset = file.ReadUInt32();

                    pairs.Add(p);
                }

                file.Seek(pos);

                e.Pairs = pairs;
                mEntries.Add(e);
            }

            file.Seek(start + baseOffs);

            while (file.Position() % 0x10 != 0)
                file.Skip(0x1);
        }

        public List<LabelEntry> mEntries;

        public struct LabelEntry
        {
            public uint PairCount;
            public List<LabelPair> Pairs;
        }

        public struct LabelPair
        {
            public string Label;
            public uint TextOffset;
        }
    }

    public class ATR1
    {
        public ATR1(ref FileBase file)
        {
            int start = file.ReadInt32();
            file.Skip(0x8);
            int baseOffset = file.Position();
            int entryCount = file.ReadInt32();
            file.Skip(0x4);

            mAttributes = new List<AttributeEntry>();

            for (int i = 0; i < entryCount; i++)
            {
                AttributeEntry e = new AttributeEntry
                {
                    _0 = file.ReadByte(),
                    _1 = file.ReadByte(),
                    _2 = file.ReadByte(),
                    _3 = file.ReadByte(),
                    _4 = file.ReadByte(),
                    _5 = file.ReadByte(),
                    _6 = file.ReadByte(),
                    _7 = file.ReadByte()
                };

                int offs = file.ReadInt32();
                int orig = file.Position();

                file.Seek(baseOffset + offs);
                e.mString = file.ReadStringUTF16();

                file.Seek(orig);
            }

            file.Seek(start + baseOffset);

            while (file.Position() % 0x10 != 0)
                file.Skip(0x1);
        }

        List<AttributeEntry> mAttributes;

        public struct AttributeEntry
        {
            public byte _0;
            public byte _1;
            public byte _2;
            public byte _3;
            public byte _4;
            public byte _5;
            public byte _6;
            public byte _7;
            public string mString;
        }
    }

    class TXT2
    {
        public TXT2(ref FileBase file)
        {
            int start = file.ReadInt32();
            file.Skip(0x8);
            int dataStartOffs = file.Position();
            int count = file.ReadInt32();

            mMessages = new Dictionary<uint, List<MessageBase>>();

            for (uint i = 0; i < count; i++)
            {
                List<MessageBase> msgs = new List<MessageBase>();

                file.Seek(Convert.ToInt32(dataStartOffs + (i * 4) + 4));
                int offs = file.ReadInt32();
                file.Seek(offs + dataStartOffs);

                short cur = -1;

                while (cur != 0)
                {
                    cur = file.ReadInt16();

                    if (cur == 0)
                    {
                        mMessages.Add(i, msgs);
                        break;
                    }

                    switch(cur)
                    {
                        // new command
                        case 0xE:
                            ushort opcode = file.ReadUInt16();

                            switch (opcode)
                            {
                                case 0:
                                    msgs.Add(new SystemGroup(ref file));
                                    break;

                                case 1:
                                    msgs.Add(new DisplayGroup(ref file));
                                    break;

                                case 2:
                                    msgs.Add(new SoundGroup(ref file));
                                    break;

                                case 3:
                                    msgs.Add(new PictureGroup(ref file));
                                    break;

                                case 4:
                                    msgs.Add(new FontSizeGroup(ref file));
                                    break;

                                case 5:
                                    msgs.Add(new LocalizeGroup(ref file));
                                    break;

                                case 6:
                                    msgs.Add(new NumberGroup(ref file));
                                    break;

                                default:
                                    Console.WriteLine($"Unsupported opcode {opcode}");
                                    break;
                            }
                            break;
                        default:
                            // normal char
                            msgs.Add(new Character(cur));
                            break;
                    }
                }
            }

            file.Seek(start + dataStartOffs);

            while (file.Position() % 0x10 != 0)
                file.Skip(0x1);
        }

        public List<MessageBase> GetMessageFromID(uint id)
        {
            return mMessages[id];
        }

        public Dictionary<uint, List<MessageBase>> mMessages;
    }

    public class MessageBase
    { }

    class Character : MessageBase
    {
        public Character(short cur)
        {
            mCharacter = (ushort)cur;
        }

        public override string ToString()
        {
            if (mCharacter == 0xA)
                return "\n";

            byte[] e = BitConverter.GetBytes(mCharacter);
            return Encoding.Unicode.GetString(e).Replace("\"", "");
        }

        ushort mCharacter;
    }

    class SystemGroup : MessageBase
    {
        public SystemGroup(ref FileBase file)
        {
            ushort mType = file.ReadUInt16();
            // we skip the data size here, we can safely determine the size by the type
            file.Skip(0x2);
            // type 0 is japanese only
            // type 3 is color
            if (mType == 3)
            {
                mColor = file.ReadInt16();
            }
        }

        public override string ToString()
        {
            return $"[color={mColor}]";
        }

        ushort mType;
        short mColor;
    }

    class PictureGroup : MessageBase
    {
        public PictureGroup(ref FileBase file)
        {
            // idx + 0x30 done intentionally by Nintendo
            mCharIdx = Convert.ToUInt16(file.ReadUInt16() + 0x30);
            mFont = file.ReadUInt16();
            mCharID = file.ReadUInt16();
        }

        public override string ToString()
        {
            return $"[img={mCharIdx}]";
        }

        ushort mCharIdx;
        ushort mFont;
        ushort mCharID;
    }

    class DisplayGroup : MessageBase
    {
        public DisplayGroup(ref FileBase file)
        {
            mType = file.ReadUInt16();

            if (mType != 0)
            {
                file.Skip(0x4);
            }
            else
            {
                file.Skip(0x2);
                mFrames = file.ReadUInt16();
            }
        }

        public override string ToString()
        {
            return $"[wait={mFrames}]";
        }

        ushort mType;
        ushort mFrames;
    }

    class FontSizeGroup : MessageBase
    {
        public FontSizeGroup(ref FileBase file)
        {
            mFontSize = file.ReadUInt16();
            file.Skip(0x2);
        }

        public override string ToString()
        {
            return $"[font={mFontSize}]";
        }

        ushort mFontSize;
    }

    class NumberGroup : MessageBase
    {
        public NumberGroup(ref FileBase file)
        {
            mMaxWidth = file.ReadUInt16();
            mWidth = file.ReadUInt16();
            mNumber = Convert.ToInt32(file.ReadBytes(mWidth));
        }

        public override string ToString()
        {
            return $"[value={mNumber}]";
        }

        ushort mMaxWidth;
        ushort mWidth;
        int mNumber;
    }

    class SoundGroup : MessageBase
    {
        public SoundGroup(ref FileBase file)
        {
            file.Skip(0x4);
            ushort len = file.ReadUInt16();

            string str = "";

            for (int i = 0; i < len / 2; i++)
            {
                byte[] e = BitConverter.GetBytes(file.ReadUInt16());
                str += Encoding.Unicode.GetString(e);
            }

            mSoundID = str;
        }

        public override string ToString()
        {
            return $"[sound=\"{mSoundID}\"]";
        }

        string mSoundID;
    }

    class LocalizeGroup : MessageBase
    {
        public LocalizeGroup(ref FileBase file)
        {
            file.Skip(0x6);
        }

        public override string ToString()
        {
            return "[player]";
        }
    }
}
