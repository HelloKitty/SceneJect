using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;

namespace SceneJect.Common
{
	/// <summary>
	/// Contract for any Type that encodes servide Type information and provides
	/// an instance of a service that can be assigned to <typeparamref name="TServiceType"/>
	/// </summary>
	/// <typeparam name="TServiceType"></typeparam>
	public interface IService<out TServiceType>
		where TServiceType : class
	{
		/// <summary>
		/// Instance of a <typeparamref name="TServiceType"/>.
		/// </summary>
		Func<IComponentContext, TServiceType> ServiceInstance { get; }
	}
}
