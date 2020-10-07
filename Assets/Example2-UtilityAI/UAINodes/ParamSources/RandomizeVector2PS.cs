using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using UnityEngine;

namespace DasikAI.Example.UAINodes.ParamSources
{
    [AINode("Example2/Param Sources/Randomize Vector2")]
    public class RandomizeVector2PS : Vector2ParamSource
    {
        [Input(ShowBackingValue.Unconnected, ConnectionType.Override, TypeConstraint.None)]
        public Vector2ParamSource PlayerPS;

        [SerializeField] private float _randomizedRadius = 10f;

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            nodeContext.CurrentDSO = new SingleValueDSO<Vector2>();
        }

        public override void OnNodeEnter(NodeContext nodeContext)//cache value 
        {
            var dso = (SingleValueDSO<Vector2>) nodeContext.CurrentDSO;
            dso.Value = nodeContext.GetParam(PlayerPS) + Random.insideUnitCircle * _randomizedRadius;
        }

        public override void OnNodeExit(NodeContext nodeContext)//remove cache
        {
            var dso = (SingleValueDSO<Vector2>) nodeContext.CurrentDSO;
            dso.Value = default;
        }

        public override Vector2 GetParam(NodeContext nodeContext)//return cached value
        {
            var dso = (SingleValueDSO<Vector2>) nodeContext.CurrentDSO;
            return dso.Value;
        }
    }
}