using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SceneJect.Common
{
	/// <summary>
	/// Contract for types that are listening
	/// for dependency injection service callbacks.
	/// </summary>
	public interface IDependencyInjectionCallbackable
	{
		/// <summary>
		/// Callback invoked when the object has
		/// had its dependencies marked with <see cref="InjectAttibute"/> injected.
		/// </summary>
		void OnDependencyInjectionFinished();
	}
}
