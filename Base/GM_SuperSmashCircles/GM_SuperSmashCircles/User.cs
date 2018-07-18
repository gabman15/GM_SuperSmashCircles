using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_SuperSmashCircles
{
    public class User
    {
        public int Number { get; set; }
        public UserInputType InputType { get; set; }
        public InputModule Input { get; set; }
        public Entity Player { get; set; }
        private Game1 game;
        public User(Game1 game, int number, InputModule input)
        {
            Number = number;
            Input = input;
            this.game = game;
        }
        public void LinkEntity(Entity ent)
        {
            Player = ent;
            Player.State.GetFunction("OnLink")?.Call(this);
        }
    }
    public abstract class InputModule
    {
        public bool GetUp()
        {
            return Get("up");
        }
        public bool GetDown()
        {
            return Get("down");
        }
        public bool GetLeft()
        {
            return Get("left");
        }
        public bool GetRight()
        {
            return Get("right");
        }
        public bool GetJump()
        {
            return Get("jump");
        }
        public bool GetDash()
        {
            return Get("dash");
        }
        public bool GetSpecial()
        {
            return Get("special");
        }
        public abstract bool Get(string name);
    }
    public class LeftKeyboardInputModule : KeyboardInputModule
    {
        public LeftKeyboardInputModule(Game1 game) : base(game)
        {
            KeyboardControls = new Dictionary<string, Keys>();
            KeyboardControls.Add("jump", Keys.Space);
            KeyboardControls.Add("up", Keys.W);
            KeyboardControls.Add("down", Keys.S);
            KeyboardControls.Add("left", Keys.A);
            KeyboardControls.Add("right", Keys.D);
            KeyboardControls.Add("dash", Keys.H);
            KeyboardControls.Add("special", Keys.J);
        }
    }
    public class RightKeyboardInputModule : KeyboardInputModule
    {
        public RightKeyboardInputModule(Game1 game) : base(game)
        {
            KeyboardControls = new Dictionary<string, Keys>();
            KeyboardControls.Add("jump", Keys.Multiply);
            KeyboardControls.Add("up", Keys.Up);
            KeyboardControls.Add("down", Keys.Down);
            KeyboardControls.Add("left", Keys.Left);
            KeyboardControls.Add("right", Keys.Right);
            KeyboardControls.Add("dash", Keys.Add);
            KeyboardControls.Add("special", Keys.Subtract);
        }
    }
    public abstract class KeyboardInputModule : InputModule
    {
        private Game1 game;
        private KeyboardState keyState;
        public Dictionary<string, Keys> KeyboardControls { get; set; }
        public KeyboardInputModule(Game1 game)
        {
            this.game = game;

            game.Events.On("update", () =>
            {
                keyState = Keyboard.GetState();
            });
        }
        public override bool Get(string name)
        {
            return keyState.IsKeyDown(KeyboardControls[name]);
        }
    }
    public class XBOneInputModule : InputModule
    {
        private Game1 game;
        private GamePadState gamepadState;
        public Dictionary<string, Buttons> GamepadControls { get; set; }
        public PlayerIndex Player { get; set; }
        public XBOneInputModule(Game1 game, PlayerIndex player)
        {
            this.game = game;
            Player = player;
            GamepadControls = new Dictionary<string, Buttons>();
            GamepadControls.Add("jump", Buttons.X);
            GamepadControls.Add("up", Buttons.DPadUp);
            GamepadControls.Add("down", Buttons.DPadDown);
            GamepadControls.Add("left", Buttons.DPadLeft);
            GamepadControls.Add("right", Buttons.DPadRight);
            GamepadControls.Add("dash", Buttons.A);
            GamepadControls.Add("special", Buttons.B);

            game.Events.On("update", () =>
            {
                gamepadState = GamePad.GetState(Player);
            });
        }
        public override bool Get(string name)
        {
            return gamepadState.IsButtonDown(GamepadControls[name]);
        }
    }
    public class LuaInputModule : InputModule
    {
        private Game1 game;
        public Lua State { get; set; }
        public LuaFunction OnUpdate { get; set; }
        public LuaInputModule(Game1 game, string filename)
        {
            State = new Lua();
            State.LoadCLRPackage();
            State.DoFile(filename);
            State.GetFunction("OnCreation")?.Call();
            OnUpdate = State.GetFunction("OnUpdate");
            game.Events.On("update", () =>
            {
                OnUpdate?.Call();
            });
        }
        public override bool Get(string name)
        {
            bool result;
            try
            {
                result = (bool)State.GetFunction("Get")?.Call(name)[0];
            }
            catch (InvalidCastException)
            {
                return false;
            }
            return result;
        }
    }
    public enum UserInputType
    {
        KeyboardLeft, KeyboardRight, XBOne, Lua
    }
}
