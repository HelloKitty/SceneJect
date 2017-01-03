using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	public abstract class ContainerServiceProvider : MonoBehaviour, IServiceRegister, IResolver
	{
		protected abstract IServiceRegister registry { get; }
		protected abstract IResolver resolver { get; }

		public void Register(DependencyTypePair pair)
		{
			registry.Register(pair);
		}

		public void Register<TTypeToRegister>(RegistrationType registerationFlags, Type registerAs = null) 
			where TTypeToRegister : class
		{
			registry.Register<TTypeToRegister>(registerationFlags, registerAs);
		}

		public void Register<TTypeToRegister>(TTypeToRegister instance, RegistrationType registerationFlags, Type registerAs = null) 
			where TTypeToRegister : class
		{
			registry.Register<TTypeToRegister>(instance, registerationFlags, registerAs);
		}

		public object Resolve(Type t)
		{
			return resolver.Resolve(t);
		}

		public TTypeToResolve Resolve<TTypeToResolve>() 
			where TTypeToResolve : class
		{
			return resolver.Resolve<TTypeToResolve>();
		}
	}
}
