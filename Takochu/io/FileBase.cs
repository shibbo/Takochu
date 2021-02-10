using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.CompilerServices;
using System.Net;
using System.Security.Policy;

namespace Takochu.io
{
    public class FileBase
    {
        public virtual void Save() { }
        public virtual void Close() { }
        public virtual void Release() { }
        public virtual void SetBigEndian(bool isBig) { }
        public virtual int GetLength() { return 0; }
        public virtual void SetLength(int len) { }
        public virtual int Position() { return 0; }
        public virtual void Seek(int where) { }
        public virtual void Skip(int numBytes) { }
        public virtual char ReadChar() { return (char)0; }
        public virtual byte ReadByte() { return 0; }
        public virtual short ReadInt16() { return 0; }
        public virtual int ReadInt32() { return 0; }
        public virtual ushort ReadUInt16() { return 0; }
        public virtual uint ReadUInt32() { return 0; }
        public virtual uint ReadUInt32At(int loc) { return 0; }
        public virtual byte[] ReadBytes(int num) { return null; }
        public virtual float ReadSingle() { return 0.0f; }
        public virtual string ReadStringLenPrefix() { return ""; }
        public virtual string ReadString() { return ""; }
        public virtual string ReadString(int len) { return ""; }
        public virtual string ReadStringAt(int loc) { return ""; }
        public virtual string ReadStringUTF16() { return ""; }
        public virtual void Write(byte val) { }
        public virtual void Write(char val) { }
        public virtual void Write(short val) { }
        public virtual void Write(int val) { }
        public virtual void Write(ushort val) { }
        public virtual void Write(uint val) { }
        public virtual void Write(float val) { }
        public virtual void Write(double val) { }
        public virtual void Write(byte[] val) { }
        public virtual void Write(char[] val) { }
        public virtual void WritePadding(byte padVal, int howMany) { }
        public virtual int WriteString(string val) { return 0; }
        public virtual int WriteStringNT(string val) { return 0; }

        public virtual byte[] GetBuffer() { return null; }
        public virtual void SetBuffer(byte[] buffer) { }
    }
}
