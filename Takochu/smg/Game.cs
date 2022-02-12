using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Takochu.io;
using Takochu.util;

namespace Takochu.smg
{
    public class Game
    {
        private const string Only_SMG2_File = "/ObjectData/ProductMapObjDataTable.arc";

        public Game(FilesystemBase filesystem)
        {
            mFilesystem = filesystem;
            SetGameVer();
            
        }

        private void SetGameVer() 
        {
            if (mFilesystem.DoesFileExist(Only_SMG2_File))
                GameUtil.SetGame(GameUtil.Game.SMG2);
            else
                GameUtil.SetGame(GameUtil.Game.SMG1);
        }

        public void Close()
        {
            mFilesystem.Close();
        }

        public bool DoesFileExist(string file)
        {
            return mFilesystem.DoesFileExist(file);
        }

        public bool HasScenario(string galaxy)
        {
            // this solution works for both games
            return mFilesystem.DoesFileExist($"/StageData/{galaxy}/{galaxy}Scenario.arc");
        }

        public List<string> GetGalaxies()
        {
            // this solution works for both games
            List<string> stageDataDirs = mFilesystem.GetDirectories("/StageData");
            return stageDataDirs.FindAll(galaxyName => HasScenario(galaxyName));
        }

        public Galaxy OpenGalaxy(string galaxy)
        {
            if (!HasScenario(galaxy))
            {
                throw new Exception("Game::OpenGalaxy() -- Requested name is not a Galaxy.");
            }

            return new Galaxy(this, galaxy);
        }

        public FilesystemBase mFilesystem;
    }
}
