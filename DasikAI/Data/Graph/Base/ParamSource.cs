using DasikAI.Scripts.Controller;
using XNode;

namespace DasikAI.Scripts.Data.Graph.Base
{
	public abstract class ParamSource : Node
	{
		public T GetParam<T>(AgentController agentController)
		{
			return (T)GetParameterValue<T>(agentController);
		}
		protected abstract object GetParameterValue<T>(AgentController agentController);

		public override object GetValue(NodePort port)
		{
			return this;
		}
	}
}
