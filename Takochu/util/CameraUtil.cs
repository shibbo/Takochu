using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Takochu.smg;
using Takochu.smg.obj;

namespace Takochu.util
{
    public class CameraUtil
    {
        public static void InitCameras()
        {
            mCameraEntries = new Dictionary<string, string[]>();

            using (StreamReader r = new StreamReader("res/camera.json"))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);

                foreach (var item in array)
                {
                    string id = item.id;
                    string[] fields = Convert.ToString(item.fields).Split(',');

                    mCameraEntries.Add(id, fields);
                }
            }

            mFieldsToType = new Dictionary<string, string>();

            string[] file = File.ReadAllLines("res/cameraFieldToType.txt");

            foreach(string l in file)
            {
                string[] split = l.Split('=');
                mFieldsToType.Add(split[0], split[1]);
            }
        }

        public static string[] GetStrings(string type)
        {
            return mCameraEntries[type];
        }

        public static string[] GetCameras()
        {
            return mCameraEntries.Keys.ToArray();
        }

        public static string GetTypeOfField(string field)
        {
            return mFieldsToType[field];
        }

        public static Dictionary<string, string> GetAll()
        {
            return mFieldsToType;
        }

        public static List<string> GetUnusedEntriesByCameraType(string type)
        {
            List<string> usedValues = mCameraEntries[type].ToList();
            return sOptionalParams.Where(p => usedValues.All(p2 => p2 != p)).ToList();
        }

        static Dictionary<string, string[]> mCameraEntries;
        static Dictionary<string, string> mFieldsToType;

        static List<string> sOptionalParams = new List<string> { "string", "dist", "axis.X", "axis.Y", "axis.Z", "wpoint.X", "wpoint.Y", "wpoint.Z", "up.X", "up.Y", "up.Z", "angleA", "angleB", "num1", "num2" };
    }
}
