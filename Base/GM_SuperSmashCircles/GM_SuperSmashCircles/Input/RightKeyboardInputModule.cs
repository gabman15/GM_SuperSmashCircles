using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GM_SuperSmashCircles.Input
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
            Name = "Right Keyboard";
            InputItems.Add("jump", new KeyboardButtonInputItem(Keys.Multiply, game));
            InputItems.Add("up", new KeyboardButtonInputItem(Keys.Up, game));
            InputItems.Add("down", new KeyboardButtonInputItem(Keys.Down, game));
            InputItems.Add("left", new KeyboardButtonInputItem(Keys.Left, game));
            InputItems.Add("right", new KeyboardButtonInputItem(Keys.Right, game));
            InputItems.Add("dash", new KeyboardButtonInputItem(Keys.Add, game));
            InputItems.Add("special", new KeyboardButtonInputItem(Keys.Subtract, game));
        }
    }
}
