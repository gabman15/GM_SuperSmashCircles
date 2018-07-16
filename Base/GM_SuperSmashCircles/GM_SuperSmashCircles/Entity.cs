using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

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
        public LuaFunction OnCollision { get; set; }
        /// <summary>
        /// the lua state for this entity
        /// </summary>
        public Lua State { get; set; }
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
            OnCollision = null;
            State = new Lua();
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
            entity.OnCollision = entity.State.GetFunction("OnCollision");
            return entity;
        }
    }
}
