using System;
using XNode;

namespace DasikAI.Common.Base.ParamSources
{
    [Serializable]
    public abstract class ParamSource : AINode
    {
        [Output(backingValue = Node.ShowBackingValue.Unconnected)]
        public AINode Consumer;

        // public abstract IEnumerable<AINode> Next(IDataStoreObject dataStoreObject, IAgentController controller);
    }

    [Serializable]
    public abstract class ParamSource<T> : ParamSource
    {
        /// <summary>
        /// for internal usage only
        /// </summary>
        /// <param name="nodeContext"></param>
        /// <returns></returns>
        public abstract T GetParam(NodeContext nodeContext);
    }
}