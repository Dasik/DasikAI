using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.UAI.Nodes.Base.Blocks;

namespace DasikAI.UAI.Nodes.Blocks.Scorers
{
    [AINode("UAI/Scorer/Empty")]
    public class EmptyScorer : UAIScorer
    {
        [Input(ShowBackingValue.Always)] public FloatParamSource Value;

        protected override float GetScore(NodeContext nodeContext)
        {
            return nodeContext.GetParam(Value);
        }
    }
}