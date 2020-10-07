using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
    [AINode("Example/Params/Player Position")]
    public class PlayerPositionSource : Vector2ParamSource
    {
        [SerializeField] private float _randomizedRadius = 10f;
        
        public override Vector2 GetParam(NodeContext nodeContext)
        {
            var playerPos = PlayerController.Instance.Position2d;
            playerPos += Random.insideUnitCircle * _randomizedRadius;
            return playerPos;
        }
    }
}