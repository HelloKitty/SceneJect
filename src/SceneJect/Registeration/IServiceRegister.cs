using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect
{
	public interface IServiceRegister
	{
		void Register<T>(T instance, RegisterationType registerationFlags, Type registerAs = null)
			where T : class;
	}
}
