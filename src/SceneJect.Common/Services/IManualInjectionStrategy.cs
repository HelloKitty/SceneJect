using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using SceneJect.Common;
using UnityEngine;

namespace SceneJect
{
	/// <summary>
	/// Contract for service that offers the ability
	/// to manually designate a collection of <see cref="MonoBehaviour"/> to
	/// have services injected into.
	/// </summary>
	public interface IManualInjectionStrategy
	{
		/// <summary>
		/// Injects dependencies into all the provided <paramref name="behaviours"/> using the provided
		/// <see cref="IResolver"/> service.
		/// </summary>
		/// <param name="behaviours">Behaviours to inject into.</param>
		void InjectDependencies([NotNull] IReadOnlyCollection<MonoBehaviour> behaviours);
	}
}
