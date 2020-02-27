using System.Collections.Generic;
using DasikAI.Controller;
using DasikAI.Data.Graph.Base.DSO;
using XNode;

namespace DasikAI.Data.Graph.Base.Blocks
{
	public abstract class AIBlock : AINode
	{
		[Input(ShowBackingValue.Always)] public AINode[] Parent = new AINode[1];
		[Node.Output(dynamicPortList = true, backingValue = ShowBackingValue.Never, connectionType = ConnectionType.Override)]
		public AINode[] next;

		public abstract IDataStoreObject DoWork(IDataStoreObject dataStoreObject, AgentController controller);

		public override IEnumerable<AINode> Next(IDataStoreObject dataStoreObject, AgentController controller)
		{
			return next;
		}
	}
}