using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using UnityEngine;

namespace DasikAI.Example.UAINodes.ParamSources
{
    [AINode("Example2/Param Sources/Player Position")]
    public class PlayerPositionPS:Vector2ParamSource
    {
        [Input(ShowBackingValue.Unconnected, ConnectionType.Override, TypeConstraint.None)]
        public PlayerPS PlayerPS;
        
        public override Vector2 GetParam(NodeContext nodeContext)
        {
            return nodeContext.GetParam(PlayerPS).Position2d;
        }
    }
}