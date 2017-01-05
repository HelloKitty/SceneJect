using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	/// <summary>
	/// Factory service for <see cref="GameObject"/> creation.
	/// </summary>
	public interface IGameObjectFactory : IGameObjectBuilder
	{
		/// <summary>
		/// Creates an empty <see cref="GameObject"/>.
		/// </summary>
		/// <returns>A non-null empty <see cref="GameObject"/>.</returns>
		GameObject Create();

		/// <summary>
		/// Creates a <see cref="IGameObjectContextualBuilder"/> service.
		/// </summary>
		/// <returns>A non-null contextual building service.</returns>
		IGameObjectContextualBuilder CreateBuilder();
	}
}
