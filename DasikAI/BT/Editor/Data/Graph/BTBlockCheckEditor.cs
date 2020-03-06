using DasikAI.Common.Controller;
using DasikAI.BT.Base;
using DasikAI.BT.Base.Blocks;
using DasikAI.BT.Controller;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.BT.Editor.Data.Graph
{
	[CustomNodeEditor(typeof(BTBlockCheck))]
	public class BTBlockCheckEditor : NodeEditor
	{
		/// <summary> Returns color for target node </summary>
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