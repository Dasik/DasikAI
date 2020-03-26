using DasikAI.Common.Controller;
using UnityEngine;
using XNode;

namespace DasikAI.Common.Controller
{
    public abstract class AgentController : MonoBehaviour,IAgentController
    {
        private Transform _transform;

        public Transform Transform
        {
            get
            {
                if (_transform == null)
                    _transform = GetComponent<Transform>();
                return _transform;
            }
        }
    }

    public abstract class AgentController<TGraph> : AgentController
        where TGraph : NodeGraph
    {
        private AIGraphController<TGraph> _graphController;

        public AIGraphController<TGraph> GraphController
        {
            get
            {
                if (_graphController == null)
                    _graphController = GetComponent<AIGraphController<TGraph>>();
                return _graphController;
            }
        }
    }
}