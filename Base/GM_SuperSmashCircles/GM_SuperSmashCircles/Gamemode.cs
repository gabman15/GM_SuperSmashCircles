using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLua;

namespace GM_SuperSmashCircles
{
    public class Gamemode
    {
        public LuaFunction OnStart { get; set; }
        public LuaFunction OnUpdate { get; set; }
        public Gamemode()
        {
            OnStart = null;
            OnUpdate = null;
        }
        public static Gamemode LoadFromFile(string filename, Game1 game)
        {
            Lua state = new Lua();
            state.LoadCLRPackage();
            state.DoFile(filename);
            Gamemode gamemode = new Gamemode();
            state.GetFunction("OnCreation")?.Call(gamemode, game);
            gamemode.OnStart = state.GetFunction("OnStart");
            gamemode.OnUpdate = state.GetFunction("OnUpdate");
            game.Events.On("update", () =>
            {
                gamemode.OnUpdate?.Call();
            });
            return gamemode;
        }
    }
}
