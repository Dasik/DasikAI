using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base;

namespace DasikAI.BT.Nodes.Blocks
{
    /// <summary>
    /// usually is root
    /// </summary>
    [AINode("Blocks/Empty")]
    public class Empty : BTBlock
    {
        public override void DoWork(NodeContext nodeContext)
        {
        }
    }
}