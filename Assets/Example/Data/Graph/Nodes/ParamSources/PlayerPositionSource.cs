using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.ParamSources
{
	[AINode("Example/Params/PlayerPostion")]
	public class PlayerPositionSource : Vector2ParamSource
	{
		[SerializeField] private float _randomizedRadius = 10f;

		public override Vector2 GetParam(Context context)
		{
			var playerPos = PlayerController.Instance.Position2d;
			playerPos += Random.insideUnitCircle * _randomizedRadius;
			return playerPos;
		}
	}
}