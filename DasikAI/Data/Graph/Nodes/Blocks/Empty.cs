using DasikAI.Controller;
using DasikAI.Data.Graph.Attributes;
using DasikAI.Data.Graph.Base.Blocks;
using DasikAI.Data.Graph.Base.DSO;

namespace DasikAI.Data.Graph.Nodes.Blocks
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