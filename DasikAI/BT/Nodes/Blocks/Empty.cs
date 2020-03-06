using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base;

namespace DasikAI.BT.Nodes.Blocks
{
    [AINode("Blocks/Empty")]
    public class Empty : BTBlock
    {
        public override void DoWork(Context context)
        {
        }
    }
}