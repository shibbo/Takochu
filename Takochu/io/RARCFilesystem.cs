using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takochu.io
{
    public class RARCFilesystem : FilesystemBase
    {
        public RARCFilesystem(FileBase file)
        {

            mFile = new Yaz0File(file);

            if (mFile.ReadString(4) != "RARC")
                throw new Exception("RARCFilesystem::RARCFilesystem() - File is not a RARC archive.");

            mFile.Seek(0xC);
            int fileDataOffset = mFile.ReadInt32() + 0x20;
            mFile.Seek(0x20);
            int dirNodeCount = mFile.ReadInt32();
            int dirNodeOffset = mFile.ReadInt32() + 0x20;
            int fileEntryCount = mFile.ReadInt32();
            int fileEntryOffset = mFile.ReadInt32() + 0x20;
            mFile.Skip(0x4);
            int stringTableOffset = mFile.ReadInt32() + 0x20;
            unkVal = mFile.ReadInt32();

            mDirectoryEntries = new Dictionary<string, DirectoryEntry>();
            mFileEntries = new Dictionary<string, FileEntry>();

            DirectoryEntry rootEntry = new DirectoryEntry();
            rootEntry.mParent = null;

            mFile.Seek(dirNodeOffset + 0x6);
            int rootNodeOffs = mFile.ReadInt16();
            mFile.Seek(stringTableOffset + rootNodeOffs);
            rootEntry.mName = mFile.ReadString();
            rootEntry.mFullName = "/" + rootEntry.mName;
            rootEntry.mTempID = 0;

            mDirectoryEntries.Add("/", rootEntry);

            for (int i = 0; i < dirNodeCount; i++)
            {
                DirectoryEntry parent = null;

                foreach (DirectoryEntry e in mDirectoryEntries.Values)
                {
                    if (e.mTempID == i)
                    {
                        parent = e;
                        break;
                    }
                }

                mFile.Seek(dirNodeOffset + (i * 0x10) + 0xA);

                short numEntries = mFile.ReadInt16();
                int firstEntryOffs = mFile.ReadInt32();

                for (int j = 0; j < numEntries; j++)
                {
                    int entryOffset = fileEntryOffset + ((j + firstEntryOffs) * 0x14);
                    mFile.Seek(entryOffset);
                    mFile.Skip(0x4);

                    int entryType = mFile.ReadInt16() & 0xFFFF;
                    int nameOffs = mFile.ReadInt16() & 0xFFFF;
                    int dataOffs = mFile.ReadInt32();
                    int dataSize = mFile.ReadInt32();

                    mFile.Seek(stringTableOffset + nameOffs);
                    string name = mFile.ReadString();

                    if (name == "." || name == "..")
                        continue;

                    string fullName = parent.mFullName + "/" + name;

                    if (entryType == 0x0200)
                    {
                        DirectoryEntry d = new DirectoryEntry
                        {
                            mParent = parent,
                            mName = name,
                            mFullName = fullName,
                            mTempID = dataOffs
                        };

                        mDirectoryEntries.Add(PathToKey(fullName), d);
                        parent.mChildren.Add(d);
                    }
                    else
                    {
                        FileEntry f = new FileEntry
                        {
                            mParent = parent,
                            mDataOffset = fileDataOffset + dataOffs,
                            mDataSize = dataSize,
                            mName = name,
                            mFullName = fullName,
                            mData = null
                        };

                        mFileEntries.Add(PathToKey(fullName), f);
                        parent.mChildrenFiles.Add(f);
                    }
                }
            }
        }

        private string PathToKey(string path)
        {
            string ret = path.ToLower();
            ret = ret.Substring(1);

            if (ret.IndexOf("/") == -1)
                return "/";

            return ret.Substring(ret.IndexOf("/"));
        }

        private int align32(int val)
        {
            return (val + 0x1F) & ~0x1F;
        }

        private int dirMagic(string name)
        {
            string upperName = name.ToUpper();

            int ret = 0;

            for (int i = 0; i < 4; i++)
            {
                ret <<= 8;
                if (i >= upperName.Length)
                    ret += 0x20;
                else
                    ret += upperName[i];
            }

            return ret;
        }

        private short NameHash(string name)
        {
            short ret = 0;

            foreach(char ch in name.ToCharArray())
            {
                ret *= 0x3;
                ret += (short)ch;
            }

            return ret;
        }

        public override void Save()
        {
            foreach(FileEntry fe in mFileEntries.Values)
            {
                if (fe.mData != null)
                    continue;

                mFile.Seek(fe.mDataOffset);
                fe.mData = mFile.ReadBytes(fe.mDataSize);
            }

            int dirOffs = 0x40;
            int fileOffs = dirOffs + align32(mDirectoryEntries.Count * 0x10);
            int strOffs = fileOffs + align32((mFileEntries.Count + (mDirectoryEntries.Count * 3) - 1) * 0x14);

            int dataOffs = strOffs;
            int dataLen = 0;

            foreach(DirectoryEntry de in mDirectoryEntries.Values)
            {
                dataOffs += de.mName.Length + 1;
            }

            foreach(FileEntry fe in mFileEntries.Values)
            {
                dataOffs += fe.mName.Length + 1;
                dataLen += align32(fe.mDataSize);
            }

            dataOffs += 5;
            dataOffs = align32(dataOffs);

            int dirSubOffs = 0;
            int fileSubOffs = 0;
            int strSubOffs = 0;
            int dataSubOffs = 0;

            mFile.SetLength(dataOffs + dataLen);

            mFile.Seek(0);
            mFile.Write(0x52415243);
            mFile.Write(dataOffs + dataLen);
            mFile.Write(0x00000020);
            mFile.Write(dataOffs - 0x20);
            mFile.Write(dataLen);
            mFile.Write(dataLen);
            mFile.Write(0x00000000);
            mFile.Write(0x00000000);
            mFile.Write(mDirectoryEntries.Count);
            mFile.Write(dirOffs - 0x20);
            mFile.Write(mFileEntries.Count + (mDirectoryEntries.Count * 3) - 1);
            mFile.Write(fileOffs - 0x20);
            mFile.Write(dataOffs - strOffs);
            mFile.Write(strOffs - 0x20);
            mFile.Write(unkVal);
            mFile.Write(0x00000000);

            mFile.Seek(strOffs);
            mFile.WriteStringNT(".");
            mFile.WriteStringNT("..");
            strSubOffs += 5;

            Stack<IEnumerator<DirectoryEntry>> dirStack = new Stack<IEnumerator<DirectoryEntry>>();
            object[] entriesArr = mDirectoryEntries.Values.ToArray();

            DirectoryEntry curDir = (DirectoryEntry)entriesArr[0];
            int c = 1;
            while (curDir.mParent != null)
                curDir = (DirectoryEntry)entriesArr[c++];

            short file_id = 0;

            for (; ;)
            {
                curDir.mTempID = dirSubOffs / 0x10;

                mFile.Seek(dirOffs + dirSubOffs);
                mFile.Write((curDir.mTempID == 0) ? 0x524F4F54 : dirMagic(curDir.mName));
                mFile.Write(strSubOffs);
                mFile.Write(NameHash(curDir.mName));
                mFile.Write((short)(2 + curDir.mChildren.Count + curDir.mChildrenFiles.Count));
                mFile.Write(fileSubOffs / 0x14);
                dirSubOffs += 0x10;

                if (curDir.mTempID > 0)
                {
                    mFile.Seek(curDir.mTempNameOffset);
                    mFile.Write((short)strSubOffs);
                    mFile.Write(curDir.mTempID);
                }

                mFile.Seek(strOffs + strSubOffs);
                mFile.WriteStringNT(curDir.mName);
                strSubOffs += curDir.mName.Length + 1;

                mFile.Seek(fileOffs + fileSubOffs);

                foreach(DirectoryEntry de in curDir.mChildren)
                {
                    mFile.Write((ushort)0xFFFF);
                    mFile.Write(NameHash(de.mName));
                    mFile.Write((short)0x0200);
                    de.mTempNameOffset = mFile.Position();
                    mFile.Skip(0x6);
                    mFile.Write(0x00000010);
                    mFile.Write(0x00000000);
                    fileSubOffs += 0x14;
                }

                foreach(FileEntry fe in curDir.mChildrenFiles)
                {
                    mFile.Seek(fileOffs + fileSubOffs);
                    mFile.Write(file_id);
                    mFile.Write(NameHash(fe.mName));
                    mFile.Write((short)0x1100);
                    mFile.Write((short)strSubOffs);
                    mFile.Write(dataSubOffs);
                    mFile.Write(fe.mDataSize);
                    mFile.Write(0x00000000);
                    fileSubOffs += 0x14;
                    file_id++;

                    mFile.Seek(strOffs + strSubOffs);
                    mFile.WriteStringNT(fe.mName);
                    strSubOffs += fe.mName.Length + 1;

                    mFile.Seek(dataOffs + dataSubOffs);
                    fe.mDataOffset = mFile.Position();
                    byte[] data = new byte[fe.mDataSize];
                    Array.Copy(fe.mData, data, fe.mDataSize);
                    mFile.Write(data);
                    dataSubOffs += align32(fe.mDataSize);
                    fe.mData = null;
                }

                mFile.Seek(fileOffs + fileSubOffs);
                mFile.Write((ushort)0xFFFF);
                mFile.Write((short)0x002E);
                mFile.Write((short)0x0200);
                mFile.Write((short)0x0000);
                mFile.Write(curDir.mTempID);
                mFile.Write(0x00000010);
                mFile.Write(0x00000000);
                mFile.Write((ushort)0xFFFF);
                mFile.Write((short)0x00B8);
                mFile.Write((short)0x0200);
                mFile.Write((short)0x0002);
                mFile.Write((curDir.mParent != null) ? curDir.mParent.mTempID : -1);
                mFile.Write(0x00000010);
                mFile.Write(0x00000000);
                fileSubOffs += 0x28;

                if (curDir.mChildren.Count != 0)
                {
                    dirStack.Push(curDir.mChildren.GetEnumerator());
                    dirStack.Peek().MoveNext();
                    curDir = dirStack.Peek().Current;
                }
                else
                {
                    curDir = null;
                    
                    while(curDir == null)
                    {
                        if (dirStack.Count == 0)
                            break;

                        IEnumerator<DirectoryEntry> it = dirStack.Peek();

                        if (it.MoveNext())
                        {
                            curDir = it.Current;
                        }
                        else
                        {
                            dirStack.Pop();
                        }
                    }

                    if (curDir == null)
                        break;
                }
            }

            mFile.Save();
        }

        public override void Close()
        {
            mFile.Close();
        }

        public override bool DoesDirectoryExist(string dir)
        {
            return mDirectoryEntries.ContainsKey(PathToKey(dir));
        }

        public override List<string> GetDirectories(string directory)
        {
            if (!mDirectoryEntries.ContainsKey(PathToKey(directory)))
                return null;

            DirectoryEntry d = mDirectoryEntries[PathToKey(directory)];

            List<string> ret = new List<string>();

            foreach (DirectoryEntry e in d.mChildren)
            {
                ret.Add(e.mName);
            }

            return ret;
        }

        public override bool DoesFileExist(string dir)
        {
            return mFileEntries.ContainsKey(PathToKey(dir));
        }

        public override List<string> GetFiles(string directory)
        {
            if (!mDirectoryEntries.ContainsKey(PathToKey(directory)))
                return null;

            DirectoryEntry d = mDirectoryEntries[PathToKey(directory)];

            List<string> ret = new List<string>();

            foreach (FileEntry e in d.mChildrenFiles)
            {
                ret.Add(e.mName);
            }

            return ret;
        }

        public override List<string> GetFilesWithExt(string directory, string ext)
        {
            if (!mDirectoryEntries.ContainsKey(PathToKey(directory)))
                return null;

            DirectoryEntry d = mDirectoryEntries[PathToKey(directory)];

            List<string> ret = new List<string>();

            foreach (FileEntry e in d.mChildrenFiles)
            {
                if (e.mName.EndsWith(ext))
                    ret.Add(e.mName);
            }

            return ret;
        }

        public override FileBase OpenFile(string file)
        {
            if (!mFileEntries.ContainsKey(PathToKey(file)))
                throw new Exception($"RARCFilesystem::OpenFile() - File {file} not found in RARC.");

            return new RARCFile(this, file);
        }

        public override void CreateFile(string parent, string file)
        {
            string parentKey = PathToKey(parent);
            string fileKey = PathToKey($"{parent}/{file}");

            if (!mDirectoryEntries.ContainsKey(parentKey))
                return;

            if (mFileEntries.ContainsKey(fileKey))
                return;

            if (mDirectoryEntries.ContainsKey(fileKey))
                return;

            DirectoryEntry d = mDirectoryEntries[parentKey];
            FileEntry fe = new FileEntry();
            fe.mData = new byte[0];
            fe.mDataSize = fe.mData.Length;
            fe.mFullName = $"{parent}/{file}";
            fe.mParent = d;

            d.mChildrenFiles.Add(fe);
            mFileEntries.Add(PathToKey(fe.mFullName), fe);
        }

        public override void DeleteFile(string file)
        {
            file = PathToKey(file);
            if (!mFileEntries.ContainsKey(file))
                return;

            FileEntry f = mFileEntries[file];
            DirectoryEntry p = f.mParent;

            p.mChildrenFiles.Remove(f);
            mFileEntries.Remove(file);
        }

        public byte[] GetContents(string name)
        {
            FileEntry f = mFileEntries[PathToKey(name)];

            if (f.mData != null)
            {
                byte[] dest = null;
                Array.Copy(f.mData, dest, f.mDataSize);
                return dest;
            }

            mFile.Seek(f.mDataOffset);
            return mFile.ReadBytes(f.mDataSize);
        }

        public override void ReplaceFileContents(string file, byte[] contents)
        {
            FileEntry f = mFileEntries[file];
            f.mData = contents;
            f.mDataSize = contents.Length;
        }

        public void ReinsertFile(RARCFile file)
        {
            FileEntry f = mFileEntries[PathToKey(file.mFileName)];
            f.mData = file.GetBuffer();
            f.mDataSize = file.GetLength();
        }

        private class FileEntry
        {
            public FileEntry()
            {
                mData = null;
            }

            public int mDataOffset;
            public int mDataSize;
            public DirectoryEntry mParent;
            public string mName;
            public string mFullName;

            public byte[] mData;
        }

        private class DirectoryEntry
        {
            public DirectoryEntry()
            {
                mChildren = new List<DirectoryEntry>();
                mChildrenFiles = new List<FileEntry>();
            }

            public DirectoryEntry mParent;
            public List<DirectoryEntry> mChildren;
            public List<FileEntry> mChildrenFiles;

            public string mName;
            public string mFullName;
            public int mTempID;
            public int mTempNameOffset;
        }

        private FileBase mFile;
        private Dictionary<string, FileEntry> mFileEntries;
        private Dictionary<string, DirectoryEntry> mDirectoryEntries;
        private int unkVal;
    }
}
