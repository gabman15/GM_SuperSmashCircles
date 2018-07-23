using System.Collections.Generic;

namespace GM_SuperSmashCircles.Input
{
    /// <summary>
    /// an input module
    /// </summary>
    public class InputModule
    {
        /// <summary>
        /// the name of this input module
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// list of input items
        /// </summary>
        public Dictionary<string, IInputItem> InputItems { get; set; }
        public InputModule()
        {
            Name = "Basic Input Module";
            InputItems = new Dictionary<string, IInputItem>();
        }
        /// <summary>
        /// gets if the given input is currently on
        /// </summary>
        /// <param name="name">name of the input to check</param>
        /// <returns>if the given input is on</returns>
        public bool Get(string name)
        {
            try
            {
                return InputItems[name].Get();
            }
            catch
            {
                return false;
            }
        }
    }
}
