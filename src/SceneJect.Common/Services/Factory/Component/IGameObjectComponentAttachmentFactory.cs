using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Common
{
	/// <summary>
	/// Factory service that creates and attaches components to <see cref="GameObject"/>s.
	/// </summary>
	public interface IGameObjectComponentAttachmentFactory : IComponentAttachmentService
	{
		/// <summary>
		/// Creates a builder object for contextual component construction services.
		/// </summary>
		/// <returns></returns>
		IComponentContextualBuilder CreateBuilder();
	}
}
