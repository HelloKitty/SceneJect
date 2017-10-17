using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using UnityEngine;

namespace SceneJect.Common
{
	/// <summary>
	/// MonoBehaviour that can be inherited from to register dependencies
	/// that aren't MonoBehaviour and thus don't exist in the scene or can't
	/// exist in the scene.
	/// </summary>
	public abstract class NonBehaviourDependency : MonoBehaviour
	{
		/// <summary>
		/// Called when registeration is happening by Sceneject.
		/// </summary>
		/// <param name="register">A non-null registeration object.</param>
		public abstract void Register(ContainerBuilder register);
	}
}
