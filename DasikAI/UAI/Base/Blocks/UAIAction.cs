using DasikAI.Common.Base;
using DasikAI.Common.Base.ParamSources;

namespace DasikAI.UAI.Base.Blocks
{
    public abstract class UAIAction : UAINode
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override, TypeConstraint.None)]
        public UAIScorer Scorer;

        public abstract void PerformAction(Context context);
    }
}