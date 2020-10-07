using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.UAI.Nodes.Base.Blocks;
using UnityEngine;

namespace DasikAI.UAI.Nodes.Blocks.Scorers
{
    [AINode("UAI/Scorer/Curve")]
    public class CurveScorer : UAIScorer
    {
        [Input] public FloatParamSource Value;
        [SerializeField] private AnimationCurve curve;

        protected override float GetScore(NodeContext nodeContext)
        {
            return curve.Evaluate(nodeContext.GetParam(Value));
        }
    }
}