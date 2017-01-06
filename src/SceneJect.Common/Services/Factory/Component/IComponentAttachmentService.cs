using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	/// <summary>
	/// Contract for services that can attach components.
	/// </summary>
	public interface IComponentAttachmentService
	{
		//TODO: Deal with non-generic

		/// <summary>
		/// Adds the specified <typeparamref name="TComponentType"/> to the <see cref="GameObject"/>.
		/// </summary>
		/// <typeparam name="TComponentType">The component type.</typeparam>
		/// <param name="gameObject">The gameobject to add it to.</param>
		/// <returns>The attached component.</returns>
		TComponentType AddTo<TComponentType>(GameObject gameObject)
			where TComponentType : MonoBehaviour;
	}
}
