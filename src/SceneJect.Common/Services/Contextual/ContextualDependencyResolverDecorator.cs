using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;

namespace SceneJect.Common
{
	public class ContextualDependencyResolverDecorator : IResolver
	{
		/// <summary>
		/// Resolution service that is being decorated.
		/// </summary>
		private IComponentContext DecoratedResolver { get; }

		/// <summary>
		/// Dictionary of mapped Types to dependency instances.
		/// </summary>
		private IDictionary<Type, Func<IComponentContext, object>> ContextualDependencyMap { get; }

		public ContextualDependencyResolverDecorator(IComponentContext resolverToDecorate, IDictionary<Type, Func<IComponentContext, object>> contextualDependencies)
		{
			if (resolverToDecorate == null)
				throw new ArgumentNullException(nameof(resolverToDecorate), $"Provided arg {nameof(resolverToDecorate)} was null.");

			if (contextualDependencies == null)
				throw new ArgumentNullException(nameof(contextualDependencies), $"Provided arg {nameof(contextualDependencies)} was null.");

			DecoratedResolver = resolverToDecorate;
			ContextualDependencyMap = contextualDependencies;
		}

		public TTypeToResolve Resolve<TTypeToResolve>() 
		{
			return (TTypeToResolve)this.Resolve(typeof(TTypeToResolve));
		}

		public object Resolve(Type t)
		{
			if (t == null) throw new ArgumentNullException(nameof(t));

			//Decorate by providing an instance of the dependency if the contextual "container" has it.
			//Otherwise we can default the the decorated resolver which is likely to happen.
			return ContextualDependencyMap.ContainsKey(t) ? ContextualDependencyMap[t](DecoratedResolver) : DecoratedResolver.Resolve(t);
		}
	}
}
