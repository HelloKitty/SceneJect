using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	//TODO: Consolidate duplicated code between the two builder types
	public class ContextualComponentDependencyBuilder : DepedencyInjectionFactoryService, IComponentContextualBuilder
	{
		/// <summary>
		/// Dictionary of mapped Types to service instance.
		/// </summary>
		private IDictionary<Type, object> serviceMap { get; }

		public ContextualComponentDependencyBuilder(IResolver defaultResolver, IInjectionStrategy injectionStrategy)
			: base(defaultResolver, injectionStrategy)
		{
			serviceMap = new Dictionary<Type, object>();
		}

		public IComponentContextualBuilder With<TServiceType>(IService<TServiceType> service)
		{
			//Just overide an existing regiseration
			serviceMap[typeof(TServiceType)] = service.ServiceInstance;

			//Return for fluent building
			return this;
		}

		public TComponentType AddTo<TComponentType>(GameObject gameObject) 
			where TComponentType : MonoBehaviour
		{
			TComponentType component = gameObject.AddComponent<TComponentType>();

			//Decorate the current resolver with one that uses the contextual services
			ContextualDependencyResolverDecorator resolver = new ContextualDependencyResolverDecorator(this.resolverService, serviceMap);

			//Inject using the decorated resolver
			injectionStrategy.InjectDependencies<MonoBehaviour>(component, resolver);

			return component;
		}
	}
}
