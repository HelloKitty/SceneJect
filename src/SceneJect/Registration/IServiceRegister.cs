using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect
{
	public interface IServiceRegister
	{
		void Register<T>(T instance, RegistrationType registerationFlags, Type registerAs = null)
			where T : class;

		void Register<T>(RegistrationType registerationFlags, Type registerAs = null)
			where T : class;
	}
}
