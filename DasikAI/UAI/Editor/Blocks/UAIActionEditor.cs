using DasikAI.UAI.Controller;
using DasikAI.UAI.Nodes.Base.Blocks;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.UAI.Editor.Blocks
{
    [CustomNodeEditorAttribute(typeof(UAIAction))]
    public class UAIActionEditor : NodeEditor
    {
        public override Color GetTint()
        {
            if (Selection.activeGameObject == null)
                return base.GetTint();
            var graphController = Selection.activeGameObject.GetComponent<UAIGraphController>();
            if (graphController == null)
                return base.GetTint();
            if (!Application.isPlaying)
                return base.GetTint();
            var node = target as UAIAction;
            if (graphController.IsActionActive(node))
                return Color.yellow;
            return base.GetTint();
        }
    }
}