using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using DasikAI.Scripts.Data.Graph.Nodes.DSO;
using UnityEngine;

namespace Assets.DasikAI.Scripts.Data.Graph.Nodes.Blocks
{
	[AINode("Blocks/SetState")]
	public class SetState: AIBlock
	{
		[SerializeField] private StatesEnum _state;
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

		public override IDataStoreObject DoWork(IDataStoreObject dataStoreObject, AgentController controller)
		{
			((StateDSO) dataStoreObject).State = _state;
			return dataStoreObject;
		}
	}
}
