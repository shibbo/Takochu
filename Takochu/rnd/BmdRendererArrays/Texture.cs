using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Takochu.rnd.BmdRendererArrays
{
    public class Texture
    {
        public static readonly TextureWrapMode[] WrapModes = {
            TextureWrapMode.ClampToEdge,
            TextureWrapMode.Repeat,
            TextureWrapMode.MirroredRepeat
        };
        public static readonly TextureMinFilter[] MinFilters = {
            TextureMinFilter.Nearest,
            TextureMinFilter.Linear,
            TextureMinFilter.NearestMipmapNearest,
            TextureMinFilter.LinearMipmapNearest,
            TextureMinFilter.NearestMipmapLinear,
            TextureMinFilter.LinearMipmapLinear
        };
        public static readonly TextureMagFilter[] MagFilters = {
            TextureMagFilter.Nearest,
            TextureMagFilter.Linear,
            TextureMagFilter.Nearest,
            TextureMagFilter.Linear,
            TextureMagFilter.Nearest,
            TextureMagFilter.Linear
        };
    }
}
