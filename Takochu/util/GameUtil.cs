using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Takochu.util
{
    public static class GameUtil
    {
        public enum Game
        {
            SMG1,
            SMG2
        }

        public static void SetGame(Game game)
        {
            sGame = game;
        }

        public static bool IsSMG1()
        {
            return sGame == Game.SMG1;
        }

        public static bool IsSMG2()
        {
            return sGame == Game.SMG2;
        }

        private static Game sGame;
    }
}
