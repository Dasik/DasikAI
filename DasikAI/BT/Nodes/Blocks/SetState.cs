﻿﻿using DasikAI.Common.Controller;
using DasikAI.BT.CustomTypes;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.DSO;
using DasikAI.BT.Nodes.DSO;
using UnityEngine;

namespace DasikAI.BT.Nodes.Blocks
{
	[AINode("Blocks/SetState")]
	public class SetState : AIBlock
	{
		[SerializeField] protected StatesEnum State;

		public override IDataStoreObject Initialize(AgentController controller)
		{
			return controller.GraphController.SharedStoreObjects[typeof(StateDSO)];
		}

		public override IDataStoreObject DoWork(IDataStoreObject dataStoreObject, AgentController controller)
		{
			((StateDSO) dataStoreObject).State = State.SelectedValue;
			return dataStoreObject;
		}
	}
}