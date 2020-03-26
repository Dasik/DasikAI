using System.Collections.Generic;
using DasikAI.Common.Controller;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;

namespace DasikAI.Common.Base
{
    public class Context
    {
        public IAgentController AgentController { get; }
        public IDataStoreObject CurrentDSO { get; set; }
        public Dictionary<object, IDataStoreObject> SharedDSO { get; }
        public Dictionary<AINode, Context> Contexts { get; }

        public Context(IAgentController agentController, Dictionary<object, IDataStoreObject> sharedDso, Dictionary<AINode, Context> contexts)
        {
            SharedDSO = sharedDso;
            Contexts = contexts;
            AgentController = agentController;
        }

        public T GetParam<T>(ParamSource<T> paramSource)
        {
            return paramSource.GetParam(Contexts[paramSource]);
        }
    }
}