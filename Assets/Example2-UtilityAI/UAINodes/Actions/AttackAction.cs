using System;
using DasikAI.Common.Base;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Example.Controller;
using DasikAI.UAI.Attributes;
using DasikAI.UAI.Nodes.Base.Blocks;
using DasikAI.UAI.Nodes.Blocks;
using UnityEngine;
using XNode;

namespace UAINodes.Actions
{
    [UAINode("Example2/Actions/Attack")]
    public class AttackAction : UAIAction
    {
        [Input] public FloatParamSource CoolingTime;
        [Input] public Vector2ParamSource Target;

        [Output(backingValue = Node.ShowBackingValue.Unconnected)]
        public UAIAction NextAction;

        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _force;

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            if (!(nodeContext.CharacterController is IControllerPosition2d))
                throw new ArgumentException(
                    $"{nameof(CharacterController)} must be instance of {nameof(IControllerPosition2d)}");
            
            base.OnNodeInitialize(nodeContext);
            nodeContext.CurrentDSO = new SingleValueDSO<float>(); //store time of previous fire
            nodeContext.SharedDSO.Add(typeof(AttackAction), nodeContext.CurrentDSO);
        }

        public override void PerformAction(UAINodeContext nodeContext)
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            if (Time.time - dso.Value < nodeContext.GetParam(CoolingTime))
                return;
            var bullet = Instantiate(_bullet, ((IControllerPosition2d) nodeContext.CharacterController).Position2d,
                Quaternion.identity);
            Destroy(bullet, 1f);
            var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            var force = (nodeContext.GetParam(Target) - bulletRigidbody.position).normalized * _force;
            bulletRigidbody.AddForce(force, ForceMode2D.Impulse);
            dso.Value = Time.time;
            if (NextAction == null)
                return;
            nodeContext.PerformAction(NextAction);
        }
    }
}