﻿using System;
using DasikAI.BT.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Common.Base;
using UnityEngine;

namespace DasikAI.BT.Nodes.Checks
{
    [BTNode("Checks/Compare")]
    public class Compare : BTBlockCheck
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)]
        public FloatParamSource ParamSource;

        [SerializeField] protected float Value = 0;
        [SerializeField] protected OperatorType OperatorType = OperatorType.Equal;

        [Output] public AINode @true;
        [Output] public AINode @false;

        protected override AINode NextOne(NodeContext nodeContext)
        {
            var param = nodeContext.GetParam(ParamSource);
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