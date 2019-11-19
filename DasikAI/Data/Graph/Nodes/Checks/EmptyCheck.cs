using DasikAI.Controller;
using DasikAI.Data.Graph.Attributes;
using DasikAI.Data.Graph.Base;
using DasikAI.Data.Graph.Base.Blocks;
using DasikAI.Data.Graph.Base.DSO;
using XNode;

namespace DasikAI.Data.Graph.Nodes.Checks
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