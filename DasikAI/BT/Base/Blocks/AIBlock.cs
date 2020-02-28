using System.Collections.Generic;
using DasikAI.Common.DSO;
using DasikAI.Common.Controller;
using DasikAI.BT.Base;
using XNode;

namespace DasikAI.BT.Base.Blocks
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