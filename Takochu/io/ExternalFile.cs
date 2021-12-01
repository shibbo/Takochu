using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Policy;
using System.Runtime.InteropServices;

namespace Takochu.io
{
    public class ExternalFile : FileBase
    {
        public ExternalFile(string path)
        {
            //if (mFile != null) mFile.Close();


            try
            {
                
                mFile = new FileStream(path, FileMode.Open);
            }
            catch 
            {
                System.Windows.Forms.MessageBox.Show("別のプログラムでこのフォルダが開かれています。\n\rまたは、このプログラムによって一度開かれているフォルダです","Error",System.Windows.Forms.MessageBoxButtons.OK);
                throw new Exception("エラーです");
            }
            
            mFile.Position = 0;
            mEncoding = Encoding.GetEncoding("shift-jis");
        }

        public override void Save()
        {
            mFile.Flush();
        }

        public override void Close()
        {
            mFile.Close();
        }

        public override void SetBigEndian(bool isBig)
        {
            mIsBigEndian = isBig;
        }
        public override short ReadInt16()
        {
            byte[] arr = new byte[2];
            mFile.Read(arr, 0, 2);

            if (mIsBigEndian)
                Array.Reverse(arr);

            return BitConverter.ToInt16(arr, 0);
        }

        public override int ReadInt32()
        {
            byte[] arr = new byte[4];
            mFile.Read(arr, 0, 4);

            if (mIsBigEndian)
                Array.Reverse(arr);

            return BitConverter.ToInt32(arr, 0);
        }

        public override ushort ReadUInt16()
        {
            byte[] arr = new byte[2];
            mFile.Read(arr, 0, 2);

            if (mIsBigEndian)
                Array.Reverse(arr);

            return BitConverter.ToUInt16(arr, 0);
        }

        public override uint ReadUInt32()
        {
            byte[] arr = new byte[4];
            mFile.Read(arr, 0, 4);

            if (mIsBigEndian)
                Array.Reverse(arr);

            return BitConverter.ToUInt32(arr, 0);
        }

        public override byte[] ReadBytes(int num)
        {
            byte[] bytes = new byte[num];
            mFile.Read(bytes, 0, num);
            return bytes;
        }

        public override string ReadString()
        {
            List<byte> bytes = new List<byte>();

            byte curByte;
            while ((curByte = (byte)mFile.ReadByte()) != 0)
                bytes.Add(curByte);

            return mEncoding.GetString(bytes.ToArray());
        }

        public override uint ReadUInt32At(int loc)
        {
            byte[] arr = new byte[4];
            mFile.Read(arr, loc, 4);

            if (mIsBigEndian)
                Array.Reverse(arr);

            return BitConverter.ToUInt32(arr, 0);
        }

        public override string ReadStringAt(int loc)
        {
            byte[] arr = new byte[1];
            // string length
            mFile.Read(arr, loc, 1);
            byte[] str_arr = new byte[arr[0]];
            mFile.Read(str_arr, loc + 1, arr[0]);
            return Encoding.ASCII.GetString(str_arr);
        }

        public override string ReadString(int len)
        {
            return mEncoding.GetString(ReadBytes(len));
        }

        public override void Write(byte val)
        {
            mFile.WriteByte(val);
        }

        public override void Write(char val)
        {
            mFile.WriteByte((byte)val);
        }

        public override void Write(short val)
        {
            byte[] arr = BitConverter.GetBytes(val);

            if (mIsBigEndian)
                Array.Reverse(arr);

            mFile.Write(arr, 0, 2);
        }

        public override void Write(int val)
        {
            byte[] arr = BitConverter.GetBytes(val);

            if (mIsBigEndian)
                Array.Reverse(arr);

            mFile.Write(arr, 0, 4);
        }

        public override void Write(ushort val)
        {
            byte[] arr = BitConverter.GetBytes(val);

            if (mIsBigEndian)
                Array.Reverse(arr);

            mFile.Write(arr, 0, 2);
        }

        public override void Write(uint val)
        {
            byte[] arr = BitConverter.GetBytes(val);

            if (mIsBigEndian)
                Array.Reverse(arr);

            mFile.Write(arr, 0, 4);
        }

        public override void Write(float val)
        {
            byte[] arr = BitConverter.GetBytes(val);

            if (mIsBigEndian)
                Array.Reverse(arr);

            mFile.Write(arr, 0, 4);
        }

        public override void Write(byte[] val)
        {
            mFile.Write(val, 0, val.Length);
        }

        public override void WritePadding(byte padVal, int howMany)
        {
            for (int i = 0; i < howMany; i++)
                Write(padVal);
        }

        public override int WriteString(string val)
        {
            byte[] arr = mEncoding.GetBytes(val);
            mFile.Write(arr, 0, arr.Length);
            return arr.Length;
        }

        public override int WriteStringNT(string val)
        {
            byte[] arr = mEncoding.GetBytes(val);
            mFile.Write(arr, 0, arr.Length);
            mFile.WriteByte((byte)'\0');
            return arr.Length + 1;
        }

        public override byte[] GetBuffer()
        {
            byte[] ret = new byte[mFile.Length];
            long oldPos = mFile.Position;
            mFile.Seek(0, SeekOrigin.Begin);
            mFile.Read(ret, 0, (int)mFile.Length);
            mFile.Seek(oldPos, SeekOrigin.Begin);
            return ret;
        }

        public override void SetBuffer(byte[] buffer)
        {
            long oldpos = mFile.Position;
            mFile.SetLength(buffer.Length);
            mFile.Seek(0, SeekOrigin.Begin);
            mFile.Write(buffer, 0, buffer.Length);
            mFile.Seek(oldpos, SeekOrigin.Begin);
        }

        protected Stream mFile;
        private bool mIsBigEndian = true;
        private Encoding mEncoding;
    }
}
