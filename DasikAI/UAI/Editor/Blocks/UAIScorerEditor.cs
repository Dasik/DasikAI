using DasikAI.UAI.Controller;
using DasikAI.UAI.Nodes.Base.Blocks;
using XNodeEditor;
using UnityEditor;
using UnityEngine;

namespace DasikAI.UAI.Editor.Blocks
{
    [CustomNodeEditorAttribute(typeof(UAIScorer))]
    public class UAIScorerEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            if (Selection.activeGameObject == null) return;
            var graphController = Selection.activeGameObject.GetComponent<UAIGraphController>();
            if (graphController == null) return;
            var node = target as UAIScorer;
            if (!Application.isPlaying)
                return;
            var score = graphController.GetParam(node);
            GUILayout.Label($"score:{score}", NodeEditorResources.styles.tooltip, GUILayout.Height(30));
        }

        public override Color GetTint()
        {
            return Color.green;
        }
    }
}