using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
	[AINode("Params/SqrDistanceToPlayer")]
	public class SqrDistanceToPlayerParam : ParamSource
	{
		public override float GetParam(AgentController agentController)
		{
			var distance = (agentController.Position2d - PlayerController.Instance.Position2d).sqrMagnitude;
			return distance;
		}
	}
}
