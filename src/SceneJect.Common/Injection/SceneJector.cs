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
		private List<DependencyTypePair> TypePairs;

		[SerializeField]
		private List<NonBehaviourDependency> NonBehaviourDependencies;

		[SerializeField]
		private ContainerServiceProvider ContainerServiceProvider;

		private void Awake()
		{
			//We remove null values from the collections because they are useless
			TypePairs = TypePairs.Where(x => x != null && x.Behaviour != null && x.SelectedType != null).ToList();

			//Try to gather all the dependency pairs on this type
			NonBehaviourDependencies = GetComponents<NonBehaviourDependency>()
				.Concat(NonBehaviourDependencies)
				.Distinct()
				.Where(x => x != null).ToList();

			if (!VerifyTypePairs(TypePairs))
				throw new InvalidOperationException($"{nameof(SceneJector)} has a malformed {nameof(DependencyTypePair)} registered. Must contain a valid MonoBehaviour and selected Type.");

			if (ContainerServiceProvider == null)
				throw new ArgumentNullException(nameof(ContainerServiceProvider), $"Cannot have a null provider for container services. {nameof(SceneJector)} requires this for DI.");

			RegisterDependencies(ContainerServiceProvider);
			InjectDependencies(ContainerServiceProvider);
		}

		private bool VerifyTypePairs(IEnumerable<DependencyTypePair> pairs)
		{
			//Don't need to check if it's empty
			return !pairs.Any() || TypePairs.Aggregate(true, (x, y) => x && y.isInitialized());
		}

		private void RegisterDependencies([NotNull] IServiceRegister register)
		{
			if (register == null) throw new ArgumentNullException(nameof(register));

			foreach (DependencyTypePair dtp in TypePairs)
				register.Register(dtp);

			//the IoC container visits each dependency registeration object
			//This allows the registeration logic to be handled differently
			foreach (var nbd in NonBehaviourDependencies)
				nbd.Register(register);

			//Register the GameObjectFactory and ComponentFactory too
			register.Register(new DefaultGameObjectFactory(ContainerServiceProvider, new DefaultInjectionStrategy()), RegistrationType.SingleInstance, typeof(IGameObjectFactory));
			register.Register(new DefaultGameObjectComponentAttachmentFactory(ContainerServiceProvider, new DefaultInjectionStrategy()), RegistrationType.SingleInstance, typeof(IGameObjectComponentAttachmentFactory));
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
