﻿﻿using System;
using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.DSO;
using DasikAI.BT.Base.ParamSources;
using UnityEngine;
using XNode;

namespace DasikAI.BT.Nodes.Checks
{
	[AINode("Checks/Compare")]
	public class Compare : AIBlockCheck
	{
		[Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)]
		public FloatParamSource ParamSource;

		[SerializeField] protected float Value = 0;
		[SerializeField] protected OperatorType OperatorType = OperatorType.Equal;

		[Node.Output] public AINode @true;
		[Node.Output] public AINode @false;

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var param = ParamSource.GetParam(controller);
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