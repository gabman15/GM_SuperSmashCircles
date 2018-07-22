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
            Name = "Left Keyboard";
            InputItems.Add("jump", new KeyboardButtonInputItem(Keys.Space, game));
            InputItems.Add("up", new KeyboardButtonInputItem(Keys.W, game));
            InputItems.Add("down", new KeyboardButtonInputItem(Keys.S, game));
            InputItems.Add("left", new KeyboardButtonInputItem(Keys.A, game));
            InputItems.Add("right", new KeyboardButtonInputItem(Keys.D, game));
            InputItems.Add("dash", new KeyboardButtonInputItem(Keys.H, game));
            InputItems.Add("special", new KeyboardButtonInputItem(Keys.J, game));
        }
    }
}
