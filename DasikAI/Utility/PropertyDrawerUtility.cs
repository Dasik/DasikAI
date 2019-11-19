using System;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace DasikAI.Utility
{
	public class PropertyDrawerUtility
	{
		public static T GetActualObjectForSerializedProperty<T>(FieldInfo fieldInfo, SerializedProperty property)
		{
			var obj = fieldInfo.GetValue(property.serializedObject.targetObject);
			if (obj == null)
			{
				return default;
			}

			T actualObject = default;
			if (obj.GetType().IsArray)
			{
				var index = Convert.ToInt32(new string(property.propertyPath.Where(c => char.IsDigit(c)).ToArray()));
				actualObject = ((T[])obj)[index];
			}
			else
			{
				actualObject = (T)obj;
			}

			return actualObject;
		}

		public static void SetActualObjectForSerializedProperty<T>(FieldInfo fieldInfo, SerializedProperty property, T value)
		{
			if (fieldInfo.FieldType.IsArray)
			{
				var index = Convert.ToInt32(new string(property.propertyPath.Where(c => char.IsDigit(c)).ToArray()));
				Array fieldValue = (Array)fieldInfo.GetValue(property.serializedObject.targetObject);
				fieldValue.SetValue(value, index);
				//property.GetArrayElementAtIndex(index).serializedObject.= value;
			}
			else
			{
				fieldInfo.SetValue(property.serializedObject.targetObject, value);
			}
			EditorUtility.SetDirty(property.serializedObject.targetObject);
			AssetDatabase.SaveAssets();
		}
	}
}