using System.Collections.Generic;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using UnityEngine;
using XNode;

namespace DasikAI.Common.Controller
{
    [RequireComponent(typeof(AgentController))]
    public class AIGraphController<TGraph> : MonoBehaviour
        where TGraph : NodeGraph
    {
        [SerializeField] protected TGraph graph = null;
        [SerializeField] protected AgentController agentController = null;

        protected readonly Dictionary<AINode, Context> Contexts = new Dictionary<AINode, Context>();

        protected Dictionary<object, IDataStoreObject> SharedDso { get; } =
            new Dictionary<object, IDataStoreObject>();

        public void Start()
        {
            if (agentController == null)
                agentController = GetComponent<AgentController>();
        }
    }
}