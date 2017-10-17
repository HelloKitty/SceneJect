using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using JetBrains.Annotations;
using UnityEngine;

namespace SceneJect.Common
{
	public class DefaultGameObjectFactory : DepedencyInjectionFactoryService, IGameObjectFactory
	{
		public DefaultGameObjectFactory(IComponentContext resolver, IInjectionStrategy injectionStrat)
			: base(resolver, injectionStrat)
		{

		}

		/// <summary>
		/// Creates an empty <see cref="GameObject"/>.
		/// </summary>
		/// <returns>A non-null empty <see cref="GameObject"/>.</returns>
		public GameObject Create()
		{
			//Default GameObjects don't require dependencies
			return new GameObject();
		}

		/// <summary>
		/// Creates an instance of the prefab <see cref="GameObject"/>.
		/// </summary>
		/// <param name="prefab">Prefab to create an instance of.</param>
		/// <returns>A non-null instance of the provided <see cref="GameObject"/> <paramref name="prefab"/>.</returns>
		public GameObject Create([NotNull] GameObject prefab)
		{
			if (prefab == null) throw new ArgumentNullException(nameof(prefab));

			//Create the GameObject and inject dependencies
			return InjectDependencies(GameObject.Instantiate(prefab));
		}

		private GameObject InjectDependencies([NotNull] GameObject obj)
		{
			if (obj == null) throw new ArgumentNullException(nameof(obj));

			InjectionStrategy.InjectDependencies<MonoBehaviour>(InjecteeLocator<MonoBehaviour>.Create(obj), ResolverService);

			return obj;
		}

		/// <summary>
		/// Creates an instance of the prefab <see cref="GameObject"/> with the provided position and rotation.
		/// </summary>
		/// <param name="prefab">Prefab to create an instance of.</param>
		/// <returns>A non-null instance of the provided <see cref="GameObject"/> <paramref name="prefab"/>.</returns>
		public GameObject Create([NotNull] GameObject prefab, Vector3 position, Quaternion rotation)
		{
			if (prefab == null) throw new ArgumentNullException(nameof(prefab));

			return InjectDependencies(GameObject.Instantiate(prefab, position, rotation) as GameObject);
		}

		public IGameObjectContextualBuilder CreateBuilder()
		{
			//Just init a new builder and let the consumer chain requests and generate the gameobject.
			return new ContextualGameObjectDependencyBuilder(ResolverService, InjectionStrategy);
		}
	}
}
