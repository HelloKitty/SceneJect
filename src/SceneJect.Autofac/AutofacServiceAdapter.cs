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
		private AutofacRegisterationStrat RegisterationStrat { get; } = new AutofacRegisterationStrat(new ContainerBuilder());
		protected override IServiceRegister Registry => RegisterationStrat;

		//Don't use lazy since we want to still support net35 for now
		private IResolver _resolver = null;
		protected override IResolver Resolver => GenerateResolver();

		private object SyncObj { get; } = new object();

		private IResolver GenerateResolver()
		{
			if(_resolver == null)
			{
				//double check locking
				lock(SyncObj)
					if (_resolver == null)
						_resolver = new AutofacResolverStrat(RegisterationStrat.Build());
			}

			return _resolver;
		}
	}
}
