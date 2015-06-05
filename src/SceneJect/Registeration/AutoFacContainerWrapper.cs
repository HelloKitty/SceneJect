using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect
{
	public class AutoFacContainerWrapper : IResolver, IServiceRegister
	{
		private IContainer _Container = null;

		private readonly ContainerBuilder builder;

		private bool locked;

		public AutoFacContainerWrapper(IEnumerable<DependencyTypePair> typePairs)
		{
			builder = new ContainerBuilder();
			locked = false;

			foreach(DependencyTypePair pair in typePairs)
			{
				builder.RegisterInstance(pair.Behaviour).As(pair.SelectedType).ExternallyOwned();
			}
		}

		public T Resolve<T>()
		{
			if (!locked)
				Build();

			if (!_Container.IsRegistered<T>())
				throw new Exception(typeof(T).ToString() + " was requested from the container but is unavailable.");

			return _Container.Resolve<T>();
		}

		public object Resolve(Type t)
		{
			if (!locked)
				Build();

			if (!_Container.IsRegistered(t))
				throw new Exception(t.ToString() + " was requested from the container but is unavailable.");

			return _Container.Resolve(t);
		}

		public void Build()
		{
			locked = true;
			_Container = builder.Build();
		}

		public void Register<T>(T instance, RegisterationType registerationFlags, Type registerAs = null)
			where T : class
		{
			if (locked)
				throw new Exception(typeof(T).ToString() + " tried to register with this container but did so after its generation.");

			var chainInstance = builder.RegisterInstance(instance);

			if (registerAs != null)
				chainInstance.As(registerAs);

			if (registerationFlags.HasFlag(RegisterationType.SingleInstance))
				if (registerationFlags.HasFlag(RegisterationType.InstancePerDependency))
					throw new Exception(typeof(T).ToString() + " tried to register as both single instance and per dependancy.");
				else
					chainInstance.SingleInstance();
			else
				if (registerationFlags.HasFlag(RegisterationType.InstancePerDependency))
					chainInstance.InstancePerDependency();

			if (registerationFlags.HasFlag(RegisterationType.AsImplementedInterface))
				chainInstance.AsImplementedInterfaces();

			if (registerationFlags.HasFlag(RegisterationType.AsSelf))
				chainInstance.AsSelf();

			if (registerationFlags.HasFlag(RegisterationType.ExternallyOwned))
				chainInstance.ExternallyOwned();
		}


		public void Register<T>(RegisterationType registerationFlags, Type registerAs = null) where T : class
		{
			if (locked)
				throw new Exception(typeof(T).ToString() + " tried to register with this container but did so after its generation.");

			var chainInstance = builder.RegisterType<T>();

			if (registerAs != null)
				chainInstance.As(registerAs);

			if (registerationFlags.HasFlag(RegisterationType.SingleInstance))
				if (registerationFlags.HasFlag(RegisterationType.InstancePerDependency))
					throw new Exception(typeof(T).ToString() + " tried to register as both single instance and per dependancy.");
				else
					chainInstance.SingleInstance();
			else
				if (registerationFlags.HasFlag(RegisterationType.InstancePerDependency))
					chainInstance.InstancePerDependency();

			if (registerationFlags.HasFlag(RegisterationType.AsImplementedInterface))
				chainInstance.AsImplementedInterfaces();

			if (registerationFlags.HasFlag(RegisterationType.AsSelf))
				chainInstance.AsSelf();

			if (registerationFlags.HasFlag(RegisterationType.ExternallyOwned))
				chainInstance.ExternallyOwned();
		}
	}
}
