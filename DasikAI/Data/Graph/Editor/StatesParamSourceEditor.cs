using DasikAI.Data.Graph.Base;
using DasikAI.Data.Graph.Nodes.ParamSources;
using UnityEngine;

namespace DasikAI.Data.Graph.Editor
{
	[CustomNodeEditor(typeof(StatesParamSource))]
	public class StatesParamSourceEditor : ParamSourceEditor
	{
		public override void OnBodyGUI()
		{
			base.OnBodyGUI();
			var graph = target.graph as AIGraph;
			if (GUILayout.Button("Update")) graph.UpdateStates();
		}
	}
}