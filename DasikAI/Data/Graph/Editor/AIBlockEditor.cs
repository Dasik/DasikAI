using DasikAI.Controller;
using DasikAI.Data.Graph.Base;
using DasikAI.Data.Graph.Base.Blocks;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.Data.Graph.Editor
{
	[CustomNodeEditor(typeof(AIBlock))]
	public class AIBlockEditor : NodeEditor
	{
		public override void OnHeaderGUI()
		{
			GUI.color = Color.white;
			var node = target as AIBlock;
			var graph = node.graph as AIGraph;
			if (graph.Root == node) GUI.color = Color.blue;
			string title = target.name;
			if (graph.Root == node)
				title += "(root)";
			GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
			GUI.color = Color.white;
		}

		public override void OnBodyGUI()
		{
			base.OnBodyGUI();
			var node = target as AIBlock;
			var graph = node.graph as AIGraph;
			if (graph.Root != node)
				if (GUILayout.Button("Set as root"))
					graph.Root = node;
		}

		public override Color GetTint()
		{
			if (Selection.activeGameObject != null)
			{
				var node = target as AINode;
				var graphBehaviour = Selection.activeGameObject.GetComponent<AIGraphBehaviourController>();
				if (graphBehaviour != null && graphBehaviour.ActiveNodes.Contains(node))
				{
					return Color.green;
				}
			}

			return base.GetTint();
		}
	}
}