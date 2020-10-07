using System;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.UAI.Nodes.Base.Blocks;

namespace DasikAI.UAI.Nodes.Blocks.Scorers
{
    [AINode("UAI/Scorer/Multiply")]
    public class MultiplyScorer : UAIScorer
    {
        [Input(ShowBackingValue.Always)] public FloatParamSource[] Values = new FloatParamSource[1];

        protected override float GetScore(NodeContext nodeContext)
        {
            var result = 1f;
            foreach (var value in Values)
            {
                result *= nodeContext.GetParam(value);
                if (Math.Abs(result) < .0000001f)
                    break;
            }

            return result;
        }
    }
}