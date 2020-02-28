using DasikAI.Common.Controller;
using DasikAI.Common.Attributes;
using DasikAI.BT.Base.Blocks;
using DasikAI.Common.DSO;
using DasikAI.BT.Nodes.DSO;
using UnityEngine;

namespace DasikAI.Example.Data.Graph.Nodes.Blocks
{
	[AINode("Example/Blocks/Attack")]
	public class Attack : AIBlock
	{
		[SerializeField] private GameObject _bullet;
		[SerializeField] private float _coolingTime;
		[SerializeField] private float _force;

		public override IDataStoreObject Initialize(AgentController controller)
		{
			base.Initialize(controller);
			return new SingleValueDSO<float>(); //store time of previous fire
		}

		public override IDataStoreObject Enter(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var dso = (SingleValueDSO<float>)dataStoreObject;
			dso.Value = Time.time;
			return base.Enter(dataStoreObject, controller);
		}

		public override IDataStoreObject DoWork(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var dso = (SingleValueDSO<float>)dataStoreObject;
			if (Time.time - dso.Value < _coolingTime)
				return dso;
			var bullet = Instantiate(_bullet, ((AgentAIController)controller).Position2d, Quaternion.identity);
			Destroy(bullet, 1f);
			var bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
			var force = (PlayerController.Instance.Position2d - bulletRigidbody.position).normalized * _force;
			bulletRigidbody.AddForce(force, ForceMode2D.Impulse);
			dso.Value = Time.time;
			return dso;
		}
	}
}