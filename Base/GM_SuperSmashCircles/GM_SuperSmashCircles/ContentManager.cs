using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// manages game content like images
    /// </summary>
    public static class ContentManager
    {
        /// <summary>
        /// dictionary of images
        /// </summary>
        private static Dictionary<string, Texture2D> imageContent = new Dictionary<string, Texture2D>();
        /// <summary>
        /// adds an image to the content list
        /// </summary>
        /// <param name="item">the item to add</param>
        /// <param name="name">the name to call the item by</param>
        public static void AddContent(Texture2D item, string name)
        {
            Console.WriteLine("loaded image \"{0}\"", name);
            imageContent.Add(name, item);
        }
        /// <summary>
        /// gets an image given its corresponding name
        /// </summary>
        /// <param name="name">the name of the image</param>
        /// <returns>the image under the given name, null if no image found</returns>
        public static Texture2D GetContent(string name)
        {
            /*foreach(KeyValuePair<string, Texture2D> pair in imageContent)
            {
                if(pair.Key.Equals(name))
                {
                    return pair.Value;
                }
            }*/
            try
            {
                return imageContent[name];
            }
            catch
            {
                return null;
            }
        }
    }
}
