﻿﻿using DasikAI.Common.Controller;
using DasikAI.BT.CustomTypes;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
using DasikAI.BT.Nodes.DSO;
  using DasikAI.Common.Base;
  using UnityEngine;

namespace DasikAI.BT.Nodes.Blocks
{
	[AINode("Blocks/SetState")]
	public class SetState : BTBlock
	{
		[SerializeField] protected StatesEnum State;

		public override void DoWork(Context context)
		{
			var stateDSO=((StateDSO)context.SharedDSO[typeof(StateDSO)]);
			stateDSO.State = State.SelectedValue;
		}
	}
}