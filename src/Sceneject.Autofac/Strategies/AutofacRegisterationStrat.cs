using Autofac;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sceneject.Autofac
{
	public class AutofacRegisterationStrat : IServiceRegister, ILockable
	{
		private readonly ContainerBuilder builder;

		public bool isLocked { get; private set; }

		public AutofacRegisterationStrat(ContainerBuilder autofacBuilder)
		{
			if (autofacBuilder == null)
				throw new ArgumentNullException(nameof(autofacBuilder), "Cannot have null builder provider in registeration strat.");

			builder = autofacBuilder;
			isLocked = false;
		}

		public IContainer Build()
		{
			Lock();
			return builder.Build();
		}

		public void Register<TTypeToRegister>(TTypeToRegister instance, RegistrationType registerationFlags, Type registerAs = null)
			where TTypeToRegister : class
		{
			if (isLocked)
				throw new Exception(typeof(TTypeToRegister).ToString() + " tried to register with this container but did so after its generation.");

			var chainInstance = builder.RegisterInstance(instance);

			if (registerAs != null)
				chainInstance.As(registerAs);

			if (registerationFlags.HasFlag(RegistrationType.SingleInstance))
				if (registerationFlags.HasFlag(RegistrationType.InstancePerDependency))
					throw new Exception(typeof(TTypeToRegister).ToString() + " tried to register as both single instance and per dependancy.");
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


		public void Register<TTypeToRegister>(RegistrationType registerationFlags, Type registerAs = null) where TTypeToRegister : class
		{
			if (isLocked)
				throw new Exception(typeof(TTypeToRegister).ToString() + " tried to register with this container but did so after its generation.");

			var chainInstance = builder.RegisterType<TTypeToRegister>();

			if (registerAs != null)
				chainInstance.As(registerAs);

			if (registerationFlags.HasFlag(RegistrationType.SingleInstance))
				if (registerationFlags.HasFlag(RegistrationType.InstancePerDependency))
					throw new Exception(typeof(TTypeToRegister).ToString() + " tried to register as both single instance and per dependancy.");
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

		public void Lock()
		{
			isLocked = true;
		}
	}
}
