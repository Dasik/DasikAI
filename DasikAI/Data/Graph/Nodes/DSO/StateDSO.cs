using System;
using DasikAI.Scripts.Data.Graph.Base;

namespace DasikAI.Scripts.Data.Graph.Nodes.DSO
{
	public class StateDSO : IStateDSO<StatesEnum>
	{
		public StatesEnum State { get; set; }
	}

	public enum StatesEnum
	{
		Idle,
		Searching,
		Attack,
		Follow
	}
}
