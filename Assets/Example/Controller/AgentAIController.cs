using System;
using DasikAI.Controller;
using DasikAI.Example.Controller;
using UnityEngine;

public class AgentAIController : AgentController, IMovementController
{
	public float SpeedLimit;

	public Action<Vector2> OnMoveDirectionChanged { get; set; }
	private Vector2 velocity;

	protected virtual void Start()
	{
		OnMoveDirectionChanged = direction => { velocity = direction.normalized * SpeedLimit; };
	}

	void FixedUpdate()
	{
		Rigidbody2D.velocity = velocity;
	}

	public virtual void OnHealthOver()
	{
		Destroy(gameObject);
	}
}