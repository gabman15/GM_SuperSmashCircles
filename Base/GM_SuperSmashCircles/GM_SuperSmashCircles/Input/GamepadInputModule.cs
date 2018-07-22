using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace GM_SuperSmashCircles.Input
{
    /// <summary>
    /// an input module for an xbox one controller (possibly works with a 360 controller)
    /// </summary>
    public class GamepadInputModule : InputModule //todo: add stick input
    {
        /// <summary>
        /// private reference to the game
        /// </summary>
        private SSCGame game;
        /// <summary>
        /// the player index
        /// </summary>
        public PlayerIndex Player { get; set; }
        /// <summary>
        /// creates a new xbox one controller input module
        /// </summary>
        /// <param name="game">a reference to the game</param>
        /// <param name="player">the player index</param>
        public GamepadInputModule(SSCGame game, PlayerIndex player)
        {
            this.game = game;
            Player = player;
            Name = "Gamepad Player " + Convert.ToString((int)Player + 1);
            double deadzone = 0.5;
            InputItems.Add("jump", new GamepadButtonInputItem(Buttons.X, Player, game));
            MultiInputItem up = new MultiInputItem();
            up.InputItems.Add(new GamepadButtonInputItem(Buttons.DPadUp, Player, game));
            GamepadStickInputItem stick = new GamepadStickInputItem(GamepadStick.LeftY, Player, -deadzone, game);
            stick.Invert = true;
            up.InputItems.Add(stick);
            InputItems.Add("up", up);
            MultiInputItem down = new MultiInputItem();
            down.InputItems.Add(new GamepadButtonInputItem(Buttons.DPadDown, Player, game));
            stick = new GamepadStickInputItem(GamepadStick.LeftY, Player, deadzone, game);
            stick.Invert = true;
            down.InputItems.Add(stick);
            InputItems.Add("down", down);
            MultiInputItem left = new MultiInputItem();
            left.InputItems.Add(new GamepadButtonInputItem(Buttons.DPadLeft, Player, game));
            left.InputItems.Add(new GamepadStickInputItem(GamepadStick.LeftX, Player, -deadzone, game));
            InputItems.Add("left", left);
            MultiInputItem right = new MultiInputItem();
            right.InputItems.Add(new GamepadButtonInputItem(Buttons.DPadRight, Player, game));
            right.InputItems.Add(new GamepadStickInputItem(GamepadStick.LeftX, Player, deadzone, game));
            InputItems.Add("right", right);
            InputItems.Add("dash", new GamepadButtonInputItem(Buttons.A, Player, game));
            InputItems.Add("special", new GamepadButtonInputItem(Buttons.B, Player, game));
        }
    }
}
