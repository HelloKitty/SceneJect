using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common
{
	[Serializable]
	[Flags]
	public enum RegistrationType : byte
	{
		/// <summary>
		/// This is the same as only having the InstancePerDepdency flag enabled.
		/// This documentation indicates this is the default behaviour: http://docs.autofac.org/en/latest/lifetime/instance-scope.html#instance-per-dependency
		/// </summary>
		Default = 0,

		/// <summary>
		/// This indicates that the dependency is owned externally from the IoC container itself.
		/// This is generally a preferred flag to set.
		/// Refer here for more information: http://docs.autofac.org/en/latest/lifetime/disposal.html
		/// </summary>
		ExternallyOwned = 1 << 0,

		/// <summary>
		/// Registers the depedency as the most derived Type. Ex. Your object is of Type CombatHandler. It will be registered as CombatHandler
		/// in the IoC container.
		/// Refer here for more information: http://docs.autofac.org/en/latest/register/registration.html
		/// </summary>
		AsSelf = 1 << 1,

		/// <summary>
		/// This flag indicates that the dependency you're providing is the reference/object that should be supplied to
		/// the Injectee's. Think of this as sort of like a Singleton that is abstracted behind the guise of DI.
		/// This is one of the flags that conflict with another. This must not be a flag if InstancePerDepedency is used and vice-versa.
		/// For more information refer here: http://docs.autofac.org/en/latest/lifetime/instance-scope.html#single-instance
		/// </summary>
		SingleInstance = 1 << 2,

		/// <summary>
		/// This flag indicates the dependency should be registered as the implemented interfaces it has.
		/// Though it isn't very helpful refer here for more information: http://autofac.org/apidoc/html/E2D57129.htm or http://docs.autofac.org/en/latest/register/scanning.html
		/// </summary>
		AsImplementedInterface = 1 << 3,

		/// <summary>
		/// This indicates that you'd like to create a new instance per depedency request. Ex. You have
		/// multiple enemy objects that each require their own instance of an IMovementHandler.
		/// This flag should not be enabled if you've selected single instance.
		/// The IoC container will invoke the constructor with the most parameters it can resolve itself. So if your dependency has dependencies
		/// you must register them too.
		/// For more information on what happens behind the scenes refer here: http://docs.autofac.org/en/latest/lifetime/instance-scope.html#instance-per-dependency
		/// and here: http://docs.autofac.org/en/latest/register/registration.html
		/// </summary>
		InstancePerDependency = 1 << 4,
	}
}
