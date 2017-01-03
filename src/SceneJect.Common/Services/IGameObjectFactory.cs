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
	public interface IGameObjectFactory
	{
		/// <summary>
		/// Creates an empty <see cref="GameObject"/>.
		/// </summary>
		/// <returns>A non-null empty <see cref="GameObject"/>.</returns>
		GameObject Create();

		/// <summary>
		/// Creates an instance of the prefab <see cref="GameObject"/>.
		/// </summary>
		/// <param name="prefab">Prefab to create an instance of.</param>
		/// <returns>A non-null instance of the provided <see cref="GameObject"/> <paramref name="prefab"/>.</returns>
		GameObject Create(GameObject prefab);

		/// <summary>
		/// Creates an instance of the prefab <see cref="GameObject"/> with the provided position and rotation.
		/// </summary>
		/// <param name="prefab">Prefab to create an instance of.</param>
		/// <returns>A non-null instance of the provided <see cref="GameObject"/> <paramref name="prefab"/>.</returns>
		GameObject Create(GameObject prefab, Vector3 position, Quaternion rotation);
	}
}
