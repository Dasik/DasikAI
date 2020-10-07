using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;

namespace DasikAI.Example.UAINodes.ParamSources
{
    [AINode("Example2/Param Sources/Player")]
    public class PlayerPS : ParamSource<PlayerController>
    {
        public override PlayerController GetParam(NodeContext nodeContext)
        {
            return PlayerController.Instance;
        }
    }
}