using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GM_SuperSmashCircles
{
    public class Platform
    {
        /// <summary>
        /// x position
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// y position
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// width
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// height
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// lua function to be called on update
        /// </summary>
        public LuaFunction OnUpdate { get; set; }
        /// <summary>
        /// lua function to be called on collision
        /// </summary>
        public LuaFunction OnCollision { get; set; }
        /// <summary>
        /// the lua state for this platform
        /// </summary>
        public Lua State { get; set; }
        /// <summary>
        /// the friction added to entities touching this platform
        /// </summary>
        public double Friction { get; set; }
        /// <summary>
        /// the image of this platform
        /// </summary>
        public Texture2D Image { get; set; }
        private string imageName { get; set; }
        /// <summary>
        /// color of this platform
        /// </summary>
        public Color Color;
        /// <summary>
        /// constructs a platform with default values
        /// </summary>
        public Platform()
        {
            X = 0;
            Y = 0;
            Width = 0;
            Height = 0;
            OnUpdate = null;
            OnCollision = null;
            State = new Lua();
            Image = null;
            imageName = "";
            Color = new Color(255, 255, 255);
            Friction = 0.5;
        }
        /// <summary>
        /// constructs an entity
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        /// <param name="ra">repel amount</param>
        public Platform(double x, double y, double w, double h, double ra) : base()
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
        /// <summary>
        /// sets the image from the content manager (likely used from lua code)
        /// </summary>
        /// <param name="name">key of the image in the content manager</param>
        public void SetImage(string name)
        {
            Texture2D image = ContentManager.GetContent(name);
            if (image != null)
            {
                Image = image;
            }
            imageName = name;
        }
        public void SetColor(int r, int g, int b)
        {
            Color.R = (byte)r;
            Color.B = (byte)b;
            Color.G = (byte)g;
        }
        public Rectangle GetRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }
        public void Draw(SpriteBatch sb)
        {
            if(Image == null)
            {
                Image = ContentManager.GetContent(imageName);
            }
            if (Image == null) return;
            sb.Draw(Image, GetRectangle(), Color);
        }
        /// <summary>
        /// loads an entity from a file
        /// </summary>
        /// <param name="filename">lua file name</param>
        /// <returns>the platform from the file</returns>
        public static Platform LoadFromFile(string filename, Game1 game)
        {
            Platform platform = new Platform();
            platform.State.LoadCLRPackage();
            platform.State.DoFile(filename);
            platform.State.GetFunction("OnCreation")?.Call(platform, game);
            platform.OnUpdate = platform.State.GetFunction("OnUpdate");
            platform.OnCollision = platform.State.GetFunction("OnCollision");
            return platform;
        }
    }
}
