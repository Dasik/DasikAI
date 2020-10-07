using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using UnityEngine;

namespace DasikAI.Example.UAINodes.ParamSources
{
    [AINode("Example2/Param Sources/Time since last meet")]
    public class TimeSinceLastMeetPS : FloatParamSource
    {
        [Input] public AINode lastMeetTime;

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            nodeContext.CurrentDSO = new SingleValueDSO<float>();
        }

        public override void OnNodeEnter(NodeContext nodeContext)//cache value
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = Time.time - ((SingleValueDSO<float>) nodeContext.SharedDSO[lastMeetTime]).Value;
        }

        public override void OnNodeExit(NodeContext nodeContext)//remove cache
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = default;
        }

        public override float GetParam(NodeContext nodeContext)//return cached value
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            return dso.Value;
        }
    }
}