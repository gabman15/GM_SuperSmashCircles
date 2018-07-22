using System;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// an event listener
    /// </summary>
    public class EventListener
    {
        /// <summary>
        /// name of the event to listen to
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// function to call when the event is triggered
        /// </summary>
        public Action Function { get; set; }
        /// <summary>
        /// if we want to get rid of this listener
        /// </summary>
        public bool Destroy { get; set; }
        /// <summary>
        /// creates a new event listener
        /// </summary>
        /// <param name="name">name of the event to listen to</param>
        /// <param name="func">function to call when the event is triggered</param>
        public EventListener(string name, Action func)
        {
            Name = name;
            Function = func;
        }
    }
}
