using DasikAI.Example.Data.Graph.Nodes.ParamSources;
using DasikAI.Scripts.Data.Graph.Base;
using UnityEngine;

namespace Assets.DasikAI.Scripts.Data.Graph.Editor
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
