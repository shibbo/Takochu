using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;
using Takochu.smg.msg;
using Takochu.util;

namespace Takochu.fmt
{
    public class MSBT
    {
        public MSBT(FileBase file)
        {
            mFile = file;

            if (file.ReadString(8) != "MsgStdBn")
            {
                throw new Exception("MSBT::MSBT() -- Invalid MSBT file!");
            }

            file.Skip(0xA);
            int fileSize = file.ReadInt32();
            file.Skip(0xA);

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

        public void Close()
        {
            mFile.Close();
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

        public bool HasGalaxyName(string galaxy)
        {
            return GetMessages().ContainsKey($"GalaxyName_{galaxy}");
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

        public string GetStringFromIndex(ushort idx)
        {
            string ret = "";
            List<MessageBase> msgs = mText.mMessages[idx];

            foreach (MessageBase m in msgs)
            {
                if (m is Character)
                {
                    ret += m.ToString();
                }
            }

            return ret;
        }

        public void Save()
        {
            int size = 0x20;

            int lbl = mLabels.CalcSize();
            int atr = mAttributes.CalcSize();
            int txt = mText.CalcSize();

            size += lbl;
            size += atr;
            size += txt;

            mFile.SetLength(size);
            mFile.Seek(0);
            mFile.WriteString("MsgStdBn");
            mFile.Write((ushort)0xFEFF);
            mFile.Write((short)0);
            mFile.Write((short)0x0103);
            mFile.Write((short)3);
            mFile.Write((short)0);
            mFile.Write(size);
            mFile.WritePadding(0, 0xA);

            mLabels.Save(ref mFile);
            mAttributes.Save(ref mFile);
            mText.Save(ref mFile);

            mFile.Save();
        }

        LBL1 mLabels;
        ATR1 mAttributes;
        TXT2 mText;
        FileBase mFile;
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

        public void Save(ref FileBase file)
        {
            file.WriteString("LBL1");
            file.Write(CalcSizeNoPadding());
            file.WritePadding(0, 0x8);
            file.Write(mEntries.Count);

            int dataOffset = (mEntries.Count * 0x8) + 4;

            List<LabelPair> pairs = new List<LabelPair>();

            foreach (LabelEntry e in mEntries)
            {
                // if the pair count is greater than zero, we can write a new count and up our current data offset
                if (e.Pairs.Count > 0)
                {
                    file.Write(e.Pairs.Count);
                    file.Write(dataOffset);

                    pairs.AddRange(e.Pairs);

                    // increment our data offset by the length of each string in the pair
                    for (int i = 0; i < e.Pairs.Count; i++)
                    {
                        dataOffset += e.Pairs[i].Label.Length + 5;
                    }
                }
                else
                {
                    // otherwise we just write another entry with no pairs and the same data offset
                    file.Write(0);
                    file.Write(dataOffset);
                }
            }

            // now we write the actual data
            foreach(LabelPair p in pairs)
            {
                file.Write((byte)p.Label.Length);
                file.WriteString(p.Label);
                file.Write(p.TextOffset);
            }

            while (file.Position() % 0x10 != 0)
                file.Write((byte)0xAB);
        }

        private int LabelHash(string label, int bucketCount)
        {
            int curGroup = 0;

            for (int i = 0; i < label.Length; i++)
            {
                curGroup *= 0x492;
                curGroup += label[i];
            }

            return curGroup % bucketCount;
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

        public int CalcSizeNoPadding()
        {
            int size = 4;
            size += (mEntries.Count * 0x8);

            // now we go through our pairs and calculate more sizes based on the strings
            foreach (LabelEntry e in mEntries)
            {
                foreach (LabelPair p in e.Pairs)
                {
                    // value
                    size += 4;
                    // string length itself (+1 for string size)
                    size += p.Label.Length + 1;
                }
            }

            return size;
        }

        public int CalcSize()
        {
            // we start with the size of this header, which is 0x14
            int size = 0x14;
            size += (mEntries.Count * 0x8);

            // now we go through our pairs and calculate more sizes based on the strings
            foreach(LabelEntry e in mEntries)
            {
               foreach(LabelPair p in e.Pairs)
                {
                    // value
                    size += 4;
                    // string length itself (+1 for string size)
                    size += p.Label.Length + 1;
                }
            }

            // alignment
            while (size % 0x10 != 0)
                size++;

            return size;
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
            mSomeValue = file.ReadInt32();

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

                file.ReadInt32();

                mAttributes.Add(e);
            }

            file.Skip(mAttributes.Count * 2);

            while (file.Position() % 0x10 != 0)
                file.Skip(0x1);
        }

        public int CalcSize()
        {
            int size = 0x18;
            size += (mAttributes.Count * 0xC);
            size += (mAttributes.Count * 0x2);

            while (size % 0x10 != 0)
                size++;

            return size;
        }

        public int CalcSizeNoPadding()
        {
            int size = 0x8;
            size += (mAttributes.Count * 0xC);
            size += (mAttributes.Count * 0x2);

            return size;
        }

        public void Save(ref FileBase file)
        {
            file.WriteString("ATR1");
            file.Write(CalcSizeNoPadding());
            file.WritePadding(0, 0x8);
            file.Write(mAttributes.Count);
            file.Write(mSomeValue);

            int dataOffset = (mAttributes.Count * 0xC) + 0x8;

            foreach(AttributeEntry e in mAttributes)
            {
                file.Write(e._0);
                file.Write(e._1);
                file.Write(e._2);
                file.Write(e._3);
                file.Write(e._4);
                file.Write(e._5);
                file.Write(e._6);
                file.Write(e._7);
                file.Write(dataOffset);

                dataOffset += 2;
            }

            for (int i = 0; i < mAttributes.Count; i++)
                file.Write((short)0x0000);

            while (file.Position() % 0x10 != 0)
                file.Write((byte)0xAB);
        }

        List<AttributeEntry> mAttributes;
        int mSomeValue;

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

        public int CalcSize()
        {
            int size = 0x14;
            // offsets
            size += (mMessages.Count * 0x4);
            // null terminators are a double 0x00 at the end of each string
            // we add it here because it's easier than trying to find the end of a message list anyways
            size += (mMessages.Count * 0x2);

            // now we move on to the messages themselves
            foreach(KeyValuePair<uint, List<MessageBase>> p in mMessages)
            {
                List<MessageBase> msgs = p.Value;

                msgs.ForEach(m =>
                {
                    size += m.CalcSize();

                    if (!(m is Character))
                        size += 0x2;

                });
            }

            while (size % 0x10 != 0)
                size++;

            return size;
        }

        public int CalcSizeNoPadding()
        {
            int size = 0x4;
            // offsets
            size += (mMessages.Count * 0x4);
            // null terminators are a double 0x00 at the end of each string
            // we add it here because it's easier than trying to find the end of a message list anyways
            size += (mMessages.Count * 0x2);

            // now we move on to the messages themselves
            foreach (KeyValuePair<uint, List<MessageBase>> p in mMessages)
            {
                List<MessageBase> msgs = p.Value;

                msgs.ForEach(m =>
                {
                    size += m.CalcSize();

                    if (!(m is Character))
                        size += 0x2;
                });
            }

            return size;
        }

        public List<MessageBase> GetMessageFromID(uint id)
        {
            return mMessages[id];
        }

        public void Save(ref FileBase file)
        {
            file.WriteString("TXT2");
            file.Write(CalcSizeNoPadding());
            file.WritePadding(0, 0x8);
            file.Write(mMessages.Count);

            int dataOffset = (mMessages.Count * 0x4) + 4;

            // first let's write the offsets
            foreach (KeyValuePair<uint, List<MessageBase>> p in mMessages)
            {
                file.Write(dataOffset);

                List<MessageBase> msgs = p.Value;

                msgs.ForEach(m =>
                {
                    dataOffset += m.CalcSize();

                    if (!(m is Character))
                        dataOffset += 0x2;
                });

                dataOffset += 0x2;
            }

            // now let's write the actual text data
            foreach(KeyValuePair<uint, List<MessageBase>> p in mMessages)
            {
                List<MessageBase> msgs = p.Value;

                foreach (MessageBase b in msgs)
                    b.Save(ref file);

                // null terminator at the end
                file.Write((ushort)0);
            }

            while (file.Position() % 0x10 != 0)
                file.Write((byte)0xAB);
        }

        public Dictionary<uint, List<MessageBase>> mMessages;
    }
}
