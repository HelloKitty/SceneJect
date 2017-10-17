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

		private Autofac.ContainerBuilder AutofacContainerBuilder { get; set; }

		private IContainer BuiltContainerResolver { get; set; }

		private void Awake()
		{
			AutofacContainerBuilder = new ContainerBuilder();

			//We remove null values from the collections because they are useless
			TypePairs = TypePairs.Where(x => x != null && x.Behaviour != null && x.SelectedType != null).ToList();

			//Try to gather all the dependency pairs on this type
			NonBehaviourDependencies = GetComponents<NonBehaviourDependency>()
				.Concat(NonBehaviourDependencies)
				.Distinct()
				.Where(x => x != null).ToList();

			if (!VerifyTypePairs(TypePairs))
				throw new InvalidOperationException($"{nameof(SceneJector)} has a malformed {nameof(DependencyTypePair)} registered. Must contain a valid MonoBehaviour and selected Type.");

			RegisterDependencies();
			InjectDependencies();
		}

		private bool VerifyTypePairs(IEnumerable<DependencyTypePair> pairs)
		{
			//Don't need to check if it's empty
			return !pairs.Any() || TypePairs.Aggregate(true, (x, y) => x && y.isInitialized());
		}

		private void RegisterDependencies()
		{
			//Register each DP as the type it selected to expose
			foreach(DependencyTypePair dtp in TypePairs)
				AutofacContainerBuilder.RegisterInstance(dtp.Behaviour)
					.As(dtp.SelectedType);

			//the IoC container visits each dependency registeration object
			//This allows the registeration logic to be handled differently
			foreach (var nbd in NonBehaviourDependencies)
				nbd.Register(AutofacContainerBuilder);

			//Register the GameObjectFactory and ComponentFactory too
			AutofacContainerBuilder.Register(context => new DefaultGameObjectFactory(context, new DefaultInjectionStrategy()))
				.As<IGameObjectFactory>()
				.SingleInstance();

			AutofacContainerBuilder.Register(context => new DefaultGameObjectComponentAttachmentFactory(context, new DefaultInjectionStrategy()))
				.As<IGameObjectComponentAttachmentFactory>()
				.SingleInstance();

			BuiltContainerResolver = AutofacContainerBuilder.Build();
		}

		private void InjectDependencies()
		{
			InjecteeLocator<MonoBehaviour> behaviours = new InjecteeLocator<MonoBehaviour>();

			foreach(MonoBehaviour b in behaviours)
			{
				Injector injector = new Injector(b, BuiltContainerResolver);

				injector.Inject();
			}
		}
	}
}
