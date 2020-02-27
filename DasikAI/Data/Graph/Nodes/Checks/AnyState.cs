using System;
using DasikAI.Controller;
using DasikAI.Data.CustomTypes;
using DasikAI.Data.Graph.Attributes;
using DasikAI.Data.Graph.Base;
using DasikAI.Data.Graph.Base.Blocks;
using DasikAI.Data.Graph.Base.DSO;
using DasikAI.Data.Graph.Nodes.DSO;
using UnityEngine;
using XNode;

namespace DasikAI.Data.Graph.Nodes.Checks
{
	[Serializable]
	[AINode("Checks/AnyState")]
	public class AnyState : AIBlockCheck
	{
		[SerializeField] private StatesEnum _state;

		[Node.Output] public AINode @true;
		[Node.Output] public AINode @false;

		public override IDataStoreObject Initialize(AgentController controller)
		{
			return controller.GraphController.SharedStoreObjects[typeof(StateDSO)];
		}

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var currentState = ((StateDSO) dataStoreObject).State;
			return _state.SelectedValue == currentState ? @true : @false;
		}
	}
}