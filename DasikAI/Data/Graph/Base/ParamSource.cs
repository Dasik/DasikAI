using DasikAI.Scripts.Controller;
using XNode;

namespace DasikAI.Scripts.Data.Graph.Base
{
	public abstract class ParamSource : Node
	{
		[Node.Output(ShowBackingValue.Unconnected)]
		public AINode Consumer;

		public abstract float GetParam(AgentController agentController);

		public override object GetValue(NodePort port)
		{
			return this;
		}
	}
}
