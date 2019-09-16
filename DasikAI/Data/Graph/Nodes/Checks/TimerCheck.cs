using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using DasikAI.Scripts.Data.Graph.Nodes.DSO;
using UnityEngine;
using XNode;

namespace DasikAI.Scripts.Data.Graph.Nodes.Checks
{
	[AINode("Checks/TimerCheck")]
	public class TimerCheck : AIBlockCheck
	{
		[SerializeField] private float _time = 0;

		[Node.Output] public AINode next;

		public override IDataStoreObject Initialize(AgentController controller)
		{
			base.Initialize(controller);
			var dso = new SingleValueDSO<float>();
			return (IDataStoreObject)dso;
		}

		public override IDataStoreObject Enter(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var dso = (SingleValueDSO<float>)dataStoreObject;
			dso.Value = Time.time;
			return dso;
		}

		public override IDataStoreObject Exit(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var dso = (SingleValueDSO<float>)dataStoreObject;
			dso.Value = -1;
			return dso;
		}

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var dso = (SingleValueDSO<float>)dataStoreObject;
			if (Time.time - dso.Value > _time)
				return next;
			return null;
		}
	}
}