using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using XNode;

namespace DasikAI.UAI.Nodes.Base.Blocks
{
    public abstract class UAIScorer : FloatParamSource
    {
        public override float GetParam(NodeContext nodeContext)//return cached value
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            return dso.Value;
        }

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            nodeContext.CurrentDSO = new SingleValueDSO<float>();
        }

        public override void OnNodeEnter(NodeContext nodeContext)//cache value
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = GetScore(nodeContext);
        }

        public override void OnNodeExit(NodeContext nodeContext)//remove cached value
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = default;
        }

        protected abstract float GetScore(NodeContext nodeContext);
    }
}