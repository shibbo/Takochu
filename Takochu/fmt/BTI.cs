using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;
using Takochu.util;

namespace Takochu.fmt
{
    class BTI
    {
        public BTI(FileBase file)
        {
            byte fmt = file.ReadByte();
            file.Skip(0x1);
            mWidth = file.ReadUInt16();
            mHeight = file.ReadUInt16();

            if (fmt != 0xE)
                return;

            file.Seek(0x20);
            mDecodedImage = ImageUtil.DecodeCMPR(file, mWidth, mHeight);
        }

        public Bitmap GetImg()
        {
            Bitmap output = new Bitmap(mWidth, mHeight);
            Rectangle rect = new Rectangle(0, 0, output.Width, output.Height);
            BitmapData bmpData = output.LockBits(rect, ImageLockMode.ReadWrite, output.PixelFormat);
            IntPtr ptr = bmpData.Scan0;
            System.Runtime.InteropServices.Marshal.Copy(mDecodedImage, 0, ptr, mDecodedImage.Length);
            output.UnlockBits(bmpData);

            return output;
        }

        byte[] mDecodedImage;
        ushort mWidth;
        ushort mHeight;
    }
}
