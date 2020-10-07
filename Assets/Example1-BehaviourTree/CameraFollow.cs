using UnityEngine;

namespace DasikAI.Example
{
	public class CameraFollow : MonoBehaviour
	{
		private Transform _transform;

		void Awake()
		{
			_transform = transform;
		}

		void Update()
		{
			if (PlayerController.Instance != null)
			{
				Vector3 pos = PlayerController.Instance.Position2d;
				pos.z = _transform.position.z;
				_transform.position = pos;
			}
		}
	}
}