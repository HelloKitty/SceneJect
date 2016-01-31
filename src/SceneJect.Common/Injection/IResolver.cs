using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common
{
	public interface IResolver
	{
		T Resolve<T>();

		object Resolve(Type t);
	}
}
