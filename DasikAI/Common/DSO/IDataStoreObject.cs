using System.Collections.Generic;

namespace DasikAI.Common.DSO
{
	public interface IDataStoreObject { }

	public struct DataStoreObject : IDataStoreObject
	{
		public IDictionary<object, object> Params { get; set; }
	}
}
