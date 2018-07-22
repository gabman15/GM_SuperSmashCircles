using System.Collections.Generic;

namespace GM_SuperSmashCircles.Input
{
    /// <summary>
    /// allows multiple inputs to trigger the same item
    /// </summary>
    public class MultiInputItem : IInputItem
    {
        /// <summary>
        /// the list of inputs to check for
        /// </summary>
        public List<IInputItem> InputItems { get; set; }
        /// <summary>
        /// creates a new multi input item
        /// </summary>
        public MultiInputItem()
        {
            InputItems = new List<IInputItem>();
        }
        /// <summary>
        /// checks each of the inputs
        /// </summary>
        /// <returns>if any of the possible inputs are true</returns>
        public bool Get()
        {
            foreach(IInputItem item in InputItems)
            {
                if(item.Get())
                {
                    return true;
                }
            }
            return false;
        }
    }
}
