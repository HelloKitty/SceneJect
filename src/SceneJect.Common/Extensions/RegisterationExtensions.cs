using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace SceneJect.Common
{
	public static class RegisterationExtensions
	{
		/// <summary>
		/// Registers an instance of type <typeparamref name="TTypeToRegister"/> as the indicated
		/// implemented <typeparamref name="TServiceType"/> Type.
		/// </summary>
		/// <typeparam name="TTypeToRegister"></typeparam>
		/// <typeparam name="TServiceType"></typeparam>
		/// <param name="registry"></param>
		/// <param name="flags"></param>
		/// <param name="service"></param>
		/// <returns></returns>
		public static IServiceRegister Register<TTypeToRegister, TServiceType>([NotNull] this IServiceRegister registry, TTypeToRegister service, RegistrationType flags)
			where TTypeToRegister : class, TServiceType
		{
			if (registry == null) throw new ArgumentNullException(nameof(registry));

			registry.Register(service, flags, typeof(TServiceType));

			return registry;
		}

		/// <summary>
		/// Registers an instance of type <typeparamref name="TTypeToRegister"/> as the indicated
		/// implemented <typeparamref name="TServiceType"/> Type.
		/// </summary>
		/// <typeparam name="TTypeToRegister"></typeparam>
		/// <typeparam name="TServiceType"></typeparam>
		/// <param name="registry"></param>
		/// <param name="service"></param>
		/// <returns></returns>
		public static IServiceRegister RegisterInstance<TTypeToRegister, TServiceType>([NotNull] this IServiceRegister registry, TTypeToRegister service)
			where TTypeToRegister : class, TServiceType
		{
			return registry.Register<TTypeToRegister, TServiceType>(service, RegistrationType.ExternallyOwned | RegistrationType.SingleInstance);
		}

		/// <summary>
		/// Registers a singleton for <typeparamref name="TTypeToRegister"/> as the indicated
		/// implemented <typeparamref name="TServiceType"/> Type.
		/// </summary>
		/// <typeparam name="TTypeToRegister"></typeparam>
		/// <typeparam name="TServiceType"></typeparam>
		/// <param name="registry"></param>
		/// <param name="service"></param>
		/// <returns></returns>
		public static IServiceRegister RegisterSingleton<TTypeToRegister, TServiceType>([NotNull] this IServiceRegister registry)
			where TTypeToRegister : class, TServiceType
		{
			if (registry == null) throw new ArgumentNullException(nameof(registry));

			registry.Register<TTypeToRegister>(RegistrationType.SingleInstance, typeof(TServiceType));

			return registry;
		}

		/// <summary>
		/// Registers a singleton for <typeparamref name="TTypeToRegister"/> as the indicated
		/// implemented <typeparamref name="TServiceType"/> Type.
		/// </summary>
		/// <typeparam name="TTypeToRegister"></typeparam>
		/// <typeparam name="TServiceType"></typeparam>
		/// <param name="registry"></param>
		/// <param name="service"></param>
		/// <returns></returns>
		public static IServiceRegister RegisterTransient<TTypeToRegister, TServiceType>([NotNull] this IServiceRegister registry)
			where TTypeToRegister : class, TServiceType
		{
			if (registry == null) throw new ArgumentNullException(nameof(registry));

			registry.Register<TTypeToRegister>(RegistrationType.InstancePerDependency, typeof(TServiceType));

			return registry;
		}

		/// <summary>
		/// Registers an instance of type <typeparamref name="TTypeToRegister"/>.
		/// </summary>
		/// <typeparam name="TTypeToRegister"></typeparam>
		/// <param name="registry"></param>
		/// <param name="service"></param>
		/// <returns></returns>
		public static IServiceRegister RegisterAsImplementedInterfaces<TTypeToRegister>([NotNull] this IServiceRegister registry, TTypeToRegister service)
			where TTypeToRegister : class
		{
			if (registry == null) throw new ArgumentNullException(nameof(registry));

			registry.Register(service, RegistrationType.ExternallyOwned | RegistrationType.AsImplementedInterface | RegistrationType.SingleInstance, null);

			return registry;
		}
	}
}
