using UnityEngine;

namespace DasikAI.Example
{
	public class PlayerController : MonoBehaviour
	{
		public static PlayerController Instance;

		public Vector2 Position2d
		{
			get { return _transform.position; }
		}

		private Transform _transform;

		void Awake()
		{
			_transform = GetComponent<Transform>();

			if (Instance != null)
				Instance = this;
			else Destroy(gameObject);
		}
	}
}