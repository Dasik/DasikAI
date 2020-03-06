﻿﻿using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using DasikAI.BT.Nodes.DSO;
  using DasikAI.Common.Base;
  using UnityEngine;
using XNode;

namespace DasikAI.BT.Nodes.Checks
{
	[AINode("Checks/MinMaxCheck")]
	public class MinMaxCheck : BTBlockCheck
	{
		[Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)]
		public FloatParamSource paramSource;

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

		public override void Initialize(Context context)
		{
			base.Initialize(context);
			var dso = new MinMaxDSO { Max = Max, Min = Min };
			context.CurrentDSO = dso;
		}

		protected override AINode NextOne(Context context)
		{
			var dso = (MinMaxDSO)context.CurrentDSO;
			var param = context.GetParam(paramSource);
			if (param >= dso.Min && param <= dso.Max)
				return @true;
			return @false;
		}
	}
}