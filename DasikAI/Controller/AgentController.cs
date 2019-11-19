using System;
using UnityEngine;

namespace DasikAI.Controller
{
	[RequireComponent(typeof(Rigidbody2D))]
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

		public Vector2 Position2d
		{
			get { return Transform.position; }
		}

		private Rigidbody2D _rigidbody2D;
		public Rigidbody2D Rigidbody2D
		{
			get
			{
				if (_rigidbody2D == null)
					_rigidbody2D = GetComponent<Rigidbody2D>();
				return _rigidbody2D;
			}
		}

		private AIGraphBehaviourController _graphBehaviourController;

		public AIGraphBehaviourController GraphBehaviourController
		{
			get
			{
				if (_graphBehaviourController == null)
					_graphBehaviourController = GetComponent<AIGraphBehaviourController>();
				return _graphBehaviourController;
			}
		}
	}
}