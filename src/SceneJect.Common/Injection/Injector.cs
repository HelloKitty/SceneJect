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
		private readonly IResolver resolver;

		private readonly Type objectType;

		private readonly object objectInstance;

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
				//find fields that request injection
				IEnumerable<FieldInfo> fields = objectType.Fields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.Where(fi => fi.HasAttribute<InjectAttribute>());

				foreach (FieldInfo fi in fields)
				{
					fi.Set(objectInstance, resolver.Resolve(fi.FieldType));
				}

				//find props that request injection
				IEnumerable<PropertyInfo> props = objectType.Properties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
					.Where(pi => pi.HasAttribute<InjectAttribute>());

				foreach (PropertyInfo pi in props)
				{
					pi.Set(objectInstance, resolver.Resolve(pi.PropertyType));
				}
			}
			catch (Exception e)
			{
				throw new InvalidOperationException("Error: " + e.Message + " failed to inject for " + objectType.ToString() + " on instance: " + objectInstance.ToString(), e);
			}
		}
	}
}
