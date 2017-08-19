using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace SceneJect.Common
{
	public abstract class ContainerServiceProvider : MonoBehaviour, IServiceRegister, IResolver
	{
		protected abstract IServiceRegister Registry { get; }

		protected abstract IResolver Resolver { get; }

		public void Register([NotNull] DependencyTypePair pair)
		{
			if (pair == null) throw new ArgumentNullException(nameof(pair));

			Registry.Register(pair);
		}

		public void Register<TTypeToRegister>(RegistrationType registerationFlags, Type registerAs = null) 
			where TTypeToRegister : class
		{
			Registry.Register<TTypeToRegister>(registerationFlags, registerAs);
		}

		public void Register<TTypeToRegister>([NotNull] TTypeToRegister instance, RegistrationType registerationFlags, Type registerAs = null) 
			where TTypeToRegister : class
		{
			if (instance == null) throw new ArgumentNullException(nameof(instance));

			Registry.Register<TTypeToRegister>(instance, registerationFlags, registerAs);
		}

		public object Resolve(Type t)
		{
			if (t == null) throw new ArgumentNullException(nameof(t));

			return Resolver.Resolve(t);
		}

		public TTypeToResolve Resolve<TTypeToResolve>() 
			where TTypeToResolve : class
		{
			return Resolver.Resolve<TTypeToResolve>();
		}
	}
}
