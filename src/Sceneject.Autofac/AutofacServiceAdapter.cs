using Autofac;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Sceneject.Autofac
{
	public class AutofacServiceAdapter : MonoBehaviour, IResolver, IServiceRegister
	{
		private IContainer _Container = null;

		private readonly ContainerBuilder builder = new ContainerBuilder();

		private bool locked = false;

		public T Resolve<T>()
			where T : class
		{
			if (!locked)
				Build();

			if (!_Container.IsRegistered<T>())
				throw new Exception(typeof(T).ToString() + " was requested from the container but is unavailable.");

			return _Container.Resolve<T>();
		}

		public object Resolve(Type t)
		{
			if (t == null)
				throw new ArgumentNullException(nameof(t), "Cannot resolve a null type.");

			if (t.IsValueType)
				throw new InvalidOperationException("Cannot resolve value types: " + t.ToString());

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

		public void Register<T>(T instance, RegistrationType registerationFlags, Type registerAs = null)
			where T : class
		{
			if (locked)
				throw new Exception(typeof(T).ToString() + " tried to register with this container but did so after its generation.");

			var chainInstance = builder.RegisterInstance(instance);

			if (registerAs != null)
				chainInstance.As(registerAs);

			if (registerationFlags.HasFlag(RegistrationType.SingleInstance))
				if (registerationFlags.HasFlag(RegistrationType.InstancePerDependency))
					throw new Exception(typeof(T).ToString() + " tried to register as both single instance and per dependancy.");
				else
					chainInstance.SingleInstance();
			else
				if (registerationFlags.HasFlag(RegistrationType.InstancePerDependency))
				chainInstance.InstancePerDependency();

			if (registerationFlags.HasFlag(RegistrationType.AsImplementedInterface))
				chainInstance.AsImplementedInterfaces();

			if (registerationFlags.HasFlag(RegistrationType.AsSelf))
				chainInstance.AsSelf();

			if (registerationFlags.HasFlag(RegistrationType.ExternallyOwned))
				chainInstance.ExternallyOwned();
		}


		public void Register<T>(RegistrationType registerationFlags, Type registerAs = null) where T : class
		{
			if (locked)
				throw new Exception(typeof(T).ToString() + " tried to register with this container but did so after its generation.");

			var chainInstance = builder.RegisterType<T>();

			if (registerAs != null)
				chainInstance.As(registerAs);

			if (registerationFlags.HasFlag(RegistrationType.SingleInstance))
				if (registerationFlags.HasFlag(RegistrationType.InstancePerDependency))
					throw new Exception(typeof(T).ToString() + " tried to register as both single instance and per dependancy.");
				else
					chainInstance.SingleInstance();
			else
				if (registerationFlags.HasFlag(RegistrationType.InstancePerDependency))
				chainInstance.InstancePerDependency();

			if (registerationFlags.HasFlag(RegistrationType.AsImplementedInterface))
				chainInstance.AsImplementedInterfaces();

			if (registerationFlags.HasFlag(RegistrationType.AsSelf))
				chainInstance.AsSelf();

			if (registerationFlags.HasFlag(RegistrationType.ExternallyOwned))
				chainInstance.ExternallyOwned();
		}

		public void Register(DependencyTypePair pair)
		{
			//pairs are externally owned MonoBehaviours that exist in the editor
			//Register them as such and as the instance to provide
			builder.RegisterInstance(pair.Behaviour).As(pair.SelectedType).ExternallyOwned();
		}
	}
}
