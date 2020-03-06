﻿﻿using System.Collections.Generic;
using System.Linq;
using DasikAI.Common.Controller;
using DasikAI.BT.CustomTypes;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
using DasikAI.BT.Nodes.DSO;
  using DasikAI.Common.Base;
  using XNode;

namespace DasikAI.BT.Nodes.Checks
{
	[AINode("Checks/StateIs")]
	public class StateIs : BTBlockCheck
	{
		[Node.Output(dynamicPortList = true, backingValue = ShowBackingValue.Always, connectionType = ConnectionType.Multiple)]
		public StatesEnum[] States;

		public override IEnumerable<AINode> Next(Context context)
		{
			var stateDSO=((StateDSO)context.SharedDSO[typeof(StateDSO)]);
			var currentState = stateDSO.State;
			var result = Enumerable.Empty<AINode>();
			for (int i = 0; i < States.Length; i++)
			{
				var state = States[i].SelectedValue;
				if (state.Equals(currentState))
					result = result.Union(
						GetPort(nameof(States) + " " + i)
							.GetConnections()
							.Select(port => port.node as AINode));
			}

			return result;
		}
	}
}