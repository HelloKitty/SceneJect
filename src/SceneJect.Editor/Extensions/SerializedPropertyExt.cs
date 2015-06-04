using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace SceneJect.CustomEditors
{
	internal static class SerializedPropertyExt
	{
		//The below helper was copy-pasted with minor modifications from http://answers.unity3d.com/questions/682932/using-generic-list-with-serializedproperty-inspect.html
		//It deals with the writing of arrays in a serialized property as well as
		//writing individual properties.
		internal static bool WriteSerializedArray<T>(this SerializedProperty sp, T[] arrayObject)
		{
			sp.Next(true); // skip generic field
			sp.Next(true); // advance to array size field

			// Set the array size
			if (!WriteSerialzedProperty(sp, arrayObject.Length)) return false;

			sp.Next(true); // advance to first array index

			// Write values to array
			int lastIndex = arrayObject.Length - 1;
			for (int i = 0; i < arrayObject.Length; i++)
			{
				if (!WriteSerialzedProperty(sp, arrayObject[i])) return false; // write the value to the property
				if (i < lastIndex) sp.Next(false); // advance without drilling into children            
			}

			return true;
		}

		internal static bool WriteSerialzedProperty(this SerializedProperty sp, object variableValue)
		{
			// Type the property and fill with new value
			SerializedPropertyType type = sp.propertyType; // get the property type

			switch (sp.propertyType)
			{
				case SerializedPropertyType.Integer:
					sp.intValue = (int)variableValue;
					break;
				case SerializedPropertyType.Boolean:
					sp.boolValue = (bool)variableValue;
					break;
				case SerializedPropertyType.Float:
					sp.floatValue = (float)variableValue;
					break;
				case SerializedPropertyType.String:
					sp.stringValue = (string)variableValue;
					break;
				case SerializedPropertyType.ArraySize:
					sp.intValue = (int)variableValue;
					break;
				case SerializedPropertyType.ObjectReference:
					sp.objectReferenceValue = (UnityEngine.Object)variableValue;
					break;

				default:
					throw new NotSupportedException("Found PropertyType: " + type.ToString() + " which cannot be serialized by default.");
			}

			return true;
		}
	}
}
