using System.Collections.Generic;
using System.Linq;
using DasikAI.Scripts.Controller;

namespace DasikAI.Scripts.Data.Graph.Base
{
	public abstract class AIBlockCheck : AINode
	{
		public override object GetValue(XNode.NodePort port)
		{
			var result = this as AINode;

			return result;
		}

		public virtual AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			return null;
		}

		public override IEnumerable<AINode> Next(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var nextOne = NextOne(dataStoreObject, controller);
			return nextOne == null ? Enumerable.Empty<AINode>() : Enumerable.Repeat(nextOne, 1);
		}
	}
}
