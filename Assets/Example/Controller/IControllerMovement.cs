using System;
using DasikAI.Common.Controller;
using UnityEngine;

namespace DasikAI.Example.Controller
{
	interface IControllerMovement:IAgentController
	{
		Action<Vector2> OnMoveDirectionChanged { get; set; }
	}
}
