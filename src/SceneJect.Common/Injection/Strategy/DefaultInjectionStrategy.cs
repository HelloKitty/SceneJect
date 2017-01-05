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

		public void InjectDependencies<TBehaviourType>(IEnumerable<TBehaviourType> behaviours, IResolver serviceResolutionService) 
			where TBehaviourType : MonoBehaviour
		{
			//Inject dependencies into each MonoBehaviour
			foreach (MonoBehaviour mb in behaviours)
			{
				//TODO: Deal with the specifics of injection through the strategy
				Injector injecter = new Injector(mb, serviceResolutionService);

				injecter.Inject();
			}
		}
	}
}
