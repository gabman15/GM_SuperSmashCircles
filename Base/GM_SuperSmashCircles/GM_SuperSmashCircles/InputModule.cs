namespace GM_SuperSmashCircles
{
    /// <summary>
    /// an input module
    /// </summary>
    public abstract class InputModule
    {
        /// <summary>
        /// gets if the given input is currently on
        /// </summary>
        /// <param name="name">name of the input to check</param>
        /// <returns>if the given input is on</returns>
        public abstract bool Get(string name);
    }
}
