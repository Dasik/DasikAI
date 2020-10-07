using System.Linq;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.UAI.Nodes.Base.Blocks;

namespace DasikAI.UAI.Nodes.Blocks.Scorers
{
    [AINode("UAI/Scorer/Sum")]
    public class SumScorer : UAIScorer
    {
        [Input(ShowBackingValue.Always)] public FloatParamSource[] Values = new FloatParamSource[1];

        protected override float GetScore(NodeContext nodeContext)
        {
            return Values.Where(paramSource => paramSource != null).Sum(nodeContext.GetParam);
        }
    }
}