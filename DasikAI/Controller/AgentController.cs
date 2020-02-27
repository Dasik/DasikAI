using UnityEngine;

namespace DasikAI.Controller
{
	public abstract class AgentController : MonoBehaviour
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

		private AIGraphBehaviourController _graphController;

		public AIGraphBehaviourController GraphController
		{
			get
			{
				if (_graphController == null)
					_graphController = GetComponent<AIGraphBehaviourController>();
				return _graphController;
			}
		}
	}
}