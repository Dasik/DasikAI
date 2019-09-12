using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using DasikAI.Scripts.Data.Graph.Nodes.DSO;
using UnityEngine;
using XNode;

namespace DasikAI.Scripts.Data.Graph.Nodes.Checks
{
	[AINode("Checks/MinMaxCheck")]
	public class MinMaxCheck : AIBlockCheck
	{
		[Input(ShowBackingValue.Always,ConnectionType.Override,TypeConstraint.None)] public ParamSource ParamSource;
		[SerializeField] private float _min;
		[SerializeField] private float _max;

		[Node.Output] public AINode @true;
		[Node.Output] public AINode @false;

		public override IDataStoreObject Initialize(AgentController controller)
		{
			base.Initialize(controller);
			var dso = new MinMaxDSO {Max = _max, Min = _min};
			return (IDataStoreObject) dso;
		}

		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			var dso = (MinMaxDSO) dataStoreObject;
			var param = ParamSource.GetParam(controller);
			if (param >= dso.Min && param <= dso.Max)
				return @true;
			return @false;
		}
	}
}