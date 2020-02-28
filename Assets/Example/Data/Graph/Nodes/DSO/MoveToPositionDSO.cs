using System.Collections.Generic;
using Dasik.PathFinder;
using Dasik.PathFinder.Task;
using DasikAI.Common.DSO;

namespace DasikAI.Example.Data.Graph.Nodes.DSO
{
	public class MoveToPlayerDSO : IDataStoreObject
	{
		public SinglePathTask PathTask;
		public IEnumerator<Cell> PathEnumerator;
	}
}