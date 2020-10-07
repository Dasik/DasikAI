using System;
using DasikAI.Common.Base.ParamSources;
using DasikAI.Common.Controller;
using UnityEditor;
using UnityEngine;
using XNodeEditor;

namespace DasikAI.Common.Editor
{
    [CustomNodeEditor(typeof(ParamSource))]
    public class ParamSourceEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            if (Selection.activeGameObject == null) return;
            var graphController = Selection.activeGameObject.GetComponent<AIGraphController>();
            if (graphController == null) return;
            var node = target as ParamSource;
            if (!Application.isPlaying)
                return;
            var value = typeof(AIGraphController)
                .GetMethod(nameof(graphController.GetParam))
                .MakeGenericMethod(GetGenericType(node.GetType()))
                .Invoke(graphController, new object[] {node});
            GUILayout.Label($"value:{value}", NodeEditorResources.styles.tooltip, GUILayout.Height(30));
        }

        private static Type GetGenericType(Type type)
        {
            while (type != null)
            {
                if (type.IsGenericType) return type.GenericTypeArguments[0];
                type = type.BaseType;
            }

            return null;
        }

        public override Color GetTint()
        {
            return Color.magenta;
        }
    }
}