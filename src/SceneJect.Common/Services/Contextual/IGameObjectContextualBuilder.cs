using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common
{
	public interface IGameObjectContextualBuilder : IGameObjectBuilder
	{
		/// <summary>
		/// Adds the provided <see cref="IService{TServiceType}"/> information to the builder.
		/// </summary>
		/// <typeparam name="TServiceType">Type of the service.</typeparam>
		/// <param name="service">Service information object.</param>
		/// <returns></returns>
		IGameObjectContextualBuilder With<TServiceType>(IService<TServiceType> service);
	}
}
