using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect
{
	[Serializable]
	[Flags]
	public enum RegisterationType : byte
	{
		Default = 0,
		ExternallyOwned = 1 << 0,
		AsSelf = 1 << 1,
		SingleInstance = 1 << 2,
		AsImplementedInterface = 1 << 3,
 		InstancePerDependency = 1 << 4,
	}
}
