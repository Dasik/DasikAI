using DasikAI.Controller;
using DasikAI.Data.CustomTypes;
using DasikAI.Data.Graph.Attributes;
using DasikAI.Data.Graph.Base.ParamSources;
using UnityEngine;

namespace DasikAI.Data.Graph.Nodes.ParamSources
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