﻿﻿using System;
using DasikAI.Common.Controller;
using DasikAI.BT.CustomTypes;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.DSO;
using DasikAI.BT.Nodes.DSO;
using UnityEngine;
using XNode;

namespace DasikAI.BT.Nodes.Checks
{
	[Serializable]
	[AINode("Checks/AnyState")]
	public class AnyState : AIBlockCheck
	{
		[SerializeField] private StatesEnum _state;

		[Node.Output] public AINode @true;
		[Node.Output] public AINode @false;

		public override IDataStoreObject Initialize(AgentController controller)
		{
			return controller.GraphController.SharedStoreObjects[typeof(StateDSO)];
		}

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var currentState = ((StateDSO) dataStoreObject).State;
			return _state.SelectedValue == currentState ? @true : @false;
		}
	}
}