using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_SuperSmashCircles
{
    public class EventEmitter
    {
        private List<EventListener> listeners;
        public EventEmitter()
        {
            listeners = new List<EventListener>();
        }
        public void On(string name, Action function)
        {
            listeners.Add(new EventListener(name, function));
        }
        public void Emit(string name)
        {
            foreach(EventListener l in listeners)
            {
                if(l.Name.Equals(name))
                {
                    l.Function.Invoke();
                }
            }
        }
    }
    public class EventListener
    {
        public string Name { get; set; }
        public Action Function { get; set; }
        public EventListener(string name, Action func)
        {
            Name = name;
            Function = func;
        }

    }
}
