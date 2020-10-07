using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Example.Controller;
using DasikAI.UAI.Nodes.Base.Blocks;
using DasikAI.UAI.Nodes.Blocks;
using UnityEngine;
using XNode;

namespace UAINodes.Actions
{
    [AINode("Example2/Actions/Save current time")]
    public class SaveCurrentTime : UAIAction
    {        
        [Input(ShowBackingValue.Always)]
        public UAIAction[] PreviousAction=new UAIAction[1];
        [Node.Output(backingValue = Node.ShowBackingValue.Unconnected)]
        public UAIAction NextAction;

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            nodeContext.CurrentDSO = new SingleValueDSO<float>{Value = float.NegativeInfinity};
            nodeContext.SharedDSO.Add(this, nodeContext.CurrentDSO);
        }

        public override void PerformAction(UAINodeContext nodeContext)
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = Time.time;

            if (NextAction == null)
                return;
            nodeContext.PerformAction(NextAction);
        }
    }
}