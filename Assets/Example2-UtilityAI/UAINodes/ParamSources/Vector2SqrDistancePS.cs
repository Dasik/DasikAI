using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using XNode;

namespace DasikAI.Example.UAINodes.ParamSources
{
    [AINode("Example2/Param Sources/Vector2 sqr distance")]
    public class Vector2SqrDistancePS : FloatParamSource
    {
        [Node.InputAttribute(Node.ShowBackingValue.Unconnected, Node.ConnectionType.Override, Node.TypeConstraint.None)]
        public Vector2ParamSource A;

        [Node.InputAttribute(Node.ShowBackingValue.Unconnected, Node.ConnectionType.Override, Node.TypeConstraint.None)]
        public Vector2ParamSource B;

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            nodeContext.CurrentDSO = new SingleValueDSO<float>();
        }

        public override void OnNodeEnter(NodeContext nodeContext) //cache value
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = (nodeContext.GetParam(A) - nodeContext.GetParam(B)).sqrMagnitude;
        }

        public override void OnNodeExit(NodeContext nodeContext) //remove cached value
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = default;
        }

        public override float GetParam(NodeContext nodeContext) //return cached value
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            return dso.Value;
        }
    }
}