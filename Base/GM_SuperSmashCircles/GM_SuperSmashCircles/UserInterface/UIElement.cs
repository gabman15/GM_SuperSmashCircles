using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GM_SuperSmashCircles.UserInterface
{
    /// <summary>
    /// build ui elements off of this
    /// </summary>
    public abstract class UIElement
    {
        /// <summary>
        /// list of properties of this ui element
        /// </summary>
        private Dictionary<string, object> props;
        /// <summary>
        /// create a new ui element
        /// </summary>
        public UIElement()
        {
            props = new Dictionary<string, object>();
        }
        /// <summary>
        /// sets a property of this ui element
        /// </summary>
        /// <param name="name">the name of the property to set</param>
        /// <param name="item">the value of the property to be set</param>
        public void SetProperty(string name, object item)
        {
            if(props.ContainsKey(name))
            {
                props.Remove(name);
            }
            props.Add(name, item);
        }
        /// <summary>
        /// gets a property from this ui element
        /// </summary>
        /// <param name="name">the name of the property to look for</param>
        /// <returns>the property value if found, null if not</returns>
        public object GetProperty(string name)
        {
            try
            {
                return props[name];
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// draws this ui element
        /// </summary>
        /// <param name="sb">the sprite batch to draw to</param>
        public abstract void Draw(SpriteBatch sb);
    }
}
