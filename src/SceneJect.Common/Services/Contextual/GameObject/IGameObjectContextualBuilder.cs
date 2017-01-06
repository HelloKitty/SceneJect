using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SceneJect.Common
{
	public interface IGameObjectContextualBuilder : IGameObjectBuilder, IContextualServiceRegisterable<IGameObjectContextualBuilder>
	{
		//consolidation interface
	}
}
