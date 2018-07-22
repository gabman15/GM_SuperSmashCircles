using Microsoft.Xna.Framework.Graphics;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_SuperSmashCircles.UserInterface
{
    /// <summary>
    /// a ui element based in lua code
    /// </summary>
    public class LuaUIElement : UIElement
    {
        /// <summary>
        /// the lua state
        /// </summary>
        public Lua State { get; set; }
        /// <summary>
        /// function to call every game update
        /// </summary>
        public LuaFunction OnUpdate { get; set; }
        /// <summary>
        /// function to call every game draw
        /// </summary>
        public LuaFunction OnDraw { get; set; }
        /// <summary>
        /// creates a new lua ui element
        /// </summary>
        public LuaUIElement()
        {
            State = new Lua();
        }
        /// <summary>
        /// draws this lua ui element
        /// </summary>
        /// <param name="sb">the sprite batch to draw to</param>
        public override void Draw(SpriteBatch sb)
        {
            OnDraw?.Call(sb);
        }
        /// <summary>
        /// loads the lua ui element from a lua file
        /// </summary>
        /// <param name="filename">the name of the lua file</param>
        /// <param name="game">the game</param>
        /// <returns>the created lua ui element</returns>
        public static LuaUIElement LoadFromFile(string filename, SSCGame game)
        {
            LuaUIElement elem = new LuaUIElement();
            elem.State.LoadCLRPackage();
            elem.State.DoFile(filename);
            elem.State.GetFunction("OnCreation")?.Call(elem, game);
            elem.OnUpdate = elem.State.GetFunction("OnUpdate");
            game.Events.On("update", () =>
            {
                elem.OnUpdate?.Call();
            });
            elem.OnDraw = elem.State.GetFunction("OnDraw");
            //don't event listen for draw here since it requires a spritebatch and my eventemitter isnt that sophisticated
            return elem;
        }
    }
}
