﻿﻿using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.DSO;
using DasikAI.BT.Base.ParamSources;
using DasikAI.BT.Nodes.DSO;
using UnityEngine;
using XNode;

namespace DasikAI.BT.Nodes.Checks
{
	[AINode("Checks/MinMaxCheck")]
	public class MinMaxCheck : AIBlockCheck
	{
		[Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)]
		public FloatParamSource ParamSource;

		[SerializeField] private float _min = 0;
		public float Min
		{
			get { return _min; }
			set { _min = value; }
		}
		[SerializeField] private float _max = 0;
		public float Max
		{
			get { return _max; }
			set { _max = value; }
		}

		[Node.Output] public AINode @true;
		[Node.Output] public AINode @false;

		public override IDataStoreObject Initialize(AgentController controller)
		{
			base.Initialize(controller);
			var dso = new MinMaxDSO { Max = Max, Min = Min };
			return (IDataStoreObject)dso;
		}

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var dso = (MinMaxDSO)dataStoreObject;
			var param = ParamSource.GetParam(controller);
			if (param >= dso.Min && param <= dso.Max)
				return @true;
			return @false;
		}
	}
}