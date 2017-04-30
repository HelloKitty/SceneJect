using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;

namespace SceneJect.Common
{
	public interface IResolver
	{
		TTypeToResolve Resolve<TTypeToResolve>()
			where TTypeToResolve : class;

		object Resolve([NotNull] Type t);
	}
}
