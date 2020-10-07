using DasikAI.BT.CustomTypes;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.BT.Nodes.DSO;
using DasikAI.Common.Base;
using UnityEngine;

namespace DasikAI.BT.Nodes.Blocks
{
    [AINode("Blocks/SetState")]
    public class SetState : BTBlock
    {
        [SerializeField] protected StatesEnum State;

        public override void DoWork(NodeContext nodeContext)
        {
            var stateDSO = (StateDSO) nodeContext.SharedDSO[typeof(StateDSO)];
            stateDSO.State = State.SelectedValue;
        }
    }
}