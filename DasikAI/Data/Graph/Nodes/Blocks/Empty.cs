using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;

namespace DasikAI.Scripts.Data.Graph.Nodes.Blocks
{
	[AINode("Blocks/Empty")]
	public class Empty : AIBlock
	{
		public override IDataStoreObject DoWork(IDataStoreObject dataStoreObject, AgentController controller)
		{
			return dataStoreObject;
		}
	}
}
