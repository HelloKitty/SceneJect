using Autofac;
using SceneJect.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Autofac
{
	public class AutofacResolverStrat : IResolver
	{
		private IContainer Container { get; }

		public AutofacResolverStrat(IContainer autofacContainer)
		{
			if (autofacContainer == null)
				throw new ArgumentNullException(nameof(autofacContainer), "Cannot have a valid resolve strat with a null container.");

			Container = autofacContainer;
		}

		public TTypeToResolve Resolve<TTypeToResolve>()
			where TTypeToResolve : class
		{
			if (!Container.IsRegistered<TTypeToResolve>())
				throw new Exception($"{typeof(TTypeToResolve)} was requested from the container but is unavailable.");

			return Container.Resolve<TTypeToResolve>();
		}

		public object Resolve(Type t)
		{
			if (t == null)
				throw new ArgumentNullException(nameof(t), "Cannot resolve a null type.");

			if (t.IsValueType)
				throw new InvalidOperationException($"Cannot resolve value types: {t}.");

			if (!Container.IsRegistered(t))
				throw new Exception($"{t} was requested from the container but is unavailable.");

			return Container.Resolve(t);
		}
	}
}
