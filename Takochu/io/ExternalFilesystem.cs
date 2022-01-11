using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms.VisualStyles;

namespace Takochu.io
{
    public class ExternalFilesystem : FilesystemBase
    {
        public ExternalFilesystem(string dir)
        {
            if (!Directory.Exists(dir))
                throw new Exception($"ExternalFilesystem::ExternalFilesystem() - Directory not found.");

            mInfo = new DirectoryInfo(dir);
        }

        public override void Save()
        {
            
        }

        public override void Close()
        {

        }

        public override List<string> GetDirectories(string dir)
        {
            if (!DoesDirectoryExist(dir))
                throw new Exception($"ExternalFilesystem::GetDirectories() - Directory not found.");

            List<string> dirs = new List<string>();

            DirectoryInfo inf = new DirectoryInfo(mInfo.FullName + dir);

            foreach (DirectoryInfo d in inf.GetDirectories())
                dirs.Add(d.Name);

            return dirs;
        }

        public override bool DoesDirectoryExist(string dir)
        {
            DirectoryInfo inf = new DirectoryInfo(mInfo.FullName + dir);
            return inf.Exists;
        }

        public override List<string> GetFiles(string dir)
        {
            if (!DoesDirectoryExist(dir))
                throw new Exception($"ExternalFilesystem::GetFiles() - Directory not found.");

            List<string> files = new List<string>();

            DirectoryInfo inf = new DirectoryInfo(mInfo.FullName + dir);

            foreach (FileInfo f in inf.GetFiles())
                files.Add(f.Name);

            return files;
        }

        public override bool DoesFileExist(string file)
        {
            FileInfo inf = new FileInfo(mInfo.FullName + file);
            return inf.Exists;
        }

        public override FileBase OpenFile(string file)
        {
            if (!DoesFileExist(file)) 
            {
                Properties.Settings.Default.GamePath = "\"\"";
                Properties.Settings.Default.Save();
                throw new Exception($"ExternalFilesystem::OpenFile() - File {file} not found.");
            }
                

            return new ExternalFile(mInfo.FullName + file);
        }

        DirectoryInfo mInfo;
    }
}
