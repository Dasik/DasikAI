using System;
using DasikAI.BT.Controller;
using DasikAI.Common.Controller;
using DasikAI.Example.Controller;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AgentAiController : AgentController, IControllerMovement, IControllerPosition2d
{
    private Rigidbody2D _rigidbody2D;
    public float SpeedLimit;
    private Vector2 _velocity;

    public Action<Vector2> OnMoveDirectionChanged { get; set; }

    // public Vector2 Position2d => new Vector2(Transform.position.x, Transform.position.y);
    public Vector2 Position2d
    {
        get => new Vector2(Transform.position.x, Transform.position.y);
        set
        {
            var pos = Transform.position;
            pos.x = value.x;
            pos.y = value.y;
            Transform.position = pos;
        }
    }

    protected virtual void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        OnMoveDirectionChanged = direction => { _velocity = direction.normalized * SpeedLimit; };
    }

    void FixedUpdate()
    {
        _rigidbody2D.velocity = _velocity;
    }

    public virtual void OnHealthOver()
    {
        Destroy(gameObject);
    }
}