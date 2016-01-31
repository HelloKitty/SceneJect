using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common
{
	public abstract class ContainerServiceProvider : IServiceRegister, IResolver
	{
		public abstract void Register(DependencyTypePair pair);
		public abstract void Register<T>(RegistrationType registerationFlags, Type registerAs = null) where T : class;
		public abstract void Register<T>(T instance, RegistrationType registerationFlags, Type registerAs = null) where T : class;

		public abstract object Resolve(Type t);
		public abstract T Resolve<T>();
	}
}
