using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System;

namespace GM_SuperSmashCircles.Input
{
    /// <summary>
    /// an input module for the keyboard
    /// </summary>
    public class KeyboardInputModule : InputModule
    {
        /// <summary>
        /// private reference to the game
        /// </summary>
        private SSCGame game;
        /// <summary>
        /// creates a new keyboard input module
        /// </summary>
        /// <param name="game">a reference to the game</param>
        public KeyboardInputModule(SSCGame game)
        {
            Name = "Keyboard";
            this.game = game;
        }
    }
}
