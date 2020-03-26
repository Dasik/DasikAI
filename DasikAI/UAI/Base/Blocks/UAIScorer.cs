using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;
using XNode;

namespace DasikAI.UAI.Base.Blocks
{
    public abstract class UAIScorer : ParamSource<float>
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)]
        private FloatParamSource _paramSource;

        [Output] private AINode _next;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(_next))
                return GetScore();
            return this; //todo: throw exception?
        }

        public abstract double GetScore();
    }
}