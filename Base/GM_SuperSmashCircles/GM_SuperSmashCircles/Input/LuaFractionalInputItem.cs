using NLua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_SuperSmashCircles.Input
{
    public class LuaFractionalInputItem : IInputItem
    {
        /// <summary>
        /// function to call to get the value
        /// </summary>
        public LuaFunction Func { get; set; }
        /// <summary>
        /// name of the value
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// the amount required to trigger
        /// </summary>
        public double Deadzone { get; set; }
        /// <summary>
        /// if we invert the fractional input
        /// </summary>
        public bool Invert { get; set; }
        /// <summary>
        /// makes a new lua fractional input item
        /// </summary>
        /// <param name="func">the function to get the value from</param>
        /// <param name="name">the name of the input to check for</param>
        public LuaFractionalInputItem(LuaFunction func, string name, double deadzone)
        {
            Func = func;
            Name = name;
            Deadzone = deadzone;
        }
        /// <summary>
        /// gets if this input item is triggered
        /// </summary>
        /// <returns>if this input item is triggered</returns>
        public bool Get()
        {
            float val;
            try
            {
                val = (float)(Func?.Call(Name)[0] ?? 0);
            }
            catch
            {
                val = 0;
            }
            if (Invert)
                val = -val;
            if(Deadzone > 0)
            {
                return val > Deadzone;
            }
            else
            {
                return val < Deadzone;
            }
        }
    }
}
