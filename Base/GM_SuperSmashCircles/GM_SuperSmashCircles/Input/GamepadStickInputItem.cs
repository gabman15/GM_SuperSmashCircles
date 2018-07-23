using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace GM_SuperSmashCircles.Input
{
    /// <summary>
    /// input item for gamepad sticks
    /// </summary>
    public class GamepadStickInputItem : IInputItem
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
        /// the stick to check for
        /// </summary>
        public GamepadStick Stick { get; set; }
        /// <summary>
        /// whether to invert the stick measurements
        /// </summary>
        public bool Invert { get; set; }
        /// <summary>
        /// the amount the stick must be moved before this is triggered
        /// </summary>
        public double Deadzone { get; set; }
        /// <summary>
        /// makes a new gamepad stick input item
        /// </summary>
        /// <param name="button">the stick to check for</param>
        /// <param name="player">which gamepad</param>
        /// <param name="game">a reference to the game</param>
        public GamepadStickInputItem(GamepadStick stick, PlayerIndex player, double deadzone, SSCGame game)
        {
            Stick = stick;
            Player = player;
            Deadzone = deadzone;
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
            float val = getStickVal();
            if(Deadzone > 0)
            {
                return val > Deadzone;
            }
            else
            {
                return val < Deadzone;
            }
        }
        /// <summary>
        /// gets the value of the stick
        /// </summary>
        /// <returns>the value of the stick</returns>
        private float getStickVal()
        {
            float val = 0;
            switch(Stick)
            {
                case GamepadStick.LeftX:
                    val = GamepadState?.ThumbSticks.Left.X ?? 0;
                    break;
                case GamepadStick.LeftY:
                    val = GamepadState?.ThumbSticks.Left.Y ?? 0;
                    break;
                case GamepadStick.RightX:
                    val = GamepadState?.ThumbSticks.Right.X ?? 0;
                    break;
                case GamepadStick.RightY:
                    val = GamepadState?.ThumbSticks.Right.Y ?? 0;
                    break;
            }
            if(Invert)
                return -val;
            return val;
        }
    }
    /// <summary>
    /// specifies which stick and which axis to check
    /// </summary>
    public enum GamepadStick
    {
        LeftX, LeftY, RightX, RightY
    }
}
