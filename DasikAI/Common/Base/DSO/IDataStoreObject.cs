using System.Collections.Generic;

namespace DasikAI.Common.Base.DSO
{
	public interface IDataStoreObject { }

	public struct DataStoreObject : IDataStoreObject
	{
		public IDictionary<object, object> Params { get; set; }
	}
}
