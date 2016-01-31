using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	[Serializable]
	public class DependencyTypePair
	{
		[SerializeField]
		private MonoBehaviour _Behaviour;

		public MonoBehaviour Behaviour
		{
			get { return _Behaviour; }
		}

		[SerializeField]
		private string[] _ImplementedTypes = new string[0];

		[SerializeField]
		public string _SelectedType;

		[SerializeField]
		private int _SelectedPopupIndex;

		public Type SelectedType
		{
			get 
			{ 
				return Type.GetType(_SelectedType);
			}
		}

		public DependencyTypePair(MonoBehaviour behaviour, Type type)
		{
			_Behaviour = behaviour;
			_SelectedType = type.AssemblyQualifiedName;
		}

		public bool isInitialized()
		{
			return Behaviour != null && SelectedType != null;
        }
	}
}
