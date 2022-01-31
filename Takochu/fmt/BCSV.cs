using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;

namespace Takochu.fmt
{
    public class BCSV
    {
        public BCSV(FileBase file)
        {
            mFile = file;

            mFields = new Dictionary<int, Field>();
            mEntries = new List<Entry>();

            if (mFile.GetLength() == 0)
            {
                return;
            }

            int entryCount = mFile.ReadInt32();
            int fieldCount = mFile.ReadInt32();
            int dataOffs = mFile.ReadInt32();
            int entryDataSize = mFile.ReadInt32();

            int stringTableOffs = (dataOffs + (entryCount * entryDataSize));

            for (int i = 0; i < fieldCount; i++)
            {
                Field f = new Field();
                mFile.Seek(0x10 + (0xC * i));

                f.mHash = mFile.ReadInt32();
                f.mMask = mFile.ReadInt32();
                f.mEntryOffset = mFile.ReadUInt16();
                f.mShiftAmount = mFile.ReadByte();
                f.mType = mFile.ReadByte();

                string fieldName = HashToFieldName(f.mHash);
                f.mName = fieldName;
                mFields.Add(f.mHash, f);
            }

            for (int i = 0; i < entryCount; i++)
            {
                Entry e = new Entry();

                foreach (Field f in mFields.Values)
                {
                    mFile.Seek(dataOffs + (i * entryDataSize) + f.mEntryOffset);

                    object val = null;

                    switch (f.mType)
                    {
                        case 0:
                        case 3:
                            val = Convert.ToInt32((mFile.ReadInt32() & f.mMask) >> f.mShiftAmount);
                            break;

                        case 4:
                            val = (short)((mFile.ReadInt16() & f.mMask) >> f.mShiftAmount);
                            break;

                        case 5:
                            val = Convert.ToByte((mFile.ReadByte() & f.mMask) >> f.mShiftAmount);
                            break;

                        case 2:
                            val = mFile.ReadSingle();
                            break;

                        case 6:
                            int offs = mFile.ReadInt32();
                            mFile.Seek(stringTableOffs + offs);
                            val = mFile.ReadString();
                            break;

                        default:
                            throw new Exception($"BCSV::BCSV() - Unknown field type {f.mType}.");
                    }

                    e[f.mHash] = val;
                }

                mEntries.Add(e);
            }
        }

        public void Save()
        {
            int[] dataSizes = { 4, -1, 4, 4, 2, 1, 4 };
            int entrySize = 0;

            foreach (Field f in mFields.Values)
            {
                short end = Convert.ToInt16(f.mEntryOffset + dataSizes[f.mType]);
                if (end > entrySize)
                    entrySize = end;
            }

            entrySize = ((entrySize + 3) & ~3);

            int dataOffs = (0x10 + (0xC * mFields.Count));
            int strTableOffs = (dataOffs + (mEntries.Count * entrySize));
            int curStr = 0;

            mFile.SetLength(strTableOffs);
            mFile.Seek(0);
            mFile.Write(mEntries.Count);
            mFile.Write(mFields.Count);
            mFile.Write(dataOffs);
            mFile.Write(entrySize);

            foreach(Field f in mFields.Values)
            {
                mFile.Write(f.mHash);
                mFile.Write(f.mMask);
                mFile.Write(f.mEntryOffset);
                mFile.Write(f.mShiftAmount);
                mFile.Write(f.mType);
            }

            int i = 0;
            Dictionary<string, int> stringOffsets = new Dictionary<string, int>();

            foreach(Entry e in mEntries)
            {
                foreach(Field f in mFields.Values)
                {
                    int valOffs = (dataOffs + (i * entrySize) + f.mEntryOffset);
                    mFile.Seek(valOffs);

                    switch(f.mType)
                    {
                        case 0:
                        case 3:
                            {
                                int val = mFile.ReadInt32();
                                val &= ~f.mMask;
                                val |= (int)(((int)e[f.mHash] << f.mShiftAmount) & f.mMask);

                                mFile.Seek(valOffs);
                                mFile.Write(val);
                                break;
                            }

                        case 4:
                            {
                                short val = mFile.ReadInt16();
                                val &= (short)~f.mMask;
                                val |= (short)(((short)e[f.mHash] << f.mShiftAmount) & f.mMask);

                                mFile.Seek(valOffs);
                                mFile.Write(val);
                                break;
                            }

                        case 5:
                            {
                                byte val = mFile.ReadByte();
                                val &= (byte)~f.mMask;
                                val |= (byte)(((byte)e[f.mHash] << f.mShiftAmount) & f.mMask);

                                mFile.Seek(valOffs);
                                mFile.Write(val);
                                break;
                            }

                        case 2:
                            mFile.Write((float)e[f.mHash]);
                            break;

                        case 6:
                            {
                                string val = (string)e[f.mHash];
                                if (stringOffsets.ContainsKey(val))
                                    mFile.Write(stringOffsets[val]);
                                else
                                {
                                    stringOffsets.Add(val, curStr);
                                    mFile.Write(curStr);
                                    mFile.Seek(strTableOffs + curStr);
                                    curStr += mFile.WriteStringNT(val);
                                }

                                break;
                            }
                    }
                }

                i++;
            }

            i = mFile.GetLength();
            mFile.Seek(i);
            int align = (i + 0x1F) & ~0x1F;

            for (; i < align; i++)
            {
                mFile.Write((byte)0x40);
            }

            mFile.Save();
        }

        public void Close()
        {
            mFile.Close();
        }

        public bool ContainsField(string fieldName)
        {
            foreach(Entry e in mEntries)
            {
                if (e.ContainsKey(fieldName))
                {
                    return true;
                }
            }

            return false;
        }

        public Field AddField(string fieldName, int type, object defaultVal)
        {
            return AddField(fieldName, -1, type, -1, 0, defaultVal);
        }

        public Field AddField(string name, int offs, int type, int mask, int shift, object val)
        {
            if (mFields.ContainsKey(FieldNameToHash(name)))
            {
                return null;
            }

            int[] sizes = { 4, -1, 4, 4, 2, 1, 4 };
            AddHash(name);

            if (type == 2 || type == 6)
            {
                mask = -1;
                shift = 0;
            }

            if (offs == -1)
            {
                foreach(Field fe in mFields.Values)
                {
                    short end = Convert.ToInt16(fe.mEntryOffset + sizes[fe.mType]);
                    if (end > offs)
                        offs = end;
                }
            }

            Field f = new Field();
            f.mName = name;
            f.mHash = FieldNameToHash(name);
            f.mMask = mask;
            f.mShiftAmount = (byte)shift;
            f.mType = (byte)type;
            f.mEntryOffset = (ushort)offs;
            mFields.Add(f.mHash, f);

            foreach(Entry e in mEntries)
            {
                e.Add(name, val);
                e.Set(name, val);
            }

            return f;
        }

        public void RemoveField(string name)
        {
            int hash = FieldNameToHash(name);

            if (!mFields.ContainsKey(hash))
                return;

            mFields.Remove(hash);

            foreach (Entry e in mEntries)
            {
                e.Remove(hash);
            }
        }

        public class Field
        {
            public int mHash;
            public int mMask;
            public ushort mEntryOffset;
            public byte mShiftAmount;
            public byte mType;

            public string mName;
        }

        public class Entry : Dictionary<int, object>
        {
            public Entry() : base() { }

            public object Get(string key)
            {
                if (!ContainsKey(key))
                    throw new Exception($"BCSV::Entry::Get() - Key {key} not found.");

                return this[FieldNameToHash(key)];
            }

            public T Get<T>(string key)
            {
                if (!ContainsKey(key))
                    throw new Exception($"BCSV::Entry::Get<T>() - Key {key} not found.");

                return (T)this[FieldNameToHash(key)];
            }

            public void Set(string key, object val)
            {
                if (!ContainsKey(key))
                    throw new Exception($"BCSV::Entry::Set() - Key {key} not found.");

                this[FieldNameToHash(key)] = val;
            }

            public void Add(string key, object val)
            {
                Add(BCSV.FieldNameToHash(key), val);
            }

            public bool ContainsKey(string key)
            {
                return this.ContainsKey(FieldNameToHash(key));
            }

            public object GetTypeOfField(string fieldName)
            {
                return this[FieldNameToHash(fieldName)].GetType();
            }
        }

        public static int FieldNameToHash(string name)
        {
            int ret = 0;

            foreach (char c in name.ToCharArray())
            {
                ret *= 0x1F;
                ret += c;
            }

            return ret;
        }

        public static string HashToFieldName(int hash)
        {
            if (!sHashTable.ContainsKey(hash))
                return String.Format($"[{hash.ToString("X").ToUpper()}]");

            return sHashTable[hash];
        }

        public static void AddHash(string field)
        {
            int hash = FieldNameToHash(field);

            if (!sHashTable.ContainsKey(hash))
                sHashTable.Add(hash, field);
        }

        public static void PopulateHashTable()
        {
            sHashTable = new Dictionary<int, string>();

            if (!File.Exists("res/FieldNames.txt"))
                throw new Exception("BCSV::PopulateHashTable() - res/FieldNames.txt not found.");

            string[] lines = File.ReadAllLines("res/FieldNames.txt");

            foreach(string line in lines)
            {
                AddHash(line);
            }
        }

        public static void PopulateFieldTypeTable()
        {
            sFieldTypeTable = new Dictionary<string, string>();

            if (!File.Exists("res/FieldTypes.txt"))
                throw new Exception("BCSV::PopulateFieldTypeTable() - res/FieldTypes.txt not found.");

            string[] lines = File.ReadAllLines("res/FieldTypes.txt");

            foreach (string line in lines)
            {
                string[] spl = line.Split('=');
                sFieldTypeTable.Add(spl[0], spl[1]);
            }
        }

        private FileBase mFile;
        public Dictionary<int, Field> mFields;
        public List<Entry> mEntries;


        public static Dictionary<int, string> sHashTable;
        public static Dictionary<string, string> sFieldTypeTable;
    }
}
