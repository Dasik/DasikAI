using DasikAI.Data.Graph.Base.ParamSources;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.Data.Graph.Editor
{
	[NodeEditor.CustomNodeEditor(typeof(ParamSource))]
	public class ParamSourceEditor : NodeEditor
	{
		public override Color GetTint()
		{
			return Color.magenta;
		}
	}
}
