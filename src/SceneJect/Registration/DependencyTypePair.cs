using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect
{
	[Serializable]
	public class DependencyTypePair : PropertyAttribute
	{
		[SerializeField]
		private MonoBehaviour _Behaviour;

		internal MonoBehaviour Behaviour
		{
			get { return _Behaviour; }
		}

		[SerializeField]
		private string[] _ImplementedTypes = new string[0];

		[SerializeField]
		public string _SelectedType;

		[SerializeField]
		private int _SelectedPopupIndex;

		internal Type SelectedType
		{
			get 
			{ 
				return Type.GetType(_SelectedType);
			}
		}
	}
}
