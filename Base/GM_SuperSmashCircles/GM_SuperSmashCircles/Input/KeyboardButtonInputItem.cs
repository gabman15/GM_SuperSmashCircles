using Microsoft.Xna.Framework.Input;

namespace GM_SuperSmashCircles.Input
{
    /// <summary>
    /// input item for gamepad buttons
    /// </summary>
    public class KeyboardButtonInputItem : IInputItem
    {
        /// <summary>
        /// the keyboard state
        /// </summary>
        public KeyboardState KeyboardState { get; set; }
        /// <summary>
        /// the key to check for
        /// </summary>
        public Keys Key { get; set; }
        /// <summary>
        /// makes a new keyboard button input item
        /// </summary>
        /// <param name="key">the key to check for</param>
        /// <param name="game">a reference to the game</param>
        public KeyboardButtonInputItem(Keys key, SSCGame game)
        {
            Key = key;
            game.Events.On("update", () =>
            {
                KeyboardState = Keyboard.GetState();
            });
        }
        /// <summary>
        /// gets the value of this input item
        /// </summary>
        /// <returns>the value of this input item</returns>
        public bool Get()
        {
            return KeyboardState.IsKeyDown(Key);
        }
    }
}
