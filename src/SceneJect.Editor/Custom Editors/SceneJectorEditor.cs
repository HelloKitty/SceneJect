using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

namespace SceneJect.CustomEditors
{
	[CustomEditor(typeof(SceneJector))]
	public class SceneJectorEditor : Editor
	{
		public override void OnInspectorGUI()
		{
			DrawDefaultInspector();

			serializedObject.ApplyModifiedProperties();
		}
	}
}
