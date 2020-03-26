using DasikAI.Common.Controller;
using DasikAI.BT.CustomTypes;
using DasikAI.BT.Nodes.DSO;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Common.Base;
using UnityEngine;

namespace DasikAI.BT.Nodes.ParamSources
{
    [System.Serializable]
    [AINode("Params/States")]
    public class StatesParamSource : ParamSource<StatesEnum>
    {
        [SerializeField] public string[] StatesList = {"Idle"};

        [HideInInspector] public StatesEnum States => new StatesEnum {Values = StatesList};

        [SerializeField] public StatesEnum InitialState;

        public override StatesEnum GetParam(Context context)
        {
            return States;
        }

        public override void OnInitialize(Context context)
        {
            base.OnInitialize(context);
            context.SharedDSO.Add(typeof(StateDSO), new StateDSO() {State = InitialState.SelectedValue});
        }
    }
}