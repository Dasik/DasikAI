using UnityEngine;

namespace DasikAI.Data.Attributes
{
	[System.AttributeUsage(System.AttributeTargets.Field)]
	public class SerializeProperty : PropertyAttribute
	{
		public string PropertyName { get; private set; }

		public SerializeProperty(string propertyName)
		{
			PropertyName = propertyName;
		}
	}
}