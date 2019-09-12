using DasikAI.Scripts.Data.Graph.Base;
using UnityEngine;
using XNodeEditor;

namespace Assets.DasikAI.Scripts.Data.Graph.Editor
{
	[NodeEditor.CustomNodeEditorAttribute(typeof(ParamSource))]
	public class ParamSourceEditor : NodeEditor
	{
		public override Color GetTint()
		{
			return Color.magenta;
		}
	}
}
