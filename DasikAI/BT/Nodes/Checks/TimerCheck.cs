﻿﻿using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
using DasikAI.BT.Nodes.DSO;
  using DasikAI.Common.Base;
  using UnityEngine;

namespace DasikAI.BT.Nodes.Checks
{
	[AINode("Checks/TimerCheck")]
	public class TimerCheck : BTBlockCheck
	{
		[SerializeField] private float _time = 0;

		[Output] public AINode IsElapsed;
		[Output] public AINode IsNotElapsed;

		public override void OnInitialize(Context context)
		{
			base.OnInitialize(context);
			var dso = new SingleValueDSO<float>();
			context.CurrentDSO = dso;
		}

		public override void OnEnter(Context context)
		{
			var dso = (SingleValueDSO<float>)context.CurrentDSO;
			dso.Value = Time.time;
		}

		public override void OnExit(Context context)
		{
			var dso = (SingleValueDSO<float>)context.CurrentDSO;
			dso.Value = 0f;
		}

		protected override AINode NextOne(Context context)
		{
			var dso = (SingleValueDSO<float>)context.CurrentDSO;
			if (Time.time - dso.Value > _time)
				return IsNotElapsed;
			return IsElapsed;
		}
	}
}