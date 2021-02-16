using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Takochu.smg
{
    class ObjectDB
    {
        public static void Load()
        {
            Actors = new Dictionary<string, Actor>();
            Objects = new Dictionary<string, Object>();

            using (var c = new WebClient())
            {
                c.DownloadFile("http://shibboleet.us.to/new_db/generate.php", "res/objectdb.xml");
            }

            XmlDocument db = new XmlDocument();
            db.Load("res/objectdb.xml");

            XmlNode actors = db.DocumentElement.ChildNodes[0];

            foreach(XmlNode actrs in actors.ChildNodes)
            {
                Actor actor = new Actor();

                actor.ActorName = actrs.Attributes["id"].Value;

                XmlNode generalFlags = actrs["flags"];
                actor.IsKnown = Convert.ToInt32(generalFlags.Attributes["known"].Value);
                actor.IsComplete = Convert.ToInt32(generalFlags.Attributes["complete"].Value);
                actor.Fields = new List<ActorField>();
                XmlNode fields = actrs["fields"];

                foreach(XmlNode field in fields.ChildNodes)
                {
                    ActorField f = new ActorField();
                    string arg = field.Attributes["id"].Value;
                    f.Arg = arg[arg.Length - 1].ToString();
                    f.Name = field.Attributes["name"].Value;
                    f.Type = field.Attributes["type"].Value;
                    f.Value = field.Attributes["values"].Value;
                    f.Notes = field.Attributes["notes"].Value;
                    actor.Fields.Add(f);
                }

                Actors.Add(actor.ActorName, actor);
            }

            XmlNode objects = db.DocumentElement.ChildNodes[1];

            foreach(XmlNode objs in objects.ChildNodes)
            {
                Object obj = new Object();

                obj.InternalName = objs.Attributes["id"].Value;

                XmlNode generalFlags = objs["attributes"];

                obj.Name = generalFlags["name"].InnerText;
                obj.Actor = generalFlags["actor"].InnerText;
                obj.Notes = generalFlags["notes"].InnerText;
                obj.Game = Convert.ToInt32(generalFlags["flags"].Attributes["games"].Value);

                Objects.Add(obj.InternalName, obj);
            }
        }

        public static Actor GetActorFromObjectName(string objName)
        {
            Object obj = Objects[objName];
            return Actors[obj.Actor];
        }

        public struct Actor
        {
            public string ActorName;
            public int IsKnown;
            public int IsComplete;
            public List<ActorField> Fields;
        }

        public struct ActorField
        {
            public string Arg;
            public string Type;
            public string Name;
            public string Value;
            public string Notes;
        }

        public struct Object
        {
            public string InternalName;
            public string Name;
            public string Actor;
            public int Game;
            public string Notes;
        }

        public static Dictionary<string, Actor> Actors;
        public static Dictionary<string, Object> Objects;
    }
}
