using DasikAI.BT.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base;
using DasikAI.Example.Controller;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.Blocks
{
    [BTNode("Example/Blocks/Attack")]
    public class Attack : BTBlock
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _coolingTime;
        [SerializeField] private float _force;

        public override void OnNodeInitialize(NodeContext nodeContext)
        {
            base.OnNodeInitialize(nodeContext);
            nodeContext.CurrentDSO = new SingleValueDSO<float>();
        }

        public override void OnNodeEnter(NodeContext nodeContext)
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            dso.Value = Time.time; //store time of previous fire
        }

        public override void DoWork(NodeContext nodeContext)
        {
            var dso = (SingleValueDSO<float>) nodeContext.CurrentDSO;
            if (Time.time - dso.Value < _coolingTime)
                return;
            var bullet = Instantiate(_bullet, ((IControllerPosition2d) nodeContext.CharacterController).Position2d,
                Quaternion.identity);
            Destroy(bullet, 1f);
            var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            var force = (PlayerController.Instance.Position2d - bulletRigidbody.position).normalized * _force;
            bulletRigidbody.AddForce(force, ForceMode2D.Impulse);
            dso.Value = Time.time;
        }
    }
}