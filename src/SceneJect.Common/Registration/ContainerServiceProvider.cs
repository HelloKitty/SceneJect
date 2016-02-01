using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	public abstract class ContainerServiceProvider : MonoBehaviour, IServiceRegister, IResolver
	{
		public abstract IServiceRegister Registry { get; }
		public abstract IResolver Resolver { get; }

		public void Register(DependencyTypePair pair)
		{
			Registry.Register(pair);
		}

		public void Register<TTypeToRegister>(RegistrationType registerationFlags, Type registerAs = null) 
			where TTypeToRegister : class
		{
			Registry.Register<TTypeToRegister>(registerationFlags, registerAs);
		}

		public void Register<TTypeToRegister>(TTypeToRegister instance, RegistrationType registerationFlags, Type registerAs = null) 
			where TTypeToRegister : class
		{
			Registry.Register<TTypeToRegister>(instance, registerationFlags, registerAs);
		}

		public object Resolve(Type t)
		{
			return Resolver.Resolve(t);
		}

		public TTypeToResolve Resolve<TTypeToResolve>() 
			where TTypeToResolve : class
		{
			return Resolver.Resolve<TTypeToResolve>();
		}
	}
}
