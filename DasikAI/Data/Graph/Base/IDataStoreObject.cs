using System.Collections.Generic;

namespace DasikAI.Scripts.Data.Graph.Base
{
	public interface IDataStoreObject { }

	public struct DataStoreObject : IDataStoreObject
	{
		public IDictionary<object, object> Params { get; set; }
	}
}
