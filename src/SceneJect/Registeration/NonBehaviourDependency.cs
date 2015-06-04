using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect
{
	public abstract class NonBehaviourDependency : MonoBehaviour
	{
		[SerializeField]
		private List<RegisterationType> _RegisterFlags;

		protected IEnumerable<RegisterationType> registerFlags
		{
			get { return _RegisterFlags; }
		}

		public abstract void Register(IServiceRegister register);

		protected RegisterationType getFlags()
		{
			return _RegisterFlags.Aggregate(RegisterationType.Default, (f, s) => f | s);
		}
	}
}
