﻿﻿using System;
using DasikAI.Common.Controller;
using DasikAI.BT.CustomTypes;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
using DasikAI.BT.Nodes.DSO;
  using DasikAI.Common.Base;
  using UnityEngine;
using XNode;

namespace DasikAI.BT.Nodes.Checks
{
	[Serializable]
	[AINode("Checks/AnyState")]
	public class AnyState : BTBlockCheck
	{
		[SerializeField] private StatesEnum _state;

		[Node.Output] public AINode @true;
		[Node.Output] public AINode @false;

		protected override AINode NextOne(Context context)
		{
			var stateDSO=((StateDSO)context.SharedDSO[typeof(StateDSO)]);
			var currentState = stateDSO.State;
			return _state.SelectedValue == currentState ? @true : @false;
		}
	}
}