using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;
using SceneJect.Common;
using UnityEngine;

namespace SceneJect
{
	public sealed class DefaultManualInjectionStrategy : IManualInjectionStrategy
	{
		/// <summary>
		/// Service for resolving dependencies.
		/// </summary>
		private IComponentContext ResolverService { get; }

		/// <inheritdoc />
		public DefaultManualInjectionStrategy([NotNull] IComponentContext resolverService)
		{
			ResolverService = resolverService ?? throw new ArgumentNullException(nameof(resolverService));
		}

		/// <inheritdoc />
		public void InjectDependencies(IReadOnlyCollection<MonoBehaviour> behaviours)
		{
			if(behaviours == null) throw new ArgumentNullException(nameof(behaviours));

			if(behaviours.Count == 0)
				return;

			//TODO: Expose this as a depedency
			new DefaultInjectionStrategy().InjectDependencies<MonoBehaviour>(behaviours, ResolverService);
		}
	}
}
