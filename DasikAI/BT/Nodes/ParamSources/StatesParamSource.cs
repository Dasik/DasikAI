using DasikAI.Common.Controller;
using DasikAI.BT.CustomTypes;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base.ParamSources;
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

		public override StatesEnum GetParam(AgentController agentController)
		{
			return States;
		}
	}
}