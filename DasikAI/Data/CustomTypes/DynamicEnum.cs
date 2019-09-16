using System;
using UnityEngine;

namespace DasikAI.Scripts.Data.CustomTypes
{
	[Serializable]
	public class DynamicEnum<T>
	{
		[SerializeField] [HideInInspector] public T[] Values { get; set; } = { default };
		[SerializeField] [HideInInspector] public int SelectedIndex { get; set; } = 0;

		public T SelectedValue
		{
			get { return Values[SelectedIndex]; }
			set
			{
				for (int i = 0; i < Values.Length; i++)
					if (Values[i].Equals(value))
					{
						SelectedIndex = i;
						return;
					}
			}
		}
	}
}
