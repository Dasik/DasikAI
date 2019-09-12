using UnityEngine;
using XNode;

namespace DasikAI.Scripts.Data.Graph.Base
{
	[CreateAssetMenu(fileName = "New AI State Graph", menuName = "xNode graph/AI State Graph")]
	public class AIGraph : NodeGraph
	{
		public AIBlock root;
	}
}