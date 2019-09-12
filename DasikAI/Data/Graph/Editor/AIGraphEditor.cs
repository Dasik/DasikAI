using System.Linq;
using DasikAI.Scripts.Data.Graph.Attributes;
using DasikAI.Scripts.Data.Graph.Base;
using UnityEngine;
using XNodeEditor;

namespace Assets.DasikAI.Scripts.Data.Graph.Editor
{
	[CustomNodeGraphEditor(typeof(AIGraph))]
	public class AIGraphEditor : NodeGraphEditor
	{
		public override void OnOpen()
		{
			base.OnOpen();
			base.window.titleContent= new GUIContent("DasikAI");
		}


		/// <summary> 
		/// Overriding GetNodeMenuName lets you control if and how nodes are categorized.
		/// In this example we are sorting out all node types that are not in the XNode.Examples namespace.
		/// </summary>
		public override string GetNodeMenuName(System.Type type)
		{
			var attr = type.GetCustomAttributes(typeof(AINodeAttribute), true).FirstOrDefault();
			return (attr as AINodeAttribute)?.editorName ?? base.GetNodeMenuName(type);
		}
	}
}