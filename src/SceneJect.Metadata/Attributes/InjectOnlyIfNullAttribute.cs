using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect
{
	/// <summary>
	/// Metadata that indicates we should only inject into the field/prop
	/// if the value is currently null.
	/// (Warning: This means it will call the properties getter to check).
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class InjectOnlyIfNullAttribute : Attribute
	{

	}
}
