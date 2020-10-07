using DasikAI.Common.Controller;
using DasikAI.UAI.Nodes.Base;
using DasikAI.UAI.Nodes.Base.Blocks;
using DasikAI.UAI.Nodes.Blocks;
using Unity.Profiling;

namespace DasikAI.UAI.Controller
{
    public abstract class UAIGraphController : AIGraphController<UAIGraph>
    {
        private static ProfilerMarker s_PerformActionProfilerMarker =
            new ProfilerMarker(nameof(LazyCachedUAIGraphController) + "@Perform action");

        public abstract bool IsActionActive(UAIAction action);

        public virtual void PerformAction(UAIAction action)
        {
            using (s_PerformActionProfilerMarker.Auto())
            {
                action.PerformAction((UAINodeContext) Contexts[action]);
            }
        }
    }
}