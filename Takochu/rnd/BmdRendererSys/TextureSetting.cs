using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;
using Takochu.fmt;

namespace Takochu.rnd.BmdRendererSys
{
    public class TextureSetting
    {
        private readonly TextureWrapMode[] WrapModes = {
            TextureWrapMode.ClampToEdge,
            TextureWrapMode.Repeat,
            TextureWrapMode.MirroredRepeat
        };
        private readonly TextureMinFilter[] MinFilters = {
            TextureMinFilter.Nearest,
            TextureMinFilter.Linear,
            TextureMinFilter.NearestMipmapNearest,
            TextureMinFilter.LinearMipmapNearest,
            TextureMinFilter.NearestMipmapLinear,
            TextureMinFilter.LinearMipmapLinear
        };
        private readonly TextureMagFilter[] MagFilters = {
            TextureMagFilter.Nearest,
            TextureMagFilter.Linear,
            TextureMagFilter.Nearest,
            TextureMagFilter.Linear,
            TextureMagFilter.Nearest,
            TextureMagFilter.Linear
        };

        private PixelInternalFormat pixelInternalFormat;
        private PixelFormat pixelFormat;
        private BMD.Texture bmdTexture;
        private int textureID;

        public TextureSetting(BMD.Texture[] bmdTextures, int id, int texid)
        {
            bmdTexture = bmdTextures[id];
            textureID = texid;
        }

        //フィルター方法、ラップ方法
        public void UploadTexture()
        {
            GL.BindTexture(TextureTarget.Texture2D, textureID);
            {
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMaxLevel, bmdTexture.MipmapCount - 1);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)MinFilters[bmdTexture.MinFilter]);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)MagFilters[bmdTexture.MagFilter]);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)WrapModes[bmdTexture.WrapS]);
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)WrapModes[bmdTexture.WrapT]);
            }

            SetPixelFormat(bmdTexture.Format);
            SetMipMap(bmdTexture.Width, bmdTexture.Height);
        }

        
        private void SetMipMap(int width, int height)
        {
            for (int MipMap = 0; MipMap < bmdTexture.MipmapCount; MipMap++)
            {
                GL.TexImage2D(
                    TextureTarget.Texture2D,
                    MipMap,
                    pixelInternalFormat,
                    width,
                    height,
                    0,
                    pixelFormat,
                    PixelType.UnsignedByte,
                    bmdTexture.Image[MipMap]
                    );
                width /= 2;
                height /= 2;
            }
        }

        private void SetPixelFormat(byte texfmt)
        {
            switch (texfmt)
            {
                case 0:
                case 1:
                    pixelInternalFormat = PixelInternalFormat.Intensity;
                    pixelFormat = PixelFormat.Luminance;
                    break;
                case 2:
                case 3:
                    pixelInternalFormat = PixelInternalFormat.Luminance8Alpha8;
                    pixelFormat = PixelFormat.LuminanceAlpha;
                    break;
                default:
                    pixelInternalFormat = PixelInternalFormat.Four;
                    pixelFormat = PixelFormat.Bgra;
                    break;
            }
        }
    }
}
