using DasikAI.Controller;
using DasikAI.Data.Graph.Attributes;
using DasikAI.Data.Graph.Base;
using DasikAI.Data.Graph.Base.Blocks;
using DasikAI.Data.Graph.Base.DSO;
using DasikAI.Data.Graph.Nodes.DSO;
using UnityEngine;

namespace DasikAI.Data.Graph.Nodes.Checks
{
	[AINode("Checks/TimerCheck")]
	public class TimerCheck : AIBlockCheck
	{
		[SerializeField] private float _time = 0;

		[Output] public AINode IsElapsed;
		[Output] public AINode IsNotElapsed;

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
			dso.Value = 0f;
			return dso;
		}

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var dso = (SingleValueDSO<float>)dataStoreObject;
			if (Time.time - dso.Value > _time)
				return IsNotElapsed;
			return IsElapsed;
		}
	}
}