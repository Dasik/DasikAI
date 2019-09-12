using System;
using UnityEngine;

namespace DasikAI.Example.Controller
{
	interface IMovementController
	{
		Action<Vector2> OnMoveDirectionChanged { get; set; }
	}
}
