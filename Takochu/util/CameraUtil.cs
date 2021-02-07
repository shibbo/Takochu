using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

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

        static Dictionary<string, string[]> mCameraEntries;
        static Dictionary<string, string> mFieldsToType;
    }
}
