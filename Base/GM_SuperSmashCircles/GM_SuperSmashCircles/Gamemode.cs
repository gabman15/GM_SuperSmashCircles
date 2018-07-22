using NLua;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// controls the current gamemode
    /// </summary>
    public class Gamemode
    {
        /// <summary>
        /// lua function to call when gamemode starts
        /// </summary>
        public LuaFunction OnStart { get; set; }
        /// <summary>
        /// lua function to call on game update
        /// </summary>
        public LuaFunction OnUpdate { get; set; }
        /// <summary>
        /// lua function to call when a new user joins
        /// </summary>
        public LuaFunction OnNewUser { get; set; }
        /// <summary>
        /// creates a basic gamemode
        /// </summary>
        public Gamemode()
        {
            OnStart = null;
            OnUpdate = null;
        }
        /// <summary>
        /// loads a gamemode from a lua file
        /// </summary>
        /// <param name="filename">the lua file to load the gamemode from</param>
        /// <param name="game">the game</param>
        /// <returns>the created gamemode</returns>
        public static Gamemode LoadFromFile(string filename, SSCGame game)
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
            gamemode.OnNewUser = state.GetFunction("OnNewUser");
            return gamemode;
        }
    }
}
