using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Example.Controller;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
    [AINode("Example/Params/SqrDistanceToPlayer")]
    public class SqrDistanceToPlayerParam : FloatParamSource
    {
        public override float GetParam(Context context)
        {
            var distance = (((IControllerPosition2d) context.AgentController).Position2d - PlayerController.Instance.Position2d).sqrMagnitude;
            return distance;
        }
    }
}