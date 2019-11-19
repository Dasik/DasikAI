using DasikAI.Controller;
using DasikAI.Data.Graph.Attributes;
using DasikAI.Data.Graph.Base.ParamSources;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
	[AINode("Example/Params/PlayerPostion")]
	public class PlayerPositionSource : Vector2ParamSource
	{
		[SerializeField] private float _randomizedRadius = 10f;

		public override Vector2 GetParam(AgentController agentController)
		{
			var playerPos = PlayerController.Instance.Position2d;
			playerPos += Random.insideUnitCircle * _randomizedRadius;
			return playerPos;
		}
	}
}