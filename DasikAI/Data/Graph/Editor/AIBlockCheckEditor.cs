using DasikAI.Controller;
using DasikAI.Data.Graph.Base;
using DasikAI.Data.Graph.Base.Blocks;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.Data.Graph.Editor
{
	[CustomNodeEditor(typeof(AIBlockCheck))]
	public class AIBlockCheckEditor : NodeEditor
	{
		/// <summary> Returns color for target node </summary>
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