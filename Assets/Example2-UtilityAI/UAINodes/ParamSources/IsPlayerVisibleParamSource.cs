using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Example;
using DasikAI.Example.Controller;
using UnityEngine;

namespace DasikAI.Example2.UAINodes.ParamSources
{
    [AINode("Example2/Param Sources/Is player visible")]
    public class IsPlayerVisibleParamSource : FloatParamSource
    {
        [Input(ShowBackingValue.Unconnected, ConnectionType.Override, TypeConstraint.None)]
        public Vector2ParamSource PositionPS;
        public override float GetParam(NodeContext nodeContext)
        {
            var characterController = nodeContext.CharacterController as IControllerPosition2d;

            var hit = Physics2D.Raycast(characterController.Position2d,
                nodeContext.GetParam(PositionPS) - characterController.Position2d);
            return hit.transform.TryGetComponent(out PlayerController _)?1:0;
        }
    }
}