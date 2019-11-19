using System;

namespace DasikAI.Data.Graph.Attributes
{
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public class AINodeAttribute : Attribute
	{
		public string editorName { get; private set; }

		public AINodeAttribute(string editorName)
		{
			this.editorName = editorName;
		}
	}
}
