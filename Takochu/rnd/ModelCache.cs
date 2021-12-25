using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Takochu.rnd
{
    public static class ModelCache
    {
        public static void AddRenderer(string name, BmdRenderer render)
        {
            if (sRenderingCache.ContainsKey(name))
                return;

            sRenderingCache.Add(name, render);
        }

        public static void AddRenderer(string name, BmdRenderer renderer,string name1 ,BmdRenderer renderer1) 
        {
            if (sRenderingCache.ContainsKey(name)|| sRenderingCache.ContainsKey(name1))
                return;
            sRenderingCache.Add(name,renderer);
            sRenderingCache.Add(name1,renderer1);
        }

        public static bool HasRenderer(string name)
        {
            return sRenderingCache.ContainsKey(name);
        }

        public static void RemoveRenderer(string name)
        {
            sRenderingCache.Remove(name);
        }

        public static BmdRenderer GetRenderer(string name)
        {
            return sRenderingCache[name];
        }

        private static Dictionary<string, BmdRenderer> sRenderingCache = new Dictionary<string, BmdRenderer>();
    }
}
