using System;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Common.Controller;
using DasikAI.Example.Controller;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
    [AINode("Example/Params/Sqr distance to player")]
    public class SqrDistanceToPlayerParam : FloatParamSource
    {
        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            if (!(nodeContext.CharacterController is IControllerPosition2d))
                throw new ArgumentException(
                    $"{nameof(CharacterController)} must be instance of {nameof(IControllerPosition2d)}");
        }

        public override float GetParam(NodeContext nodeContext)
        {
            var distance =
                (((IControllerPosition2d) nodeContext.CharacterController).Position2d -
                 PlayerController.Instance.Position2d).sqrMagnitude;
            return distance;
        }
    }
}