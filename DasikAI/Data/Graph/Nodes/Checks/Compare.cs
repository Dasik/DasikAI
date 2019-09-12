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
		[SerializeField] private float _value;
		[SerializeField] private OperatorType _operatorType;

		[Node.Output] public AINode @true;
		[Node.Output] public AINode @false;

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var param = ParamSource.GetParam(controller);
			var result = false;
			switch (_operatorType)
			{
				case OperatorType.Less:
					result = param < _value;
					break;
				case OperatorType.LessOrEqual:
					result = param <= _value;
					break;
				case OperatorType.Equal:
					result = Math.Abs(param - _value) < 0.000000001f;
					break;
				case OperatorType.GreaterOrEqual:
					result = param >= _value;
					break;
				case OperatorType.Greater:
					result = param > _value;
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