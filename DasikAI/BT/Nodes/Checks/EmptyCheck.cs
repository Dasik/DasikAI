﻿﻿using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
  using DasikAI.Common.Base;
  using XNode;

namespace DasikAI.BT.Nodes.Checks
{
	[AINode("Checks/EmptyCheck")]
	public class EmptyCheck : BTBlockCheck
	{
		[Node.Output] public AINode next;

		protected override AINode NextOne(Context context)
		{
			return next;
		}
	}
}