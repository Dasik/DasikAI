using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.CustomTypes;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
	[AINode("Params/States")]
	public class StatesParamSource : ParamSource
	{
		public string[] StatesList = { "Idle" };

		[SerializeField]
		[HideInInspector]
		public StatesEnum States
		{
			get
			{
				return new StatesEnum() { Values = StatesList };
			}
			set { StatesList = value == null ? new string[0] : value.Values; }
		}

		protected override object GetParameterValue<T>(AgentController agentController)
		{
			return States;
		}
	}
}
