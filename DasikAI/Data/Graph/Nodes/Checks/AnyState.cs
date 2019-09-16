using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.CustomTypes;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using DasikAI.Scripts.Data.Graph.Nodes.DSO;
using UnityEngine;
using XNode;

namespace DasikAI.Scripts.Data.Graph.Nodes.Checks
{
	[AINode("Checks/AnyState")]
	public class AnyState : AIBlockCheck
	{
		[SerializeField] private StatesEnum _state = new StatesEnum();

		[Node.Output] public AINode @true;
		[Node.Output] public AINode @false;

		public override IDataStoreObject Initialize(AgentController controller)
		{
			base.Initialize(controller);
			IDataStoreObject dso;
			if (controller.GraphBehaviourController.SharedStoreObjects.ContainsKey(typeof(StateDSO)))
			{
				dso = controller.GraphBehaviourController.SharedStoreObjects[typeof(StateDSO)];
				return dso;
			}

			dso = new StateDSO();
			controller.GraphBehaviourController.SharedStoreObjects.Add(typeof(StateDSO), dso);
			return dso;
		}

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var currentState = ((StateDSO)dataStoreObject).State;
			return _state.SelectedValue == currentState ? @true : @false;
		}
	}
}
