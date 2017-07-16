using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Fasterflect;

namespace SceneJect.Common
{
	public class Injector
	{
		/// <summary>
		/// Dependency resolution service.
		/// </summary>
		private IResolver resolver { get; }

		/// <summary>
		/// Type of the object.
		/// </summary>
		private Type objectType { get; }

		/// <summary>
		/// Instance of the object
		/// </summary>
		private object objectInstance { get; }

		//TODO: Stronger typing?
		public Injector(object instance, IResolver res)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance), "Cannot inject into a null instance.");

			if (res == null)
				throw new ArgumentNullException(nameof(res), "Cannot resolve types for injection with a null resolver.");

			objectInstance = instance;
			objectType = instance.GetType();
			resolver = res;
		}

		//TODO: Implement full caching to remember injectable fields for certain types.
		public void Inject()
		{
			try
			{
				//TODO: Implement FreecraftCore injection lambdas
				foreach (MemberInfo mi in objectType.FieldsAndPropertiesWith(Flags.InstanceAnyVisibility, typeof(InjectAttribute)))
				{
					mi.Set(objectInstance, resolver.Resolve(mi.Type()));
				}
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Error: {e.Message} failed to inject for {objectType} on instance: {objectInstance}", e);
			}
		}
	}
}
