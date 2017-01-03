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
		private readonly AutofacRegisterationStrat registerationStrat = new AutofacRegisterationStrat(new ContainerBuilder());
		public override IServiceRegister Registry { get { return registerationStrat; } }

		private IResolver resolver = null;
		public override IResolver Resolver { get { return GenerateResolver(); } }

		private readonly object syncObj = new object();

		private IResolver GenerateResolver()
		{
			if(resolver == null)
			{
				//double check locking
				lock(syncObj)
					if (resolver == null)
						resolver = new AutofacResolverStrat(registerationStrat.Build());
			}

			return resolver;
		}
	}
}
