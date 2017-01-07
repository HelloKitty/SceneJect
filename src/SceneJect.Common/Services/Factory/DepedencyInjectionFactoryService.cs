using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common
{
	public abstract class DepedencyInjectionFactoryService
	{
		/// <summary>
		/// Service for resolving dependencies.
		/// </summary>
		protected IResolver resolverService { get; }

		/// <summary>
		/// Strategy for injection.
		/// </summary>
		protected IInjectionStrategy injectionStrategy { get; }

		public DepedencyInjectionFactoryService(IResolver resolver, IInjectionStrategy injectionStrat)
		{
			if (resolver == null)
				throw new ArgumentNullException(nameof(resolver), $"Provided {nameof(IResolver)} service provided is null.");

			if (injectionStrat == null)
				throw new ArgumentNullException(nameof(injectionStrat), $"Provided {nameof(IInjectionStrategy)} service provided is null.");

			injectionStrategy = injectionStrat;
			resolverService = resolver;
		}
	}
}
