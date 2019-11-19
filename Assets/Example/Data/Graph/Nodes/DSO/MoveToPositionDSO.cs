using System.Collections.Generic;
using Dasik.PathFinder;
using Dasik.PathFinder.Task;
using DasikAI.Data.Graph.Base.DSO;

namespace DasikAI.Example.Data.Graph.Nodes.DSO
{
	public class MoveToPlayerDSO : IDataStoreObject
	{
		public SinglePathTask PathTask;
		public IEnumerator<Cell> PathEnumerator;
	}
}