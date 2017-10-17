using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using JetBrains.Annotations;

namespace SceneJect.Common
{
	public class AutoFacToIResolverAdapter : IResolver
	{
		private IComponentContext AutofacContainer { get; }

		public AutoFacToIResolverAdapter([NotNull] IComponentContext autofacContainer)
		{
			if(autofacContainer == null) throw new ArgumentNullException(nameof(autofacContainer));

			AutofacContainer = autofacContainer;
		}

		/// <inheritdoc />
		public TServiceType Resolve<TServiceType>()
		{
			return AutofacContainer.Resolve<TServiceType>();
		}

		/// <inheritdoc />
		public object Resolve(Type serviceType)
		{
			return AutofacContainer.Resolve(serviceType);
		}
	}
}
