using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneJect.Common
{
	/// <summary>
	/// Contract for a type that can resolve.
	/// </summary>
	public interface IResolver
	{
		/// <summary>
		/// Resolved the service typ.
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <returns></returns>
		TServiceType Resolve<TServiceType>();

		object Resolve(Type serviceType);
	}
}
