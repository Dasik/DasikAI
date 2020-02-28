using System;

namespace DasikAI.Common.DSO
{
	/// <summary>
	/// 
	/// </summary>
	/// <typeparam name="T">must be enum</typeparam>
	public interface IStateDSO<T> : IDataStoreObject where T : IComparable, IConvertible
	{
		T State { get; set; }
	}
}