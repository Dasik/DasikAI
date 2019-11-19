using System.Linq;
using DasikAI.Data.Graph.Attributes;
using DasikAI.Data.Graph.Base;
using DasikAI.Data.Graph.Base.ParamSources;
using DasikAI.Utility;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace DasikAI.Data.Graph.Editor
{
	[CustomNodeGraphEditor(typeof(AIGraph))]
	public class AIGraphEditor : NodeGraphEditor
	{
		public override void OnOpen()
		{
			base.OnOpen();
			base.window.titleContent = new GUIContent("DasikAI");
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

		public override Color GetPortColor(NodePort port)
		{
			//var nodeBaseType = port.node.GetType().BaseType;
			//if (nodeBaseType != null &&
			//	(nodeBaseType.IsGenericType || nodeBaseType.IsConstructedGenericType) &&
			//	nodeBaseType.GetGenericTypeDefinition().IsAssignableFrom(typeof(ParamSource<>)))
			//{
			//	var color = base.GetPortColor(port);
			//	color.a = 0.1f;
			//	return color;
			//}
			var color = base.GetPortColor(port);
			if (port.node.GetType().IsAssignableFromGeneric(typeof(ParamSource<>)))
				color.a *= 0.1f;
			return color;
		}
	}
}