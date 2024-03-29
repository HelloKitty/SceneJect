﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using JetBrains.Annotations;
using UnityEngine;

namespace SceneJect.Common
{
	//TODO: Consolidate duplicated code between the two builder types
	public class ContextualComponentDependencyBuilder : DepedencyInjectionFactoryService, IComponentContextualBuilder
	{
		/// <summary>
		/// Dictionary of mapped Types to service instance.
		/// </summary>
		private IDictionary<Type, Func<IComponentContext, object>> ServiceMap { get; }

		public ContextualComponentDependencyBuilder([NotNull] ILifetimeScope defaultResolver, IInjectionStrategy injectionStrategy)
			: base(defaultResolver, injectionStrategy)
		{
			ServiceMap = new Dictionary<Type, Func<IComponentContext, object>>(5);
		}

		public IComponentContextualBuilder With<TServiceType>([NotNull] IService<TServiceType> service) 
			where TServiceType : class
		{
			if (service == null) throw new ArgumentNullException(nameof(service));

			//Just overide an existing regiseration
			ServiceMap[typeof(TServiceType)] = service.ServiceInstance;

			//Return for fluent building
			return this;
		}

		public TComponentType AddTo<TComponentType>([NotNull] GameObject gameObject) 
			where TComponentType : MonoBehaviour
		{
			if (gameObject == null) throw new ArgumentNullException(nameof(gameObject));

			TComponentType component = gameObject.AddComponent<TComponentType>();

			//Decorate the current resolver with one that uses the contextual services
			IResolver resolver = new ContextualDependencyResolverDecorator(this.ResolverService, ServiceMap);

			//Inject using the decorated resolver
			InjectionStrategy.InjectDependencies<MonoBehaviour>(component, resolver);

			return component;
		}
	}
}
