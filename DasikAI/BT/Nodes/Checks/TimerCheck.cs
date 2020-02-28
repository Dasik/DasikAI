﻿﻿using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.DSO;
using DasikAI.BT.Nodes.DSO;
using UnityEngine;

namespace DasikAI.BT.Nodes.Checks
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