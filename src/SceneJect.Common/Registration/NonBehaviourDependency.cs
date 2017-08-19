using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	public abstract class NonBehaviourDependency : MonoBehaviour
	{
		[SerializeField]
		private List<RegistrationType> registerFlags;

		protected IEnumerable<RegistrationType> RegisterFlags => registerFlags;

		public abstract void Register(IServiceRegister register);

		protected RegistrationType ComputeFlags()
		{
			return registerFlags.Distinct()
				.Aggregate(RegistrationType.Default, (f, s) => f | s);
		}
	}
}
