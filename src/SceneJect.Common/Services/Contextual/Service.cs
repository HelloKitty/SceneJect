using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using JetBrains.Annotations;

namespace SceneJect.Common
{
	//Basically this service provides/produces an instance of the service without any additional controlling attributes.
	/// <summary>
	/// Represents a mapping between a <typeparamref name="TServiceType"/> and an instance
	/// of the service.
	/// </summary>
	/// <typeparam name="TServiceType"></typeparam>
	public class Service<TServiceType> : IService<TServiceType> 
		where TServiceType : class
	{
		/// <summary>
		/// An instance of the service.
		/// </summary>
		public Func<IComponentContext, TServiceType> ServiceInstance { get; }

		//Hide so that they use fluent As construction

		protected Service(Func<IComponentContext, TServiceType> instance)
		{
			if(instance == null) throw new ArgumentNullException(nameof(instance));

			ServiceInstance = instance;
		}

		/// <summary>
		/// Constructs a new Service pair that maps the <paramref name="instance"/> to all services of <typeparamref name="TServiceType"/>.
		/// </summary>
		/// <typeparam name="TServiceInstanceType"></typeparam>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static Service<TServiceType> As<TServiceInstanceType>([NotNull] TServiceInstanceType instance)
			where TServiceInstanceType : TServiceType
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance), $"Provided {nameof(instance)} parameter that should be of Type {typeof(TServiceType).FullName} was null.");

			return new Service<TServiceType>(resolver => instance);
		}

		/// <summary>
		/// Constructs a new Service pair that maps the <paramref name="instance"/> to all services of <typeparamref name="TServiceType"/>.
		/// </summary>
		/// <param name="instance"></param>
		/// <returns></returns>
		public static Service<TServiceType> As(Func<IComponentContext, TServiceType> instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance), $"Provided {nameof(instance)} parameter that should be of Type {typeof(TServiceType).FullName} was null.");

			return new Service<TServiceType>(instance);
		}
	}
}
