using DasikAI.BT.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base;

namespace DasikAI.BT.Nodes.Blocks
{
    /// <summary>
    /// usually is root
    /// </summary>
    [BTNode("Blocks/Empty")]
    public class Empty : BTBlock
    {
        public override void DoWork(NodeContext nodeContext)
        {
        }
    }
}