using DasikAI.BT.Attributes;
using DasikAI.BT.CustomTypes;
using DasikAI.BT.Nodes.DSO;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Common.Base;
using UnityEngine;

namespace DasikAI.BT.Nodes.ParamSources
{
    [System.Serializable]
    [BTNode("Params/States")]
    public class StatesParamSource : ParamSource<StatesEnum>
    {
        [SerializeField] public string[] StatesList = {"Idle"};

        public StatesEnum States => new StatesEnum {Values = StatesList};

        [SerializeField] public StatesEnum InitialState;

        public override StatesEnum GetParam(NodeContext nodeContext)
        {
            return States;
        }

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            nodeContext.SharedDSO.Add(typeof(StateDSO), new StateDSO() {State = InitialState.SelectedValue});
        }
    }
}