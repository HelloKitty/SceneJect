using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace SceneJect.Common
{
	/// <summary>
	/// Default injection strategy for injecting services into Monobehaviours.
	/// </summary>
	public class DefaultInjectionStrategy : IInjectionStrategy
	{
		public DefaultInjectionStrategy()
		{
			//dont need to do anything.
		}

		public void InjectDependencies<TBehaviourType>([NotNull] TBehaviourType behaviour, [NotNull] IResolver serviceResolutionService) 
			where TBehaviourType : MonoBehaviour
		{
			if (behaviour == null) throw new ArgumentNullException(nameof(behaviour));
			if (serviceResolutionService == null) throw new ArgumentNullException(nameof(serviceResolutionService));

			//TODO: Deal with the specifics of injection through the strategy
			Injector injecter = new Injector(behaviour, serviceResolutionService);

			injecter.Inject();
		}

		public void InjectDependencies<TBehaviourType>([NotNull] IEnumerable<TBehaviourType> behaviours, [NotNull] IResolver serviceResolutionService) 
			where TBehaviourType : MonoBehaviour
		{
			if (behaviours == null) throw new ArgumentNullException(nameof(behaviours));
			if (serviceResolutionService == null) throw new ArgumentNullException(nameof(serviceResolutionService));

			//Inject dependencies into each MonoBehaviour
			foreach (MonoBehaviour mb in behaviours)
			{
				InjectDependencies(mb, serviceResolutionService);
			}
		}
	}
}
