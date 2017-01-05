using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common
{
	/// <summary>
	/// Contract for any Type that encodes servide Type information and provides
	/// an instance of a service that can be assigned to <typeparamref name="TServiceType"/>
	/// </summary>
	/// <typeparam name="TServiceType"></typeparam>
	public interface IService<TServiceType>
	{
		/// <summary>
		/// Instance of a <typeparamref name="TServiceType"/>.
		/// </summary>
		TServiceType ServiceInstance { get; }
	}
}
