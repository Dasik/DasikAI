using DasikAI.Common.Base.ParamSources;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.BT.Editor.Data.Graph
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
