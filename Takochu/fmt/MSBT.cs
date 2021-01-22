using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;

namespace Takochu.fmt
{
    public class MSBT
    {
        public MSBT(FileBase file)
        {
            if (file.ReadString(8) != "MsgStdBn")
            {
                throw new Exception("MSBT::MSBT() -- Invalid MSBT file!");
            }
        }
    }
}
