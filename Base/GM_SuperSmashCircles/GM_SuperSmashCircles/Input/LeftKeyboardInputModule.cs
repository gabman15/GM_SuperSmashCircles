using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GM_SuperSmashCircles.Input
{
    /// <summary>
    /// a left side of the keyboard input module
    /// </summary>
    public class LeftKeyboardInputModule : KeyboardInputModule
    {
        /// <summary>
        /// creates a new left side of the keyboard input module
        /// </summary>
        /// <param name="game">a reference to the game</param>
        public LeftKeyboardInputModule(SSCGame game) : base(game)
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
        /// <summary>
        /// gets the name of this input module
        /// </summary>
        /// <returns>the name of this input module</returns>
        public override string GetName()
        {
            return "Left Keyboard";
        }
    }
}
