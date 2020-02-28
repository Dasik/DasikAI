using System;
using System.Collections.Generic;
using DasikAI.Common.Controller;
using DasikAI.Common.DSO;
using XNode;

namespace DasikAI.BT.Base.ParamSources
{
	[Serializable]
	public abstract class ParamSource : AINode
	{
		[Node.Output(backingValue = ShowBackingValue.Unconnected)]
		public AINode Consumer;

		public override IEnumerable<AINode> Next(IDataStoreObject dataStoreObject, AgentController controller)
		{
			throw new NotImplementedException();
		}
	}

	[Serializable]
	public abstract class ParamSource<T> : ParamSource
	{
		public abstract T GetParam(AgentController agentController);
	}
}