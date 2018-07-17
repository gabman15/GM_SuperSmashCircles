﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GM_SuperSmashCircles
{
    public class Entity
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
        /// x velocity
        /// </summary>
        public double DX { get; set; }
        /// <summary>
        /// y velocity
        /// </summary>
        public double DY { get; set; }
        /// <summary>
        /// width
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// height
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// the amount this entity repels other entities
        /// </summary>
        public double RepelAmount { get; set; }
        /// <summary>
        /// lua function to be called on update
        /// </summary>
        public LuaFunction OnUpdate { get; set; }
        /// <summary>
        /// lua function to be called on collision
        /// </summary>
        public LuaFunction OnEntityCollision { get; set; }
        /// <summary>
        /// lua function to be called on collision
        /// </summary>
        public LuaFunction OnSolidCollision { get; set; }
        /// <summary>
        /// the lua state for this entity
        /// </summary>
        public Lua State { get; set; }
        /// <summary>
        /// the image of this entity
        /// </summary>
        public Texture2D Image { get; set; }
        private string imageName { get; set; }
        /// <summary>
        /// color of this entity
        /// </summary>
        public Color Color;
        /// <summary>
        /// whether this entity will collide with platforms
        /// </summary>
        public bool CollideWithPlatforms { get; set; }
        /// <summary>
        /// whether this entity will collide with other entities
        /// </summary>
        public bool CollideWithEntities { get; set; }
        /// <summary>
        /// constructs an entity with default values
        /// </summary>
        public Entity()
        {
            X = 0;
            Y = 0;
            DX = 0;
            DY = 0;
            Width = 0;
            Height = 0;
            RepelAmount = 0;
            OnUpdate = null;
            OnEntityCollision = null;
            OnSolidCollision = null;
            State = new Lua();
            Image = null;
            imageName = "";
            Color = new Color(255, 255, 255);
            CollideWithPlatforms = true;
        }
        /// <summary>
        /// constructs an entity
        /// </summary>
        /// <param name="x">x position</param>
        /// <param name="y">y position</param>
        /// <param name="w">width</param>
        /// <param name="h">height</param>
        /// <param name="ra">repel amount</param>
        public Entity(double x, double y, double w, double h, double ra) : base()
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
            RepelAmount = ra;
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
        /// <summary>
        /// sets the color of this entity
        /// </summary>
        /// <param name="r">red value</param>
        /// <param name="g">green value</param>
        /// <param name="b">blue value</param>
        public void SetColor(int r, int g, int b)
        {
            Color.R = (byte)r;
            Color.B = (byte)b;
            Color.G = (byte)g;
        }
        /// <summary>
        /// gets the rectangle for this entity
        /// </summary>
        /// <returns>the rectangle for this entity</returns>
        public Rectangle GetRectangle()
        {
            return new Rectangle((int)X, (int)Y, (int)Width, (int)Height);
        }
        /// <summary>
        /// draws this entity
        /// </summary>
        /// <param name="sb">the game's sprite batch</param>
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
        /// <returns>the entity from the file</returns>
        public static Entity LoadFromFile(string filename, Game1 game)
        {
            Entity entity = new Entity();
            entity.State.LoadCLRPackage();
            entity.State.DoFile(filename);
            entity.State.GetFunction("OnCreation")?.Call(entity, game);
            entity.OnUpdate = entity.State.GetFunction("OnUpdate");
            entity.OnEntityCollision = entity.State.GetFunction("OnEntityCollision");
            entity.OnSolidCollision = entity.State.GetFunction("OnSolidCollision");
            return entity;
        }
    }
}