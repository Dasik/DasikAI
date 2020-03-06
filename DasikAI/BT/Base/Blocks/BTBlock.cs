using System.Collections.Generic;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Controller;
using DasikAI.BT.Base;
using DasikAI.Common.Base;
using XNode;

namespace DasikAI.BT.Base.Blocks
{
	public abstract class BTBlock : BTNode
	{
		[Input(ShowBackingValue.Always)] public AINode[] Parent = new AINode[1];
		[Node.Output(dynamicPortList = true, backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
		public AINode[] next;

		public abstract void DoWork(Context context);

		public override IEnumerable<AINode> Next(Context context)
		{
			return next;
		}
	}
}