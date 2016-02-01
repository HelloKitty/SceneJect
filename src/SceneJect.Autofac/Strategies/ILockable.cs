using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Autofac
{
	public interface ILockable
	{
		bool isLocked { get; }

		void Lock();
	}
}
