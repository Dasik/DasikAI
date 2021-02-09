using DasikAI.BT.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base;

namespace DasikAI.BT.Nodes.Checks
{
    /// <summary>
    /// just return next node
    /// </summary>
    [BTNode("Checks/EmptyCheck")]
    public class EmptyCheck : BTBlockCheck
    {
        [Output] public AINode next;

        protected override AINode NextOne(NodeContext nodeContext)
        {
            return next;
        }
    }
}