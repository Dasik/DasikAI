using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.UAI.Nodes.Base.Blocks;

namespace DasikAI.UAI.Nodes.Blocks.Actions
{
    /// <summary>
    /// Empty action. Use it for idle.
    /// </summary>
    [AINode("UAI/Actions/Empty")]
    public class EmptyAction : UAIAction
    {
        public override void PerformAction(UAINodeContext nodeContext)
        {
        }
    }
}