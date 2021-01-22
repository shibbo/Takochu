using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperBMDLib;
using SuperBMDLib.BMD;
using Takochu.io;

namespace Takochu.fmt
{
    public class BMD
    {
        public BMD(FileBase file)
        {
            mFile = file;
            byte[] data = mFile.GetBuffer();
            Stream stream = new MemoryStream(data);
            mModel = Model.Load(stream);
        }

        public Model mModel;
        private FileBase mFile;
    }
}
