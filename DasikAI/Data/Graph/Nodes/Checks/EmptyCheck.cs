using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using XNode;

namespace DasikAI.Scripts.Data.Graph.Nodes.Checks
{
	[AINode("Checks/EmptyCheck")]
	public class EmptyCheck : AIBlockCheck
	{
		[Node.Output] public AINode next;
		public override AINode NextOne(IDataStoreObject dataStoreObject, AgentController controller)
		{
			return next;
		}
	}
}
