using DasikAI.Controller;
using DasikAI.Data.CustomTypes;
using DasikAI.Data.Graph.Attributes;
using DasikAI.Data.Graph.Base.Blocks;
using DasikAI.Data.Graph.Base.DSO;
using DasikAI.Data.Graph.Nodes.DSO;
using UnityEngine;

namespace DasikAI.Data.Graph.Nodes.Blocks
{
	[AINode("Blocks/SetState")]
	public class SetState : AIBlock
	{
		[SerializeField] protected StatesEnum State;

		public override IDataStoreObject Initialize(AgentController controller)
		{
			return controller.GraphBehaviourController.SharedStoreObjects[typeof(StateDSO)];
		}

		public override IDataStoreObject DoWork(IDataStoreObject dataStoreObject, AgentController controller)
		{
			((StateDSO) dataStoreObject).State = State.SelectedValue;
			return dataStoreObject;
		}
	}
}