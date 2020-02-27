using DasikAI.Controller;
using DasikAI.Data.Graph.Attributes;
using DasikAI.Data.Graph.Base.ParamSources;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
	[AINode("Example/Params/SqrDistanceToPlayer")]
	public class SqrDistanceToPlayerParam : FloatParamSource
	{
		public override float GetParam(AgentController agentController)
		{
			var distance = (((AgentAIController)agentController).Position2d - PlayerController.Instance.Position2d).sqrMagnitude;
			return distance;
		}
	}
}