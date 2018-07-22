using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GM_SuperSmashCircles.Input
{
    /// <summary>
    /// interface for input items
    /// </summary>
    public interface IInputItem
    {
        /// <summary>
        /// gets the state of this input item
        /// </summary>
        /// <returns>the state of this input item</returns>
        bool Get();
    }
}
