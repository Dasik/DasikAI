using System;
using XNode;

namespace DasikAI.Common.Base.ParamSources
{
	[Serializable]
	public abstract class ParamSource : AINode
	{
		[Node.Output(backingValue = Node.ShowBackingValue.Unconnected)]
		public AINode Consumer;

		// public abstract IEnumerable<AINode> Next(IDataStoreObject dataStoreObject, IAgentController controller);
	}

	[Serializable]
	public abstract class ParamSource<T> : ParamSource
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context"></param>
		/// <returns>for internal usage only</returns>
		public abstract T GetParam(Context context);
	}
}