using System;
using DasikAI.BT.Attributes;
using DasikAI.BT.CustomTypes;
using DasikAI.BT.Base.Blocks;
using DasikAI.BT.Nodes.DSO;
using DasikAI.Common.Base;
using UnityEngine;

namespace DasikAI.BT.Nodes.Checks
{
    [Serializable]
    [BTNode("Checks/CheckState")]
    public class CheckState : BTBlockCheck
    {
        [SerializeField] private StatesEnum _state;

        [Output] public AINode @true;
        [Output] public AINode @false;

        protected override AINode NextOne(NodeContext nodeContext)
        {
            var stateDSO = ((StateDSO) nodeContext.SharedDSO[typeof(StateDSO)]);
            var currentState = stateDSO.State;
            return _state.SelectedValue == currentState ? @true : @false;
        }
    }
}