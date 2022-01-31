using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Takochu.fmt;
using Takochu.smg;
using Takochu.smg.msg;
using Takochu.util;
using static Takochu.util.EditorUtil;

namespace Takochu
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            BCSV.PopulateHashTable();
            BCSV.PopulateFieldTypeTable();
            CameraUtil.InitCameras();
            ObjectDB.Load();
            RenderUtil.AssignColors();
            EditorActionHolder.Initialize();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainWindow());
        }

        //public static Translator sTranslator;
        public static string sLanguage;
        public static Game sGame;
        public static int sUniqueID;
    }
}
