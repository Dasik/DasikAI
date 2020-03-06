using System.Collections.Generic;
using System.Linq;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Controller;
using DasikAI.BT.Base;
using DasikAI.Common.Base;

namespace DasikAI.BT.Base.Blocks
{
	public abstract class BTBlockCheck : BTNode
	{
		[Input(ShowBackingValue.Always)] public AINode[] Parent = new AINode[1];
		public override object GetValue(XNode.NodePort port)
		{
			var result = this as AINode;

			return result;
		}

		protected virtual AINode NextOne(Context context)
		{
			return null;
		}

		public override IEnumerable<AINode> Next(Context context)
		{
			var nextOne = NextOne(context);
			return nextOne == null ? Enumerable.Empty<AINode>() : Enumerable.Repeat(nextOne, 1);
		}
	}
}
