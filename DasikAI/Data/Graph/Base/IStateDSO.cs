using System;

namespace DasikAI.Scripts.Data.Graph.Base
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">must be enum</typeparam>
	public interface IStateDSO<T> : IDataStoreObject where T : struct, IComparable, IFormattable, IConvertible
	{
		T State { get; set; }
	}
}