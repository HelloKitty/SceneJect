using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Fasterflect;

namespace SceneJect.Common
{
	/// <summary>
	/// Providers a locator object that will find and produce an <see cref="IEnumerable"/> of objects that are targeted with the <see cref="InjecteeAttribute"/> attribute.
	/// </summary>
	/// <typeparam name="TBehaviourType"></typeparam>
	public class InjecteeLocator<TBehaviourType> : IEnumerable<TBehaviourType>
		where TBehaviourType : MonoBehaviour
	{
		/// <summary>
		/// Creates a new default Injectee Location service.
		/// </summary>
		/// <returns>A non-null injection location service.</returns>
		public static InjecteeLocator<TBehaviourType> Create()
		{
			return new InjecteeLocator<TBehaviourType>();
		}

		/// <summary>
		/// Creates a new default Injectee Location service that parses
		/// the provided <see cref="GameObject"/>.
		/// </summary>
		/// <returns>A non-null injection location service.</returns>
		public static InjecteeLocator<TBehaviourType> Create(GameObject go)
		{
			return new InjecteeLocator<TBehaviourType>(go);
		}

		/// <summary>
		/// The located <see cref="MonoBehaviour"/>s that are of type T and are of a <see cref="Type"/> marked by an <see cref="Attribute"/> called <see cref="InjecteeAttribute"/>.
		/// </summary>
		private IEnumerable<TBehaviourType> locatedBehaviours { get; }

		/// <summary>
		/// This default constructor defaults to searching the scene for injectees which are declared by targeting
		/// classes with <see cref="InjecteeAttribute"/>, an attribute.
		/// </summary>
		public InjecteeLocator()
			: this(Resources.FindObjectsOfTypeAll<TBehaviourType>()) //this parses everything, could include components on prefabs. However, injecting into them is semi-ok. Not a great solution but only solution.
		{

		}

		public InjecteeLocator(IEnumerable<TBehaviourType> behavioursToParse)
		{
			if (behavioursToParse == null)
				throw new ArgumentNullException(nameof(behavioursToParse), nameof(InjecteeLocator<TBehaviourType>) + " requires a non-null collection of objects to parse.");

			//'is' keyword should be the fastest way to determine if it's of type T.
			//Also, we can avoid another Where call by relying on short-circuit evaluation not executing the second portion if
			//uneeded which is nice.
			locatedBehaviours = behavioursToParse
				.Where(x => x is TBehaviourType)
				.Where(x => x.GetType().Attributes<InjecteeAttribute>().Count > 0);
		}

		/// <summary>
		/// Locates injectee's via the provided <paramref name="rootObject"/> <see cref="GameObject"/>.
		/// </summary>
		/// <param name="rootObject"><see cref="GameObject"/> root (heirarchy).</param>
		public InjecteeLocator(GameObject rootObject)
		{
			//Grabs all components from the GameObject.
			locatedBehaviours = rootObject.GetComponentsInChildren<TBehaviourType>(true);
		}

		public IEnumerator<TBehaviourType> GetEnumerator()
		{
			return locatedBehaviours.GetEnumerator();
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return locatedBehaviours.GetEnumerator();
		}
	}
}
