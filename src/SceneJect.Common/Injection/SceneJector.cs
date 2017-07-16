using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
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
			//We remove null values from the collections because they are useless
			typePairs = typePairs.Where(x => x != null && x.Behaviour != null && x.SelectedType != null).ToList();

			//Try to gather all the dependency pairs on this type
			nonBehaviourDependencies = GetComponents<NonBehaviourDependency>()
				.Concat(nonBehaviourDependencies)
				.Distinct()
				.Where(x => x != null).ToList();

			if (!VerifyTypePairs(typePairs))
				throw new InvalidOperationException($"{nameof(SceneJector)} has a malformed {nameof(DependencyTypePair)} registered. Must contain a valid MonoBehaviour and selected Type.");

			if (containerServiceProvider == null)
				throw new ArgumentNullException(nameof(containerServiceProvider), $"Cannot have a null provider for container services. {nameof(SceneJector)} requires this for DI.");

			RegisterDependencies(containerServiceProvider);
			InjectDependencies(containerServiceProvider);
		}

		private bool VerifyTypePairs(IEnumerable<DependencyTypePair> pairs)
		{
			//Don't need to check if it's empty
			return !pairs.Any() || typePairs.Aggregate(true, (x, y) => x && y.isInitialized());
		}

		private void RegisterDependencies([NotNull] IServiceRegister register)
		{
			if (register == null) throw new ArgumentNullException(nameof(register));

			foreach (DependencyTypePair dtp in typePairs)
				register.Register(dtp);

			//the IoC container visits each dependency registeration object
			//This allows the registeration logic to be handled differently
			foreach (var nbd in nonBehaviourDependencies)
				nbd.Register(register);

			//Register the GameObjectFactory and ComponentFactory too
			register.Register(new DefaultGameObjectFactory(containerServiceProvider, new DefaultInjectionStrategy()), RegistrationType.SingleInstance, typeof(IGameObjectFactory));
			register.Register(new DefaultGameObjectComponentAttachmentFactory(containerServiceProvider, new DefaultInjectionStrategy()), RegistrationType.SingleInstance, typeof(IGameObjectComponentAttachmentFactory));
		}

		private void InjectDependencies([NotNull] IResolver resolver)
		{
			if (resolver == null) throw new ArgumentNullException(nameof(resolver));

			InjecteeLocator<MonoBehaviour> behaviours = new InjecteeLocator<MonoBehaviour>();

			foreach(MonoBehaviour b in behaviours)
			{
				Injector injector = new Injector(b, resolver);

				injector.Inject();
			}
		}
	}
}
