using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using UAINodes.Actions;
using UnityEngine;

namespace DasikAI.Example.UAINodes.ParamSources
{
    [AINode("Example2/Param Sources/Time left to next shot")]
    public class TimeLeftToNextShotPS : FloatParamSource
    {
        [Input(ShowBackingValue.Unconnected)] public FloatParamSource coolingTime;

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            nodeContext.CurrentDSO = new SingleValueDSO<float>();
        }

        public override void OnNodeEnter(NodeContext nodeContext)//cache value
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            var attackDSO = (SingleValueDSO<float>) nodeContext.SharedDSO[typeof(AttackAction)];
            dso.Value = attackDSO.Value + nodeContext.GetParam(coolingTime) - Time.time;
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