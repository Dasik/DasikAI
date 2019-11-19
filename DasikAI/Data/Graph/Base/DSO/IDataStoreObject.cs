using System.Collections.Generic;

namespace DasikAI.Data.Graph.Base.DSO
{
	public interface IDataStoreObject { }

	public struct DataStoreObject : IDataStoreObject
	{
		public IDictionary<object, object> Params { get; set; }
	}
}
