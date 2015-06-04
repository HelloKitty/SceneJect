using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect
{
	/// <summary>
	/// Attribute that should target fields or properties that require a dependency be injected.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class InjectAttribute : Attribute
	{

	}
}
