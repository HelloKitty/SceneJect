using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace SceneJect.Common
{
	[Serializable]
	public class DependencyTypePair
	{
		[SerializeField]
		private MonoBehaviour behaviour;
		public MonoBehaviour Behaviour => behaviour;

		//TODO: Should these be hidden behind properties?
		[SerializeField]
		private string[] ImplementedTypes = new string[0];

		[SerializeField]
		public string SelectedTypeString;

		[SerializeField]
		private int SelectedPopupIndex;

		public Type SelectedType => Type.GetType(SelectedTypeString);

		public DependencyTypePair(MonoBehaviour behaviour, Type type)
		{
			this.behaviour = behaviour;
			SelectedTypeString = type.AssemblyQualifiedName;
		}

		//TODO: Why do we do this? Why are we not forcing non-null?
		public bool isInitialized()
		{
			return Behaviour != null && SelectedType != null;
		}
	}
}
