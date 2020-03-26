using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.Base.DSO;
using DasikAI.Common.Base;
using DasikAI.Example.Controller;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.Blocks
{
    [AINode("Example/Blocks/Attack")]
    public class Attack : BTBlock
    {
        [SerializeField] private GameObject _bullet;
        [SerializeField] private float _coolingTime;
        [SerializeField] private float _force;

        public override void OnInitialize(Context context)
        {
            base.OnInitialize(context);
            context.CurrentDSO = new SingleValueDSO<float>(); //store time of previous fire
        }

        public override void OnEnter(Context context)
        {
            var dso = (SingleValueDSO<float>) context.CurrentDSO;
            dso.Value = Time.time;
        }

        public override void DoWork(Context context)
        {
            var dso = (SingleValueDSO<float>) context.CurrentDSO;
            if (Time.time - dso.Value < _coolingTime)
                return;
            var bullet = Instantiate(_bullet, ((IControllerPosition2d) context.AgentController).Position2d, Quaternion.identity);
            Destroy(bullet, 1f);
            var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
            var force = (PlayerController.Instance.Position2d - bulletRigidbody.position).normalized * _force;
            bulletRigidbody.AddForce(force, ForceMode2D.Impulse);
            dso.Value = Time.time;
        }
    }
}