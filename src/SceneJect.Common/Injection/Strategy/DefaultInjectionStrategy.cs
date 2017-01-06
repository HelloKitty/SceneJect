using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

		public void InjectDependencies<TBehaviourType>(TBehaviourType behaviour, IResolver serviceResolutionService) 
			where TBehaviourType : MonoBehaviour
		{
			//TODO: Deal with the specifics of injection through the strategy
			Injector injecter = new Injector(behaviour, serviceResolutionService);

			injecter.Inject();
		}

		public void InjectDependencies<TBehaviourType>(IEnumerable<TBehaviourType> behaviours, IResolver serviceResolutionService) 
			where TBehaviourType : MonoBehaviour
		{
			//Inject dependencies into each MonoBehaviour
			foreach (MonoBehaviour mb in behaviours)
			{
				InjectDependencies(mb, serviceResolutionService);
			}
		}
	}
}
