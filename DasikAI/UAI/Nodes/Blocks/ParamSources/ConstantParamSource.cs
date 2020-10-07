using DasikAI.Common.Attributes;
using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using UnityEngine;

namespace DasikAI.UAI.Nodes.Blocks.ParamSources
{
    [AINode("UAI/Param Sources/Constant")]
    public class ConstantParamSource : FloatParamSource
    {
        [SerializeField] private float _value;

        public override float GetParam(NodeContext nodeContext)
        {
            return _value;
        }
    }
}