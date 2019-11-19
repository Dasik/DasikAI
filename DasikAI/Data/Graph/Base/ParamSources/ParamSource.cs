using DasikAI.Controller;
using XNode;

namespace DasikAI.Data.Graph.Base.ParamSources
{
	[System.Serializable]
	public abstract class ParamSource<T> : Node
	{
		[Node.Output(backingValue = ShowBackingValue.Unconnected)]
		public AINode Consumer;

		public abstract T GetParam(AgentController agentController);

		public override object GetValue(NodePort port)
		{
			return this;
		}
	}
}