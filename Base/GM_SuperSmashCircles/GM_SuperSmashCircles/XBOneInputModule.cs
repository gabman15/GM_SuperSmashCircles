using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// an input module for an xbox one controller (possibly works with a 360 controller)
    /// </summary>
    public class XBOneInputModule : InputModule //todo: add stick input
    {
        /// <summary>
        /// private reference to the game
        /// </summary>
        private SSCGame game;
        /// <summary>
        /// the current gamepad state
        /// </summary>
        private GamePadState gamepadState;
        /// <summary>
        /// gamepad keybinds
        /// </summary>
        public Dictionary<string, Buttons> GamepadControls { get; set; }
        /// <summary>
        /// the player index
        /// </summary>
        public PlayerIndex Player { get; set; }
        /// <summary>
        /// creates a new xbox one controller input module
        /// </summary>
        /// <param name="game">a reference to the game</param>
        /// <param name="player">the player index</param>
        public XBOneInputModule(SSCGame game, PlayerIndex player)
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
        /// <summary>
        /// checks if the given key is pressed
        /// </summary>
        /// <param name="name">name of the input to check</param>
        /// <returns>if the key is pressed</returns>
        public override bool Get(string name)
        {
            try
            {
                return gamepadState.IsButtonDown(GamepadControls[name]);
            }
            catch
            {
                return false;
            }
        }
    }
}
