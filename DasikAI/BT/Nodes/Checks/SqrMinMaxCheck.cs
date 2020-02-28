using DasikAI.Common.Attributes;
using UnityEngine;

namespace DasikAI.BT.Nodes.Checks
{
	[AINode("Checks/SqrMinMaxCheck")]
	public class SqrMinMaxCheck : MinMaxCheck
	{
		public float Min { get; set; }
		public float Max { get; set; }

		protected override void Init()
		{
			base.Init();
			Min = base.Min * base.Min;
			Max = base.Max * base.Max;
		}
	}
}