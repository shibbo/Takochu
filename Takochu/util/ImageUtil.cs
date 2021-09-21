using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;

namespace Takochu.util
{
    class ImageUtil
    {
        public static byte[] DecodeI4(ref FileBase file, int height, int width)
        {
            byte[] image = new byte[width * height * 4];

            for (int blockY = 0; blockY < height; blockY += 8)
            {
                for (int blockX = 0; blockX < width; blockX += 8)
                {
                    for (int y = blockY; y < blockY + 8; y++)
                    {
                        for (int x = blockX; x < (blockX + 8); x += 2)
                        {
                            byte val = file.ReadByte();
                            int index = ((y * width) + x) * 4;

                            if (x < width && y < height)
                            {
                                byte component = (byte)(((val >> 4) & 0xF) * 0x11);

                                image[index] = component;
                                image[index + 1] = component;
                                image[index + 2] = component;
                                image[index + 3] = 0xFF;
                            }
                        }
                    }
                }
            }

            return image;
        }

        public static byte[] DecodeI8(ref FileBase file, int height, int width)
        {
            byte[] image = new byte[width * height * 4];

            for (int blockY = 0; blockY < height; blockY += 4)
            {
                for (int blockX = 0; blockX < width; blockX += 8)
                {
                    for (int y = blockY; y < blockY + 4; y++)
                    {
                        for (int x = blockX; x < (blockX + 8); x++)
                        {
                            byte component = file.ReadByte();

                            int index = ((y * width) + x) * 4;

                            if (x < width && y < height)
                            {
                                image[index] = component;
                                image[index + 1] = component;
                                image[index + 2] = component;
                                image[index + 3] = 0xFF;
                            }
                        }
                    }
                }
            }

            return image;
        }

        public static byte[] DecodeIA4(ref FileBase file, int height, int width)
        {
            byte[] image = new byte[width * height * 4];

            for (int blockY = 0; blockY < height; blockY += 4)
            {
                for (int blockX = 0; blockX < width; blockX += 8)
                {
                    for (int y = blockY; y < blockY + 4; y++)
                    {
                        for (int x = blockX; x < blockX + 8; x++)
                        {
                            byte val = file.ReadByte();

                            byte component = (byte)((val & 0xF) * 0x11);
                            byte alpha = (byte)(((val >> 4) & 0xF) * 0x11);

                            int index = ((y * width) + x) * 4;

                            if (x < width && y < height)
                            {
                                image[index] = component;
                                image[index + 1] = component;
                                image[index + 2] = component;
                                image[index + 3] = alpha;
                            }
                        }
                    }
                }
            }

            return image;
        }

        public static byte[] DecodeIA8(ref FileBase file, int height, int width)
        {
            byte[] image = new byte[width * height * 4];

            for (int blockY = 0; blockY < height; blockY += 4)
            {
                for (int blockX = 0; blockX < width; blockX += 4)
                {
                    for (int y = blockY; y < blockY + 4; y++)
                    {
                        for (int x = blockX; x < blockX + 4; x++)
                        {
                            ushort val = file.ReadUInt16();

                            byte alpha = (byte)(val & 0xFF);
                            byte component = (byte)(val >> 8);

                            int index = ((y * width) + x) * 4;

                            if (x < width && y < height)
                            {
                                image[index] = component;
                                image[index + 1] = component;
                                image[index + 2] = component;
                                image[index + 3] = alpha;
                            }
                        }
                    }
                }
            }

            return image;
        }

        public static byte[] DecodeRGB565(ref FileBase file, int height, int width)
        {
            byte[] image = new byte[width * height * 4];

            for (int blockY = 0; blockY < height; blockY += 4)
            {
                for (int blockX = 0; blockX < width; blockX += 4)
                {
                    for (int y = blockY; y < blockY + 4; y++)
                    {
                        for (int x = blockX; x < blockX + 4; x++)
                        {
                            ushort val = file.ReadUInt16();

                            byte r = (byte)(((val >> 11) & 0x1F) * 0x8);
                            byte g = (byte)(((val >> 5) & 0x3F) * 0x4);
                            byte b = (byte)((val & 0x1F) * 0x8);

                            int index = ((y * width) + x) * 4;

                            if (x < width && y < height)
                            {
                                image[index] = r;
                                image[index + 1] = g;
                                image[index + 2] = b;
                                image[index + 3] = 0xFF;
                            }
                        }
                    }
                }
            }

            return image;
        }

        public static byte[] DecodeRGB5A3(ref FileBase file, int height, int width)
        {
            byte[] image = new byte[width * height * 4];

            for (int blockY = 0; blockY < height; blockY += 4)
            {
                for (int blockX = 0; blockX < width; blockX += 4)
                {
                    for (int y = blockY; y < blockY + 4; y++)
                    {
                        for (int x = blockX; x < blockX + 4; x++)
                        {
                            byte r, g, b, a;

                            ushort val = file.ReadUInt16();

                            int index = ((y * width) + x) * 4;

                            if (x < width && y < height)
                            {
                                bool hasAlpha = ((val >> 15) & 0x1) == 0;

                                if (hasAlpha)
                                {
                                    r = (byte)(((val >> 8) & 0xF) * 0x11);
                                    g = (byte)(((val >> 4) & 0xF) * 0x11);
                                    b = (byte)((val & 0xF) * 0x11);
                                    a = (byte)(((val >> 12) & 0x7) * 0x20);
                                }
                                else
                                {
                                    r = (byte)(((val >> 10) & 0x1F) * 0x8);
                                    g = (byte)(((val >> 5) & 0x1F) * 0x8);
                                    b = (byte)((val & 0x1F) * 0x8);
                                    a = 0xFF;
                                }

                                image[index] = b;
                                image[index + 1] = g;
                                image[index + 2] = r;
                                image[index + 3] = a;
                            }
                        }
                    }
                }
            }

            return image;
        }

        /* big thanks to Switch Toolbox for CMPR / RGBA32 decoding, slightly modified to work with FileBase */

        public static byte[] DecodeRGBA32(ref FileBase file, uint width, uint height)
        {
            uint numWidthBlocks = width / 4;
            uint numHeightBlocks = height / 4;

            byte[] dest = new byte[width * height * 4];

            for (int yBlock = 0; yBlock < numHeightBlocks; yBlock++)
            {
                for (int xBlock = 0; xBlock < numWidthBlocks; xBlock++)
                {
                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 4; pX++)
                        {
                            if ((xBlock * 4 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;

                            uint destIndex = (uint)(4 * (width * ((yBlock * 4) + pY) + (xBlock * 4) + pX));
                            dest[destIndex + 3] = file.ReadByte(); //Alpha
                            dest[destIndex + 2] = file.ReadByte(); //Red
                        }
                    }

                    for (int pY = 0; pY < 4; pY++)
                    {
                        for (int pX = 0; pX < 4; pX++)
                        {
                            if ((xBlock * 4 + pX >= width) || (yBlock * 4 + pY >= height))
                                continue;
                            uint destIndex = (uint)(4 * (width * ((yBlock * 4) + pY) + (xBlock * 4) + pX));
                            dest[destIndex + 1] = file.ReadByte();
                            dest[destIndex + 0] = file.ReadByte();
                        }
                    }

                }
            }

            return dest;
        }

        public static byte[] DecodeCMPR(FileBase file, uint width, uint height)
        {
            //Decode S3TC1
            byte[] buffer = new byte[width * height * 4];

            for (int y = 0; y < height / 4; y += 2)
            {
                for (int x = 0; x < width / 4; x += 2)
                {
                    for (int dy = 0; dy < 2; ++dy)
                    {
                        for (int dx = 0; dx < 2; ++dx)
                        {
                            if (4 * (x + dx) < width && 4 * (y + dy) < height)
                            {
                                byte[] fileData = file.ReadBytes(8);
                                Buffer.BlockCopy(fileData, 0, buffer, (int)(8 * ((y + dy) * width / 4 + x + dx)), 8);
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < width * height / 2; i += 8)
            {
                // Micro swap routine needed
                Swap(ref buffer[i], ref buffer[i + 1]);
                Swap(ref buffer[i + 2], ref buffer[i + 3]);

                buffer[i + 4] = S3TC1ReverseByte(buffer[i + 4]);
                buffer[i + 5] = S3TC1ReverseByte(buffer[i + 5]);
                buffer[i + 6] = S3TC1ReverseByte(buffer[i + 6]);
                buffer[i + 7] = S3TC1ReverseByte(buffer[i + 7]);
            }

            //Now decompress the DXT1 data within it.
            return DecompressDxt1(buffer, width, height);
        }
        private static void Swap(ref byte b1, ref byte b2)
        {
            byte tmp = b1; b1 = b2; b2 = tmp;
        }

        private static ushort Read16Swap(byte[] data, uint offset)
        {
            return (ushort)((Buffer.GetByte(data, (int)offset + 1) << 8) | Buffer.GetByte(data, (int)offset));
        }

        private static uint Read32Swap(byte[] data, uint offset)
        {
            return (uint)((Buffer.GetByte(data, (int)offset + 3) << 24) | (Buffer.GetByte(data, (int)offset + 2) << 16) | (Buffer.GetByte(data, (int)offset + 1) << 8) | Buffer.GetByte(data, (int)offset));
        }

        private static byte S3TC1ReverseByte(byte b)
        {
            byte b1 = (byte)(b & 0x3);
            byte b2 = (byte)(b & 0xC);
            byte b3 = (byte)(b & 0x30);
            byte b4 = (byte)(b & 0xC0);

            return (byte)((b1 << 6) | (b2 << 2) | (b3 >> 2) | (b4 >> 6));
        }

        private static byte[] DecompressDxt1(byte[] src, uint width, uint height)
        {
            uint dataOffset = 0;
            byte[] finalData = new byte[width * height * 4];

            for (int y = 0; y < height; y += 4)
            {
                for (int x = 0; x < width; x += 4)
                {
                    // Haha this is in little-endian (DXT1) so we have to swap the already swapped bytes.
                    ushort color1 = Read16Swap(src, dataOffset);
                    ushort color2 = Read16Swap(src, dataOffset + 2);
                    uint bits = Read32Swap(src, dataOffset + 4);
                    dataOffset += 8;

                    byte[][] ColorTable = new byte[4][];
                    for (int i = 0; i < 4; i++)
                        ColorTable[i] = new byte[4];

                    RGB565ToRGBA8(color1, ref ColorTable[0], 0);
                    RGB565ToRGBA8(color2, ref ColorTable[1], 0);

                    if (color1 > color2)
                    {
                        ColorTable[2][0] = (byte)((2 * ColorTable[0][0] + ColorTable[1][0] + 1) / 3);
                        ColorTable[2][1] = (byte)((2 * ColorTable[0][1] + ColorTable[1][1] + 1) / 3);
                        ColorTable[2][2] = (byte)((2 * ColorTable[0][2] + ColorTable[1][2] + 1) / 3);
                        ColorTable[2][3] = 0xFF;

                        ColorTable[3][0] = (byte)((ColorTable[0][0] + 2 * ColorTable[1][0] + 1) / 3);
                        ColorTable[3][1] = (byte)((ColorTable[0][1] + 2 * ColorTable[1][1] + 1) / 3);
                        ColorTable[3][2] = (byte)((ColorTable[0][2] + 2 * ColorTable[1][2] + 1) / 3);
                        ColorTable[3][3] = 0xFF;
                    }
                    else
                    {
                        ColorTable[2][0] = (byte)((ColorTable[0][0] + ColorTable[1][0] + 1) / 2);
                        ColorTable[2][1] = (byte)((ColorTable[0][1] + ColorTable[1][1] + 1) / 2);
                        ColorTable[2][2] = (byte)((ColorTable[0][2] + ColorTable[1][2] + 1) / 2);
                        ColorTable[2][3] = 0xFF;

                        ColorTable[3][0] = (byte)((ColorTable[0][0] + 2 * ColorTable[1][0] + 1) / 3);
                        ColorTable[3][1] = (byte)((ColorTable[0][1] + 2 * ColorTable[1][1] + 1) / 3);
                        ColorTable[3][2] = (byte)((ColorTable[0][2] + 2 * ColorTable[1][2] + 1) / 3);
                        ColorTable[3][3] = 0x00;
                    }

                    for (int iy = 0; iy < 4; ++iy)
                    {
                        for (int ix = 0; ix < 4; ++ix)
                        {
                            if (((x + ix) < width) && ((y + iy) < height))
                            {
                                int di = (int)(4 * ((y + iy) * width + x + ix));
                                int si = (int)(bits & 0x3);
                                finalData[di + 0] = ColorTable[si][0];
                                finalData[di + 1] = ColorTable[si][1];
                                finalData[di + 2] = ColorTable[si][2];
                                finalData[di + 3] = ColorTable[si][3];
                            }
                            bits >>= 2;
                        }
                    }
                }
            }

            return finalData;
        }

        private static void RGB565ToRGBA8(ushort sourcePixel, ref byte[] dest, int destOffset)
        {
            //This repo fixes some decoding bugs SuperBMD had
            //https://github.com/RenolY2/SuperBMD/tree/master/SuperBMDLib/source

            byte r, g, b;
            r = (byte)((sourcePixel & 0xF100) >> 11);
            g = (byte)((sourcePixel & 0x7E0) >> 5);
            b = (byte)((sourcePixel & 0x1F));

            r = (byte)((r << (8 - 5)) | (r >> (10 - 8)));
            g = (byte)((g << (8 - 6)) | (g >> (12 - 8)));
            b = (byte)((b << (8 - 5)) | (b >> (10 - 8)));

            dest[destOffset] = b;
            dest[destOffset + 1] = g;
            dest[destOffset + 2] = r;
            dest[destOffset + 3] = 0xFF; //Set alpha to 1
        }
    }
}
