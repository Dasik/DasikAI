using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base;
using UnityEngine;

namespace DasikAI.BT.Nodes.Checks
{
    [AINode("Checks/TimerCheck")]
    public class TimerCheck : BTBlockCheck
    {
        [SerializeField] private float _time = 0;

        [Output] public AINode IsElapsed;
        [Output] public AINode IsNotElapsed;

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            var dso = new SingleValueDSO<float>();
            nodeContext.CurrentDSO = dso;
        }

        public override void OnNodeEnter(NodeContext nodeContext)
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = Time.time;
        }

        public override void OnNodeExit(NodeContext nodeContext)
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = 0f;
        }

        protected override AINode NextOne(NodeContext nodeContext)
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            if (Time.time - dso.Value > _time)
                return IsElapsed;
            return IsNotElapsed;
        }
    }
}