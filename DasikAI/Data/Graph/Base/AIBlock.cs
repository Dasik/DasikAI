using System.Collections.Generic;
using DasikAI.Scripts.Controller;
using XNode;

namespace DasikAI.Scripts.Data.Graph.Base
{
	public abstract class AIBlock : AINode
	{
		[Node.Output(dynamicPortList = true,backingValue = ShowBackingValue.Never,connectionType = ConnectionType.Override)] public AINode[] next;
		public abstract IDataStoreObject DoWork(IDataStoreObject dataStoreObject, AgentController controller);

		public override IEnumerable<AINode> Next(IDataStoreObject dataStoreObject, AgentController controller)
		{
			return next;
		}
	}
}