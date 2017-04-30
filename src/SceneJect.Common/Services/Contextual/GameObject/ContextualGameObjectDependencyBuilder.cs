using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace SceneJect.Common
{
	public class ContextualGameObjectDependencyBuilder : DepedencyInjectionFactoryService, IGameObjectContextualBuilder
	{
		/// <summary>
		/// Dictionary of mapped Types to service instance.
		/// </summary>
		private IDictionary<Type, object> serviceMap { get; }

		public ContextualGameObjectDependencyBuilder(IResolver defaultResolver, IInjectionStrategy injectionStrategy)
			: base(defaultResolver, injectionStrategy)
		{
			serviceMap = new Dictionary<Type, object>();
		}

		public IGameObjectContextualBuilder With<TServiceType>([NotNull] IService<TServiceType> service)
		{
			if (service == null) throw new ArgumentNullException(nameof(service));

			//Just overide an existing regiseration
			serviceMap[typeof(TServiceType)] = service.ServiceInstance;

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

			//Decorate the current resolver with one that uses the contextual services
			ContextualDependencyResolverDecorator resolver = new ContextualDependencyResolverDecorator(this.resolverService, serviceMap);

			//Inject using the decorated resolver
			injectionStrategy.InjectDependencies<MonoBehaviour>(InjecteeLocator<MonoBehaviour>.Create(obj), resolver);

			return obj;
		}
	}
}
