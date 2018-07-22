using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GM_SuperSmashCircles.Input
{
    /// <summary>
    /// input item for gamepad buttons
    /// </summary>
    public class GamepadButtonInputItem : IInputItem
    {
        /// <summary>
        /// the gamepad state
        /// </summary>
        public GamePadState? GamepadState { get; set; }
        /// <summary>
        /// the player index
        /// </summary>
        public PlayerIndex Player { get; set; }
        /// <summary>
        /// the button to check for
        /// </summary>
        public Buttons Button { get; set; }
        /// <summary>
        /// makes a new gamepad button input item
        /// </summary>
        /// <param name="button">the button to check for</param>
        /// <param name="player">which gamepad</param>
        /// <param name="game">a reference to the game</param>
        public GamepadButtonInputItem(Buttons button, PlayerIndex player, SSCGame game)
        {
            Button = button;
            Player = player;
            GamepadState = null;
            game.Events.On("update", () =>
            {
                GamepadState = GamePad.GetState(Player);
            });
        }
        /// <summary>
        /// gets the value of this input item
        /// </summary>
        /// <returns>the value of this input item</returns>
        public bool Get()
        {
            return GamepadState?.IsButtonDown(Button) ?? false;
        }
    }
}
