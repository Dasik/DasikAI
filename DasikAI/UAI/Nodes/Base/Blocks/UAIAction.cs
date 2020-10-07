using DasikAI.UAI.Nodes.Blocks;

namespace DasikAI.UAI.Nodes.Base.Blocks
{
    public abstract class UAIAction : UAINode
    {
        [Input(ShowBackingValue.Always, ConnectionType.Override)]
        public UAIScorer Scorer;

        /// <summary>
        /// Called for best action
        /// </summary>
        /// <param name="nodeContext"></param>
        public abstract void PerformAction(UAINodeContext nodeContext);
    }
}