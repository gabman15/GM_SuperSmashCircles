using System;
using System.Collections.Generic;

namespace GM_SuperSmashCircles
{
    /// <summary>
    /// allows triggering of events and adding listeners to events
    /// </summary>
    public class EventEmitter
    {
        /// <summary>
        /// list of event listeners
        /// </summary>
        private List<EventListener> listeners;
        /// <summary>
        /// creates an new event emitter
        /// </summary>
        public EventEmitter()
        {
            listeners = new List<EventListener>();
        }
        /// <summary>
        /// adds an event listener
        /// </summary>
        /// <param name="name">name of the event to listen to</param>
        /// <param name="function">the function to be called when the event is triggered</param>
        /// <returns>the created event listener</returns>
        public EventListener On(string name, Action function)
        {
            EventListener el = new EventListener(name, function);
            listeners.Add(el);
            return el;
        }
        /// <summary>
        /// trigger an event
        /// </summary>
        /// <param name="name">the name of the event</param>
        public void Emit(string name)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
            {
                EventListener l = listeners[i];
                if(l.Destroy)
                {
                    listeners.RemoveAt(i);
                }
                else if (l.Name.Equals(name))
                {
                    l.Function.Invoke();
                }
            }
        }
    }
}
