using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;

namespace DasikAI.BT.Nodes.Checks
{
    [AINode("Checks/If-Else statement")]
    public class IfElseStatement : BTBlockCheck
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override)]
        public BooleanParamSource ParamSource;

        [Output] public AINode @true;
        [Output] public AINode @false;

        protected override AINode NextOne(NodeContext nodeContext)
        {
            var result = nodeContext.GetParam(ParamSource);
            return result ? @true : @false;
        }
    }
}