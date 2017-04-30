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
		private MonoBehaviour _Behaviour;

		public MonoBehaviour Behaviour => _Behaviour;

		[SerializeField]
		private string[] _ImplementedTypes = new string[0];

		[SerializeField]
		public string _SelectedType;

		[SerializeField]
		private int _SelectedPopupIndex;

		public Type SelectedType => Type.GetType(_SelectedType);

		public DependencyTypePair(MonoBehaviour behaviour, Type type)
		{
			_Behaviour = behaviour;
			_SelectedType = type.AssemblyQualifiedName;
		}

		//TODO: Why do we do this? Why are we not forcing non-null?
		public bool isInitialized()
		{
			return Behaviour != null && SelectedType != null;
		}
	}
}
