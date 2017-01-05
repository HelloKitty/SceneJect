using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	public class ContextualGameObjectDependencyBuilder : IGameObjectContextualBuilder
	{
		/// <summary>
		/// Dictionary of mapped Types to service instance.
		/// </summary>
		private IDictionary<Type, object> serviceMap { get; }

		/// <summary>
		/// Default service resolution service.
		/// </summary>
		private IResolver defaultServiceResolver { get; }

		/// <summary>
		/// Injection service.
		/// </summary>
		private IInjectionStrategy injectionStrategyService { get; }

		public ContextualGameObjectDependencyBuilder(IResolver defaultResolver, IInjectionStrategy injectionStrategy)
		{
			if (defaultResolver == null)
				throw new ArgumentNullException(nameof(defaultResolver), $"Provided arg {nameof(defaultResolver)} of Type: {nameof(IResolver)} was null.");

			if (injectionStrategy == null)
				throw new ArgumentNullException(nameof(injectionStrategy), $"Provided arg {nameof(injectionStrategy)} of Type: {nameof(IInjectionStrategy)} was null.");

			serviceMap = new Dictionary<Type, object>();
			injectionStrategyService = injectionStrategy;
			defaultServiceResolver = defaultResolver;
		}

		public IGameObjectContextualBuilder With<TServiceType>(IService<TServiceType> service)
		{
			//Just overide an existing regiseration
			serviceMap[typeof(TServiceType)] = service.ServiceInstance;

			//Return for fluent building
			return this;
		}

		public GameObject Create(GameObject prefab)
		{
			return Create(prefab, Vector3.zero, Quaternion.identity);
		}

		public GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation)
		{
			GameObject obj = GameObject.Instantiate(prefab, position, rotation) as GameObject;

			//Decorate the current resolver with one that uses the contextual services
			ContextualDependencyResolverDecorator resolver = new ContextualDependencyResolverDecorator(this.defaultServiceResolver, serviceMap);

			//Inject using the decorated resolver
			injectionStrategyService.InjectDependencies(InjecteeLocator<MonoBehaviour>.Create(obj), resolver);

			return obj;
		}
	}
}
