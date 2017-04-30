using Autofac;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace SceneJect.Autofac
{
	public class AutofacServiceAdapter : ContainerServiceProvider, IResolver, IServiceRegister
	{
		private AutofacRegisterationStrat registerationStrat { get; } = new AutofacRegisterationStrat(new ContainerBuilder());
		protected override IServiceRegister registry => registerationStrat;

		private IResolver _resolver = null;
		protected override IResolver resolver => GenerateResolver();

		private object syncObj { get; } = new object();

		private IResolver GenerateResolver()
		{
			if(_resolver == null)
			{
				//double check locking
				lock(syncObj)
					if (_resolver == null)
						_resolver = new AutofacResolverStrat(registerationStrat.Build());
			}

			return _resolver;
		}
	}
}
