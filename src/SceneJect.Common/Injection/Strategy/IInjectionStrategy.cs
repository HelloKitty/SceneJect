using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using JetBrains.Annotations;
using UnityEngine;

namespace SceneJect.Common
{
	/// <summary>
	/// Contract for any type that implements injection.
	/// </summary>
	public interface IInjectionStrategy
	{
		/// <summary>
		/// Injects dependencies into all the provided <paramref name="behaviours"/> using the provided
		/// <see cref="IResolver"/> service.
		/// </summary>
		/// <typeparam name="TBehaviourType">MonoBehaviour type.</typeparam>
		/// <param name="behaviours">Behaviours to inject into.</param>
		/// <param name="serviceResolutionService">Service resolution service.</param>
		void InjectDependencies<TBehaviourType>([NotNull] IEnumerable<TBehaviourType> behaviours, [NotNull] IComponentContext serviceResolutionService)
			where TBehaviourType : MonoBehaviour;

		/// <summary>
		/// Injects dependencies into all the provided <paramref name="behaviours"/> using the provided
		/// <see cref="IResolver"/> service.
		/// </summary>
		/// <typeparam name="TBehaviourType">MonoBehaviour type.</typeparam>
		/// <param name="behaviours">Behaviours to inject into.</param>
		/// <param name="serviceResolutionService">Service resolution service.</param>
		void InjectDependencies<TBehaviourType>([NotNull] IEnumerable<TBehaviourType> behaviours, [NotNull] IResolver serviceResolutionService)
			where TBehaviourType : MonoBehaviour;

		/// <summary>
		/// Injects dependencies into the provided behaviour instance.
		/// <see cref="IResolver"/> service.
		/// </summary>
		/// <typeparam name="TBehaviourType">MonoBehaviour type.</typeparam>
		/// <param name="behaviour">Behaviour to inject into.</param>
		/// <param name="serviceResolutionService">Service resolution service.</param>
		void InjectDependencies<TBehaviourType>([NotNull] TBehaviourType behaviour, [NotNull] IComponentContext serviceResolutionService)
			where TBehaviourType : MonoBehaviour;

		/// <summary>
		/// Injects dependencies into the provided behaviour instance.
		/// <see cref="IResolver"/> service.
		/// </summary>
		/// <typeparam name="TBehaviourType">MonoBehaviour type.</typeparam>
		/// <param name="behaviour">Behaviour to inject into.</param>
		/// <param name="serviceResolutionService">Service resolution service.</param>
		void InjectDependencies<TBehaviourType>([NotNull] TBehaviourType behaviour, [NotNull] IResolver serviceResolutionService)
			where TBehaviourType : MonoBehaviour;
	}
}
