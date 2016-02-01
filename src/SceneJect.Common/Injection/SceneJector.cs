using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	public sealed class SceneJector : MonoBehaviour
	{
		[SerializeField]
		private List<DependencyTypePair> typePairs;

		[SerializeField]
		private List<NonBehaviourDependency> nonBehaviourDependencies;

		[SerializeField]
		private ContainerServiceProvider containerServiceProvider;

		private void Awake()
		{
			if(!VerifyTypePairs(typePairs))
				throw new InvalidOperationException(nameof(SceneJector) + " has a malformed " + nameof(DependencyTypePair) +
						" registered. Must contain a valid MonoBehaviour and selected Type.");

			if (containerServiceProvider == null)
				throw new ArgumentNullException(nameof(containerServiceProvider), "Cannot have a null provider for container services. " + nameof(SceneJector) + " requires this for DI.");

			RegisterDependencies(containerServiceProvider.Registry);
			InjectDependencies(containerServiceProvider.Resolver);
		}

		private bool VerifyTypePairs(IEnumerable<DependencyTypePair> pairs)
		{
			//Don't need to check if it's empty
			if (pairs.Count() == 0)
				return true;
			else
				return typePairs.Aggregate(true, (x, y) => x && y.isInitialized());
		}

		private void RegisterDependencies(IServiceRegister register)
		{
			foreach (DependencyTypePair dtp in typePairs)
				register.Register(dtp);

			//the IoC container visits each dependency registeration object
			//This allows the registeration logic to be handled differently
			foreach (var nbd in nonBehaviourDependencies)
				nbd.Register(register);
		}

		private void InjectDependencies(IResolver resolver)
		{
			InjecteeLocator<MonoBehaviour> behaviours = new InjecteeLocator<MonoBehaviour>();

			foreach(MonoBehaviour b in behaviours)
			{
				Injector injector = new Injector(b, resolver);

				injector.Inject();
			}
		}
	}
}
