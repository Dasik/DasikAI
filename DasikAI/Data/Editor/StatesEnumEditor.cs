using DasikAI.Data.CustomTypes;
using DasikAI.Utility;
using UnityEditor;
using UnityEngine;


namespace DasikAI.Data.Editor
{
	[CustomPropertyDrawer(typeof(StatesEnum), true)]
	public class StatesEnumEditor : PropertyDrawer
	{
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
		{
			EditorGUI.BeginProperty(position, label, property);
			position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
			var indent = EditorGUI.indentLevel;
			EditorGUI.indentLevel = 0;
			var value =
				PropertyDrawerUtility.GetActualObjectForSerializedProperty<StatesEnum>(fieldInfo, property);
			if (value != null)
			{
				EditorGUI.BeginChangeCheck();
				var newSelectedIndex = EditorGUI.Popup(position, value.SelectedIndex, value.Values);
				if (EditorGUI.EndChangeCheck())
				{
					value.SelectedIndex = newSelectedIndex;
					PropertyDrawerUtility.SetActualObjectForSerializedProperty(fieldInfo, property, value);
					property.serializedObject.ApplyModifiedProperties();
				}

				EditorGUI.indentLevel = indent;
				EditorGUI.EndProperty();
			}
		}
	}
}