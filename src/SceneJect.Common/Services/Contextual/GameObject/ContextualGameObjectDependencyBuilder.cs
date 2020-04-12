using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using JetBrains.Annotations;
using UnityEngine;

namespace SceneJect.Common
{
	public class ContextualGameObjectDependencyBuilder : DepedencyInjectionFactoryService, IGameObjectContextualBuilder
	{
		/// <summary>
		/// Dictionary of mapped Types to service instance.
		/// </summary>
		private IDictionary<Type, Func<IComponentContext, object>> ServiceMap { get; }

		public ContextualGameObjectDependencyBuilder(ILifetimeScope defaultResolver, IInjectionStrategy injectionStrategy)
			: base(defaultResolver, injectionStrategy)
		{
			ServiceMap = new Dictionary<Type, Func<IComponentContext, object>>(5);
		}

		public IGameObjectContextualBuilder With<TServiceType>([NotNull] IService<TServiceType> service) 
			where TServiceType : class
		{
			if (service == null) throw new ArgumentNullException(nameof(service));

			//Just overide an existing regiseration
			ServiceMap[typeof(TServiceType)] = service.ServiceInstance;

			//Return for fluent building
			return this;
		}

		public GameObject Create([NotNull] GameObject prefab)
		{
			if (prefab == null) throw new ArgumentNullException(nameof(prefab));

			return Create(prefab, Vector3.zero, Quaternion.identity);
		}

		public GameObject Create([NotNull] GameObject prefab, Vector3 position, Quaternion rotation)
		{
			if (prefab == null) throw new ArgumentNullException(nameof(prefab));

			GameObject obj = GameObject.Instantiate(prefab, position, rotation) as GameObject;

			using(var newLifetimeScope = ResolverService.BeginLifetimeScope(builder =>
			{
				//TODO: This could be slow, any way to speed this up?
				foreach(var entries in ServiceMap)
					builder.RegisterInstance(entries.Value(ResolverService))
						.As(entries.Key)
						.SingleInstance();
			}))
			{
				//Decorate the current resolver with one that uses the contextual services
				IResolver resolver = new ContextualDependencyResolverDecorator(newLifetimeScope, ServiceMap);

				//Inject using the decorated resolver
				InjectionStrategy.InjectDependencies<MonoBehaviour>(InjecteeLocator<MonoBehaviour>.Create(obj), resolver);
			}

			return obj;
		}
	}
}
