﻿﻿using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.DSO;
using XNode;

namespace DasikAI.BT.Nodes.Checks
{
	[AINode("Checks/EmptyCheck")]
	public class EmptyCheck : AIBlockCheck
	{
		[Node.Output] public AINode next;

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			return next;
		}
	}
}