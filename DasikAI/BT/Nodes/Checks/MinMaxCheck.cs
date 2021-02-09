using DasikAI.BT.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Common.Base;
using UnityEngine;

namespace DasikAI.BT.Nodes.Checks
{
    [BTNode("Checks/MinMaxCheck")]
    public class MinMaxCheck : BTBlockCheck
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)]
        public FloatParamSource paramSource;

        [SerializeField] private float _min = 0;
        [SerializeField] private float _max = 0;

        [Output] public AINode @true;
        [Output] public AINode @false;

        protected override AINode NextOne(NodeContext nodeContext)
        {
            var param = nodeContext.GetParam(paramSource);
            if (param >= _min && param <= _max)
                return @true;
            return @false;
        }
    }
}