using DasikAI.Common.Controller;
using UnityEngine;
using XNode;

namespace DasikAI.Common.Controller
{
    public abstract class CharacterController : MonoBehaviour, ICharacterController
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

    public abstract class CharacterController<TGraph> : CharacterController
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