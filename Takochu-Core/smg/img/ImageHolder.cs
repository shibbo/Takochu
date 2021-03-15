using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.fmt;
using Takochu.io;

namespace Takochu.smg.img
{
    class ImageHolder
    {
        public static void Initialize()
        {
            mImages = new Dictionary<string, BTI>();

            RARCFilesystem fs = new RARCFilesystem(Program.sGame.mFilesystem.OpenFile("/ObjectData/GalaxyInfoTexture.arc"));

            List<string> files = fs.GetFiles("/root/");

            foreach(string file in files)
            {
                BTI b = new BTI(fs.OpenFile($"/root/{file}"));
                mImages.Add(file.Split('.')[0], b);
            }

            fs.Close();
        }

        public static Bitmap GetImage(string stage)
        {
            return mImages[stage].GetImg();
        }

        private static Dictionary<string, BTI> mImages;
    }
}
