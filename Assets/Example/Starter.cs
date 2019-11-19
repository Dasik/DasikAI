using System.Collections;
using System.Collections.Generic;
using Dasik.PathFinder;
using UnityEngine;

namespace DasikAI.Example
{
	public class Starter : MonoBehaviour
	{
		// Start is called before the first frame update
		void Start()
		{
			Map.Instance.ScanArea(new Vector2(-100f, -100f), new Vector2(100f, 100f), 0.5f);
		}

		// Update is called once per frame
		void Update()
		{
		}
	}
}