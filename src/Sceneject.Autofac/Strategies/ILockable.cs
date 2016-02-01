using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sceneject.Autofac
{
	public interface ILockable
	{
		bool isLocked { get; }

		void Lock();
	}
}
