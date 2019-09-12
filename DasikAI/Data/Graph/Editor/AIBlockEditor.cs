using DasikAI.Scripts.Controller;
using DasikAI.Scripts.Data.Graph.Base;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace Assets.DasikAI.Scripts.Data.Graph.Editor
{
	[CustomNodeEditor(typeof(AIBlock))]
	public class AIBlockEditor : NodeEditor
	{

		public override void OnHeaderGUI()
		{
			GUI.color = Color.white;
			var node = target as AIBlock;
			var graph = node.graph as AIGraph;
			if (graph.root == node) GUI.color = Color.blue;
			string title = target.name;
			if (graph.root == node)
				title += "(root)";
			GUILayout.Label(title, NodeEditorResources.styles.nodeHeader, GUILayout.Height(30));
			GUI.color = Color.white;
		}

		public override void OnBodyGUI()
		{
			base.OnBodyGUI();
			var node = target as AIBlock;
			var graph = node.graph as AIGraph;
			if (graph.root != node)
				if (GUILayout.Button("Set as root")) graph.root = node;
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
