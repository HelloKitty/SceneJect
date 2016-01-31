using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common
{
	/// <summary>
	/// This attribute marks a class for injection.
	/// If you expect SceneJect to inject dependancies within the class then you should target
	/// the class with this attribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class InjecteeAttribute : Attribute
	{

	}
}
