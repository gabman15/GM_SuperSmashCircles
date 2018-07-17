using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_SuperSmashCircles
{
    public static class ContentManager
    {
        private static Dictionary<string, Texture2D> imageContent = new Dictionary<string, Texture2D>();
        public static void AddContent(Texture2D item, string name)
        {
            Console.WriteLine("loaded image \"{0}\"", name);
            imageContent.Add(name, item);
        }
        public static Texture2D GetContent(string name)
        {
            foreach(KeyValuePair<string, Texture2D> pair in imageContent)
            {
                if(pair.Key.Equals(name))
                {
                    return pair.Value;
                }
            }
            return null;
        }
    }
}
