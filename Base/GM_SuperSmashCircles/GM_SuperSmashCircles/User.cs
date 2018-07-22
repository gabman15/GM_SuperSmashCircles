using GM_SuperSmashCircles.Input;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// represents a user in the game
    /// </summary>
    public class User
    {
        /// <summary>
        /// the number of the user
        /// </summary>
        public int Number { get; set; }
        /// <summary>
        /// input module for this user
        /// </summary>
        public InputModule Input { get; set; }
        /// <summary>
        /// the entity linked to this user
        /// </summary>
        public Entity Player { get; set; }
        /// <summary>
        /// private reference to the game
        /// </summary>
        private SSCGame game;
        /// <summary>
        /// creates a user
        /// </summary>
        /// <param name="game">the game</param>
        /// <param name="number">the number of the user</param>
        /// <param name="input">input module for the user</param>
        public User(SSCGame game, int number, InputModule input)
        {
            Number = number;
            Input = input;
            this.game = game;
        }
        /// <summary>
        /// links an entity to this user
        /// </summary>
        /// <param name="ent">the entity to link</param>
        public void LinkEntity(Entity ent)
        {
            Player = ent;
            Player.State.GetFunction("OnLink")?.Call(this);
        }
    }
}
