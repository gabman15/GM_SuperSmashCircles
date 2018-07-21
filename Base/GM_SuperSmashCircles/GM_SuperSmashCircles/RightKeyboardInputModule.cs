using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// an input module for the right side of the keyboard
    /// </summary>
    public class RightKeyboardInputModule : KeyboardInputModule
    {
        /// <summary>
        /// creates a new right side of the keyboard input module
        /// </summary>
        /// <param name="game">a reference to the game</param>
        public RightKeyboardInputModule(SSCGame game) : base(game)
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
}
