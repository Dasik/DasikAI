﻿﻿using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.DSO;

namespace DasikAI.BT.Nodes.Blocks
{
	[AINode("Blocks/Empty")]
	public class Empty : AIBlock
	{
		public override IDataStoreObject DoWork(IDataStoreObject dataStoreObject, AgentController controller)
		{
			return dataStoreObject;
		}
	}
}