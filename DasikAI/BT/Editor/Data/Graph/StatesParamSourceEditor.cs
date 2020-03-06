using DasikAI.BT.Base;
using DasikAI.BT.Nodes.ParamSources;
using UnityEngine;

namespace DasikAI.BT.Editor.Data.Graph
{
	[CustomNodeEditor(typeof(StatesParamSource))]
	public class StatesParamSourceEditor : ParamSourceEditor
	{
		public override void OnBodyGUI()
		{
			base.OnBodyGUI();
			var graph = target.graph as BTGraph;
			if (GUILayout.Button("Update")) graph.UpdateStates();
		}
	}
}