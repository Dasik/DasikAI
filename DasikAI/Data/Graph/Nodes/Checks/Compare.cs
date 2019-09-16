using System;
using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using UnityEngine;
using XNode;

namespace DasikAI.Scripts.Data.Graph.Nodes.Checks
{
	[AINode("Checks/Compare")]
	public class Compare : AIBlockCheck
	{
		[Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)] public ParamSource ParamSource;
		[SerializeField] protected float Value = 0;
		[SerializeField] protected OperatorType OperatorType = OperatorType.Equal;

		[Node.Output] public AINode @true;
		[Node.Output] public AINode @false;

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var param = ParamSource.GetParam<float>(controller);
			var result = false;
			switch (OperatorType)
			{
				case OperatorType.Less:
					result = param < Value;
					break;
				case OperatorType.LessOrEqual:
					result = param <= Value;
					break;
				case OperatorType.Equal:
					result = Math.Abs(param - Value) < 0.000000001f;
					break;
				case OperatorType.GreaterOrEqual:
					result = param >= Value;
					break;
				case OperatorType.Greater:
					result = param > Value;
					break;
			}
			return result ? @true : @false;
		}
	}

	public enum OperatorType
	{
		Less,
		LessOrEqual,
		Equal,
		GreaterOrEqual,
		Greater,
	}
}