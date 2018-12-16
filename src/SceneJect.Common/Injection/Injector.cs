using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using Fasterflect;

namespace SceneJect.Common
{
	public class Injector
	{
		/// <summary>
		/// Dependency resolution service.
		/// </summary>
		private IResolver Resolver { get; }

		/// <summary>
		/// Type of the object.
		/// </summary>
		private Type ObjectType { get; }

		/// <summary>
		/// Instance of the object
		/// </summary>
		private object ObjectInstance { get; }

		//TODO: Stronger typing?
		public Injector(object instance, IResolver res)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance), "Cannot inject into a null instance.");

			if (res == null)
				throw new ArgumentNullException(nameof(res), "Cannot resolve types for injection with a null resolver.");

			ObjectInstance = instance;
			ObjectType = instance.GetType();
			Resolver = res;
		}

		//TODO: Implement full caching to remember injectable fields for certain types.
		public void Inject()
		{
			try
			{
				//TODO: Check if autofac performance better than this.
				//TODO: Implement FreecraftCore injection lambdas
				foreach (MemberInfo mi in ObjectType.FieldsAndPropertiesWith(Flags.InstanceAnyVisibility, typeof(InjectAttribute)))
				{
					//Skip if the field/prop is marked with OnlyIfNull but is not null.
					if(mi.Attribute<InjectOnlyIfNullAttribute>() != null)
						if(mi.Get(ObjectInstance) != null)
							continue;

					//Based on FreecraftCore's: https://github.com/FreecraftCore/FreecraftCore.Serializer/blob/152fda27c46fcfcaa72d3568c1a591d728773f33/src/FreecraftCore.Serializer.API/Reflection/Serialization/MemberSerializationMediator.cs
					if (mi is PropertyInfo && !((PropertyInfo)mi).CanWrite)
					{
						//TODO: Unity2018 has issues with this FasterFlect PlatformNotSupportedException: Operation is not supported on this platform.
						//If it's a property and it's a readonly one we'll try to grab the backing field
						//ObjectInstance.SetFieldValue($"<{mi.Name}>k__BackingField", Resolver.Resolve(mi.Type()));
						FieldInfo field = ObjectType.GetField($"<{mi.Name}>k__BackingField", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.GetField);
						if(field == null)
							throw new InvalidOperationException($"Unable to get backing field for {mi.Name}");

						field.SetValue(ObjectInstance, Resolver.Resolve(mi.Type()), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, CultureInfo.CurrentCulture);
						//ObjectInstance.SetFieldValue($"<{mi.Name}>k__BackingField", Resolver.Resolve(mi.Type()));
					}
					else
					{
						//TODO: Unity2018 has issues with this FasterFlect PlatformNotSupportedException: Operation is not supported on this platform.
						//mi.Set(ObjectInstance, Resolver.Resolve(mi.Type()));
						if(mi is PropertyInfo p)
							p.SetValue(ObjectInstance, Resolver.Resolve(mi.Type()));
						else if(mi is FieldInfo f)
							f.SetValue(ObjectInstance, Resolver.Resolve(mi.Type()));
					}
				}

				//If we didn't fail we should invoke the dependency injection callbacks
				//if the object implemented them
				if(this.ObjectInstance is IDependencyInjectionCallbackable dpc)
					dpc.OnDependencyInjectionFinished();
			}
			catch (Exception e)
			{
				throw new InvalidOperationException($"Error: {e.Message} failed to inject for {ObjectType} on instance: {ObjectInstance}", e);
			}
		}
	}
}
