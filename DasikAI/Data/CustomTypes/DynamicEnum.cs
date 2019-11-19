using System;
using UnityEngine;

namespace DasikAI.Data.CustomTypes
{
	[Serializable]
	public class DynamicEnum<T>
	{
		[SerializeField] [HideInInspector] public T[] Values = {default};
		[SerializeField] [HideInInspector] public int SelectedIndex;

		public T SelectedValue
		{
			get => Values[SelectedIndex];
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