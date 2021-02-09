using DasikAI.BT.Attributes;
using DasikAI.BT.CustomTypes;
using DasikAI.BT.Base.Blocks;
using DasikAI.BT.Nodes.DSO;
using DasikAI.Common.Base;
using UnityEngine;

namespace DasikAI.BT.Nodes.Blocks
{
    [BTNode("Blocks/SetState")]
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