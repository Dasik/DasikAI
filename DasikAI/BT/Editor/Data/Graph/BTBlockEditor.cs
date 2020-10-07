using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.BT.Controller;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.BT.Editor.Data.Graph
{
	[CustomNodeEditor(typeof(BTBlock))]
	public class BTBlockEditor : NodeEditor
	{
		public override void OnHeaderGUI()
		{
			GUI.color = Color.white;
			var node = target as BTBlock;
			var graph = node.graph as BTGraph;
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
			var node = target as BTBlock;
			var graph = node.graph as BTGraph;
			if (graph.Root != node)
				if (GUILayout.Button("Set as root"))
					graph.Root = node;
		}

		public override Color GetTint()
		{
			if (Selection.activeGameObject != null)
			{
				var node = target as BTNode;
				var graphBehaviour = Selection.activeGameObject.GetComponent<BtGraphController>();
				if (graphBehaviour != null && graphBehaviour.ActiveNodes.Contains(node))
				{
					return Color.green;
				}
			}

			return base.GetTint();
		}
	}
}