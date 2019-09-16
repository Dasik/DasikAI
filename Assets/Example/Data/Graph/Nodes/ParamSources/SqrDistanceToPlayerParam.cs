using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using XNode;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
	[AINode("Params/SqrDistanceToPlayer")]
	public class SqrDistanceToPlayerParam : ParamSource
	{
		[Node.Output(ShowBackingValue.Unconnected)]
		public AINode Consumer;
		protected override object GetParameterValue<T>(AgentController agentController)
		{
			var distance = (agentController.Position2d - PlayerController.Instance.Position2d).sqrMagnitude;
			return distance;
		}
	}
}
