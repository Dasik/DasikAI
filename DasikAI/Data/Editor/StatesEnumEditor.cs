using DasikAI.Scripts.Data.CustomTypes;
using DasikAI.Utility;
using UnityEditor;
using UnityEngine;


namespace DasikAI.Scripts.Data.Editor
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

				var selectedIndex = value.SelectedIndex;
				var newSelectedIndex = EditorGUI.Popup(position, value.SelectedIndex, value.Values);
				if (selectedIndex != newSelectedIndex)
				{
					value.SelectedIndex = newSelectedIndex;
					PropertyDrawerUtility.SetActualObjectForSerializedProperty(fieldInfo, property, value);
				}
			}

			EditorGUI.indentLevel = indent;
			EditorGUI.EndProperty();
		}
	}
}