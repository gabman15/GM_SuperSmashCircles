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
        /// current keyboard state
        /// </summary>
        private KeyboardState keyState;
        /// <summary>
        /// keybinds
        /// </summary>
        public Dictionary<string, Keys> KeyboardControls { get; set; }
        /// <summary>
        /// creates a new keyboard input module
        /// </summary>
        /// <param name="game">a reference to the game</param>
        public KeyboardInputModule(SSCGame game)
        {
            this.game = game;

            game.Events.On("update", () =>
            {
                keyState = Keyboard.GetState();
            });
        }
        /// <summary>
        /// checks if a key is pressed
        /// </summary>
        /// <param name="name">name of the input to check</param>
        /// <returns>if the input key is pressed</returns>
        public override bool Get(string name)
        {
            try
            {
                return keyState.IsKeyDown(KeyboardControls[name]);
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// gets the name of this input module
        /// </summary>
        /// <returns>the name of this input module</returns>
        public override string GetName()
        {
            return "Keyboard";
        }
    }
}
