using UnityEngine;

namespace DasikAI.Example
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerController : MonoBehaviour
	{
		public static PlayerController Instance;
		public float Speed = 5f;

		private Vector2 _targetPosition;

		public Vector2 Position2d
		{
			get { return _transform.position; }
		}

		private Transform _transform;

		private Rigidbody2D _rigidbody2D;

		void Awake()
		{
			_transform = GetComponent<Transform>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_targetPosition = Position2d;

			if (Instance == null)
				Instance = this;
			else Destroy(gameObject);
		}

		void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				_targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				_rigidbody2D.velocity = (_targetPosition - Position2d).normalized * Speed;
			}

			if ((_targetPosition - Position2d).sqrMagnitude < 0.1)
				_rigidbody2D.velocity = Vector2.zero;
		}
	}
}