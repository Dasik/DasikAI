using DasikAI.Common.Base.ParamSources;
using XNode;

namespace DasikAI.Common.Base
{
    public abstract class AIScorer : FloatParamSource
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)]
        private FloatParamSource _paramSource;
        [Output] private AINode _next;

        public override object GetValue(NodePort port)
        {
            if (port.fieldName == nameof(_next))
                return GetScore();
            return this;//todo: throw exception?
        }

        public abstract double GetScore();
    }
}