﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using JetBrains.Annotations;
using UnityEngine;

namespace SceneJect.Common
{
	public class DefaultGameObjectComponentAttachmentFactory : DepedencyInjectionFactoryService, IGameObjectComponentAttachmentFactory
	{
		public DefaultGameObjectComponentAttachmentFactory(ILifetimeScope resolver, IInjectionStrategy injectionStrat)
			: base(resolver, injectionStrat)
		{

		}

		public TComponentType AddTo<TComponentType>([NotNull] GameObject gameObject) 
			where TComponentType : MonoBehaviour
		{
			if (gameObject == null) throw new ArgumentNullException(nameof(gameObject));

			//Attach the component to the gameobject
			TComponentType component = gameObject.AddComponent<TComponentType>();

			//After you attach it then you must pass it along to the injection strategy so that its dependencies can be resolved.
			this.InjectionStrategy.InjectDependencies(component, this.ResolverService);

			return component;
		}

		public IComponentContextualBuilder CreateBuilder()
		{
			//Just return the default one
			return new ContextualComponentDependencyBuilder(ResolverService, InjectionStrategy);
		}
	}
}
